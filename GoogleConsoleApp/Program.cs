using System;

namespace GoogleDriveHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            string credentialPath = "credentials.json";
            string tokenPath = "tokens"; // Thư mục lưu trữ token

            GoogleDriveServiceHelper driveServiceHelper = new GoogleDriveServiceHelper(credentialPath, tokenPath);

            // Tên của thư mục cha mà bạn muốn tìm.
            string parentFolderName = "Parent Folder Name";
            string parentFolderId = driveServiceHelper.FindFolderIdByName(parentFolderName);

            if (!string.IsNullOrEmpty(parentFolderId))
            {
                string newFolderId = driveServiceHelper.CreateFolder("New Subfolder", parentFolderId);
                Console.WriteLine("Created Subfolder ID: " + newFolderId);
            }
            else
            {
                string newFolderId = driveServiceHelper.CreateFolder("New Folder From ConsoleApp");

                Console.WriteLine("Parent folder not found. Create from Root");
            }
        }
    }
}
