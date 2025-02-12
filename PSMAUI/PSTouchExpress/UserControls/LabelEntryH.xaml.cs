//using Android.Content.Res;
//using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using PSTouchExpress.Helpers;

namespace PSTouchExpress.UserControls;

public partial class LabelEntryH : ContentView, IDisposable
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), 
        typeof(LabelEntryH), default(string), BindingMode.TwoWay);
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
        typeof(LabelEntryH), default(string), BindingMode.TwoWay);
    public static readonly BindableProperty IsNumericProperty = BindableProperty.Create(nameof(IsNumeric), typeof(bool),
        typeof(LabelEntryH), default(bool), BindingMode.TwoWay);

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

    public LabelEntryH()
	{
        Initialize();
        UIElements();

//#if ANDROID
//        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
//        {
//            h.NativeView.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
//        });
//#endif
    }

    async void Initialize()
    {
        await PSTasks.ActionTask(() =>
        {
            InitializeComponent();
            title.Text = Title;
            entry.Text = Text;
            entry.TextChanged += OnTextChanged;
        });
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
    }

    public void Dispose()
    {
        //entry.Focused -= Entry_Focused;
        //entry.Unfocused -= Entry_Unfocused;
        //entry.TextChanged -= OnTextChanged;

        //GC.SuppressFinalize(this);
    }

    private Color CurrentColor { get; set; }
    private static Color _defaultBlueColor => Color.FromArgb(PSColor.DefaultBlueColor);

}