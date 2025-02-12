using Microsoft.Extensions.Localization;
using NNN.Core.Common.Parameters;
using NNN.Core.Instruments.Capabilities;
using NNN.Core.Presentation.Controls;
using NNN.Core.Presentation.MAUI.Helpers;

namespace NNN.Core.Presentation.MAUI.UserControls;

public partial class ValidationSummary : ContentView, IDisposable
{
    private StackLayout _stackLayout;

    public ValidationSummary()
	{
		InitializeComponent();

        
    }

    private ParameterGroup _parameterGroup;
    public ParameterGroup ParameterGroup
    {
        get { return _parameterGroup; }
        set
        {
            _parameterGroup = value;
            OnPropertyChanged();
        }
    }

    private ParameterGroup _methodParameterGroup;
    public ParameterGroup MethodParameterGroup
    {
        get { return _methodParameterGroup; }
        set
        {
            _methodParameterGroup = value;
            OnPropertyChanged();
        }
    }
    private IStringLocalizer _stringLocalizer;
    public IStringLocalizer StringLocalizer
    {
        get { return _stringLocalizer; }
        set { _stringLocalizer = value; OnPropertyChanged(); }
    }
    private IStringLocalizer _validationStringLocalizer;
    public IStringLocalizer ValidationStringLocalizer
    {
        get { return _validationStringLocalizer; }
        set { _validationStringLocalizer = value; OnPropertyChanged(); }
    }
    public bool _isReadOnly;
    public bool IsReadOnly
    {
        get { return _isReadOnly; }
        set { _isReadOnly = value; OnPropertyChanged(); }
    }

    public static readonly BindableProperty ValidationErrorsProperty = BindableProperty.Create(nameof(ValidationErrors), typeof(IEnumerable<ValidationError>),
       typeof(ValidationSummary), default(IEnumerable<ValidationError>), BindingMode.TwoWay, propertyChanged: OnValidationErrorsChanged);
    public IEnumerable<ValidationError> ValidationErrors
    {
        get => (IEnumerable<ValidationError>)GetValue(ValidationErrorsProperty);
        set => SetValue(ValidationErrorsProperty, value);
    }
    private static void OnValidationErrorsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        throw new NotImplementedException();
    }

    private void ShowValidationSummary(IReadOnlyList<ValidationError> _ValidationErrors)
    {
        if (_ValidationErrors.Count > 0)
        {
            Grid grid = new() { ColumnSpacing = 5 };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

            var headerLabel = new Label();

            headerLabel.FontSize = 14;
            headerLabel.Text = String.Format(" > {0}", _ValidationErrors.Count.ToString());
            headerLabel.BackgroundColor = Color.FromArgb(PSColor.Red);
            headerLabel.TextColor = Color.FromArgb(PSColor.DefaultWhiteColor);
            headerLabel.Padding = new Thickness(5, 5, 5, 5);

            grid.Children.Add(headerLabel);

            for (int i = 0; i < _ValidationErrors.Count; i++)
            {
                HorizontalStackLayout _layout = new HorizontalStackLayout();
                Label _iconLabel = new Label
                {
                    FontFamily = "SegMDL2",
                    Text = "\uEB90",
                    Margin = new Thickness(2, 0, 0, 0),
                    TextColor = Color.FromArgb(PSColor.Red),
                    VerticalOptions = LayoutOptions.Center,
                };
                _layout.Children.Add(_iconLabel);
                Label _label = new Label
                {
                    Text = _ValidationErrors[i].Id,
                    Margin = new Thickness(3, 0, 0, 0),
                    VerticalOptions = LayoutOptions.Center,
                };
                _layout.Children.Add(_label);

                grid.Children.Add(_layout);
            }

            thisContent.Content = grid;
        }
        else
        {
            thisContent.Content = null;
        }
    }


    private bool _disposedValue;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                //entry.Focused -= Entry_Focused;
                //entry.Unfocused -= Entry_Unfocused;
                //entry.TextChanged -= OnTextChanged;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~ValidationSummary() => Dispose(disposing: false);

    void IDisposable.Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void thisContent_BindingContextChanged(object sender, EventArgs e)
    {
        var ValidationErrors = (IReadOnlyList<ValidationError>)this.BindingContext;

        ShowValidationSummary(ValidationErrors);
    }
}