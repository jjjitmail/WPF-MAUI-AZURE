using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserServices
    {
        Task<List<UserModel>> GetUsers();
        Task<UserModel> GetUser(int Id);
        Task<bool> UploadUserPhoto(string container, string filename, string fullpath);
        Task<bool> IsBlobOnline(string container);
        Task<bool> IsPhotoValid(string fileName, string fullPath);

    }
}
