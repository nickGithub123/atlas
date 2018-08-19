using System;
using System.Net;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Antech.Atlas.Objects;
using Antech.Client;

namespace Antech.Report
{
    /// <summary>
    /// Summary description for LabReport
    /// </summary>
    public class AOLConnect
    {
        static string userroot = ConfigurationManager.AppSettings["APIUsersRoot"];

        static async Task<AOLToken> CreatUserTokenAsync (AOLUser user)
        {
            HttpResponseMessage rsp = await APIClient.Instance().Client.PostAsJsonAsync($"{userroot}/Token", user);
            rsp.EnsureSuccessStatusCode();

            //remove credentials
            string tokenString = await rsp.Content.ReadAsStringAsync();
            AOLToken token = new AOLToken
            {
                Token = JsonConvert.DeserializeObject<string>(tokenString),
                Expiration = rsp.Content.Headers.Expires ?? null
            };

            return token;
        }

        static async Task<ReportFile> GetLabReportFileAsync (string fileType, string token, string accessionId, int localTimeZone, int? isDownload = 1)
        {
            HttpResponseMessage rsp = await APIClient.Instance().Client.GetAsync(
                                    $"api/v1.1/LabResults/{fileType}?accessToken={token}&accessionIDs={accessionId}&localTimeZone={localTimeZone}&isDownload={isDownload}");

            rsp.EnsureSuccessStatusCode();

            ReportFile readFile = new ReportFile
            {
                Name = rsp.Content.Headers.ContentDisposition?.FileName ?? string.Empty,
                FileType = fileType,
                Content = await rsp.Content.ReadAsByteArrayAsync(),
                ContentType = rsp.Content.Headers.ContentType?.MediaType ?? $"application/{fileType}"
            };
            return readFile;
        }

        static async Task<bool> TokenValidated(string accessToken)
        {
            try
            {
                HttpResponseMessage rsp = await APIClient.Instance().Client.GetAsync(
                                            $"{userroot}/ValidateToken?accessToken={accessToken}");
                rsp.EnsureSuccessStatusCode();

                return await rsp.Content.ReadAsAsync<bool>();
            }
            catch (HttpRequestException hre)
            {
                throw new Exception($"Message: {hre.Message}/r/nStackTrace: {hre.StackTrace}/r/nSource:{hre.Source}", hre.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception($"Message: {ex.Message}/r/nStackTrace: {ex.StackTrace}/r/nSource:{ex.Source}", ex.InnerException);
            }
        }

        static async Task<ReportFile> RunClientAsync (string accession, string fileType, int timezone, AOLToken sessionToken)
        {
            try
            {
                if (sessionToken == null || !sessionToken.IsValidToken)
                {
                    //invalid session token then create one
                    sessionToken = await CreatUserTokenAsync(new AOLUser());
                    ReportFile readFile = await GetLabReportFileAsync(fileType, sessionToken.Token, accession, timezone);
                    readFile.SessionToken = sessionToken;

                    return readFile;
                }

                return await GetLabReportFileAsync(fileType, sessionToken.Token, accession, timezone);
            }
            catch (HttpRequestException hre)
            {
                throw new Exception($"Message: {hre.Message}/r/nStackTrace: {hre.StackTrace}/r/nSource:{hre.Source}", hre.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception($"Message: {ex.Message}/r/nStackTrace: {ex.StackTrace}/r/nSource:{ex.Source}", ex.InnerException);
            }
        }

        public static ReportFile GetReportContent (string accession, string fileType, int timezone, AOLToken sessionToken)
        {
            Task<ReportFile> content = Task.Run<ReportFile>(
                                        async() =>await RunClientAsync(accession, fileType, timezone, sessionToken));
            content.Wait();
            return content.Result;
        }

        public static bool TokenInvalidated(string token)
        {
            Task<bool> validated = Task.Run<bool>(
                                         async () => await TokenValidated(token));

            validated.Wait();
            return validated.Result;
        }
    }
}