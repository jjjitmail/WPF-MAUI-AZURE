//using Android.Content.Res;
//using Microsoft.Maui.Controls.Compatibility.Platform.Android;
//using Microsoft.Maui.Controls;
using NNN.Core.Common.Parameters;
using NNN.Core.Presentation.MAUI.Controls;
using NNN.Core.Presentation.MAUI.Converters;
using NNN.Core.Presentation.MAUI.Helpers;
using NNN.Core.Presentation.MAUI.Models;

namespace NNN.Core.Presentation.MAUI.UserControls;

public partial class LabelEntryNumberic : ContentView, IDisposable
{
    public static readonly BindableProperty ParameterProperty = BindableProperty.Create(nameof(Parameter), typeof(Parameter),
        typeof(LabelEntryNumberic), default(Parameter), BindingMode.TwoWay, propertyChanged: OnParameterChanged);

    private static void OnParameterChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is not Parameter parameter ||
                bindable is not LabelEntryNumberic control) return;

        control.ParameterValue = parameter.Value;
    }

    public static readonly BindableProperty ParameterValueProperty = BindableProperty.Create(nameof(ParameterValue), typeof(ParameterValue),
        typeof(LabelEntryNumberic), default(ParameterValue), BindingMode.TwoWay, propertyChanged: valueChanged);

    private static void valueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not LabelEntryNumberic control) return;
        var converter = new ParameterConverter();
        control.entry.Text = converter.Convert(newValue, typeof(string), control.Parameter, null) as string;
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), 
        typeof(LabelEntryNumberic), default(string), BindingMode.TwoWay);
    public static readonly BindableProperty ErrorLabelProperty = BindableProperty.Create(nameof(ErrorLabel), typeof(string),
        typeof(LabelEntryNumberic), default(string), BindingMode.TwoWay);

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
        typeof(LabelEntryNumberic), default(string), BindingMode.TwoWay);
    public static readonly BindableProperty UnitLabelProperty = BindableProperty.Create(nameof(UnitLabel), typeof(string),
        typeof(LabelEntryNumberic), default(string), BindingMode.TwoWay);
    public static readonly BindableProperty IsNumericProperty = BindableProperty.Create(nameof(IsNumeric), typeof(bool),
        typeof(LabelEntryNumberic), default(bool), BindingMode.TwoWay);
    private bool _disposedValue;

    public Parameter Parameter
    {
        get => (Parameter)GetValue(ParameterProperty);
        set => SetValue(ParameterProperty, value);
    }
    public ParameterValue ParameterValue
    {
        get => (ParameterValue)GetValue(ParameterValueProperty);
        set => SetValue(ParameterValueProperty, value);
    }

    public double GetParameterValue(Parameter parameter)
    {
        return parameter.Type switch
        {
            ParameterType.Integer => Convert.ToDouble(parameter.Value!.AsInteger(), Thread.CurrentThread.CurrentUICulture),
            ParameterType.Double => parameter.Value!.AsDouble(),
            ParameterType.ParameterGroup => parameter.Values.Count,
            _ => 0
        };
    }

    public void SetParameterValue(double d)
    {
        if (double.IsNaN(d)) entry.Text = GetParameterValue(Parameter).ToString();

        switch (Parameter.Type)
        {
            case ParameterType.Integer:
            Parameter.Value = Convert.ToInt64(d, Thread.CurrentThread.CurrentUICulture);
            break;
            case ParameterType.Double:
            Parameter.Value = d;
            break;
            case ParameterType.ParameterGroup:
            SetCollectionCount(Convert.ToInt32(d, Thread.CurrentThread.CurrentUICulture));
            break;
        }
    }

    private void SetCollectionCount(int count)
    {
        if (Parameter == null || Parameter.Values.Count == 0 || count < 1) return;

        int prevCount = Parameter.Values.Count;
        var values = Parameter.Values.ToArray();
        var last = values[prevCount - 1];
        Array.Resize(ref values, count);

        for (int i = prevCount; i < count; ++i) values[i] = last.Type == ParameterType.ParameterGroup ? new ParameterGroup(last.AsParameterGroup().Capabilities) : last;

        Parameter.Values = values;
    }

    public bool IsNumeric
    {
        get
        {
            return (bool)GetValue(IsNumericProperty);
        }
        set
        {
            SetValue(IsNumericProperty, value);
        }
    }
    public string Title
    {
        get
        {
            return (string)GetValue(TitleProperty);
        }
        set
        {
            SetValue(TitleProperty, value);
        }
    }
    public string ErrorLabel
    {
        get
        {
            return (string)GetValue(ErrorLabelProperty);
        }
        set
        {
            SetValue(ErrorLabelProperty, value);
        }
    }
    public string Text
    {
        get
        {
            return (string)GetValue(TextProperty);
        }
        set
        {
            SetValue(TextProperty, value);
        }
    }
    public string UnitLabel
    {
        get
        {
            return (string)GetValue(UnitLabelProperty);
        }
        set
        {
            SetValue(UnitLabelProperty, value);
        }
    }

    public LabelEntryNumberic()
	{
        Initialize();
//#if ANDROID
//        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
//        {
//            h.NativeView.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
//        });
//#endif
    }

    async void Initialize()
    {
        InitializeComponent();
        BindingContext = this;
        await PSTasks.ActionTask(() =>
        {
            title.Text = Title;            
            UnitLbl.Text = UnitLabel;
            entry.TextChanged += OnTextChanged;
        });
    }

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        PSUnderline.IsVisible = false;
        title.TextColor = Color.FromArgb(PSColor.DefaultTextColor);
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        PSUnderline.IsVisible = true;
        title.TextColor = Color.FromArgb(PSColor.DefaultBlueColor);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var converter = new ParameterConverter();

        //Parameter.Value = converter.ConvertBack(e.NewTextValue, typeof(ParameterValue), Parameter, null) as ParameterValue;
        string newText = e.NewTextValue;
        string oldText = e.OldTextValue;

        //Parameter.Capabilities.m

        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            switch (Parameter.Type)
            {
                case ParameterType.String: Parameter.Value = newText; break;
                case ParameterType.Double: 
                    if (double.TryParse(newText, out var doubleValue)) 
                        Parameter.Value = doubleValue; 
                    break;
                case ParameterType.ParameterGroup:
                if (!string.IsNullOrEmpty(newText)
                    && newText != oldText 
                    && double.TryParse(newText, out double count))
                {
                    if (count > 0)
                    {
                        SetCollectionCount(Convert.ToInt32(count, Thread.CurrentThread.CurrentUICulture));
                    }
                    else
                        newText = oldText;
                    
                }
                break;
            }
        }
        Text = newText;
        ErrorLabel = "";
        if (Parameter.ValidationErrors.Count > 0)
        {
            ErrorLabel = "\uEB90"; //Parameter.ValidationErrors.First().Id;

            //// need to be in ParameterGroup
            //PSTasks.ActionTask(() =>
            //{
            //    PSMessaging.Publish<ValidationModel>(new ValidationModel { Parameter = Parameter }, "ValidationSummary");
            //});
        }
        //Parameter.Parent


    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == TitleProperty.PropertyName)
        {
            title.Text = Title;
        }
        else if (propertyName == TextProperty.PropertyName)
        {
            entry.Text = Text;
        }
        else if (propertyName == UnitLabelProperty.PropertyName)
        {
            UnitLbl.Text = UnitLabel;
        }
        else if (propertyName == ErrorLabelProperty.PropertyName)
        {
            errorLabel.Text = ErrorLabel;
        }
    }

    private Color CurrentColor { get; set; }
    private static Color _defaultBlueColor => Color.FromArgb(PSColor.DefaultBlueColor);

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                entry.Focused -= Entry_Focused;
                entry.Unfocused -= Entry_Unfocused;
                entry.TextChanged -= OnTextChanged;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~LabelEntryNumberic() => Dispose(disposing: false);

    void IDisposable.Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}