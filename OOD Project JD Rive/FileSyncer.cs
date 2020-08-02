using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace OOD_Project_JD_Rive
{
    public class FileSyncer
    {
        private string _username, _password, _serverURL;

        public FileSyncer(string username, string password, string serverURL)
        {
            this._username = username;
            this._password = password;
            this._serverURL = serverURL;
        }

        public async System.Threading.Tasks.Task downloadAllAsync(string localDirectory, List<string> remoteFilenames)
        {
            //List all local files after first sync
            string [] localFiles = Directory.GetFiles(localDirectory);

            //Add their filenames to a list
            List<string> localFileNames = new List<string>();
            foreach (var localFileName in localFiles)
            {
                localFileNames.Add(Path.GetFileName(localFileName));
            }

            //Download all remote files that have not already been synced
            foreach (string remoteFile in remoteFilenames)
            {
                if (!localFileNames.Contains(remoteFile))
                {
                    //MessageBox.Show(remoteFile + " is not in local dir");
                    HttpClient httpClientGetter = new HttpClient();
                    MultipartFormDataContent formGetter = new MultipartFormDataContent();
                    formGetter.Add(new StringContent(_username), "username");
                    formGetter.Add(new StringContent(_password), "password");
                    HttpResponseMessage responseGetter = await httpClientGetter.PostAsync(_serverURL + "/file-download-api" + "/" + remoteFile, formGetter);
                    //responseGetter.EnsureSuccessStatusCode();
                    httpClientGetter.Dispose();
                    File.WriteAllBytes(localDirectory + "/" + remoteFile, await responseGetter.Content.ReadAsByteArrayAsync());
                }
            }
        }
    }
}