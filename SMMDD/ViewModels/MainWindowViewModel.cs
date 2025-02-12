using SMMDD.Interfaces;
using SMMDD.Models;
using SMMDD.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMMDD.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IViewModel
    {
        public MainWindowViewModel()
        {
            Initialize();
        }

        async void Initialize()
        {
            LoginViewModel = await BindContext<Login, LoginViewModel>();

            Navigator.Register(GoToSecurityAndMarketPepthPage);
        }

        private LoginViewModel _LoginViewModel;
        public LoginViewModel LoginViewModel
        {
            get { return _LoginViewModel; }
            set { _LoginViewModel = value; OnPropertyChanged(); }
        }
        public ActionResultModel ActionResultModel { get; set; }
        public Task BtnAction(Func<Task> func)
        {
            throw new NotImplementedException();
        }
    }
}
