//using Android.Content.Res;
//using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using PSTouchExpress.Helpers;

namespace PSTouchExpress.UserControls;

public partial class LabelCheckBox : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), 
        typeof(LabelCheckBox), default(string), BindingMode.TwoWay);

    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool),
        typeof(LabelCheckBox), default(bool), BindingMode.TwoWay);

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

    public LabelCheckBox()
	{
        Initialize();
    }

    async void Initialize()
    {
        await PSTasks.ActionTask(() =>
        {
            InitializeComponent();
            title.Text = Title;
            CheckBox.IsChecked = IsChecked;
        });
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == TitleProperty.PropertyName)
        {
            title.Text = Title;
        }
        else if (propertyName == IsCheckedProperty.PropertyName)
        {
            CheckBox.IsChecked = IsChecked;
        }
    }
}