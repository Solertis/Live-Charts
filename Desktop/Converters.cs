﻿//The MIT License(MIT)

//copyright(c) 2016 Greg Dennis & Alberto Rodriguez

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace LiveChartsDesktop
{
    public class TimespanMillisecondsConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var valueString = value as string;

            if (valueString != null)
                return TimeSpan.FromMilliseconds(double.Parse(valueString));

            return base.ConvertFrom(context, culture, value);
        }
    }

    internal class SerieConverter : IValueConverter
    {
        public static SerieConverter Instance { get; set; }

        static SerieConverter()
        {
            Instance = new SerieConverter();
        }
        private SerieConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var series = value as IEnumerable<Series>;
            if (series != null)
                return series.Select(x => new SeriesStandin
                {

                });

            var serie = value as Series;
            if (serie != null)
                return new SeriesStandin
                {
                    Title = serie.Title,
                    Stroke = serie.Stroke,
                    Fill = serie.Fill
                };

            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
