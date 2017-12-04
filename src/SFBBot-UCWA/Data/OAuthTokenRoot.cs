namespace SFBBot_UCWA.Data
{
    public class OAuthTokenRoot
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string ms_rtc_identityscope { get; set; }
        public string token_type { get; set; }

    }
}
