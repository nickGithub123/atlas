using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace Antech.Client
{
    /// <summary>
    /// Summary description for APIClient
    /// </summary>
    class APIClient
    {
        //private const string url = "https://onlineapi.antechdiagnostics.com";
        private static APIClient _instance;
        private static object syncLock = new object();
        private HttpClient _client;

        private APIClient ()
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["AOLAPI"];
            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public static APIClient Instance ()
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new APIClient();
                    }
                }
            }

            return _instance;
        }

        internal HttpClient Client { get { return _client; } }
    }
}
