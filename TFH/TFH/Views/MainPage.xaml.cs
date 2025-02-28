using TFH.ViewModels;
using TFH.Views;

namespace TFH
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainViewModel model)
        {
            InitializeComponent();
            BindingContext = model;
            //HandlerChanged += OnHandlerChanged;
        }

        void OnHandlerChanged(object sender, EventArgs e)
        {
            BindingContext = Handler.MauiContext.Services.GetService<MainViewModel>();
        }
    }
}
