# Automation using Cognitive Chat Bots
### Intranet Chat Bot using Skype for Business On-Premise and Microsoft Bot Framework

> *Note: A new version of the Skype for Business Intranet Chat Bot developed on Dotnet Core using Bot Framework SDK V4 is available [here](https://github.com/abhinabsarkar/sfb-on-prem-bot)*

Skype for Business is the one of the most common enterprise Instant Messaging platform for thousands of businesses worldwide. However, as a messaging platform, Skype for Business 2015 On-Premise does not support bots out of the box. As per Microsoft, the Skype for Business Bot Framework channel is currently supported for Skype for Business Online and Skype for Business Hybrid environments only. Refer [this](https://msdn.microsoft.com/en-us/skype/skype-for-business-bot-framework/docs/overview#version-suport).

Skype for Business On-Premise has a set of APIs called Unified Communications Web API (UCWA) which can be leveraged to integrate with the Bot framework. UCWA 2.0 is a REST API that exposes Skype for Business Server 2015 instant messaging (IM) and presence capabilities. For a bot to send and receive message using UCWA, the following steps are required to be performed.
1.	Auto Discovery – finding the Lync Server.
2.	Authenticate the Bot with the Lync Server.
3.	Register the App on the Lync Server.

Once the App is registered on Lync server, one can send and receive messages to the bot using Skype. The detailed steps to register an application on the Lync server can be found here.
https://ucwa.skype.com/documentation/keytasks-createapplication

At this point, the high level architecture of the application will look like this.![Alt text](https://github.com/abhinabsarkar/intranet-chat-bot/blob/master/images/Application%20architecture.png)

 
The application will now poll the Lync server for incoming messages. When a message is received by the application, we bootstrap our bot and pass the text as properly formatted Activity message. The bot then follows usual flow of sending the text to LUIS and determining the intent. Based on the context, it will then send the response back to the user. The end to end architecture of the bot can be seen below. ![Alt text](https://github.com/abhinabsarkar/intranet-chat-bot/blob/master/images/High%20level%20architecture.png)

# Code
The source code can be found [here](https://github.com/abhinabsarkar/intranet-chat-bot/tree/master/src). It is based on [Ankit Sinha’s sample](https://github.com/ankitbko/ucwa-bot), very well explained blog, written for Skype for Business Online. 

# Benefits
The Skype for Business On-Premise bot has advantages which the Enterprise demands like 
 1.	Keeping the data and the application On-Premise
 2.	Using the common communication channel IM 
 3.	Leveraging Cloud services like LUIS cognitive service and App Insights
 4.	Work behind corporate proxy.
