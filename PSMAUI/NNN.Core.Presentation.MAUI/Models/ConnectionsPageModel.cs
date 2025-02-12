using NNN.Core.Presentation.MAUI;
using NNN.Core.Presentation.MAUI.Helpers;
using NNN.Core.Presentation.MAUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceType = NNN.Core.Presentation.MAUI.DeviceType;

namespace NNN.Core.Presentation.MAUI.Models
{
    public class ConnectionsPageModel : BasePageModel, IPageModel
    {
        public ConnectionsPageModel() : base()
        {
            Initialize();
        }
        async void Initialize()
        {
            await PSTasks.ActionTask(() =>
            {
                ConnectionsTextColor = Color.FromArgb(PSColor.DefaultLightBlueColor);
            });
        }
        public async Task BtnPressAction(Func<Task> func)
        {
            await func();
            await Task.Delay(10);
            await PSTasks.ActionTask(() =>
            {
                ConnectionsTextColor = DefaultWhiteTextColor;
                ConnectionsFontAttributes = DefaultSelectedFontAttributes;
            });
        }
        public string ConnectionsIcon { get; set; } = "\uEE77";
        public string AddVirtualInstrumentIcon => "\uF19D";
        public string AddVirtualInstrumentText => "ADD VIRTUAL \nINSTRUMENT";
        public string ScanAvailableInstrumentIcon => "\uE72C";
        public string ScanAvailableInstrumentText => "SCAN AVAILABLE \nINSTRUMENTS";

        public string ConnectionsText { get; set; } = "Connections";
        public Color ConnectionsTextColor { get; set; }

        private FontAttributes _connectionsFontAttributes;

        public FontAttributes ConnectionsFontAttributes
        {
            get
            {
                return _connectionsFontAttributes;
            }
            set
            {
                _connectionsFontAttributes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ConnectionItem> ConnectionItemSources =>
           new()
           {
               new() { Active = false, Serial = "ES3+20S123", DeviceType = DeviceType.EmStat3, ConnectionType = ConnectionType.USB },
               new() { Active = true,  Serial = "ES3+20S123", DeviceType = DeviceType.EmStat3, ConnectionType = ConnectionType.BLE },
               new() { Active = false, Serial = "ES3+20S123", DeviceType = DeviceType.EmStat3, ConnectionType = ConnectionType.BLC },
               new() { Active = false, Serial = "ES3+20S123", DeviceType = DeviceType.EmStat3, ConnectionType = ConnectionType.USB },
               new() { Active = false, Serial = "ES3+20S123", DeviceType = DeviceType.EmStat3, ConnectionType = ConnectionType.USB }
           };
    }
}
