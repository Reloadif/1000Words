﻿using MobileMainProject.Services;
using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace MobileMainProject.Infrastructure.Converters
{
    public class TranslateWordsToBeatifyString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Join(", ", MainService.TranslateWordsToList(value.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
