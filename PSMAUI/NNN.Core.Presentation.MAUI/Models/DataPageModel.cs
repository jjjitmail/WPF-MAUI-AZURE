using NNN.Core.Presentation.MAUI.Helpers;
using NNN.Core.Presentation.MAUI.Interfaces;
using NNN.Core.Presentation.MAUI.Models;
//using PSTouchExpress.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//using Telerik.Maui.Controls;

namespace NNN.Core.Presentation.MAUI.Models
{   
    public class DataPageModel : BasePageModel, IPageModel
    {
        public DataPageModel() : base()
        {
            Initialize();
        }

        async void Initialize()
        {
            await PSTasks.ActionTask(() =>
            {
                DataTextColor = Color.FromArgb(PSColor.DefaultLightBlueColor);
                ExplandPlotDataViewCommand = new Command(OnTagExplandPlotDataViewCommand);
                TabViewCommand = new Command(OnTagTabViewCommand);
            });
        }

        public async Task BtnPressAction(Func<Task> func)
        {
            await func();
            await Task.Delay(10);
            await PSTasks.ActionTask(() =>
            {
                DataTextColor = DefaultWhiteTextColor;
                DataFontAttributes = DefaultSelectedFontAttributes;                
            });
        }

        public bool IsSelected { get; set; } = true;

        public string DataIcon { get; set; } = "\uE9D9";
        public string DataText { get; set; } = "Data";

        public bool PlotDataViewpresented { get; set; }
        public ICommand ExplandPlotDataViewCommand { get; private set; }
        public ICommand TabViewCommand { get; private set; }
        public double currentHeight { get; private set; }

        public double PlotItemListStartY => 310;

        async void OnTagExplandPlotDataViewCommand(object value)
        {
            if (value != null)
            {
                VisualElement _element = value as VisualElement;
                double _Y = PlotItemListStartY;
                string _ExpandIconBtn = BackToWindowIconBtn;
                if (!PlotDataViewpresented)
                {
                    currentHeight = _element.Height;
                    _element.HeightRequest = 700;
                    _Y = -20;
                }
                else
                {
                    _ExpandIconBtn = DefaultExpandIconBtn;
                    //_element.HeightRequest = currentHeight; //it causes the crash of the app for paremt/child issue
                }
                await _element.TranslateTo(0, _Y, 500, Easing.CubicInOut);
                //await _element.RotateTo(360, 2000);
                PlotDataViewpresented = !PlotDataViewpresented;
                ExpandIconBtn = _ExpandIconBtn;
            }
        }
        async void OnTagTabViewCommand(object value)
        {
            //if (value != null && value is TabViewHeaderItem)
            //{
            //    VisualElement _element = value as VisualElement;
            //    var tab = value as TabViewHeaderItem;
            //    tab.Focus();
            //    tab.IsSelected = true;                
            //}
        }

        public string Tab_DATA { get; set; } = "\uE9D9 DATA";
        public string Tab_LSV { get; set; } = "LSV GOLD &#xE9D9";
        public string Tab_EIS { get; set; } = "EIS AG &#xE9D9";

        private string _expandIconBtn;
        public string ExpandIconBtn
        {
            get 
            {
                if (string.IsNullOrEmpty(_expandIconBtn))
                {
                    return DefaultExpandIconBtn;
                }
                return _expandIconBtn; 
            }
            set 
            {
                _expandIconBtn = value;
                OnPropertyChanged();
            }
        }
        public GridLength PlotActionBtnAreaHeight => new GridLength(60);
        public GridLength PlotItemsListHeight => new GridLength(80);
        public GridLength PlotAreaHeight => new GridLength(this.AppContentHeight - 60 - 80);

        public string MagnifierIconBtn { get; set; } = "\uE12E";
        public string SquareIconBtn { get; set; } = "\uECE9";
        public string MeasurementIconBtn { get; set; } = "\uEB3C";
        public string SettingsIconBtn { get; set; } = "\uF8B0";
        public string SaveIconBtn { get; set; } = "\uE74E";
        public string PlotMoreIconBtn { get; set; } = "\uE712";       
        public string CloseIconBtn { get; set; } = "\uE10A";
        public string BackToWindowIconBtn { get; set; } = "\uE73F";
        private string DefaultExpandIconBtn => "\uE1D9";

        private Color _dataTextColor;
        public Color DataTextColor 
        {
            get { return _dataTextColor; } 
            set { _dataTextColor = value; OnPropertyChanged(); }
        }

        private FontAttributes _dataFontAttributes;
        public FontAttributes DataFontAttributes
        {
            get
            {
                return _dataFontAttributes;
            }
            set
            {
                _dataFontAttributes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PlotItem> PlotItemSources =>
            new()
            {
                new() { Title = "DPV1", DateTime = DateTime.Now, Pb = 2.22 },
                new() { Title = "DPV2", DateTime = DateTime.Now, Pb = 2.23 },
                new() { Title = "DPV3", DateTime = DateTime.Now, Pb = 2.24 },
                new() { Title = "DPV4", DateTime = DateTime.Now, Pb = 2.25 },
                new() { Title = "DPV5", DateTime = DateTime.Now, Pb = 2.26 }
            };
    }
}
