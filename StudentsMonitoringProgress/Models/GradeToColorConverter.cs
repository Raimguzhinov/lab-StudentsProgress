using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace StudentsMonitoringProgress.Models;

public class GradeToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return null!;

        if (value is not double grade)
            grade = System.Convert.ToDouble(value, culture);

        if (double.IsNaN(grade) || double.IsInfinity(grade))
            return null!;

        if (grade < 1)
            return Brushes.Tomato;
        else if (grade < 1.5)
            return Brushes.Gold;
        else
            return Brushes.YellowGreen;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}