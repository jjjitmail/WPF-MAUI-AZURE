//using Android.Content.Res;
//using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using NNN.Core.Common.Parameters;
using NNN.Core.Presentation.MAUI.Helpers;

namespace NNN.Core.Presentation.MAUI.UserControls;

public partial class CheckBoxLabelEntry : ContentView, IDisposable
{
    public static readonly BindableProperty ParameterProperty = BindableProperty.Create(nameof(Parameter), typeof(Parameter),
typeof(LabelEntryH), default(Parameter), BindingMode.TwoWay, propertyChanged: OnParameterChanged);

    private static void OnParameterChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is not Parameter parameter ||
                bindable is not CheckBoxLabelEntry control) return;

        control.IsNumeric = parameter.Type == ParameterType.Double;

        control.entry.Text = parameter.Value.ToString();
        control.UnitLbl.Text = parameter.Unit.ToString();
        control.CheckBox.IsChecked = false; // todo; data from viewmodel
        control.entry.IsReadOnly = !control.CheckBox.IsChecked;
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), 
        typeof(CheckBoxLabelEntry), default(string), BindingMode.TwoWay);
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
        typeof(CheckBoxLabelEntry), default(string), BindingMode.TwoWay);
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool),
        typeof(CheckBoxLabelEntry), default(bool), BindingMode.TwoWay);
    public static readonly BindableProperty UnitTextProperty = BindableProperty.Create(nameof(UnitText), typeof(string),
        typeof(CheckBoxLabelEntry), default(string), BindingMode.TwoWay);

    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool),
        typeof(CheckBoxLabelEntry), default(bool), BindingMode.TwoWay);
    public static readonly BindableProperty IsNumericProperty = BindableProperty.Create(nameof(IsNumeric), typeof(bool),
    typeof(LabelEntryH), default(bool), BindingMode.TwoWay);
    private bool _disposedValue;

    public Parameter Parameter
    {
        get => (Parameter)GetValue(ParameterProperty);
        set => SetValue(ParameterProperty, value);
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
    public bool IsReadOnly
    {
        get
        {
            return (bool)GetValue(IsReadOnlyProperty);
        }
        set
        {
            SetValue(IsReadOnlyProperty, value);
        }
    }

    public bool IsChecked
    {
        get
        {
            return (bool)GetValue(IsCheckedProperty);
        }
        set
        {
            SetValue(IsCheckedProperty, value);
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
    public string UnitText
    {
        get
        {
            return (string)GetValue(UnitTextProperty);
        }
        set
        {
            SetValue(UnitTextProperty, value);
        }
    }

    public CheckBoxLabelEntry()
	{
        Initialize();
        UIElements();
    }

    async void Initialize()
    {
        await PSTasks.ActionTask(() =>
        {
            InitializeComponent();
            title.Text = Title;
            entry.Text = Text;
            UnitLbl.Text = UnitText;
            entry.TextChanged += OnTextChanged;
            CheckBox.CheckedChanged += CheckBox_CheckedChanged;
        });
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        entry.IsReadOnly = CheckBox.IsChecked == false;
    }

    async void UIElements()
    {
        await PSTasks.ActionTask(async () =>
        {
            await Task.Delay(100);
            PSUnderline.IsVisible = false;
            if (IsNumeric)
            {
                entry.Keyboard = Keyboard.Numeric;
            }
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
        // todo: validation
        Text = e.NewTextValue;        
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
        else if (propertyName == IsCheckedProperty.PropertyName)
        {
            CheckBox.IsChecked = IsChecked; // not necessary
        }
        else if (propertyName == UnitTextProperty.PropertyName)
        {
            UnitLbl.Text = UnitText;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                entry.TextChanged -= OnTextChanged;
                CheckBox.CheckedChanged -= CheckBox_CheckedChanged;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~CheckBoxLabelEntry() => Dispose(disposing: false);

    void IDisposable.Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}