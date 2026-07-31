using System;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    /// <summary>
    /// USB Barcode Scanner helper for POS integration.
    /// Detects barcode scanner input and triggers product lookup.
    /// </summary>
    public static class clsBarcodeScanner
    {
        private static string _buffer = "";
        private static DateTime _lastKeyPress = DateTime.MinValue;
        private const int ScannerTimeoutMs = 100; // Time to wait for complete barcode scan

        /// <summary>
        /// Event raised when a complete barcode is scanned.
        /// </summary>
        public static event Action<string> BarcodeScanned;

        /// <summary>
        /// Processes a key press to detect barcode scanner input.
        /// Call this from the form's KeyPress event.
        /// </summary>
        public static bool ProcessKeyPress(KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;

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
                    BarcodeScanned?.Invoke(barcode);
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
        /// Resets the barcode buffer.
        /// </summary>
        public static void Reset()
        {
            _buffer = "";
            _lastKeyPress = DateTime.MinValue;
        }
    }
}
