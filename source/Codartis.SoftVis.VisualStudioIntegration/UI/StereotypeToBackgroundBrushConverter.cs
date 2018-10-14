﻿using System;
using System.Globalization;
using System.Windows.Data;
using Codartis.SoftVis.Modeling;

namespace Codartis.SoftVis.VisualStudioIntegration.UI
{
    /// <summary>
    /// Converts a model entity stereotype to its background brush.
    /// </summary>
    internal class StereotypeToBackgroundBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stereotype = (ModelNodeStereotype)value;
            return StereotypeToVisualPropertyMaps.StereotypeToBackgroundBrushMap[stereotype];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
