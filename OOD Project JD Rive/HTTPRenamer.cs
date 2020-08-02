using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project_JD_Rive
{
    public class HTTPRenamer
    {
        private string _server;

        public HTTPRenamer(string server)
        {
            this._server = server;
        }

        public async Task<string> rename(string _username, string _password, string oldPath, string newPath)
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(_username), "username");
            form.Add(new StringContent(_password), "password");
            form.Add(new StringContent(Path.GetFileName(oldPath)), "file-to-rename");
            form.Add(new StringContent(Path.GetFileName(newPath)), "file-new-name");
            HttpResponseMessage response = await httpClient.PostAsync(_server + "/file-rename-api", form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}