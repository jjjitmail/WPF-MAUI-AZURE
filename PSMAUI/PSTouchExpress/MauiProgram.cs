using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Maui.Controls.Compatibility;
using Telerik.Maui.Controls.Compatibility;
using PSTouchExpress.Pages;
//using PSTouchExpress.Models;

namespace PSTouchExpress
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
                .UseTelerik()
                .UseMauiApp<App>()
//#if ANDROID
//                .ConfigureMauiHandlers(handlers =>
//                {
//                    //handlers.AddCompatibilityRenderer(typeof(Microsoft.Maui.Controls.BoxView),
//                    //    typeof(Microsoft.Maui.Controls.Compatibility.Platform.Android.BoxRenderer));
//                    //handlers.AddCompatibilityRenderer(typeof(Microsoft.Maui.Controls.Frame),
//                    //    typeof(Microsoft.Maui.Controls.Compatibility.Platform.Android.FastRenderers.FrameRenderer));

//                    // Register ALL handlers in the Xamarin Community Toolkit assembly
//                    //handlers.AddCompatibilityRenderers(typeof(Xamarin.CommunityToolkit.UI.Views.MediaElementRenderer).Assembly)

//                    // Register just one handler for the control you need
//                    //handlers.AddCompatibilityRenderer(typeof(Xamarin.CommunityToolkit.UI.Views.MediaElement), typeof(Xamarin.CommunityToolkit.UI.Views.MediaElementRenderer));
//                })
//#elif IOS
//                .ConfigureMauiHandlers(handlers =>
//                {
//                    //handlers.AddCompatibilityRenderer(typeof(Microsoft.Maui.Controls.BoxView),
//                    //    typeof(Microsoft.Maui.Controls.Compatibility.Platform.iOS.BoxRenderer));
//                    //handlers.AddCompatibilityRenderer(typeof(Microsoft.Maui.Controls.Frame),
//                    //    typeof(Microsoft.Maui.Controls.Compatibility.Platform.iOS.FrameRenderer));

//                    // Register ALL handlers in the Xamarin Community Toolkit assembly
//                    //handlers.AddCompatibilityRenderers(typeof(Xamarin.CommunityToolkit.UI.Views.MediaElementRenderer).Assembly)

//                    // Register just one handler for the control you need
//                    //handlers.AddCompatibilityRenderer(typeof(Xamarin.CommunityToolkit.UI.Views.MediaElement), typeof(Xamarin.CommunityToolkit.UI.Views.MediaElementRenderer));
//                })
//#endif
                .ConfigureFonts(fonts =>
				{
                    fonts.AddFont("Roboto-Regular.ttf", "DefaultTextFont");
                    fonts.AddFont("segmdl2.ttf", "SegMDL2");
				});

            //builder.Services.AddSingleton<MainPage>(); //
            //builder.Services.AddSingleton<ConnectionsPageModel>();

            //builder.Services.AddTransient<MainPageModel>();
            //builder.Services.AddTransient<MainPage>();

            //builder.Services.UseFreshMvvm();

            return builder.Build();
		}
	}
}
