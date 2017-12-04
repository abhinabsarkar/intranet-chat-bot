using System;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFBBot_UCWA.Data;
using SFBBot_UCWA.Utils;
using System.Reflection;
using Microsoft.ApplicationInsights;

namespace SFBBot_UCWA.UcwaSfbo
{
    public class UcwaMakeMeAvailable
    {
        public static String GetMakeMeAvailableUri(String ucwaApplicationHostRootUri, TelemetryClient tc)
        {
            string getMakeMeAvailableUri = ucwaApplicationHostRootUri + ConfigData.ucwaApplication + "/communication/makeMeAvailable";
            return getMakeMeAvailableUri;
        }

        public static bool MakeMeAvailable(HttpClient httpClient, OAuthTokenRoot authToken, String ucwaMakeMeAvailableRootUri,
            BotAttributes botAttributes, TelemetryClient tc)
        {
            try
            {
                string makeMeAvailableResults = string.Empty;

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.access_token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var makeMeAvailablePostData = JsonConvert.SerializeObject(botAttributes);
                //Successfull response is Http 204 No content
                var httpResponseMessage =
                    httpClient.PostAsync(ucwaMakeMeAvailableRootUri, new StringContent(makeMeAvailablePostData, Encoding.UTF8,
                    "application/json")).Result;
                string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (result == String.Empty)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to set the status of the bot as Online");
                tc.TrackTrace("Failed to set the status of the bot as Online");
                tc.TrackException(ex);
                throw new CustomException("Error occured in " + MethodBase.GetCurrentMethod().Name + ":" + ex.Message +
                    " TargetSite:" + ex.TargetSite + " StackTrace: " + ex.StackTrace);
            }
        }
    }
}
