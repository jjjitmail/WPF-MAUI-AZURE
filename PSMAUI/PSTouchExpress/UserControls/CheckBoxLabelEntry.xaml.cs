//using Android.Content.Res;
//using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using PSTouchExpress.Helpers;

namespace PSTouchExpress.UserControls;

public partial class CheckBoxLabelEntry : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), 
        typeof(CheckBoxLabelEntry), default(string), BindingMode.TwoWay);
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
        typeof(CheckBoxLabelEntry), default(string), BindingMode.TwoWay);
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool),
        typeof(CheckBoxLabelEntry), default(bool), BindingMode.TwoWay);
    public static readonly BindableProperty UnitTextProperty = BindableProperty.Create(nameof(UnitText), typeof(string),
        typeof(CheckBoxLabelEntry), default(string), BindingMode.TwoWay);

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
            Unit.Text = UnitText;
            entry.TextChanged += OnTextChanged;
        });
    }

    async void UIElements()
    {
        await PSTasks.ActionTask(async () =>
        {
            await Task.Delay(100);
            PSUnderline.IsVisible = false;
            entry.Keyboard = Keyboard.Numeric;
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
            CheckBox.IsChecked = IsChecked;
        }
        else if (propertyName == UnitTextProperty.PropertyName)
        {
            Unit.Text = UnitText;
        }
    }
}