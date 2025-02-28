using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reactive.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TFH.ViewModels
{
    public partial class UserViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty] private string _firstName;

        [ObservableProperty] private string _lastName;

        [ObservableProperty] private string _selectedEmployeeType;

        [ObservableProperty] private string _photoSource;

        [ObservableProperty] private bool _online;

        [ObservableProperty] private bool _valid;

        [ObservableProperty] private string _resultMessage;

        private readonly IUserServices _userServices;

        private string UploadedPhotoName { get; set; }

        const string defaultIcon = "default_icon.jpg";
        
        public List<string> EmployeeTypes => Enum.GetNames(typeof(EmployeeType)).ToList();

        public UserViewModel(IUserServices userServices)
        {
            _userServices = userServices;
            SelectedEmployeeType = EmployeeType.Permanent.ToString();
            PhotoSource = defaultIcon;            
        }

        //public UserViewModel()
        //{
        //    _userServices = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<IUserServices>();
        //}
        //public UserViewModel(IServiceProvider serviceProvider)
        //{
        //    _userServices = serviceProvider.GetService<IUserServices>();
        //}

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("id"))
            {
                LoadUserAsync(query);
            }
            Observable.Start(() => { CheckBlobService(); });
        }
        private async Task LoadUserAsync(IDictionary<string, object> query)
        {
            var u = await _userServices.GetUser(Convert.ToInt32(query["id"]));

            FirstName = u.FirstName;
            LastName = u.LastName;
            SelectedEmployeeType = u.EmployeeType.ToString();
        }

        private async void UploadPhoto()
        {
            ResultMessage = "Uploading...";
            
            var result = await _userServices.UploadUserPhoto("photo", UploadedPhotoName, PhotoSource);

            await Task.Delay(2000);

            ResultMessage = result ? "Success" : "Not success";

            if (!result)
            {
                PhotoSource = defaultIcon;
            }

            await Task.Delay(2000);
            ResultMessage = "";
        }

        private async void CheckBlobService()
        {
            Online = await _userServices.IsBlobOnline("photo");
        }

        [RelayCommand]
        private async Task ChoosePhoto()
        {
            Valid = true;
            var option = new PickOptions()
            {
                FileTypes = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".jpg", ".jpeg", ".png", ".bmp" } }
                })
            };
            var result = await FilePicker.Default.PickAsync(option);

            Valid = await _userServices.IsPhotoValid(result.FileName, result?.FullPath);

            if (Valid)
            {
                UploadedPhotoName = result.FileName;
                PhotoSource = result?.FullPath;
            }
            else
            {
                ResultMessage = "Invalid photo";
                PhotoSource = defaultIcon;
                await Task.Delay(3000);
                ResultMessage = "";
            }
        }
        //

        [RelayCommand]
        private async Task Save()
        {
            UploadPhoto();
        }

        [RelayCommand]
        private async Task Reset()
        {
            SelectedEmployeeType = EmployeeType.Permanent.ToString();
            PhotoSource = defaultIcon;
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        [RelayCommand]
        private async Task Clear()
        {
            SelectedEmployeeType = EmployeeType.Unknown.ToString();
            PhotoSource = defaultIcon;
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        [RelayCommand]
        private async Task NavigateToMain() 
        {
            Shell.Current.GoToAsync($"main");
        }
    }
}
