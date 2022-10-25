using System.Windows.Input;
using Xamarin.Forms;

namespace MobileMainProject.Infrastructure.CustomControls
{
    public partial class CustomIntStepper : ContentView
    {
        #region BindableProperty
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(int), typeof(CustomIntStepper), 0, BindingMode.TwoWay);
        public static readonly BindableProperty MinimumValueProperty = BindableProperty.Create(nameof(MinimumValue), typeof(int), typeof(CustomIntStepper), int.MinValue);
        public static readonly BindableProperty MaximumValueProperty = BindableProperty.Create(nameof(MaximumValue), typeof(int), typeof(CustomIntStepper), int.MaxValue);

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set
            {
                SetValue(ValueProperty, value);
                (MinusCommand as Command).ChangeCanExecute();
                (AddCommand as Command).ChangeCanExecute();
            }
        }
        public int MinimumValue
        {
            get => (int)GetValue(MinimumValueProperty);
            set => SetValue(MinimumValueProperty, value);
        }
        public int MaximumValue
        {
            get => (int)GetValue(MaximumValueProperty);
            set => SetValue(MaximumValueProperty, value);
        }
        #endregion

        public CustomIntStepper()
        {
            InitializeComponent();

            MinusCommand = new Command(ExecuteMinusCommand, CanExecuteMinusCommand);
            AddCommand = new Command(ExecuteAddCommand, CanExecuteAddCommand);

            XamlStepperMinusButton.SetBinding(Button.CommandProperty, new Binding("MinusCommand", source: this));
            XamlStepperLabel.SetBinding(Label.TextProperty, new Binding("Value", source: this));
            XamlStepperAddButton.SetBinding(Button.CommandProperty, new Binding("AddCommand", source: this));
        }

        #region Commands
        public ICommand MinusCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

        private void ExecuteMinusCommand(object obj)
        {
            Value -= 1;
        }
        private void ExecuteAddCommand(object obj)
        {
            Value += 1;
        }

        private bool CanExecuteMinusCommand(object obj)
        {
            return Value > MinimumValue;
        }
        private bool CanExecuteAddCommand(object obj)
        {
            return Value < MaximumValue;
        }
        #endregion
    }
}