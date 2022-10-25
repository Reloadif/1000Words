using System;
using Xamarin.Forms;

namespace MobileMainProject.Infrastructure.Behaviors
{
    public class AnimatedBoxViewBehavior : Behavior<BoxView>
    {
        #region Properties
        public BoxView AssosiatedObject { get; set; }
        #endregion

        #region BindableProperty
        public static readonly BindableProperty AnimationTriggerProperty = BindableProperty.Create(nameof(AnimationTrigger), typeof(int), typeof(AnimatedBoxViewBehavior), propertyChanged: OnAnimationTriggerChanged);

        private static void OnAnimationTriggerChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as AnimatedBoxViewBehavior).HandleAnimationTrigger();
        }

        public int AnimationTrigger
        {
            get => (int)GetValue(AnimationTriggerProperty);
            set => SetValue(AnimationTriggerProperty, value);
        }
        #endregion

        protected override void OnAttachedTo(BoxView bindable)
        {
            base.OnAttachedTo(bindable);
            AssosiatedObject = bindable;
            bindable.BindingContextChanged += BindableBindingContextChanged;
        }

        protected override void OnDetachingFrom(BoxView bindable)
        {
            bindable.BindingContextChanged -= BindableBindingContextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void BindableBindingContextChanged(object sender, EventArgs e)
        {
            BoxView stack = sender as BoxView;
            BindingContext = stack?.BindingContext;
        }

        private async void HandleAnimationTrigger()
        {
            _ = await AssosiatedObject.RotateXTo(180);
            AssosiatedObject.RotationX = 0;
        }
    }
}
