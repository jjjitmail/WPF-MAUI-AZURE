using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFH.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IUserServices _userServices;

        [ObservableProperty]
        private List<UserModel> _users = [];

        public MainViewModel(IUserServices userServices)
        {
            _userServices = userServices;
            LoadUsers();
        }

        //[RelayCommand]
        //async Task NavigateToUser()
        //{
        //    await Shell.Current.GoToAsync($"user");
        //}

        private async Task LoadUsers()
        {
            Users = await _userServices.GetUsers();            
        }

        [RelayCommand]
        private async Task NavigateToUser(UserModel user)
        { 
            await Shell.Current.GoToAsync($"user?id={user?.Id}");
        }
    }
}
