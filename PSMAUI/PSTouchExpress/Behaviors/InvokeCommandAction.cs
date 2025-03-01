﻿using System.Threading.Tasks;
using System.Windows.Input;

#pragma warning disable 1998

namespace PSTouchExpress.Behaviors
{
	[Preserve(AllMembers = true)]
	public sealed class InvokeCommandAction : BindableObject, IAction
	{
		public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(InvokeCommandAction), null);
		public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(InvokeCommandAction), null);
		public static readonly BindableProperty InputConverterProperty = BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(InvokeCommandAction), null);
		public static readonly BindableProperty InputConverterParameterProperty = BindableProperty.Create(nameof(ConverterParameter), typeof(object), typeof(InvokeCommandAction), null);

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
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public IValueConverter Converter
		{
			get { return (IValueConverter)GetValue(InputConverterProperty); }
			set { SetValue(InputConverterProperty, value); }
		}

        public object ConverterParameter
		{
			get { return GetValue(InputConverterParameterProperty); }
			set { SetValue(InputConverterParameterProperty, value); }
		}
              
		public async Task<bool> Execute(object sender, object parameter)
		{
			if (Command == null)
			{
				return false;
			}

			object resolvedParameter;
			if (CommandParameter != null)
			{
				resolvedParameter = CommandParameter;
			}
			else if (Converter != null)
			{
				resolvedParameter = Converter.Convert(parameter, typeof(object), ConverterParameter, null);
			}
			else 
			{
				resolvedParameter = parameter;
			}

			if (!Command.CanExecute(resolvedParameter))
			{
				return false;
			}

			Command.Execute(resolvedParameter);

            if (ClickEventTarget != null)
            {
                IButton element = ClickEventTarget as IButton;

                element.Clicked();
            }

			return true;
		}
	}
}

