using NNN.Core.Common.Parameters;

namespace NNN.Core.Presentation.MAUI;

public partial class CheckBoxControl : ContentView
{
	public CheckBoxControl()
	{
		InitializeComponent();
	}
    public Parameter CheckBoxParameter
    {
        get => (Parameter)GetValue(CheckBoxParameterProperty);
        set => SetValue(CheckBoxParameterProperty, value);
    }

    public object CheckBoxContent
    {
        get => GetValue(CheckBoxContentProperty);
        set => SetValue(CheckBoxContentProperty, value);
    }

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    public static readonly BindableProperty CheckBoxParameterProperty = BindableProperty.Create(nameof(CheckBoxParameter), typeof(Parameter),
        typeof(CheckBoxControl), default(Parameter), BindingMode.TwoWay);

    public static readonly BindableProperty CheckBoxContentProperty = BindableProperty.Create(nameof(CheckBoxContent), typeof(object),
        typeof(CheckBoxControl), default(object), BindingMode.TwoWay);

    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool),
        typeof(CheckBoxControl), default(bool), BindingMode.TwoWay);


    public event EventHandler IsCheckedChanged;

    public bool? GetBoolValue(Parameter parameter)
    {
        return parameter.Value?.AsBoolean();
    }

    public void SetBoolValue(bool? b)
    {
        if (b is not { } boolValue) return;
        CheckBoxParameter.Value = boolValue;
        IsChecked = boolValue;

        IsCheckedChanged?.Invoke(this, new EventArgs());
    }
}