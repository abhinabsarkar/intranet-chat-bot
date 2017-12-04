using System;
using Microsoft.ApplicationInsights;
using System.Net.Http;
using SFBBot_UCWA.Data;
using System.Net.Http.Headers;

namespace SFBBot_UCWA.UcwaSfbo
{
    public class UCWASetTyping
    {
        public static string SetTypingUri(String ucwaApplicationHostRootUri, String conversationId)
        {
            string setTypingUri = ucwaApplicationHostRootUri + ConfigData.ucwaApplication + 
                "/communication/conversations/" + conversationId + "/messaging/typing";
            return setTypingUri;
        }

        public static void SetTyping(HttpClient httpClient, OAuthTokenRoot authToken, String setTypingUri,
            TelemetryClient tc)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.access_token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Successfull response is Http 204 No content
                var httpResponseMessage =
                    httpClient.PostAsync(setTypingUri, new StringContent(string.Empty)).Result;
                string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (result != String.Empty)
                {
                    tc.TrackTrace("Failed to set the typing status.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to set the typing status.");
                tc.TrackTrace("Failed to set the typing status. More details can be found in the exception.");
                tc.TrackException(ex);
            }
        }
    }
}
