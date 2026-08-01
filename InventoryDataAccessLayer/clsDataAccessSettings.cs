using System;
using System.Configuration;
using System.IO;

namespace InventoryDataAccessLayer
{
    public class clsDataAccessSettings
    {
        public static string connectionString = LoadConnectionString();

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
    }
}
