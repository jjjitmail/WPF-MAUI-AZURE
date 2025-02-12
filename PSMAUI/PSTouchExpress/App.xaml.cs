//using PSTouchExpress.Models;
using PSTouchExpress.Pages;
using Application = Microsoft.Maui.Controls.Application;

namespace PSTouchExpress
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //if (Device.Idiom == TargetIdiom.Phone)
            //    Shell.Current.CurrentItem = PhoneTabs;

            //Routing.RegisterRoute(nameof(ConnectionsPage), typeof(ConnectionsPage));
            //Routing.RegisterRoute(nameof(InstrumentPage), typeof(InstrumentPage));
            //Routing.RegisterRoute(nameof(DataPage), typeof(DataPage));

            //Routing.RegisterRoute("connections", typeof(ConnectionsPage));
            //Routing.RegisterRoute("instrument", typeof(InstrumentPage));
            //Routing.RegisterRoute("data", typeof(DataPage));

            Page _page = new MainPage();
            _page.BindingContext = new MainPageModel();
            //var _page = new ShellPage();
            //_page.BindingContext = new ConnectionsPageModel();

            //var _ConnectionsPage = new ConnectionsPage();
            //_ConnectionsPage.BindingContext = new ConnectionsPageModel();
            //model.MainPageContentPresenter = _ConnectionsPage;

            //_page.BindingContext = model;

            MainPage = _page;

            //var device = DeviceDisplay.MainDisplayInfo;

            //MainPage = new MainPage();
            //MainPage.BindingContext = new ConnectionsPageModel();

            //var tabbedPage = new FreshMvvm.Maui.FreshTabbedNavigationContainer();
            //tabbedPage.AddTab<MainPageModel>("Connections", null);
            //tabbedPage.AddTab<MainPageModel>("Instrument", null);
            //tabbedPage.AddTab<MainPageModel>("Data", null);

            //MainPage = tabbedPage;
        }
    }
}