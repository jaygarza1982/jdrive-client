using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project_JD_Rive
{
    public class HTTPUploader
    {
        private string _server;

        public HTTPUploader(string server)
        {
            this._server = server;
        }

        public async Task<string> upload(string _username, string _password, string filePath)
        {
            var file_bytes = FileReader.readFile(filePath);

            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(_username), "username");
            form.Add(new StringContent(_password), "password");
            form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "jd-files", Path.GetFileName(filePath));
            HttpResponseMessage response = await httpClient.PostAsync(this._server + "/file-upload-api", form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}