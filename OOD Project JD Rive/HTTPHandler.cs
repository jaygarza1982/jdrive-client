using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.IO;
using System.Windows.Forms;

namespace OOD_Project_JD_Rive
{
    public class HTTPHandler
    {
        private string _serverURL = "", _username, _password, _action;
        public HTTPHandler(string server, string action, string username, string password)
        {
            _serverURL = server;
            _action = action;
            _username = username;
            _password = password;
        }

        public async void upload(string filePath)
        {
            var file_bytes = readFile(filePath);

            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(_username), "username");
            form.Add(new StringContent(_password), "password");
            form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "jd-files", Path.GetFileName(filePath));
            HttpResponseMessage response = await httpClient.PostAsync(_serverURL + _action, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;
        }

        public async void delete(string filePath)
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(_username), "username");
            form.Add(new StringContent(_password), "password");
            form.Add(new StringContent(Path.GetFileName(filePath)), "file-to-delete");
            HttpResponseMessage response = await httpClient.PostAsync(_serverURL + _action, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;
        }

        public async void rename(string oldPath, string newPath)
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(_username), "username");
            form.Add(new StringContent(_password), "password");
            form.Add(new StringContent(Path.GetFileName(oldPath)), "file-to-rename");
            form.Add(new StringContent(Path.GetFileName(newPath)), "file-new-name");
            HttpResponseMessage response = await httpClient.PostAsync(_serverURL + _action, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;
        }

        public async void pull(string serverDirectory, string localDirectory)
        {
            string[] filesInDir = Directory.GetFiles(localDirectory);

            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(_username), "username");
            form.Add(new StringContent(_password), "password");
            HttpResponseMessage response = await httpClient.PostAsync(_serverURL + _action + serverDirectory, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string responseStr = response.Content.ReadAsStringAsync().Result;

            //Array of filenames and last modified dates separated by commas
            string[] remoteFileStats = responseStr.Split('\n');

            //Create a list of file names
            List<string> remoteFileListing = new List<string>();
            foreach (var file in remoteFileStats)
            {
                if (file.Split(',').Length >= 2)
                {
                    remoteFileListing.Add(file.Split(',')[0]);
                }
            }

            foreach (var localFile in filesInDir)
            {
                //Is a file and not a directory
                //MessageBox.Show(localFile);
                if (File.Exists(localFile))
                {
                    MessageBox.Show("File exists: " + localFile);

                    //If the file appears in the remote server listing
                    if (remoteFileListing.Contains(Path.GetFileName(localFile)))
                    {
                        //MessageBox.Show(localFile + " is on the server.");
                        //MessageBox.Show(File.GetLastWriteTime(localFile).Date.ToString());

                        DateTime localFileDate = new FileInfo(localFile).LastWriteTime;   //.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
   

                        double remoteFileDate = this.getRemoteFileDate(remoteFileStats, Path.GetFileName(localFile));
                        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dtDateTime.AddSeconds(remoteFileDate).ToLocalTime();

                        MessageBox.Show("Local date: " + localFileDate.ToString() + "\nRemote date: " + remoteFileDate.ToString());
                        if (localFileDate > dtDateTime)
                        {
                            MessageBox.Show("Local file is newer!");
                            HTTPHandler handler = new HTTPHandler(_serverURL, "/file-upload-api", _username, _password);
                            handler.upload(localFile);
                        }
                        else
                        {
                            MessageBox.Show("Remote file is newer!");





                            HttpClient httpClientGetter = new HttpClient();
                            MultipartFormDataContent formGetter = new MultipartFormDataContent();
                            formGetter.Add(new StringContent(_username), "username");
                            formGetter.Add(new StringContent(_password), "password");
                            HttpResponseMessage responseGetter = await httpClientGetter.PostAsync(_serverURL + "/file-download-api" + "/" + Path.GetFileName(localFile), formGetter);
                            //responseGetter.EnsureSuccessStatusCode();
                            httpClientGetter.Dispose();
                            //var resp = await responseGetter.Content;//.ReadAsStringAsync();
                            //MessageBox.Show("Remote file newer response: " + resp);
                            File.WriteAllBytes(localFile, await responseGetter.Content.ReadAsByteArrayAsync());




                        }
                    }
                }
            }

            //foreach (var remoteFile in remoteFileListing)
            //{
            //    string[] stats = remoteFile.Split(',');
            //    string remoteFileName = stats[0];
            //    string remoteLastModified = "";
            //    if (stats.Length > 1)
            //        remoteLastModified = stats[1];

            //    if (filesInDir.Contains(remoteFileName))
            //    {
            //        DateTime localFileDate = File.GetLastWriteTime(localDirectory + "/" + remoteFileName);
            //        DateTime remoteFileDate = DateTime.Parse(remoteLastModified);

            //        if (remoteFileDate > localFileDate)
            //        {
            //            HttpClient httpClientGetter = new HttpClient();
            //            MultipartFormDataContent formGetter = new MultipartFormDataContent();
            //            form.Add(new StringContent(_username), "username");
            //            form.Add(new StringContent(_password), "password");
            //            HttpResponseMessage responseGetter = await httpClientGetter.PostAsync(_serverURL + "/file-download-api/" + serverDirectory, form);
            //            responseGetter.EnsureSuccessStatusCode();
            //            httpClientGetter.Dispose();
            //            var resp = responseGetter.Content;
            //            File.WriteAllBytes(localDirectory + "/" + remoteFile, await resp.ReadAsByteArrayAsync());
            //        }
            //    }
            //    else
            //    {

            //    }

            //    MessageBox.Show(remoteFileName + " " + remoteLastModified);
            //}
        }

        private double getRemoteFileDate(string[] fileListings, string filename)
        {
            foreach (var fileListing in fileListings)
            {
                string[] split = fileListing.Split(',');

                //MessageBox.Show("file listing " + fileListing);
                if (split.Length >= 2)
                {
                    MessageBox.Show("remote file: " + split[0] + "\n filename: " + filename + "\n remote: " + split[1]);
                    if (split[0] == filename)
                    {
                        return double.Parse(split[1]);
                    }
                }
            }

            //If we do not find the file, we say that it is old
            return 0;
        }

        private byte[] readFile(string filePath)
        {
            //Wait until we can read file
            while (true)
            {
                try
                {
                    using (StreamReader stream = new StreamReader(filePath))
                    {
                        byte[] bytes = File.ReadAllBytes(filePath);
                        return bytes;
                    }
                }
                catch
                {
                    System.Threading.Thread.Sleep(50);
                }
            }
        }
    }
}