using System.Collections.Generic;

namespace SFBBot_UCWA.Data
{
    public class BotAttributes
    {
        public string signInAs { get; set; }
        public string phoneNumber { get; set; }

        public List<string> supportedMessageFormats { get; set; }
        public List<string> supportedModalities { get; set; }
    }
}
