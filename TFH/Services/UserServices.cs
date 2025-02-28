using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.VisualBasic;
using Models;
using System.Drawing;

namespace Services
{
    public class UserServices : IUserServices
    {
        //private BlobServiceClient _BlobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=blobtam2025;AccountKey=Nei6bYXXBEJo1i2cDkMywWj/nbHzdWahwn8ExRrziwg5+BdtbGcvojyCwl3mR5qk3xVMSxnLWnK1+ASt4pjvng==;EndpointSuffix=core.windows.net"); // using Account Name/Key

        private BlobServiceClient _BlobServiceClient = new BlobServiceClient(
            new Uri("https://blobtam2025.blob.core.windows.net"), new DefaultAzureCredential()); // you need to setup your own Uri and Credential environment (local machine & Azure IAM) in order to make it work

        public async Task<List<UserModel>> GetUsers()
        {
            return new List<UserModel>() {
                new UserModel() { Id = 1, FirstName = "user1", LastName = "b1", EmployeeType = EmployeeType.Temporary },
                new UserModel() { Id = 2, FirstName = "user2", LastName = "b2", EmployeeType = EmployeeType.Unknown },
                new UserModel() { Id = 3, FirstName = "user3", LastName = "b3", EmployeeType = EmployeeType.Permanent } };
        }

        public async Task<UserModel> GetUser(int Id)
        {
            return GetUsers().Result.First(x=> x.Id == Id);
        }

        public async Task<bool> UploadUserPhoto(string container, string filename, string fullpath)
        {
            try
            {
                BlobContainerClient containerClient = _BlobServiceClient.GetBlobContainerClient(container);
                BlobClient blobClient = containerClient.GetBlobClient(filename);
                await blobClient.UploadAsync(fullpath, true);
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            
            return true;
        }

        public async Task<bool> IsBlobOnline(string container)
        {
            bool result = false;
            try
            {
                BlobContainerClient containerClient = _BlobServiceClient.GetBlobContainerClient(container);
                var count = containerClient.GetBlobs().ToList().Count;

                result = true;
            }
            catch (Exception)
            {

                //throw;
            }            

            return result;
        }

        public async Task<bool> IsPhotoValid(string fileName, string fullPath)
        {
            bool Valid = true;
            try
            {
                FileInfo fileInfo = new FileInfo(fullPath);
                if (fileInfo.Exists)
                {
                    if (fileInfo.Length > 2000000)
                    {
                        Valid = false;
                    }
                    else
                    {
                        using (Bitmap bmp = new Bitmap(fullPath))
                        {
                            if (bmp.Height > 200 || bmp.Width > 200)
                            {
                                Valid = false;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Valid = false;
                //throw;
            }
            

            return Valid;
        }
    }
}
