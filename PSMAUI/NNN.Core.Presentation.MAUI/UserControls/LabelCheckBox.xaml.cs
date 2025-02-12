//using Android.Content.Res;
//using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using NNN.Core.Common.Parameters;
using NNN.Core.Presentation.MAUI.Helpers;

namespace NNN.Core.Presentation.MAUI.UserControls;

public partial class LabelCheckBox : ContentView
{
    public static readonly BindableProperty ParameterProperty = BindableProperty.Create(nameof(Parameter), typeof(Parameter),
typeof(LabelEntryH), default(Parameter), BindingMode.TwoWay, propertyChanged: OnParameterChanged);

    private static void OnParameterChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is not Parameter parameter ||
                bindable is not LabelCheckBox control) return;

        control.IsChecked = parameter.Value.AsBoolean() == true;
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), 
        typeof(LabelCheckBox), default(string), BindingMode.TwoWay);

    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool),
        typeof(LabelCheckBox), default(bool), BindingMode.TwoWay);

    public Parameter Parameter
    {
        get => (Parameter)GetValue(ParameterProperty);
        set => SetValue(ParameterProperty, value);
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