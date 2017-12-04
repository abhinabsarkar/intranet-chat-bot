using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace SFBBot_UCWA.Bot
{    
    [LuisModel("LUIS App Id", "LUIS subscription key", domain: "LUIS domain")]
    [Serializable]
    public class LuisDialog : LuisDialog<object>
    {       
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            var name = context.Activity.From.Id.Split(' ');
            string message = "I am a greeting bot. I say Hello!!!";            
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            var name = context.Activity.From.Id.Split(' ');
            string message = "Hello " + name[1] + "!!!";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
    }
}
