using System;
using System.Configuration;
using System.Net.Http;

namespace InventoryBusinessLayer
{
    public static class clsMLServiceClient
    {
        private static readonly HttpClient _http = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };
        private static string BaseUrl => ConfigurationManager.AppSettings["MLServiceUrl"] ?? "http://localhost:5055";

        public static bool TriggerForecastTraining(out string errorMessage)
        {
            return Trigger("/train/forecast", out errorMessage);
        }

        public static bool TriggerAssociationTraining(out string errorMessage)
        {
            return Trigger("/train/associations", out errorMessage);
        }

        public static bool TriggerSegmentTraining(out string errorMessage)
        {
            return Trigger("/train/segments", out errorMessage);
        }

        private static bool Trigger(string path, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                var response = _http.PostAsync(BaseUrl + path, null).Result;
                if (!response.IsSuccessStatusCode)
                {
                    errorMessage = $"ML service returned {(int)response.StatusCode}.";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Could not reach ML service. Is it running? " + ex.Message;
                return false;
            }
        }

        public static bool CheckHealth(out string errorMessage)
        {
            errorMessage = "";
            try
            {
                var response = _http.GetAsync(BaseUrl + "/health").Result;
                if (!response.IsSuccessStatusCode)
                {
                    errorMessage = $"ML service health check returned {(int)response.StatusCode}.";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Could not reach ML service. Is it running? " + ex.Message;
                return false;
            }
        }
    }
}
