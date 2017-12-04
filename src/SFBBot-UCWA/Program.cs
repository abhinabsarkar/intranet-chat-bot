using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using SFBBot_UCWA.Data;
using SFBBot_UCWA.UcwaSfbo;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace SFBBot_UCWA
{
    class Program
    {
        //Skype credentials
        private static string userName = ConfigurationManager.AppSettings["UserName"].ToString();
        private static string domain = ConfigurationManager.AppSettings["Domain"].ToString();
        private static string password = ConfigurationManager.AppSettings["Password"].ToString();

        //Lync server OAuth uri
        private static string lyncOAuthUri = ConfigurationManager.AppSettings["LyncOAuthUrl"].ToString();
        //UCWA server applications resource uri
        private static string ucwaApplicationsUri = ConfigurationManager.AppSettings["UCWAApplicationsUrl"].ToString();
        //UCWA application host server
        private static string ucwaApplicationsHost = ConfigurationManager.AppSettings["UCWAApplicationsHost"].ToString();

        // make sure you grant at least these three permissions to your app
        // Create Skype Meetings
        // Initiate conversations and join meetings
        // Read/write Skype user information (preview)        

        // createUcwaAppsResults - stores the result of making a POST call to
        // ucwaApplicationsUri.  This is a JSON string that contains the link to
        // UCWA application resources, such as:
        // me, people, onlineMeetings, and communciation

        private static string createUcwaAppsResults = "";

        // Be resource efficient and declare and re-use single System.Net.Http.HttpClient 
        // for use across your entire app.  Otherwise you'll run out of resources over time
        // httpClient is thread and re-entrant safe

        // You will need to pass an httpClient to each UCWA network operation

        internal static HttpClient httpClient = new HttpClient();

        static void Main(string[] args)
        {
            string commandString = string.Empty;
            Console.ForegroundColor = ConsoleColor.White;

            //Configure the app insights instrumentation key
            string instrumentationKey = ConfigurationManager.AppSettings["AppInsightsIKey"];
            TelemetryConfiguration.Active.InstrumentationKey = instrumentationKey;
            TelemetryClient tc = new TelemetryClient();

            try
            {
                //Set the UCWA applications host
                ConfigData.ucwaApplicationsHost = ucwaApplicationsHost;

                //Get the Authentication token                
                ConfigData.authToken = LyncAuth.GetOAuthToken(httpClient, userName, password, domain, lyncOAuthUri, tc);
                if (ConfigData.authToken.access_token != null)
                {
                    //Create an application on the UCWA server
                    List<string> Modalities = new List<string>();
                    Modalities.Add("PhoneAudio");
                    Modalities.Add("Messaging");
                    //Set the properties for the applications resource
                    UcwaMyApps ucwaMyAppsObject = new UcwaMyApps()
                    {
                        UserAgent = "IntranetBot",
                        EndpointId = Guid.NewGuid().ToString(),
                        Culture = "en-US"
                    };
                    //Create an application on the Skype UCWA server and register it.
                    createUcwaAppsResults = UcwaApplications.CreateUcwaApps(httpClient, ConfigData.authToken, ucwaApplicationsUri, ucwaMyAppsObject, tc);

                    //Set IM status as online
                    SetIMStatus(tc);

                    //Get the message. The method "GetIM_Step03_Events" in the class UCWAReceiveMessage is a recursive function 
                    //which will keep listening for the incoming messages
                    GetMessage(tc, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tc.TrackException(ex);
            }

            while (!commandString.Equals("Exit", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Enter exit to exit");
                commandString = Console.ReadLine();
                //commandString = "msg";
                switch (commandString.ToUpper())
                {
                    case "EXIT":
                        Console.WriteLine("Bye!"); ;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }

        static void SetIMStatus(TelemetryClient tc)
        {
            List<string> MessageFormats = new List<string>();
            MessageFormats.Add("Plain");
            MessageFormats.Add("Html");

            List<string> Modalities = new List<string>();
            // Modalities.Add("PhoneAudio");
            Modalities.Add("Messaging");

            BotAttributes botAttributes = new BotAttributes
            {
                phoneNumber = "",
                signInAs = "Online", // Status - Online, Busy, DoNotDisturb, BeRightBack, Away, or Offwork
                supportedMessageFormats = MessageFormats,
                supportedModalities = Modalities

            };

            var ucwaMakeMeAvailableRootUri = UcwaMakeMeAvailable.GetMakeMeAvailableUri(ucwaApplicationsHost, tc);
            if (ucwaMakeMeAvailableRootUri != String.Empty)
            {

                if (UcwaMakeMeAvailable.MakeMeAvailable(httpClient, ConfigData.authToken, ucwaMakeMeAvailableRootUri, botAttributes, tc))
                {
                    Console.WriteLine("Skype ID's status set to Online.");
                    tc.TrackTrace("Skype ID's status set to Online.");
                }
                else
                {
                    Console.WriteLine("Unable to set Skype ID's status as Online.");
                    tc.TrackTrace("Unable to set Skype ID's status as Online.");
                }
                return;
            }

        }

        #region Commands

        static void GetMessage(TelemetryClient tc, bool IsNextMsg = false)
        {
            if (ConfigData.authToken == null)
            {
                Console.WriteLine("You haven't logged in yet!");
                tc.TrackTrace("You haven't logged in yet!");
                return;
            }

            Console.WriteLine("Bot is listening to messages...");
            tc.TrackEvent("BotActive");
            UcwaReciveMessage.GetMessage(httpClient, tc, ConfigData.authToken, IsNextMsg);
        }
        #endregion
    }
}
