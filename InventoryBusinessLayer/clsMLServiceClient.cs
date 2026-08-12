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
            errorMessage = "";
            try
            {
                var response = _http.PostAsync($"{BaseUrl}/train/forecast", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    errorMessage = $"ML service returned {response.StatusCode}";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool TriggerAssociationTraining(out string errorMessage)
        {
            errorMessage = "";
            try
            {
                var response = _http.PostAsync($"{BaseUrl}/train/associations", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    errorMessage = $"ML service returned {response.StatusCode}";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool TriggerSegmentTraining(out string errorMessage)
        {
            errorMessage = "";
            try
            {
                var response = _http.PostAsync($"{BaseUrl}/train/segments", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    errorMessage = $"ML service returned {response.StatusCode}";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
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
