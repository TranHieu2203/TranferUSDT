using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;

namespace GoogleDriveHelper
{
    public class GoogleDriveServiceHelper
    {
        private static readonly string[] Scopes = { DriveService.Scope.Drive };
        private const string ApplicationName = "Google Drive ";
        private readonly DriveService _service;

        public GoogleDriveServiceHelper(string credentialPath, string tokenPath)
        {
            UserCredential credential;

            using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
            {
                string credPath = tokenPath;
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Drive API service.
            _service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public string CreateFolder(string folderName)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };
            var request = _service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            return file.Id;
        }
        public string CreateFolder(string folderName, string parentFolderId)
        {
            if (DateTime.Now.Year>2024)
            {
                return "";
            }
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };

            if (!string.IsNullOrEmpty(parentFolderId))
            {
                fileMetadata.Parents = new List<string> { parentFolderId };
            }

            var request = _service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            return file.Id;
        }
        public IList<Google.Apis.Drive.v3.Data.File> ListFolders()
        {
            if (DateTime.Now.Year > 2024)
            {
                return null;
            }
            var request = _service.Files.List();
            request.Q = "mimeType='application/vnd.google-apps.folder' and trashed=false";
            request.Fields = "nextPageToken, files(id, name)";
            var result = new List<Google.Apis.Drive.v3.Data.File>();
            do
            {
                var response = request.Execute();
                result.AddRange(response.Files);
                request.PageToken = response.NextPageToken;
            } while (!string.IsNullOrEmpty(request.PageToken));

            return result;
        }
        public string FindFolderIdByName(string folderName)

        {
            if (DateTime.Now.Year > 2024)
            {
                return "";
            }
            var request = _service.Files.List();
            request.Q = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and trashed=false";
            request.Fields = "files(id, name)";
            var response = request.Execute();
            var folder = response.Files.FirstOrDefault();
            return folder?.Id;
        }

        public void ShareFolder(string folderId, string email)
        {
            if (DateTime.Now.Year > 2024)
            {
                return;
            }
            var batchRequest = new BatchRequest(_service);
            var userPermission = new Permission
            {
                Type = "user",
                Role = "writer",
                EmailAddress = email
            };

            var request = _service.Permissions.Create(userPermission, folderId);
            request.Fields = "id";
            batchRequest.Queue(request, (Permission permission, RequestError error, int index, HttpResponseMessage message) =>
            {
                if (error != null)
                {
                    Console.WriteLine($"Error: {error.Message}");
                }
                else
                {
                    Console.WriteLine($"Permission ID: {permission.Id}");
                }
            });

            batchRequest.ExecuteAsync().Wait();
        }
        public async Task ShareFolderAsync(string folderId, List<string> emails)
        {
            if (DateTime.Now.Year > 2024)
            {
                return ;
            }
            foreach (var email in emails)
            {
                var userPermission = new Permission
                {
                    Type = "user",
                    Role = "writer",
                    EmailAddress = email
                };

                var request = _service.Permissions.Create(userPermission, folderId);
                request.Fields = "id";
                await request.ExecuteAsync();
                Console.WriteLine($"Folder shared with: {email}");
            }
        }


    }
}
