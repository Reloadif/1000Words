using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileMainProject.Infrastructure.Behaviors
{
    public class PickerSelectedItemBehavior : Behavior<Picker>
    {
        #region BindableProperty
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(PickerSelectedItemBehavior));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        #endregion

        protected override void OnAttachedTo(Picker bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.BindingContextChanged += BindableBindingContextChanged;
            bindable.SelectedIndexChanged += BindableSelectedIndexChanged;
        }

        protected override void OnDetachingFrom(Picker bindable)
        {
            bindable.SelectedIndexChanged -= BindableSelectedIndexChanged;
            bindable.BindingContextChanged -= BindableBindingContextChanged;

            base.OnDetachingFrom(bindable);
        }

        private void BindableBindingContextChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            BindingContext = picker?.BindingContext;
        }

        private void BindableSelectedIndexChanged(object sender, EventArgs e)
        {
            Command?.Execute((sender as Picker).SelectedItem);
        }
    }
}
