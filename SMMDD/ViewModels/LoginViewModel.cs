using SMMDD.Command;
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
    public class LoginViewModel : ViewModelBase, IViewModel
    {
        public LoginViewModel()
        {
            UserName = "Tam";
            Password = "secret";
        }

        public List<MyComboBoxItem> LoginComboBoxSource
        {
            get 
            {
                var list = new List<MyComboBoxItem>();
                for (int i = 1; i < 11; i++)
                {
                    string _color = "White";
                    if (i % 2 == 0)
                        _color = "Red";
                    list.Add(new MyComboBoxItem() { ID = i, Color = _color });
                }
                return list; 
            } 
        }

        private string _userNameTxtBgColor;
        public string UserNameTxtBgColor
        {
            get { return _userNameTxtBgColor; }
            set { _userNameTxtBgColor = value; OnPropertyChanged(); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set 
            { 
                _userName = value;
                UserNameTxtBgColor = "White";
                if (_userName != "Tam")
                    UserNameTxtBgColor = "Red";
                OnPropertyChanged(); 
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }
        //

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new CommandHandler(() => OnLoginCommand(), () => CanExecute));
            }
        }
        public bool CanExecute
        {
            get
            {
                return true;
            }
        }

        public ActionResultModel ActionResultModel { get; set; }

        public async void OnLoginCommand()
        {
            if (UserName != "Tam" || Password != "secret")
            {
                Message = "You are unauthorized.";
                //UserNameTxtBgColor = "Red";
                return;
            }                
            Navigator.Call(GoToSecurityAndMarketPepthPage);
        }

        public async Task BtnAction(Func<Task> func)
        {
            await func();
        }
    }
}
