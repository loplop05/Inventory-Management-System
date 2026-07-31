using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Centralized search helper for advanced search functionality.
    /// Provides filtering, saved searches, and search history with proper RowFilter string escaping.
    /// </summary>
    public static class clsSearchHelper
    {
        // ─── String Escaping for DataView.RowFilter ──────────────────────────────

        /// <summary>
        /// Escapes special characters for use inside DataView RowFilter LIKE clauses.
        /// </summary>
        public static string EscapeLikeValue(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            // Normalize Arabic characters for better search matching
            string normalized = NormalizeArabic(input);
            StringBuilder sb = new StringBuilder();
            foreach (char c in normalized)
            {
                if (c == '\'')
                    sb.Append("''");
                else if (c == '[' || c == ']' || c == '*' || c == '%')
                    sb.Append("[").Append(c).Append("]");
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Normalizes Arabic characters for consistent search (Alef forms, Ta Marbuta).
        /// </summary>
        private static string NormalizeArabic(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            // Normalize to form C for canonical composition
            string normalized = input.Normalize(System.Text.NormalizationForm.FormC);
            // Replace Alef variants with standard Alef
            normalized = normalized.Replace('\u0623', '\u0627'); // Alef with Hamza above → Alef
            normalized = normalized.Replace('\u0625', '\u0627'); // Alef with Hamza below → Alef
            normalized = normalized.Replace('\u0622', '\u0627'); // Alef with Madda → Alef
            // Replace Ta Marbuta with Ha
            normalized = normalized.Replace('\u0629', '\u0647'); // Ta Marbuta → Ha
            return normalized;
        }

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
        /// Applies multiple filters to a DataTable safely.
        /// Returns a filtered DataView.
        /// </summary>
        public static DataView ApplyFilters(DataTable table, List<SearchFilter> filters)
        {
            if (table == null || filters == null || filters.Count == 0)
                return table != null ? table.DefaultView : new DataView();

            var filterBuilder = new StringBuilder();
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
            try
            {
                view.RowFilter = filterBuilder.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"RowFilter error: {ex.Message}");
                view.RowFilter = "";
            }
            return view;
        }

        private static string BuildFilterCondition(SearchFilter filter)
        {
            try
            {
                string escapedVal = EscapeLikeValue(filter.Value);
                switch (filter.Type)
                {
                    case FilterType.Contains:
                        return $"[{filter.ColumnName}] LIKE '%{escapedVal}%'";

                    case FilterType.StartsWith:
                        return $"[{filter.ColumnName}] LIKE '{escapedVal}%'";

                    case FilterType.EndsWith:
                        return $"[{filter.ColumnName}] LIKE '%{escapedVal}'";

                    case FilterType.ExactMatch:
                        return $"[{filter.ColumnName}] = '{escapedVal}'";

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

        /// <summary>
        /// Filters a DataTable across multiple string columns safely.
        /// </summary>
        public static DataView QuickSearch(DataTable table, string searchTerm, params string[] columns)
        {
            if (table == null)
                return new DataView();

            if (string.IsNullOrWhiteSpace(searchTerm) || columns == null || columns.Length == 0)
                return table.DefaultView;

            string escapedTerm = EscapeLikeValue(searchTerm.Trim());
            var conditions = new List<string>();

            foreach (var column in columns)
            {
                if (table.Columns.Contains(column))
                {
                    // If column is integer/numeric, cast or use Convert if possible, otherwise string LIKE
                    Type colType = table.Columns[column].DataType;
                    if (colType == typeof(int) || colType == typeof(long) || colType == typeof(decimal) || colType == typeof(double))
                    {
                        conditions.Add($"Convert([{column}], 'System.String') LIKE '%{escapedTerm}%'");
                    }
                    else
                    {
                        conditions.Add($"[{column}] LIKE '%{escapedTerm}%'");
                    }
                }
            }

            var view = table.DefaultView;
            try
            {
                view.RowFilter = conditions.Count > 0 ? string.Join(" OR ", conditions) : "";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"QuickSearch RowFilter error: {ex.Message}");
                view.RowFilter = "";
            }
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
            return new List<string>(_savedSearches.Keys);
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
            if (textBox == null || string.IsNullOrWhiteSpace(searchTerm)) return;

            AddToHistory(context, searchTerm);
            SetupAutoComplete(textBox, context);
        }
    }
}
