using System;
using System.Windows.Input;

namespace NNN.Core.Presentation.MAUI.Behaviors
{
    public class ButtonClickBehavior : Behavior<Button>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(ListViewSelectedItemBehavior), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(ListViewSelectedItemBehavior), null);
        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create("Converter", typeof(IValueConverter), typeof(ListViewSelectedItemBehavior), null);

        public static readonly BindableProperty ClickEventTargetProperty = BindableProperty.Create(nameof(ClickEventTarget), typeof(object), typeof(InvokeCommandAction), null);

        public object ClickEventTarget
        {
            get { return (object)GetValue(ClickEventTargetProperty); }
            set { SetValue(ClickEventTargetProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(InputConverterProperty); }
            set { SetValue(InputConverterProperty, value); }
        }

        public Button AssociatedObject { get; private set; }

        protected override void OnAttachedTo(Button bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            //bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.Clicked += OnButtonClicked;
        }

        protected override void OnDetachingFrom(Button bindable)
        {
            base.OnDetachingFrom(bindable);
            //bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.Clicked -= OnButtonClicked;
            AssociatedObject = null;
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            if (Command == null)
            {
                return;
            }

            object parameter = Converter.Convert(e, typeof(object), null, null);
            if (Command.CanExecute(parameter))
            {
                Command.Execute(parameter);
            }

            if (ClickEventTarget != null)
            {
                IButton element = ClickEventTarget as IButton;

                element.Clicked();
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}
