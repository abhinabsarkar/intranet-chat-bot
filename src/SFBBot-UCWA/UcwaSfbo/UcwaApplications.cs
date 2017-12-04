using System;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using SFBBot_UCWA.Data;
using SFBBot_UCWA.Utils;
using System.Reflection;
using Microsoft.ApplicationInsights;

namespace SFBBot_UCWA.UcwaSfbo
{
    public class UcwaApplications
    {
       public static string CreateUcwaApps(HttpClient httpClient, OAuthTokenRoot authToken, string ucwaApplicationsRootUri,
            UcwaMyApps ucwaAppsObject, TelemetryClient tc)
        {
            try
            {
                string createUcwaAppsResults = string.Empty;

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.access_token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var createUcwaPostData = JsonConvert.SerializeObject(ucwaAppsObject);
                var httpResponseMessage =
                    httpClient.PostAsync(ucwaApplicationsRootUri, new StringContent(createUcwaPostData, Encoding.UTF8,
                    "application/json")).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {                    
                    tc.TrackTrace("Application on the UCWA server created successfully.");
                    Console.WriteLine("Application on the UCWA server created successfully.");
                    createUcwaAppsResults = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    ApplicationRoot obj = new ApplicationRoot();
                    JsonConvert.PopulateObject(createUcwaAppsResults, obj);
                    if (obj != null)
                    {
                        ConfigData.ucwaApplication = obj._links.self.href;
                        // ConfigData.ucwaApplications += ConfigData.ucwaApplication;
                        ConfigData.ucwaEvents = obj._links.events.href;
                    }
                }
                else
                {                    
                    tc.TrackTrace("Failed to create application on the UCWA server.");
                    tc.TrackEvent("CreateUcwaApps-Failed");
                    Console.WriteLine("Failed to create application on the UCWA server.");
                }
                return createUcwaAppsResults;
            }
            catch (Exception ex)
            {
                tc.TrackException(ex);
                tc.TrackEvent("CreateUcwaApps-Exception");
                Console.WriteLine("Failed to create application on the UCWA server.");
                throw new CustomException("Error occured in " + MethodBase.GetCurrentMethod().Name + ":" + ex.Message +
                    " TargetSite:" + ex.TargetSite + " StackTrace: " + ex.StackTrace);
            }
        }
    }
}
