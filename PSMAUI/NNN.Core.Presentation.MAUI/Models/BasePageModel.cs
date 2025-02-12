using NNN.Core.Presentation.MAUI.Helpers;
using NNN.Core.Presentation.MAUI.Interfaces;
//using PSTouchExpress.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
//using PSTouchExpress.Views;
//using Microsoft.Maui.Essentials;
using NNN.Core.Presentation.MAUI.UserControls;
using NNN.Core.Presentation.MAUI.Models;
using NNN.Core.Common.Parameters;

namespace NNN.Core.Presentation.MAUI.Models
{
    public abstract class BasePageModel : NotifyPropertyChangedBase
    {        
        public BasePageModel()
        {
            Init();
        }

        public string Range_StartIcon => "\uE896";
        public string Range_MinIcon => "\uEBE7";
        public string Range_MaxIcon => "\uE898";
        public string DropDownButtonIcon => "\uEDDC";

        #region ICommand Properties
        public ICommand FlyOutMenuItemCommand { get; private set; }

        ICommand _appStartCommand;
        public ICommand AppStartCommand => _appStartCommand;

        
        ICommand _flyOutMenuBtnCommand;
        public ICommand FlyOutMenuBtnCommand => _flyOutMenuBtnCommand;

        public ICommand TagOnContentPageCommand { get; private set; }

        ICommand _tagOnFlyOutMenuCommand;
        public ICommand TagOnFlyOutMenuCommand => _tagOnFlyOutMenuCommand;

        #endregion 

        #region ICommand Events 

        async void OnAppStartCommand(object value)
        {
            var obj = value;
        }
        async void OnFlyOutMenuItemCommand(object value)
        {
            await Task.Delay(10);

            FlyOutMenuItem item = (FlyOutMenuItem)value;                        

            var type = item.FlyOutMenuItemType;
            switch (type)
            {
                case FlyOutMenuItemType.Share:
                break;
                case FlyOutMenuItemType.SelectAppMode:
                break;
                case FlyOutMenuItemType.LoadMethod:
                break;
                case FlyOutMenuItemType.SaveMethod:
                break;
                case FlyOutMenuItemType.OverlayData:
                break;
                case FlyOutMenuItemType.LoadData:
                break;
                case FlyOutMenuItemType.SaveData:
                break;
                case FlyOutMenuItemType.ExportDataToCSV:
                break;
                case FlyOutMenuItemType.ClearPlot:
                break;
                case FlyOutMenuItemType.ManualControl:
                break;
                case FlyOutMenuItemType.Settings:
                break;
                case FlyOutMenuItemType.Help:
                break;
                case FlyOutMenuItemType.Disconnect:
                break;
                default:
                break;
            }
            await PSTasks.ActionTask(() =>
                MessagingCenter.Send<FlyOutMenuStatus>(new FlyOutMenuStatus() { IsPresented = true, NeedReset = true }, "fmenu")
            );
        }       

        async void OnFlyOutMenuBtnCommand(object value)
        {
            await PSTasks.ActionTask(() => MoreTaggedBackgroundColor = PSColor.SelectedBlueColor);
            await Task.Delay(100);
            await PSTasks.ActionTask(() =>
            {
                ShowFlyOutMenu = !ShowFlyOutMenu;
            });
        }
        async void OnTagOnContentPageCommand(object value)
        {
            await PSTasks.ActionTask(() =>
            {
                MessagingCenter.Send<FlyOutMenuStatus>(new FlyOutMenuStatus() { IsPresented = true, NeedReset = true }, "fmenu");
                //PSMessaging.Publish<LabelEntryH>(new LabelEntryH(), "Instrument_Fields");
            });
        }
        async void OnTagOnFlyOutMenuCommand(object value)
        {
            //
        }
        #endregion

        #region Properties

        public const double DefaultHeaderHeight = 55;

        private double _appHeaderHeight;
        public double AppHeaderHeight
        {
            get
            {
                if (this._appHeaderHeight < 1)
                    return DefaultHeaderHeight;
                return this._appHeaderHeight;
            }
            set
            {
                if (value != this._appHeaderHeight)
                {
                    this._appHeaderHeight = value;
                    OnPropertyChanged();
                }
            }
        }
        public double AppFooterHeight => 85;
        
        public double AppContentHeight => PSApplication.AppHeight - AppHeaderHeight - AppFooterHeight;
        public double AppContentWidth => PSApplication.AppHeight;


        public IPageModel PageModel { get; set; }
        public IView FlyOutMenuViewPage { get; set; }
        private FlyOutMenuItem LastFlyOutMenuItemSelected { get; set; }
        private FlyOutMenuItem _selectedFlyOutMenuItem;
        public FlyOutMenuItem SelectedFlyOutMenuItem
        {
            get
            {
                return this._selectedFlyOutMenuItem;
            }
            set
            {
                if (LastFlyOutMenuItemSelected != null)
                    LastFlyOutMenuItemSelected.FlyOutMenuItemSelected = false;

                if (value != this._selectedFlyOutMenuItem)
                {
                    this._selectedFlyOutMenuItem = value;
                    if (SelectedFlyOutMenuItem != null)
                    {
                        SelectedFlyOutMenuItem.FlyOutMenuItemSelected = true;
                        LastFlyOutMenuItemSelected = this._selectedFlyOutMenuItem;
                    }
                    OnPropertyChanged();
                }
            }
        }

        private bool _showFlyOutMenu;
        public bool ShowFlyOutMenu
        {
            get
            {
                return this._showFlyOutMenu;
            }

            set
            {
                if (value != this._showFlyOutMenu)
                {
                    this._showFlyOutMenu = value;
                    OnPropertyChanged();
                }
            }
        }

        private Parameter _parameter;
        public Parameter Parameter 
        { 
            get { return this._parameter; }
            set 
            {
                if (value != this._parameter)
                {
                    this._parameter = value;
                    OnPropertyChanged();
                }
            }
        }

        public double AppWidth => PSApplication.AppWidth;
        public double AppWidth3 => PSApplication.AppWidth / 3;
        public double AppHeight => PSApplication.AppHeight;

        //public double AppHeight { get; set; }

        public double AppWidth_50 => AppWidth * 0.5;

        private bool _isActivityIndicatorBusy;
        public bool IsActivityIndicatorBusy
        {
            get { return this._isActivityIndicatorBusy; }
            set { this._isActivityIndicatorBusy = value; OnPropertyChanged(); }
        }
        public string MoreIconBtn { get; set; } = "\uE10C";
        public string HelpIcon => "\uE897";

        private string _moreTaggedBackgroundColor;
        public string MoreTaggedBackgroundColor
        {
            get
            {
                return this._moreTaggedBackgroundColor;
            }

            set
            {
                if (value != this._moreTaggedBackgroundColor)
                {
                    this._moreTaggedBackgroundColor = value;
                    OnPropertyChanged();
                }
            }
        }

        private Color _flyOutMenuItemBgColor;
        public Color FlyOutMenuItemBgColor
        {
            get
            {
                return this._flyOutMenuItemBgColor;
            }

            set
            {
                if (value != this._flyOutMenuItemBgColor)
                {
                    this._flyOutMenuItemBgColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color DefaultWhiteTextColor => Color.FromArgb(PSColor.DefaultWhiteColor);

        public FontAttributes DefaultSelectedFontAttributes { get; set; } = FontAttributes.Bold;

        public string SelectedFont { get; set; } = "Bold";

        public string TextColor { get; set; } = "White";

        public IPageModel CurrentModel { get; set; }

        private ContentView _mainPageContentPresenter;
        public ContentView MainPageContentPresenter
        {
            get { return _mainPageContentPresenter; }
            set
            {
                if (value != this._mainPageContentPresenter)
                {
                    this._mainPageContentPresenter = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ContentPresenterSlideToLeft { get; set; }

        double _prefixedContentPresenterWidth = -425; // todo: update with Actual App Width
        #endregion

        #region Methods/Events        

        public async Task<Model> BindContext<IPage, Model>(bool TranslateTo = false, Func<Task> CallBack = null)
            where Model : IPageModel
            where IPage : ContentView
        {
            var model = Activator.CreateInstance<Model>();
            
            double elementWicth = !ContentPresenterSlideToLeft ? AppContentWidth : -AppContentWidth;

            await model.BtnPressAction(async () =>
            {
                ContentView currentpage = Activator.CreateInstance<IPage>();
                currentpage.BindingContext = model;
                MainPageContentPresenter = currentpage;

                if (TranslateTo)
                {
                    MainPageContentPresenter.TranslateTo(-elementWicth, 0, 0, Easing.Linear);
                    IsActivityIndicatorBusy = false;
                    await Task.Delay(100);
                    await PSTasks.ActionTask(() =>
                    {
                        MainPageContentPresenter.TranslateTo(0, 0, 300, Easing.Linear);
                    });
                }
            });
            CallBack?.Invoke();
            return model;
        }

        async void Init()
        {
            await PSTasks.ActionTask(() =>
            {
                FlyOutMenuItemCommand = new Command<FlyOutMenuItem>(OnFlyOutMenuItemCommand);

                _appStartCommand = new Command(OnAppStartCommand);

                _flyOutMenuBtnCommand = new Command(OnFlyOutMenuBtnCommand);

                TagOnContentPageCommand = new Command(OnTagOnContentPageCommand);

                _tagOnFlyOutMenuCommand = new Command(OnTagOnFlyOutMenuCommand);
            });
        }

        #endregion

        public static ObservableCollection<FlyOutMenuItem> FlyOutMenuItemList => 
                new()
                { 
                    new() { Name = "Share", FlyOutMenuItemType = FlyOutMenuItemType.Share, Page = null, Model = null },
                    new() { Name = "Select app mode", FlyOutMenuItemType = FlyOutMenuItemType.SelectAppMode, Page = null, Model = null},
                    new() { Name = "Load method", FlyOutMenuItemType = FlyOutMenuItemType.LoadMethod },
                    new() { Name = "Save method", FlyOutMenuItemType = FlyOutMenuItemType.SaveMethod },
                    new() { Name = "Over lay data", FlyOutMenuItemType = FlyOutMenuItemType.OverlayData },
                    new() { Name = "Load data", FlyOutMenuItemType = FlyOutMenuItemType.LoadData },
                    new() { Name = "Save data", FlyOutMenuItemType = FlyOutMenuItemType.SaveData },
                    new() { Name = "Export data to CSV", FlyOutMenuItemType = FlyOutMenuItemType.ExportDataToCSV },
                    new() { Name = "Clear plot", FlyOutMenuItemType = FlyOutMenuItemType.ClearPlot },
                    new() { Name = "Manual control", FlyOutMenuItemType = FlyOutMenuItemType.ManualControl },
                    new() { Name = "Settings", FlyOutMenuItemType = FlyOutMenuItemType.Settings },
                    new() { Name = "Help", FlyOutMenuItemType = FlyOutMenuItemType.Help },
                    new() { Name = "Disconnect", FlyOutMenuItemType = FlyOutMenuItemType.Disconnect }
                };
    }
}
