using BE;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
namespace DAL
{
    public class GoogleDriveAPIHelper
    {
        //add scope
        public string[] Scopes = { Google.Apis.Drive.v3.DriveService.Scope.Drive };

        /// <summary>
        /// create Drive API service
        /// </summary>
        /// &lt;returns&gt;&lt;/returns&gt;
        private Google.Apis.Drive.v3.DriveService GetService()
        {
            //get Credentials from client_secret.json file
            UserCredential credential;
            //Root Folder of project
            var CSPath = System.Web.Hosting.HostingEnvironment.MapPath(" ~/");
            using (var stream = new FileStream(@" C:\Users\User\Desktop\project\DAL\bin\Debug\credentials.json",
                FileMode.Open, FileAccess.Read))
            {
                String FolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/ ");
                String FilePath = Path.Combine(FolderPath, "DriveServiceCredentials.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(FilePath, true)).Result;
            }
            //create Drive API service.
            Google.Apis.Drive.v3.DriveService service = new Google.Apis.Drive.v3.DriveService(new
            BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleDriveMVCUpload",
            });
            return service;
        }
        #region DELETE
        /**
          * Permanently delete a file, skipping the trash.
          *
          * @param fileId ID of the file to delete.
          */
        public void deleteFile(string fileName)
        {
            string fileId = (from file in this.GetDriveFiles()
                             where @"/images/"+file.Name == (fileName + Path.GetExtension(file.Name))
                             select file.Id).FirstOrDefault();
            if (fileId != null)
            {
                //create service
                DriveService service = GetService();
                FilesResource.DeleteRequest DeleteRequest = service.Files.Delete(fileId);
                DeleteRequest.Execute();
            }
        }
        public void deleteAllFiles(string filesName)
        {
            var fileIds = (from file in this.GetDriveFiles()
                           where file.Name == (filesName + Path.GetExtension(file.Name))
                           select file.Id).ToList();
            DriveService service = GetService();
            foreach (var fileId in fileIds)
            {
                FilesResource.DeleteRequest DeleteRequest = service.Files.Delete(fileId);
                DeleteRequest.Execute();
            }
        }
        #endregion

        /// <summary>
        /// file Upload to the Google Drive root folder
        /// </summary>
        /// <param name="file"</param>
        public void UplaodFileOnDrive(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                //create service
                Google.Apis.Drive.v3.DriveService service = GetService();
                string path =
                Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
                Path.GetFileName(file.FileName));
                file.SaveAs(path);
                var FileMetaData = new Google.Apis.Drive.v3.Data.File();
                FileMetaData.Name = Path.GetFileName(file.FileName);
                FileMetaData.MimeType = MimeMapping.GetMimeMapping(path);
                Google.Apis.Drive.v3.FilesResource.CreateMediaUpload request;
                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }

            }
        }

        private string folderNameToFolderId(string folderName)
        {
            return (from folder in this.GetDriveFolders()
                    where folder.Name == folderName
                    select folder.Id).FirstOrDefault();

        }
        /// &lt;summary&gt;
        /// File upload to the Google Drive in existing folder
        /// &lt;/summary&gt;

        /// &lt;param name=&quot;file&quot;&gt;&lt;/param&gt;
        /// &lt;param name=&quot;folderId&quot;&gt;&lt;/param&gt;
        public void UplaodFileOnDriveInFolder(HttpPostedFileBase file, string folderName)
        {
            if (file != null && file.ContentLength > 0)
            {
                //convert from folder name to it Id
                string folderId = this.folderNameToFolderId(folderName);
                //if folder not founded- create one and enter the file to the new folder
                if (folderId == null)
                {
                    this.CreateFolder(folderName);
                    folderId = this.folderNameToFolderId(folderName);
                }
                //create service
                DriveService service = GetService();
                //get file path
                string path =
                Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
                Path.GetFileName(file.FileName));
                file.SaveAs(path);
                //create file metadata
                var FileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(file.FileName),
                    MimeType = MimeMapping.GetMimeMapping(path),
                    //id of parent folder
                    Parents = new List<string>
            {
                folderId
            }

                };
                Google.Apis.Drive.v3.FilesResource.CreateMediaUpload request;
                //create stream and upload
                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
                var file1 = request.ResponseBody;
            }
        }

        /// &lt;summary&gt;
        /// File upload to the Google Drive in existing folder with new file name
        /// &lt;/summary&gt;
        /// &lt;param name=&quot;file&quot;&gt;&lt;/param&gt;
        /// &lt;param name=&quot;folderId&quot;&gt;&lt;/param&gt;
        /// &lt;param name=&quot;newFileName&quot;&gt;&lt;/param&gt;
        public void UplaodFileOnDriveInFolder(HttpPostedFileBase file, string newFileName,
        string folderName)
        {
            if (file != null && file.ContentLength > 0)
            {
                //convert from folder name to it Id
                string folderId = this.folderNameToFolderId(folderName);
                //if folder not founded- create one and enter the file to the new folder
                if (folderId == null)
                {
                    this.CreateFolder(folderName);
                    folderId = this.folderNameToFolderId(folderName);
                }
                //create service
                DriveService service = GetService();
                //get file path
                string path =
                Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
                Path.GetFileName(file.FileName));
                file.SaveAs(path);
                //create file metadata
                var FileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = newFileName + Path.GetExtension(file.FileName),
                    MimeType = MimeMapping.GetMimeMapping(path),
                    //id of parent folder
                    Parents = new List<string>
        {
            folderId
            }
                };
                Google.Apis.Drive.v3.FilesResource.CreateMediaUpload request;
                //create stream and upload
                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
                var file1 = request.ResponseBody;
            }
        }
        /// &lt;summary&gt;
        /// returns all files from drive
        /// &lt;/summary&gt;
        /// &lt;returns&gt;&lt;/returns&gt;
        public List<GoogleDriveFile> GetDriveFiles()
        {
            Google.Apis.Drive.v3.DriveService service = GetService();
            // Define parameters of request.
            Google.Apis.Drive.v3.FilesResource.ListRequest FileListRequest = service.Files.List();

            // for getting folders only.
            //FileListRequest.Q = "mimeType=&#39;application/vnd.google-apps.folder&#39;";
            FileListRequest.Fields = "nextPageToken, files(*)";
            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            List<GoogleDriveFile> FileList = new List<GoogleDriveFile>();
            // For getting only folders
            // files = files.Where(x =&gt; x.MimeType == &quot;application/vnd.google-apps.folder ").ToList();
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    GoogleDriveFile File = new GoogleDriveFile
                    {
                        Id = file.Id,
                        Name = file.Name,
                        Size = file.Size,
                        Version = file.Version,
                        CreatedTime = file.CreatedTime,
                        Parents = file.Parents,

                        MimeType = file.MimeType
                    };
                    FileList.Add(File);
                }
            }
            return FileList;
        }

        /// &lt;summary&gt;
        /// get only the folders from google drive
        /// &lt;/summary&gt;
        /// &lt;returns&gt;&lt;/returns&gt;
        public List<GoogleDriveFile> GetDriveFolders()
        {
            DriveService service = GetService();
            List<GoogleDriveFile> FolderList = new List<GoogleDriveFile>();
            FilesResource.ListRequest request = service.Files.List();
            request.Q = "mimeType='application/vnd.google-apps.folder'";
            request.Fields = "files(id, name)";
            Google.Apis.Drive.v3.Data.FileList result = request.Execute();
            foreach (var file in result.Files)
            {
                GoogleDriveFile File = new GoogleDriveFile
                {
                    Id = file.Id,
                    Name = file.Name,
                    Size = file.Size,
                    Version = file.Version,
                    CreatedTime = file.CreatedTime
                };
                FolderList.Add(File);
            }
            return FolderList;
        }
        /// &lt;summary&gt;
        /// Create Folder in root
        /// &lt;/summary&gt;
        /// &lt;param name=&quot;FolderName&quot;&gt;&lt;/param&gt;
        public void CreateFolder(string FolderName)
        {
            DriveService service = GetService();
            var FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = FolderName;
            //this mimetype specify that we need folder in google drive
            FileMetaData.MimeType = "application / vnd.google - apps.folder ";
            Google.Apis.Drive.v3.FilesResource.CreateRequest request;
            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            var file = request.Execute();

        }

        /// &lt;summary&gt;
        /// Download file from Google Drive by file name (the name should be without extension)
        /// &lt;/summary&gt;
        /// &lt;param name=&quot;fileName&quot;&gt;&lt;/param&gt;
        /// &lt;returns&gt;&lt;/returns&gt;
        public string DownloadGoogleFileByName(string fileName)
        {
            string fileId = (from file in this.GetDriveFiles()
                             where file.Name == (fileName + Path.GetExtension(file.Name))
                             select file.Id).FirstOrDefault();
            if (fileId != null)
                return this.DownloadGoogleFile(fileId);
            else
                return "icorrect file name";
        }

        /// &lt;summary&gt;
        /// Download file from Google Drive by fileId
        /// &lt;/summary&gt;
        /// &lt;param name=&quot;fileId&quot;&gt;&lt;/param&gt;
        /// &lt;returns&gt;&lt;/returns&gt;
        public string DownloadGoogleFile(string fileId)
        {
            DriveService service = GetService();
            string FolderPath = HttpContext.Current.Server.MapPath("/ GoogleDriveFiles /");
            Google.Apis.Drive.v3.FilesResource.GetRequest request = service.Files.Get(fileId);
            string FileName = request.Execute().Name;
            string FilePath = Path.Combine(FolderPath, FileName);
            MemoryStream stream1 = new MemoryStream();
            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged += (IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case DownloadStatus.Downloading:
                        {
                    //Console.WriteLine(progress.BytesDownloaded);

                    break;
                        }
                    case DownloadStatus.Completed:
                        {
                    // Console.WriteLine(&quot;Download complete.&quot;);
                    SaveStream(stream1, FilePath);
                            break;
                        }
                    case DownloadStatus.Failed:
                        {
                    //Console.WriteLine(&quot;Download failed.&quot;);
                    break;
                        }
                }
            };
            request.Download(stream1);
            return FilePath;
        }

        /// &lt;summary&gt;
        /// file save to server path
        /// &lt;/summary&gt;
        /// &lt;param name=&quot;stream&quot;&gt;&lt;/param&gt;
        /// &lt;param name=&quot;FilePath&quot;&gt;&lt;/param&gt;
        private void SaveStream(MemoryStream stream, string FilePath)
        {
            using (FileStream file = new FileStream(FilePath, FileMode.Create,
            FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }

        }

        
        /*
        public List<GoogleDriveFile> GetContainsInFolder(String folderId)
        {
            List<string> ChildList = new List<string>();
            Google.Apis.Drive.v2.DriveService ServiceV2 = GetService();
            ChildrenResource.ListRequest ChildrenIDsRequest = ServiceV2.Children.List(folderId);

            // for getting only folders    
            //ChildrenIDsRequest.Q = "mimeType='application/vnd.google-apps.folder'";    
            do
            {
                var children = ChildrenIDsRequest.Execute();

                if (children.Items != null && children.Items.Count > 0)
                {
                    foreach (var file in children.Items)
                    {
                        ChildList.Add(file.Id);
                    }
                }
                ChildrenIDsRequest.PageToken = children.NextPageToken;

            } while (!String.IsNullOrEmpty(ChildrenIDsRequest.PageToken));

            //Get All File List    
            List<GoogleDriveFile> AllFileList = GetDriveFiles();
            List<GoogleDriveFile> Filter_FileList = new List<GoogleDriveFile>();

            foreach (string Id in ChildList)
            {
                Filter_FileList.Add(AllFileList.Where(x => x.Id == Id).FirstOrDefault());
            }
            return Filter_FileList;
        }
        */

        // Create Folder in existing folder    
        public void CreateFolderInFolder(string folderId, string FolderName)
        {

            Google.Apis.Drive.v3.DriveService service = GetService();

            var FileMetaData = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(FolderName),
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string>
                   {
                       folderId
                   }
            };


            Google.Apis.Drive.v3.FilesResource.CreateRequest request;

            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            var file = request.Execute();
            Console.WriteLine("Folder ID: " + file.Id);

            var file1 = request;

        }

        // File upload in existing folder    
        public void FileUploadInFolder(string folderId, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                Google.Apis.Drive.v3.DriveService service = GetService();

                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
                Path.GetFileName(file.FileName));
                file.SaveAs(path);

                var FileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(file.FileName),
                    MimeType = MimeMapping.GetMimeMapping(path),
                    Parents = new List<string>
                   {
                       folderId
                   }
                };

                Google.Apis.Drive.v3.FilesResource.CreateMediaUpload request;
                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
                var file1 = request.ResponseBody;
            }
        }


        // check Folder name exist or note in root    
        public bool CheckFolder(string FolderName)
        {
            bool IsExist = false;

            Google.Apis.Drive.v3.DriveService service = GetService();

            // Define the parameters of the request.    
            Google.Apis.Drive.v3.FilesResource.ListRequest FileListRequest = service.Files.List();
            FileListRequest.Fields = "nextPageToken, files(*)";

            // List files.    
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            List<GoogleDriveFile> FileList = new List<GoogleDriveFile>();


            //For getting only folders    
            files = files.Where(x => x.MimeType == "application/vnd.google-apps.folder" && x.Name == FolderName).ToList();

            if (files.Count > 0)
            {
                IsExist = false;
            }
            return IsExist;
        }

        public string MoveFiles(String fileId, String folderId)
        {
            Google.Apis.Drive.v3.DriveService service = GetService();

            // Retrieve the existing parents to remove    
            Google.Apis.Drive.v3.FilesResource.GetRequest getRequest = service.Files.Get(fileId);
            getRequest.Fields = "parents";
            Google.Apis.Drive.v3.Data.File file = getRequest.Execute();
            string previousParents = String.Join(",", file.Parents);

            // Move the file to the new folder    
            Google.Apis.Drive.v3.FilesResource.UpdateRequest updateRequest = service.Files.Update(new Google.Apis.Drive.v3.Data.File(), fileId);
            updateRequest.Fields = "id, parents";
            updateRequest.AddParents = folderId;
            updateRequest.RemoveParents = previousParents;

            file = updateRequest.Execute();
            if (file != null)
            {
                return "Success";
            }
            else
            {
                return "Fail";
            }
        }
        public string CopyFiles(String fileId, String folderId)
        {
            Google.Apis.Drive.v3.DriveService service = GetService();

            // Retrieve the existing parents to remove    
            Google.Apis.Drive.v3.FilesResource.GetRequest getRequest = service.Files.Get(fileId);
            getRequest.Fields = "parents";
            Google.Apis.Drive.v3.Data.File file = getRequest.Execute();

            // Copy the file to the new folder    
            Google.Apis.Drive.v3.FilesResource.UpdateRequest updateRequest = service.Files.Update(new Google.Apis.Drive.v3.Data.File(), fileId);
            updateRequest.Fields = "id, parents";
            updateRequest.AddParents = folderId;
            //updateRequest.RemoveParents = previousParents;    
            file = updateRequest.Execute();
            if (file != null)
            {
                return "Success";
            }
            else
            {
                return "Fail";
            }
        }


    }
}
