using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project_JD_Rive
{
    public class HTTPDeleter
    {
        private string _server;

        public HTTPDeleter(string server)
        {
            this._server = server;
        }

        public async Task<string> delete(string _username, string _password, string file)
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(_username), "username");
            form.Add(new StringContent(_password), "password");
            form.Add(new StringContent(Path.GetFileName(file)), "file-to-delete");
            HttpResponseMessage response = await httpClient.PostAsync(_server + "/file-delete-api", form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}