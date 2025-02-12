using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using NNN.Core.Presentation.MAUI.Helpers;
using System.Windows.Input;
using NNN.Core.Presentation.MAUI.Interfaces;

namespace NNN.Core.Presentation.MAUI.Models
{
    public class FlyOutMenuItem : NotifyPropertyChangedBase
    {
        public FlyOutMenuItem()
        {
            
        }

        public string Name { get; set; }
        public FlyOutMenuItemType FlyOutMenuItemType { get; set; }

        public IPageModel Model { get; set; }

        public ContentView Page { get; set; }

        private bool _flyOutMenuItemSelected;
        public bool FlyOutMenuItemSelected
        {
            get
            {
                return this._flyOutMenuItemSelected;
            }

            set
            {
                if (value != this._flyOutMenuItemSelected)
                {
                    this._flyOutMenuItemSelected = value;
                    OnPropertyChanged();
                }
            }
        }
    }

    public enum FlyOutMenuItemType
    {
        Share = 1,
        SelectAppMode = 2,
        LoadMethod = 3,
        SaveMethod = 4,
        OverlayData = 5,
        LoadData = 6,
        SaveData = 7,
        ExportDataToCSV = 8,
        ClearPlot = 9,
        ManualControl = 10,
        Settings = 11,
        Help = 12,
        Disconnect= 13
    }
}
