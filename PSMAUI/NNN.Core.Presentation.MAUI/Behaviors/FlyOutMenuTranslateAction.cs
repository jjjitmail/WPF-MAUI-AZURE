using NNN.Core.Presentation.MAUI.Helpers;
using NNN.Core.Presentation.MAUI.Models;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Behaviors
{
    [Preserve(AllMembers = true)]
    public class FlyOutMenuTranslateAction : AnimationBase, IAction
    {
        public static readonly BindableProperty XProperty = BindableProperty.Create(nameof(X), typeof(double), typeof(TranslateAction), 1.0);
        public static readonly BindableProperty YProperty = BindableProperty.Create(nameof(Y), typeof(double), typeof(TranslateAction), 1.0);

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public async Task<bool> Execute(object sender, object parameter)
        {
            await PSTasks.ActionTask(() =>
            {
                PSMessaging.Publish<FlyOutMenuStatus>(new FlyOutMenuStatus() { IsPresented = IsFlyOutMenuPresented }, "fmenu");
                IsFlyOutMenuPresented = !IsFlyOutMenuPresented;
            });
            
            return true;
        }
    }
}
