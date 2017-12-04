using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using SFBBot_UCWA.Data;
using SFBBot_UCWA.Utils;
using System.Reflection;
using Microsoft.ApplicationInsights;

namespace SFBBot_UCWA.UcwaSfbo
{
    public class LyncAuth
    {
        /// <summary>
        /// Send a POST request to get the Authentication token from Lync Server
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="domain"></param>
        /// <param name="lyncOAuthUri"></param>
        /// <returns></returns>
        public static OAuthTokenRoot GetOAuthToken(HttpClient httpClient, string userName, string password, string domain, 
            string lyncOAuthUri, TelemetryClient tc)
        {
            try
            {
                OAuthTokenRoot oAuthTokenResult = new OAuthTokenRoot();
                httpClient.DefaultRequestHeaders.Clear();
                var authDic = new Dictionary<string, string>();
                authDic.Add("grant_type", "password");
                authDic.Add("username", domain + "\\" + userName);
                authDic.Add("password", password);
                HttpResponseMessage httpResponseMessage = httpClient.PostAsync(lyncOAuthUri, 
                    new FormUrlEncodedContent(authDic)).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var getOAuthTokenResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    JsonConvert.PopulateObject(getOAuthTokenResult, oAuthTokenResult);                    
                    tc.TrackTrace("Authentication token for the Skype Id received successfully.");
                    Console.WriteLine("Authentication token for the Skype Id received successfully.");
                }
                else
                {                    
                    tc.TrackTrace("Failed to receive authentication token for the Skype Id. " + httpResponseMessage.ToString());
                    tc.TrackEvent("GetOAuthToken-Failed");
                    Console.WriteLine("Unable to get the authentication token.");
                }
                return oAuthTokenResult;
            }
            catch (Exception ex)
            {
                tc.TrackException(ex);
                tc.TrackEvent("GetOAuthToken-Exception");
                Console.WriteLine("Failed to acquire authentication token for the Skype Id.");
                throw new CustomException("Error occured in " + MethodBase.GetCurrentMethod().Name + ":" + ex.Message + 
                    " TargetSite:" + ex.TargetSite + " StackTrace: " + ex.StackTrace);
            }
        }
    }
}
