using System;
using System.Configuration;
using Newtonsoft.Json;

namespace Antech.Atlas.Objects
{
    /// <summary>
    /// Summary description for JsonObjects
    /// </summary>
    public class ClientIssueCallback
    {
        public string RowId { get; set; }
        public bool Required { get; set; }
        public string NoteType { get; set; }
        public string Details { get; set; }
        public string PersonContacted { get; set; }
        public string Error { get; set; }

        public static ClientIssueCallback FromString(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<ClientIssueCallback>(jsonString);
            }
            catch(Exception e)
            {
                return new ClientIssueCallback { Error = e.StackTrace };
            }
        }
    }

    public class AOLUser
    {
        //To do:  user credentials should be encrypted.
        public int ClinicID
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ClinicId"]); }
        }
        public string UserName
        {
            get { return ConfigurationManager.AppSettings["AOLName"]; }
        }
        public string Password
        {
            get { return ConfigurationManager.AppSettings["AOLWord"];  }
        }
    }

    public class AOLToken
    {
        public string Token { get; set; }
        public DateTimeOffset? Expiration { get; set; }
        public bool IsValidToken
        {
            get
            {
                if (string.IsNullOrEmpty(Token))
                {
                    return false;
                }

                return Antech.Report.AOLConnect.TokenInvalidated(Token);
            }
        }
    }

    public class ReportFile
    {
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public AOLToken SessionToken { get; set; }
    }
}