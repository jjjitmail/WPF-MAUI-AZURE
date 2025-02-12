using PalmSens.Core.Presentation.MAUI.Helpers;
using PSTouchExpress.Models;
//using PSTouchExpress.Pages;
using PalmSens.Core.Presentation.MAUI.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PalmSens.Core.Presentation.MAUI.Models
{
    internal class MainPageModel : BasePageModel
    {
        public MainPageModel()
        {
            //NoiseProgress = 126;
            Initialize();
            UpdateStatus();
            ShowNoise();
            BusySwap();
        }
        int PrefixNoiseProgress = 126;
        async void Initialize()
        {
            await PSTasks.ActionTask(async () =>
            {
                CurrentType = "i = ";
                CurrentValue = "0.2674 *";
                CurrentLabel = "100 pA";
                PotentialType = "E = ";
                PotentialValue = "2.3420";
                InstrumentTitle = "PalmSens4";
                PotentialLabel = "V";
                PotentialStatusType = PotentialStatusType.OK;
                CurrentStatusType = CurrentStatusType.Underload;

                AppBarRows = GetAppBarRows();

                AppBarStatusVisible = true;

                _tagDataBtnCommand = new Command(OnTagDataBtnCommand);
                _tagInstrumentBtnCommand = new Command(OnTagInstrumentBtnCommand);
                _tagConnectionBtnCommand = new Command(OnTagConnectionBtnCommand);
                _appCloudCommand = new Command(OnTagAppCloudCommand);

                DataPageModel = new DataPageModel();
                InstrumentPageModel = new InstrumentPageModel();
                ConnectionsPageModel = await BindContext<ConnectionsPage, ConnectionsPageModel>();
            });
        } 
        async void ShowNoise()
        {
            await PSTasks.ActionTask(async () =>
            {
                for (int i = PrefixNoiseProgress; i > -1; i--)
                {
                    i -= 13;
                    await Task.Delay(350);
                    NoiseProgress = i;
                    if (i < 1)
                    {
                        await Task.Delay(5000);
                        NoiseProgress = PrefixNoiseProgress;
                        await Task.Delay(5000);
                        i = NoiseProgress;
                    }
                }
            });
        }
        async void BusySwap()
        {
            await PSTasks.ActionTask(async () => 
            {
                do
                {
                    await Task.Delay(500);
                    AnimatedSwapIcon = AnimatedSwapIcon == "<" ? ">" : "<";
                } while (1<2);
                
            });
        }
        async void UpdateStatus()
        {
            await PSTasks.ActionTask(async () =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    await Task.Delay(2500);
                    PotentialStatusType = PotentialStatusType.OK;
                    CurrentStatusType = CurrentStatusType.OverloadWarning;
                    await Task.Delay(2000);
                    CurrentStatusType = CurrentStatusType.Underload;
                    await Task.Delay(3000);
                    PotentialStatusType = PotentialStatusType.Underload;
                    CurrentStatusType = CurrentStatusType.OK;
                    await Task.Delay(2000);
                    PotentialStatusType = PotentialStatusType.OverloadWarning;
                    CurrentStatusType = CurrentStatusType.OverloadWarning;
                }
            });                    
        }

        ICommand _appCloudCommand;

        public ICommand AppCloudCommand => _appCloudCommand;

        ICommand _tagConnectionBtnCommand;
        public ICommand TagConnectionBtnCommand => _tagConnectionBtnCommand;
        ICommand _tagInstrumentBtnCommand;
        public ICommand TagInstrumentBtnCommand => _tagInstrumentBtnCommand;
        ICommand _tagDataBtnCommand;
        public ICommand TagDataBtnCommand => _tagDataBtnCommand;

        private RowDefinitionCollection _appBarRows;
        public RowDefinitionCollection AppBarRows 
        {
            get
            {
                return this._appBarRows;
            }

            set
            {
                if (value != this._appBarRows)
                {
                    this._appBarRows = value;
                    OnPropertyChanged();
                }               
            }
        }
        private RowDefinitionCollection GetAppBarRows(double AppBarHeight = DefaultHeaderHeight)
        {
            return new RowDefinitionCollection()
            {
                new RowDefinition { Height = AppBarHeight },
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = AppFooterHeight },
            };
        }

        private bool _appBarStatusVisible;
        public bool AppBarStatusVisible
        {
            get
            {
                return this._appBarStatusVisible;
            }

            set
            {
                if (value != this._appBarStatusVisible)
                {
                    this._appBarStatusVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        async void OnTagAppCloudCommand(object value)
        {
            if (value is VisualElement)
            {
                VisualElement _element = (VisualElement)value;
                await Task.WhenAll(
                    _element.TranslateTo(0, AppBarStatusVisible ? AppHeaderHeight : -AppHeaderHeight, 100, Easing.CubicInOut),
                     PSTasks.ActionTask(() => AppBarRows = GetAppBarRows(!AppBarStatusVisible ? AppHeaderHeight : AppHeaderHeight * 2))                     
                    );
                FlyOutMenuMarginTop = AppBarStatusVisible ? 137.0 : DefaultHeaderHeight + 27;
                AppBarStatusVisible = !AppBarStatusVisible;
                
            }
        }

        async void OnTagConnectionBtnCommand(object value)
        {
            ContentPresenterSlideToLeft = false;
            await PSTasks.ActionTask(() => 
            {
                DataPageModel = new DataPageModel();
                InstrumentPageModel = new InstrumentPageModel();
                ConnectionBtnBgColor = Color.FromArgb(PSColor.SelectedBlueColor); 
            });
            await PSTasks.ActionTask(() => IsActivityIndicatorBusy = true);
            await Task.Delay(10);
            ConnectionsPageModel = await BindContext<ConnectionsPage, ConnectionsPageModel>(true);
            await PSTasks.ActionTask(() => ConnectionBtnBgColor = Color.FromArgb(PSColor.DefaultBlueColor));
        }
        async void OnTagInstrumentBtnCommand(object value)
        {
            ContentPresenterSlideToLeft = !ContentPresenterSlideToLeft;
            await PSTasks.ActionTask(() => 
            {
                MainPageContentPresenter = null;
                ConnectionsPageModel = new ConnectionsPageModel();
                DataPageModel = new DataPageModel();
                InstrumentBtnBgColor = Color.FromArgb(PSColor.SelectedBlueColor); 
            });
            await PSTasks.ActionTask(() => IsActivityIndicatorBusy = true);
            await Task.Delay(10);
            InstrumentPageModel = await BindContext<InstrumentPage, InstrumentPageModel>(true);            
            await PSTasks.ActionTask(() => InstrumentBtnBgColor = Color.FromArgb(PSColor.DefaultBlueColor));
        }
        async void OnTagDataBtnCommand(object value)
        {
            ContentPresenterSlideToLeft = true;
            await PSTasks.ActionTask(async () =>
            {
                MainPageContentPresenter = null;
                await PSTasks.ActionTask(() => IsActivityIndicatorBusy = true);
                ConnectionsPageModel = new ConnectionsPageModel();
                InstrumentPageModel = new InstrumentPageModel();
                DataBtnBgColor = Color.FromArgb(PSColor.SelectedBlueColor);
            });
            
            await Task.Delay(10);
            await PSTasks.ActionTask(async() => DataPageModel = await BindContext<DataPage, DataPageModel>(true));
            await PSTasks.ActionTask(() => DataBtnBgColor = Color.FromArgb(PSColor.DefaultBlueColor));
        }

        private ConnectionsPageModel _connectionsPageModel;
        public ConnectionsPageModel ConnectionsPageModel 
        {
            get { return _connectionsPageModel; }
            set { _connectionsPageModel = value; OnPropertyChanged(); }
        }

        private InstrumentPageModel _instrumentPageModel;
        public InstrumentPageModel InstrumentPageModel
        {
            get { return _instrumentPageModel; }
            set { _instrumentPageModel = value; OnPropertyChanged(); }
        }

        private DataPageModel _dataPageModel;
        public DataPageModel DataPageModel 
        {
            get { return _dataPageModel; }
            set { _dataPageModel = value; OnPropertyChanged(); }
        }

        private Color _connectionBtnBgColor;
        public Color ConnectionBtnBgColor
        {
            get
            {
                return this._connectionBtnBgColor;
            }

            set
            {
                if (value != this._connectionBtnBgColor)
                {
                    this._connectionBtnBgColor = value;
                    OnPropertyChanged();
                }
            }
        }
        private Color _instrumentBtnBgColor;
        public Color InstrumentBtnBgColor
        {
            get
            {
                return this._instrumentBtnBgColor;
            }

            set
            {
                if (value != this._instrumentBtnBgColor)
                {
                    this._instrumentBtnBgColor = value;
                    OnPropertyChanged();
                }
            }
        }
        private Color _dataBtnBgColor;
        public Color DataBtnBgColor
        {
            get
            {
                return this._dataBtnBgColor;
            }

            set
            {
                if (value != this._dataBtnBgColor)
                {
                    this._dataBtnBgColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public double FlyOutMenuWidth => AppWidth / 1.7;

        private double _flyOutMenuMarginTop;
        public double FlyOutMenuMarginTop
        {
            get 
            {
                if (_flyOutMenuMarginTop < 1)
                {
                    _flyOutMenuMarginTop = 80;
                }
                
                return _flyOutMenuMarginTop;                
            }
            set { _flyOutMenuMarginTop = value; OnPropertyChanged(); OnPropertyChanged("FlyOutMenuMargin"); }
        }

        private Thickness _flyOutMenuMargin;

        public Thickness FlyOutMenuMargin
        {
            get
            {
                if (_flyOutMenuMargin == new Thickness(0))
                    return new Thickness(AppWidth + FlyOutMenuWidth + 10, FlyOutMenuMarginTop, 2, 20);
                return _flyOutMenuMargin;
            }
            set { _flyOutMenuMargin = value; OnPropertyChanged(); }
        }
        private int _noiseProgress;
        public int NoiseProgress
        {
            get
            {
                if (this._noiseProgress < 1)
                    return PrefixNoiseProgress;
                return this._noiseProgress;
            }

            set
            {
                if (value != this._noiseProgress)
                {
                    this._noiseProgress = value;
                    OnPropertyChanged();
                }
            }
        }

        private PotentialStatusType _potentialStatusType;
        public PotentialStatusType PotentialStatusType 
        {
            get
            {
                return this._potentialStatusType;
            }

            set
            {
                if (value != this._potentialStatusType)
                {
                    this._potentialStatusType = value;
                    OnPropertyChanged();
                }
            }
        }

        private CurrentStatusType _currentStatusType;
        public CurrentStatusType CurrentStatusType
        {
            get
            {
                return this._currentStatusType;
            }

            set
            {
                if (value != this._currentStatusType)
                {
                    this._currentStatusType = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DataIcon { get; set; } = "\uE9D9";
        public string CloudIcon { get; set; } = "\uE753";
        public string StartIcon { get; set; } = "\uF5B0";
        public string MoreIcon { get; set; } = "\uE10C";
        //ActivityIndicator
        
        //public string HeartIcon => "\uE95E";
        private string _animatedSwapIcon;
        public string AnimatedSwapIcon 
        { 
            get { return this._animatedSwapIcon; }
            set { this._animatedSwapIcon = value; OnPropertyChanged(); }
        }

        private string _potentialType;
        public string PotentialType
        {
            get
            {
                return this._potentialType;
            }

            set
            {
                if (value != this._potentialType)
                {
                    this._potentialType = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _potentialValue;
        public string PotentialValue
        {
            get
            {
                return this._potentialValue;
            }

            set
            {
                if (value != this._potentialValue)
                {
                    this._potentialValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _potentialLabel;
        public string PotentialLabel
        {
            get
            {
                return this._potentialLabel;
            }

            set
            {
                if (value != this._potentialLabel)
                {
                    this._potentialLabel = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _currentType;
        public string CurrentType
        {
            get
            {
                return this._currentType;
            }

            set
            {
                if (value != this._currentType)
                {
                    this._currentType = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _currentValue;
        public string CurrentValue
        {
            get
            {
                return this._currentValue;
            }

            set
            {
                if (value != this._currentValue)
                {
                    this._currentValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _currentLabel;
        public string CurrentLabel
        {
            get
            {
                return this._currentLabel;
            }

            set
            {
                if (value != this._currentLabel)
                {
                    this._currentLabel = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _instrumentTitle;
        public string InstrumentTitle
        {
            get
            {
                return this._instrumentTitle;
            }

            set
            {
                if (value != this._instrumentTitle)
                {
                    this._instrumentTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        //public string ConnectionsText { get; set; } = "Connections";
        //public string InstrumentText { get; set; } = "Instrument";
        //public string DataText { get; set; } = "Data";

        public ObservableCollection<FlyOutMenuItem> FlyOutMenuItemSources => FlyOutMenuItemList;
    }
}