using System;
using System.Configuration;
using System.Net;

namespace SFBBot_UCWA
{
    public class MyProxy : IWebProxy
    {
        public Uri GetProxy(Uri destination)
        {            
            string proxy = ConfigurationManager.AppSettings["proxyaddress"];
            return new Uri(proxy);
        }

        public bool IsBypassed(Uri host)
        {
            //Bypass it only for the corporate domain, else go through the proxy
            if (host.ToString().Contains(".mycompanydomain.com"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ICredentials Credentials
        {
            get
            {
                string proxyusername = ConfigurationManager.AppSettings["proxyusername"];
                string proxypassword = ConfigurationManager.AppSettings["proxypassword"];
                return new NetworkCredential(proxyusername, proxypassword);
            }
            set { }
        }
    }
}
