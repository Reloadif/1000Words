using MobileMainProject.Infrastructure.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace MobileMainProject.Infrastructure.Converters
{
    public class AnswerStateToColorConverter : IValueConverter
    {
        public static readonly IDictionary<AnswerState, Color> AnswerStateToColor = new Dictionary<AnswerState, Color>();

        public AnswerStateToColorConverter()
        {
            AnswerStateToColor[AnswerState.None] = Color.Transparent;
            AnswerStateToColor[AnswerState.Right] = Color.Green;
            AnswerStateToColor[AnswerState.Pass] = Color.Orange;
            AnswerStateToColor[AnswerState.Wrong] = Color.DarkRed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AnswerStateToColor[(AnswerState)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
