using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Centralized search helper for advanced search functionality.
    /// Provides filtering, saved searches, and search history.
    /// </summary>
    public static class clsSearchHelper
    {
        // ─── Search Filter Types ────────────────────────────────────────────────

        public enum FilterType
        {
            Contains,
            StartsWith,
            EndsWith,
            ExactMatch,
            GreaterThan,
            LessThan,
            Between
        }

        public class SearchFilter
        {
            public string ColumnName { get; set; }
            public string Value { get; set; }
            public FilterType Type { get; set; }
            public bool Enabled { get; set; } = true;

            public SearchFilter(string columnName, string value, FilterType type = FilterType.Contains)
            {
                ColumnName = columnName;
                Value = value;
                Type = type;
            }
        }

        // ─── Search History ─────────────────────────────────────────────────────

        private static readonly Dictionary<string, List<string>> _searchHistory = 
            new Dictionary<string, List<string>>();
        private const int MaxHistoryItems = 10;

        /// <summary>Adds a search term to history for a specific context.</summary>
        public static void AddToHistory(string context, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return;

            if (!_searchHistory.ContainsKey(context))
                _searchHistory[context] = new List<string>();

            var history = _searchHistory[context];
            history.Remove(searchTerm); // Remove if exists to move to top
            history.Insert(0, searchTerm);

            // Keep only recent items
            if (history.Count > MaxHistoryItems)
                history.RemoveAt(history.Count - 1);
        }

        /// <summary>Gets search history for a specific context.</summary>
        public static List<string> GetHistory(string context)
        {
            return _searchHistory.ContainsKey(context) 
                ? new List<string>(_searchHistory[context]) 
                : new List<string>();
        }

        /// <summary>Clears search history for a specific context.</summary>
        public static void ClearHistory(string context)
        {
            if (_searchHistory.ContainsKey(context))
                _searchHistory[context].Clear();
        }

        // ─── Advanced Filtering ────────────────────────────────────────────────

        /// <summary>
        /// Applies multiple filters to a DataTable.
        /// Returns a filtered DataView.
        /// </summary>
        public static DataView ApplyFilters(DataTable table, List<SearchFilter> filters)
        {
            if (table == null || filters == null || filters.Count == 0)
                return table.DefaultView;

            var filterBuilder = new System.Text.StringBuilder();
            var activeFilters = filters.Where(f => f.Enabled && !string.IsNullOrWhiteSpace(f.Value)).ToList();

            for (int i = 0; i < activeFilters.Count; i++)
            {
                var filter = activeFilters[i];
                string condition = BuildFilterCondition(filter);

                if (!string.IsNullOrWhiteSpace(condition))
                {
                    if (filterBuilder.Length > 0)
                        filterBuilder.Append(" AND ");

                    filterBuilder.Append(condition);
                }
            }

            var view = table.DefaultView;
            view.RowFilter = filterBuilder.ToString();
            return view;
        }

        private static string BuildFilterCondition(SearchFilter filter)
        {
            try
            {
                switch (filter.Type)
                {
                    case FilterType.Contains:
                        return $"[{filter.ColumnName}] LIKE '%{filter.Value}%'";

                    case FilterType.StartsWith:
                        return $"[{filter.ColumnName}] LIKE '{filter.Value}%'";

                    case FilterType.EndsWith:
                        return $"[{filter.ColumnName}] LIKE '%{filter.Value}'";

                    case FilterType.ExactMatch:
                        return $"[{filter.ColumnName}] = '{filter.Value}'";

                    case FilterType.GreaterThan:
                        if (decimal.TryParse(filter.Value, out decimal gtValue))
                            return $"[{filter.ColumnName}] > {gtValue}";
                        break;

                    case FilterType.LessThan:
                        if (decimal.TryParse(filter.Value, out decimal ltValue))
                            return $"[{filter.ColumnName}] < {ltValue}";
                        break;

                    case FilterType.Between:
                        var parts = filter.Value.Split('-');
                        if (parts.Length == 2 && 
                            decimal.TryParse(parts[0], out decimal min) && 
                            decimal.TryParse(parts[1], out decimal max))
                        {
                            return $"[{filter.ColumnName}] >= {min} AND [{filter.ColumnName}] <= {max}";
                        }
                        break;
                }
            }
            catch
            {
                // Invalid filter, return empty
            }

            return string.Empty;
        }

        // ─── Quick Search ───────────────────────────────────────────────────────

        
        public static DataView QuickSearch(DataTable table, string searchTerm, params string[] columns)
        {
            if (table == null || string.IsNullOrWhiteSpace(searchTerm) || columns == null || columns.Length == 0)
                return table.DefaultView;

            var conditions = new List<string>();
            foreach (var column in columns)
            {
                if (table.Columns.Contains(column))
                {
                    conditions.Add($"[{column}] LIKE '%{searchTerm}%'");
                }
            }

            var view = table.DefaultView;
            view.RowFilter = string.Join(" OR ", conditions);
            return view;
        }

        // ─── Saved Searches ────────────────────────────────────────────────────

        private static readonly Dictionary<string, List<SearchFilter>> _savedSearches = 
            new Dictionary<string, List<SearchFilter>>();

        /// <summary>Saves a search configuration with a name.</summary>
        public static void SaveSearch(string name, List<SearchFilter> filters)
        {
            if (string.IsNullOrWhiteSpace(name) || filters == null) return;
            _savedSearches[name] = new List<SearchFilter>(filters);
        }

        /// <summary>Loads a saved search by name.</summary>
        public static List<SearchFilter> LoadSearch(string name)
        {
            return _savedSearches.ContainsKey(name) 
                ? new List<SearchFilter>(_savedSearches[name]) 
                : null;
        }

        /// <summary>Gets all saved search names.</summary>
        public static List<string> GetSavedSearchNames()
        {
            var names = new List<string>();
            foreach (var key in _savedSearches.Keys)
            {
                names.Add(key);
            }
            return names;
        }

        /// <summary>Deletes a saved search.</summary>
        public static void DeleteSearch(string name)
        {
            if (_savedSearches.ContainsKey(name))
                _savedSearches.Remove(name);
        }

        // ─── UI Helpers ────────────────────────────────────────────────────────

        /// <summary>
        /// Sets up a TextBox with auto-complete from search history.
        /// </summary>
        public static void SetupAutoComplete(TextBox textBox, string context)
        {
            if (textBox == null) return;

            var history = GetHistory(context);
            var source = new AutoCompleteStringCollection();
            source.AddRange(history.ToArray());

            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox.AutoCompleteCustomSource = source;
            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        /// <summary>
        /// Updates auto-complete suggestions when a search is performed.
        /// </summary>
        public static void UpdateAutoComplete(TextBox textBox, string context, string searchTerm)
        {
            if (textBox == null) return;

            AddToHistory(context, searchTerm);
            SetupAutoComplete(textBox, context);
        }
    }
}
