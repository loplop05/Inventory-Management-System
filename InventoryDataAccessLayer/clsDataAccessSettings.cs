using System;
using System.Configuration;
using System.IO;

namespace InventoryDataAccessLayer
{
    public class clsDataAccessSettings
    {
        public static string connectionString = LoadConnectionString();
        public static decimal TaxRate = LoadTaxRate();

        private static string LoadConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["InventoryDB"].ConnectionString;

            string localConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App.config.local");
            if (!File.Exists(localConfigPath))
                return connectionString;

            try
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = localConfigPath
                };

                Configuration localConfig = ConfigurationManager.OpenMappedExeConfiguration(
                    configMap, ConfigurationUserLevel.None);

                ConnectionStringSettings localSetting = localConfig.ConnectionStrings.ConnectionStrings["InventoryDB"];
                if (localSetting != null && !string.IsNullOrWhiteSpace(localSetting.ConnectionString))
                    return localSetting.ConnectionString;
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("clsDataAccessSettings.LoadConnectionString", ex);
            }

            return connectionString;
        }

        private static decimal LoadTaxRate()
        {
            string taxRateStr = ConfigurationManager.AppSettings["TaxRate"];
            decimal taxRate;

            if (decimal.TryParse(taxRateStr, out taxRate))
                return taxRate;

            // Default to 7% if not configured
            return 0.07m;
        }
    }
}
