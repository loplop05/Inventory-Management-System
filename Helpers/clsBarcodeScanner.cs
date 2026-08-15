using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    /// <summary>
    /// USB Barcode Scanner helper for POS integration.
    /// Detects barcode scanner input and triggers product lookup.
    /// Includes fast-path caching for frequently scanned products.
    /// </summary>
    public static class clsBarcodeScanner
    {
        private static string _buffer = "";
        private static DateTime _lastKeyPress = DateTime.MinValue;
        private const int ScannerTimeoutMs = 100; // Time to wait for complete barcode scan
        
        // Fast-path cache for frequently scanned products
        private static readonly Dictionary<string, CachedProduct> _productCache = new Dictionary<string, CachedProduct>();
        private const int MaxCacheSize = 100;
        private const int CacheExpiryMinutes = 30;

        private class CachedProduct
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public DateTime CachedAt { get; set; }
        }

        /// <summary>
        /// Event raised when a complete barcode is scanned.
        /// </summary>
        public static event Action<string> BarcodeScanned;

        /// <summary>
        /// Event raised when a product is found via barcode (fast-path).
        /// </summary>
        public static event Action<int, string, decimal> ProductFound;

        /// <summary>
        /// Processes a key press to detect barcode scanner input.
        /// Call this from the form's KeyPress event.
        /// Only processes barcode input when focus is NOT on a text input field.
        /// </summary>
        public static bool ProcessKeyPress(KeyPressEventArgs e, Control activeControl = null)
        {
            char keyChar = e.KeyChar;

            // Don't intercept if user is typing in a textbox (e.g., coupon, search, payment)
            if (activeControl is TextBox)
            {
                return false;
            }

            // Barcode scanners typically send digits and end with Enter
            if (char.IsDigit(keyChar) || keyChar == '-')
            {
                _buffer += keyChar;
                _lastKeyPress = DateTime.Now;
                e.Handled = true; // Consume the character
                return true;
            }
            else if (keyChar == '\r' || keyChar == '\n') // Enter key
            {
                if (!string.IsNullOrEmpty(_buffer) && (DateTime.Now - _lastKeyPress).TotalMilliseconds < ScannerTimeoutMs)
                {
                    string barcode = _buffer;
                    _buffer = "";
                    e.Handled = true;
                    
                    // Try fast-path cache first
                    if (TryGetFromCache(barcode, out var cachedProduct))
                    {
                        ProductFound?.Invoke(cachedProduct.ProductID, cachedProduct.ProductName, cachedProduct.Price);
                    }
                    else
                    {
                        // Fall back to regular barcode event for full lookup
                        BarcodeScanned?.Invoke(barcode);
                    }
                    return true;
                }
                _buffer = "";
            }
            else if ((DateTime.Now - _lastKeyPress).TotalMilliseconds > ScannerTimeoutMs)
            {
                // Reset buffer if too much time passed (manual typing)
                _buffer = "";
            }

            return false;
        }

        /// <summary>
        /// Tries to get product from cache.
        /// </summary>
        private static bool TryGetFromCache(string barcode, out CachedProduct product)
        {
            if (_productCache.TryGetValue(barcode, out product))
            {
                // Check if cache entry is still valid
                if ((DateTime.Now - product.CachedAt).TotalMinutes < CacheExpiryMinutes)
                {
                    return true;
                }
                else
                {
                    // Remove expired entry
                    _productCache.Remove(barcode);
                }
            }
            return false;
        }

        /// <summary>
        /// Adds a product to the cache after successful barcode lookup.
        /// Call this after retrieving product details from database.
        /// </summary>
        public static void AddToCache(string barcode, int productID, string productName, decimal price)
        {
            if (string.IsNullOrEmpty(barcode) || productID <= 0)
                return;

            // Evict old entries if cache is full
            if (_productCache.Count >= MaxCacheSize)
            {
                EvictOldestEntry();
            }

            _productCache[barcode] = new CachedProduct
            {
                ProductID = productID,
                ProductName = productName,
                Price = price,
                CachedAt = DateTime.Now
            };
        }

        /// <summary>
        /// Removes the oldest cache entry.
        /// </summary>
        private static void EvictOldestEntry()
        {
            string oldestKey = null;
            DateTime oldestTime = DateTime.MaxValue;

            foreach (var kvp in _productCache)
            {
                if (kvp.Value.CachedAt < oldestTime)
                {
                    oldestTime = kvp.Value.CachedAt;
                    oldestKey = kvp.Key;
                }
            }

            if (oldestKey != null)
            {
                _productCache.Remove(oldestKey);
            }
        }

        /// <summary>
        /// Clears the product cache.
        /// </summary>
        public static void ClearCache()
        {
            _productCache.Clear();
        }

        /// <summary>
        /// Gets cache statistics for monitoring.
        /// </summary>
        public static (int Count, int ExpiredCount) GetCacheStats()
        {
            int expiredCount = 0;
            foreach (var kvp in _productCache)
            {
                if ((DateTime.Now - kvp.Value.CachedAt).TotalMinutes >= CacheExpiryMinutes)
                {
                    expiredCount++;
                }
            }
            return (_productCache.Count, expiredCount);
        }

        /// <summary>
        /// Resets the barcode buffer.
        /// </summary>
        public static void Reset()
        {
            _buffer = "";
            _lastKeyPress = DateTime.MinValue;
        }
    }
}
