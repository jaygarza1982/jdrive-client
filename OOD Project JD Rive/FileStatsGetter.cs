using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project_JD_Rive
{
    public class HTTPFileStatsGetter
    {
        private string _server;
        public HTTPFileStatsGetter(string server)
        {
            this._server = server;
        }
        public async Task<string> getFileStatsAPI(string _username, string _password, string localDirectory, string serverDirectory)
        {
            //Use the file-stats-api route to get fileanmes and their last date modified
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(_username), "username");
            form.Add(new StringContent(_password), "password");
            HttpResponseMessage response = await httpClient.PostAsync(_server + "/file-stats-api" + serverDirectory, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            return response.Content.ReadAsStringAsync().Result;

            ////Array of filenames and last modified dates separated by commas
            //string[] remoteFileStats = responseStr.Split('\n');

            //return remoteFileStats;
        }
    }
}