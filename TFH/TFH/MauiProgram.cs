using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Services;
using TFH.Views;
using TFH.ViewModels;
using Syncfusion.Maui.Toolkit.Hosting;

namespace TFH
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<IUserServices, UserServices>();
            builder.Services.AddSingleton<UserViewModel>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransientWithShellRoute<UserView, UserViewModel>("user");
            builder.Services.AddTransientWithShellRoute<MainPage, MainViewModel>("main");
            return builder.Build();
        }
    }
}
