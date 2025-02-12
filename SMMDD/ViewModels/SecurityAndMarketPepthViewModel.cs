using SMMDD.Command;
using SMMDD.Helper;
using SMMDD.Interfaces;
using SMMDD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SMMDD.ViewModels
{
    public class SecurityAndMarketPepthViewModel : ViewModelBase, IViewModel
    {
        public SecurityAndMarketPepthViewModel()
        {
            _marketDepthSource = new MarketDepthSource();
            Add_Event_OnDepthUpdated();
            SelectedSymbolType = -1;
            DatagridVisibility = Visibility.Hidden;
            ActionResultModel = new ActionResultModel();

            if (SecurityList == null || SecurityList.Count == 0)
            {
                // add some dummie data
                SecurityList = new ObservableCollection<Security>()
                {
                    new Security(){ Type = SecurityType.FXSpot, ID = "EUR/USD" },
                    new Security(){ Type = SecurityType.FXForward, ID = "USD/GBP-20220819"}
                };
            }            
        }

        #region Event/Method
        private void _marketDepthSource_OnDepthUpdated(object sender, EventArgs e)
        {
            MarketDepthGrids = _marketDepthSource.MarketDepthGrids;
        }
        public void Add_Event_OnDepthUpdated()
        {
            _marketDepthSource.OnDepthUpdated += _marketDepthSource_OnDepthUpdated;
        }
        public void Min_Event_OnDepthUpdated()
        {
            _marketDepthSource.OnDepthUpdated -= _marketDepthSource_OnDepthUpdated;
        }
        public async Task BtnAction(Func<Task> func)
        {
            await func();
        }
        #endregion

        #region Properties
        private string _resultMessage;
        public string ResultMessage
        {
            get { return _resultMessage; }
            set { _resultMessage = value; OnPropertyChanged(); }
        }
        private Visibility _datagridVisibility;
        public Visibility DatagridVisibility
        {
            get { return _datagridVisibility; }
            set { _datagridVisibility = value; OnPropertyChanged(); }
        }
        private DateTime _displayDateStart;
        public DateTime DisplayDateStart
        {
            get { return DateTime.Today; }
            set { _displayDateStart = value; OnPropertyChanged(); }
        }

        private bool _isDatePickerEnabled;
        public bool IsDatePickerEnabled
        {
            get { return _isDatePickerEnabled; }
            set { _isDatePickerEnabled = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Security> _securityList;
        public ObservableCollection<Security> SecurityList
        {
            get { return _securityList; }
            set { _securityList = value; OnPropertyChanged(); }
        }
        private int _selectedSymbolType;
        public int SelectedSymbolType
        {
            get { return _selectedSymbolType; }
            set 
            { 
                _selectedSymbolType = value;                
                OnPropertyChanged(); 
            }
        }

        private string _symbolID;
        public string SymbolID
        {
            get { return _symbolID; }
            set { _symbolID = value; OnPropertyChanged(); }
        }
        //
        private Nullable<DateTime> _selectedDate = null;
        public Nullable<DateTime> SelectedDate
        {
            get
            {
                if (_selectedDate == null)
                {
                    _selectedDate = DateTime.Today;
                }
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        private Security _selectedSecurity;
        public Security SelectedSecurity
        {
            get { return _selectedSecurity; }
            set 
            {
                _selectedSecurity = value;
                OnPropertyChanged(); 
            }
        }
        private MarketDepthSource _marketDepthSource;
        public MarketDepthSource MarketDepthSource
        {
            get { return _marketDepthSource; }
            set
            {
                _marketDepthSource = value;
                OnPropertyChanged();
            }
        }
        private List<MarketDepthGrid> _marketDepthGrids;
        public List<MarketDepthGrid> MarketDepthGrids
        {
            get { return _marketDepthGrids; }
            set
            {
                _marketDepthGrids = value;
                OnPropertyChanged();
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
        #endregion

        #region Command, Method
        private ICommand _comboBoxSelectionChangedCommand;
        public ICommand ComboBoxSelectionChangedCommand
        {
            get
            {
                return _comboBoxSelectionChangedCommand ?? (_comboBoxSelectionChangedCommand = new CommandHandler(() => OnComboBoxSelectionChangedCommand(), () => CanExecute));
            }
        }
        private async void OnComboBoxSelectionChangedCommand()
        {
            IsDatePickerEnabled = SelectedSymbolType == (int)SecurityType.FXForward;
        }

        private ICommand _securitySelectionChangedCommand;
        public ICommand SecuritySelectionChangedCommand
        {
            get
            {
                return _securitySelectionChangedCommand ?? (_securitySelectionChangedCommand = new CommandHandler(() => OnSecuritySelectionChangedCommand(), () => CanExecute));
            }
        }
        private async void OnSecuritySelectionChangedCommand()
        {
            if (SelectedSecurity != null)
            {
                var _sercurity = SecurityList.FirstOrDefault(t => t.ID == SelectedSecurity.ID);
                DatagridVisibility = Visibility.Collapsed;
                if (_sercurity != null)
                {
                    if (_sercurity.Type != null)
                    {
                        DatagridVisibility = Visibility.Visible;
                    }
                    _marketDepthSource.Subscribe(SelectedSecurity.ID);
                    SymbolID = SelectedSecurity.ID;
                    FormateSelectedSecurity();
                }
                SelectedSymbolType = _sercurity.Type == null ? -1 : (int)_sercurity.Type;
            }
        }

        private void FormateSelectedSecurity()
        {
            if (SelectedSecurity != null && SelectedSecurity.Type == SecurityType.FXForward)
            {
                SymbolID = SelectedSecurity.ID.Split('-')[0];
                var date = SelectedSecurity.ID.Split('-')[1];
                SelectedDate = new DateTime(int.Parse(date.Substring(0, 4)),
                    int.Parse(date.Substring(4, 2)), int.Parse(date.Substring(6, 2)));
            }else if (SymbolID.Contains("-"))
            {
                var id = SymbolID.Split('-')[0];
                var date = SymbolID.Split('-')[1];
                SymbolID = id;                
                SelectedDate = new DateTime(int.Parse(date.Substring(0, 4)),
                    int.Parse(date.Substring(4, 2)), int.Parse(date.Substring(6, 2)));
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new CommandHandler(() => OnSaveCommand(), () => CanExecute));
            }
        }
        
        private async void OnSaveCommand()
        {
            ActionResultModel.IsValid = true;
            if (string.IsNullOrEmpty(SymbolID) || SymbolID.Trim().Length < 7)
            {
                ActionResultModel.IsValid = false;
                ResultMessage = "ID is not valid. It must contain 7 characters";
            }
            if (SelectedSymbolType < 0)
            {
                ActionResultModel.IsValid = false;
                ResultMessage = "Select a Type, please.";
            }
            if (ActionResultModel.IsValid)
            {
                SymbolID = SymbolID.Trim().ToUpper();
                if (SelectedSymbolType == (int)SecurityType.FXForward
                    && SymbolID.Length == 7)
                {
                    SymbolID = SymbolID + "-" + SelectedDate.Value.Year + Utl.FormatTwoDigit(SelectedDate.Value.Month) + Utl.FormatTwoDigit(SelectedDate.Value.Day);
                }

                // check if SymbolID is already taken
                var _sercurity = SecurityList.FirstOrDefault(t => t.ID == SymbolID);
                if (_sercurity != null && SelectedSecurity.ID != SymbolID)
                {
                    ActionResultModel.IsValid = false;
                    ResultMessage = "ID is already taken, choose other ID";
                    FormateSelectedSecurity();
                }                
                                
                if (ActionResultModel.IsValid)
                {
                    // Remove the selected security first
                    Min_Event_OnDepthUpdated();
                    SecurityList.Remove(SelectedSecurity);
                    Add_Event_OnDepthUpdated();

                    // from here, new ID is free to be saved
                    var newSecurity = new Security() { ID = SymbolID, Type = (SecurityType)Enum.ToObject(typeof(SecurityType), SelectedSymbolType) };
                    SecurityList.Add(newSecurity);
                    SelectedSecurity = newSecurity;
                    Utl.Serialize<List<Security>>(SecurityList.Where(t => t.ID != "...").ToList());
                    DatagridVisibility = Visibility.Collapsed;
                    ResultMessage = "Saved successful.";
                }                
            }
            await DelayAction(() => ResultMessage = string.Empty);
        }
        private async Task DelayAction(Action action)
        {
            await Task.Delay(5000);
            action();
        }

        private ICommand _addSymbolCommand;
        public ICommand AddSymbolCommand
        {
            get
            {
                return _addSymbolCommand ?? (_addSymbolCommand = new CommandHandler(() => OnAddSymbolCommand(), () => CanExecute));
            }
        }

        private async void OnAddSymbolCommand()
        {
            ResetSecurity();
            DatagridVisibility = Visibility.Hidden;
            var _sercurity = SecurityList.FirstOrDefault(t => t.ID == "...");
            if (_sercurity== null)
            {
                var newSecurity = new Security() { ID = "...", Type = null };
                SecurityList.Add(newSecurity);
                SelectedSecurity = newSecurity;
            }                
        }

        private void ResetSecurity()
        {
            SymbolID = string.Empty;
            SelectedSymbolType = -1;            
        }
        #endregion
    }
}
