using Microsoft.Extensions.Localization;
using NNN.Core.Common.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Controls.Parameters
{
    public class ValidationControl : ContentView
    {
        public ValidationControl()
        {
            //DefaultStyleKey = typeof(ValidationControl);

            //GenerateValidationControl();

            //Content = new ScrollViewer
            //{
            //    VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            //    Content = _stackPanel
            //};
        }

        public static readonly BindableProperty StringLocalizerProperty = BindableProperty.Create(nameof(StringLocalizer), 
            typeof(IStringLocalizer), typeof(ValidationControl), 
            default(IStringLocalizer), BindingMode.TwoWay);

        public static readonly BindableProperty ValidationControlTypeProperty = BindableProperty.Create(nameof(ValidationControlType),
            typeof(ValidationControlType), typeof(ValidationControl),
            default(ValidationControlType), BindingMode.TwoWay);

        public static readonly BindableProperty ValidationStringLocalizerProperty = BindableProperty.Create(nameof(StringLocalizer),
            typeof(IStringLocalizer), typeof(ValidationControl),
            default(IStringLocalizer), BindingMode.TwoWay);

        public static readonly BindableProperty ValidationErrorsProperty = BindableProperty.Create(nameof(ValidationErrors),
            typeof(IReadOnlyList<ValidationError>), typeof(ValidationControl),
            default(IReadOnlyList<ValidationError>), BindingMode.TwoWay);

        public IStringLocalizer StringLocalizer
        {
            get => (IStringLocalizer)GetValue(StringLocalizerProperty);
            set => SetValue(StringLocalizerProperty, value);
        }

        public ValidationControlType ValidationControlType
        {
            get => (ValidationControlType)GetValue(ValidationControlTypeProperty);
            set => SetValue(ValidationControlTypeProperty, value);
        }

        public IStringLocalizer ValidationStringLocalizer
        {
            get => (IStringLocalizer)GetValue(ValidationStringLocalizerProperty);
            set => SetValue(ValidationStringLocalizerProperty, value);
        }

        public IReadOnlyList<ValidationError> ValidationErrors
        {
            get => (IReadOnlyList<ValidationError>)GetValue(ValidationErrorsProperty);
            set => SetValue(ValidationErrorsProperty, value);
        }
    }

    public enum ValidationControlType
    {
        Parameter,
        Header,
        List
    }
}
