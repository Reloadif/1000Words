using MobileMainProject.Infrastructure.Shared;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileMainProject.Infrastructure.Behaviors
{
    public class AnimatedLabelBehavior : Behavior<Label>
    {
        #region Properties
        public Label AssosiatedObject { get; set; }
        #endregion

        #region BindableProperty
        public static readonly BindableProperty AnimationTriggerProperty = BindableProperty.Create(nameof(AnimationTrigger), typeof(AnimationInfo), typeof(AnimatedBoxViewBehavior), propertyChanged: OnAnimationTriggerChanged);
        public static readonly BindableProperty TaskWaitProperty = BindableProperty.Create(nameof(TaskWait), typeof(Task<bool>), typeof(AnimatedBoxViewBehavior), null, BindingMode.OneWayToSource);

        private static void OnAnimationTriggerChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as AnimatedLabelBehavior).HandleAnimationTrigger((AnimationInfo)newValue);
        }

        public AnimationInfo AnimationTrigger
        {
            get => (AnimationInfo)GetValue(AnimationTriggerProperty);
            set => SetValue(AnimationTriggerProperty, value);
        }

        public Task<bool> TaskWait
        {
            get => (Task<bool>)GetValue(TaskWaitProperty);
            set => SetValue(TaskWaitProperty, value);
        }
        #endregion

        protected override void OnAttachedTo(Label bindable)
        {
            base.OnAttachedTo(bindable);

            AssosiatedObject = bindable;
            bindable.BindingContextChanged += BindableBindingContextChanged;
        }

        protected override void OnDetachingFrom(Label bindable)
        {
            bindable.BindingContextChanged -= BindableBindingContextChanged;

            base.OnDetachingFrom(bindable);
        }

        private void BindableBindingContextChanged(object sender, EventArgs e)
        {
            Label label = sender as Label;
            BindingContext = label?.BindingContext;
        }

        private void HandleAnimationTrigger(AnimationInfo info)
        {
            if (info == AnimationInfo.AnimationOut)
            {
                TaskWait = AssosiatedObject.TranslateTo(Application.Current.MainPage.Width, 0);
            }
            if (info == AnimationInfo.AnimationIn)
            {
                AssosiatedObject.TranslationX = -Application.Current.MainPage.Width;
                TaskWait = AssosiatedObject.TranslateTo(0, 0);
            }
        }
    }
}
