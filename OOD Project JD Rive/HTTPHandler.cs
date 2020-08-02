using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

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

        public async Task<string> upload(string filePath)
        {
            HTTPUploader uploader = new HTTPUploader(this._serverURL);
            return await uploader.upload(_username, _password, filePath);
        }

        public async Task<string> delete(string filePath)
        {
            HTTPDeleter deleter = new HTTPDeleter(this._serverURL);
            return await deleter.delete(_username, _password, filePath);
        }

        public async Task<string> rename(string oldPath, string newPath)
        {
            HTTPRenamer renamer = new HTTPRenamer(this._serverURL);
            return await renamer.rename(_username, _password, oldPath, newPath);
        }

        public async Task<string> pull(string serverDirectory, string localDirectory)
        {
            HTTPFileStatsGetter fileStatsGetter = new HTTPFileStatsGetter(this._serverURL);
            string files = await fileStatsGetter.getFileStatsAPI(_username, _password, localDirectory, serverDirectory);
            //MessageBox.Show(files);
            string []remoteFileStats = files.Split('\n');

            //Create a list of file names
            List<string> remoteFilenames = new List<string>();
            foreach (var file in remoteFileStats)
            {
                if (file.Split(',').Length >= 2)
                {
                    remoteFilenames.Add(file.Split(',')[0]);
                }
            }

            string[] localFiles = Directory.GetFiles(localDirectory);
            foreach (var localFile in localFiles)
            {
                //Is a file and not a directory
                if (File.Exists(localFile))
                {
                    //MessageBox.Show("File exists: " + localFile);

                    //If the file appears in the remote server listing
                    if (remoteFilenames.Contains(Path.GetFileName(localFile)))
                    {
                        DateTime localFileDate = new FileInfo(localFile).LastWriteTime;

                        double remoteFileDate = RemoteFileDateGetter.getDate(remoteFileStats, Path.GetFileName(localFile));
                        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dtDateTime.AddSeconds(remoteFileDate).ToLocalTime();

                        //If the local file is newer than the remote file, upload it to the server
                        if (localFileDate > dtDateTime)
                        {
                            HTTPHandler handler = new HTTPHandler(_serverURL, "/file-upload-api", _username, _password);
                            await handler.upload(localFile);
                        }
                        //If the remote file is newer, download it and replace the local file
                        else
                        {
                            HttpClient httpClientGetter = new HttpClient();
                            MultipartFormDataContent formGetter = new MultipartFormDataContent();
                            formGetter.Add(new StringContent(_username), "username");
                            formGetter.Add(new StringContent(_password), "password");
                            HttpResponseMessage responseGetter = await httpClientGetter.PostAsync(_serverURL + "/file-download-api" + "/" + Path.GetFileName(localFile), formGetter);
                            //responseGetter.EnsureSuccessStatusCode();
                            httpClientGetter.Dispose();
                            File.WriteAllBytes(localFile, await responseGetter.Content.ReadAsByteArrayAsync());
                        }


                    }
                }
            }

            FileSyncer syncer = new FileSyncer(_username, _password, _serverURL);
            await syncer.downloadAllAsync(localDirectory, remoteFilenames);

            return "Done";
        }
    }
}