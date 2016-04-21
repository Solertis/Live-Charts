﻿//The MIT License(MIT)

//copyright(c) 2016 Alberto Rodriguez

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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using LiveChartsCore;

namespace LiveChartsDesktop
{
    public abstract class Series : FrameworkElement, ISeriesView
    {
        private readonly LiveChartsCore.Series _model;

        protected Series()
        {
            _model = new LiveChartsCore.Series(this);
            DefaultFillOpacity = 0.35;
        }

        protected Series(SeriesConfiguration configuration)
        {
            _model = new LiveChartsCore.Series(this)
            {
                Configuration = configuration
            };
            DefaultFillOpacity = 0.35;
        }

        public ISeriesModel Model
        {
            get { return _model; }
        }

        #region Properties

        public double DefaultFillOpacity { get; set; }

        public static readonly DependencyProperty ValuesProperty = DependencyProperty.Register(
            "Values", typeof (IChartValues), typeof (Series),
            new PropertyMetadata(default(IChartValues), OnValuesChanged));
        /// <summary>
        /// Gets or sets chart values.
        /// </summary>
        public IChartValues Values
        {
            get { return (IChartValues) GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof (string), typeof (Series),
            new PropertyMetadata(default(string), OnPropertyChanged((v, m) => m.Title = v.Title)));
        /// <summary>
        /// Gets or sets serie title
        /// </summary>
        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke", typeof (Brush), typeof (Series), 
            new PropertyMetadata(default(Brush), OnPropertyChanged()));
        /// <summary>
        /// Gets or sets series stroke, if this property is null then a SolidColorBrush will be assigned according to series position in collection and Chart.Colors property
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush) GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness", typeof (double), typeof (Series), 
            new PropertyMetadata(default(double), OnPropertyChanged()));
        /// <summary>
        /// Gets or sets the series stroke thickness.
        /// </summary>
        public double StrokeThickness
        {
            get { return (double) GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill", typeof (Brush), typeof (Series), 
            new PropertyMetadata(default(Brush), OnPropertyChanged()));
        /// <summary>
        /// Gets or sets series fill color, if this property is null then a SolidColorBrush will be assigned according to series position in collection and Chart.Colors property, also Fill property has a default opacity according to chart type.
        /// </summary>
        public Brush Fill
        {
            get { return (Brush) GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly DependencyProperty DataLabelsProperty = DependencyProperty.Register(
            "DataLabels", typeof (bool), typeof (Series), 
            new PropertyMetadata(default(bool), OnPropertyChanged()));
        /// <summary>
        /// Gets or sets if series should include a label over each data point.
        /// </summary>
        public bool DataLabels
        {
            get { return (bool) GetValue(DataLabelsProperty); }
            set { SetValue(DataLabelsProperty, value); }
        }

        public static readonly DependencyProperty AnimationsSpeedProperty = DependencyProperty.Register(
            "AnimationsSpeed", typeof (TimeSpan?), typeof (Series),
            new PropertyMetadata(default(TimeSpan?), AnimationsSpeedPropertyChanged));
        /// <summary>
        /// Gets or sets the animation speed of this series, if this property is null then the animations speed is taken from the chart AnimationsSpeed property.
        /// </summary>
        [TypeConverter(typeof(TimespanMillisecondsConverter))]
        public TimeSpan? AnimationsSpeed
        {
            get { return (TimeSpan?) GetValue(AnimationsSpeedProperty); }
            set { SetValue(AnimationsSpeedProperty, value); }
        }

        public static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register(
            "FontFamily", typeof (FontFamily), typeof (Series), 
            new PropertyMetadata(default(FontFamily), OnPropertyChanged()));
        /// <summary>
        /// Gets or sets labels font family
        /// </summary>
        public FontFamily FontFamily
        {
            get { return (FontFamily) GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
            "FontSize", typeof (double),
            typeof (Series), new PropertyMetadata(10d, OnPropertyChanged()));
        /// <summary>
        /// Gets or sets labels font size
        /// </summary>
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register(
            "FontWeight", typeof (FontWeight), typeof (Series),
            new PropertyMetadata(FontWeights.Bold, OnPropertyChanged()));
        /// <summary>
        /// Gets or sets labels font weight
        /// </summary>
        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        public static readonly DependencyProperty FontStyleProperty = DependencyProperty.Register(
            "FontStyle", typeof (FontStyle),
            typeof (Series), new PropertyMetadata(FontStyles.Normal, OnPropertyChanged()));
        /// <summary>
        /// Gets or sets labels font style
        /// </summary>
		public FontStyle FontStyle
        {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        public static readonly DependencyProperty FontStretchProperty = DependencyProperty.Register(
            "FontStretch", typeof (FontStretch),
            typeof (Series), new PropertyMetadata(FontStretches.Normal, OnPropertyChanged()));
        /// <summary>
        /// Gets or sets labels font strech
        /// </summary>
		public FontStretch FontStretch
        {
            get { return (FontStretch)GetValue(FontStretchProperty); }
            set { SetValue(FontStretchProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
            "Foreground", typeof (Brush),
            typeof (Series), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(55, 71, 79)), OnPropertyChanged()));
        /// <summary>
        /// Gets or sets labels text color.
        /// </summary>
		public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register(
            "StrokeDashArray", typeof(DoubleCollection), typeof(Series), 
            new PropertyMetadata(default(DoubleCollection), OnPropertyChanged()));
        /// <summary>
        /// Gets or sets the stroke dash array of a series
        /// </summary>
        public DoubleCollection StrokeDashArray
        {
            get { return (DoubleCollection)GetValue(StrokeDashArrayProperty); }
            set { SetValue(StrokeDashArrayProperty, value); }
        }

        #endregion

        #region Internal Helpers

        internal TextBlock BindATextBlock(int rotate)
        {
            var tb = new TextBlock();

            tb.SetBinding(TextBlock.FontFamilyProperty,
                new Binding { Path = new PropertyPath(FontFamilyProperty), Source = this });
            tb.SetBinding(FontSizeProperty,
                new Binding { Path = new PropertyPath(FontSizeProperty), Source = this });
            tb.SetBinding(TextBlock.FontStretchProperty,
                new Binding { Path = new PropertyPath(FontStretchProperty), Source = this });
            tb.SetBinding(TextBlock.FontStyleProperty,
                new Binding { Path = new PropertyPath(FontStyleProperty), Source = this });
            tb.SetBinding(TextBlock.FontWeightProperty,
                new Binding { Path = new PropertyPath(FontWeightProperty), Source = this });
            tb.SetBinding(TextBlock.ForegroundProperty,
                 new Binding { Path = new PropertyPath(ForegroundProperty), Source = this });

            return tb;
        }

        #endregion

        public void Erase()
        {
            throw new NotImplementedException();
        }

        #region Callbacks

        private static void OnValuesChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            var series = o as Series;
            if (series == null) return;

            var observable = series.Model.Values as INotifyCollectionChanged;
            if (observable == null) return;

            observable.CollectionChanged += (sender, eventArgs) =>
            {
                series.Model.Chart.Update();
            };

            OnPropertyChanged((view, model) => model.Values = view.Values);
        }

        private static void AnimationsSpeedPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
#if DEBUG
            OnPropertyChanged(true);
#endif
        }

        private static PropertyChangedCallback OnPropertyChanged(bool animate = false)
        {
            return (o, args) =>
            {
                var wpfSeries = o as Series;
                if (wpfSeries == null) return;
                wpfSeries.Model.Chart.Update(animate);
            };
        }

        private static PropertyChangedCallback OnPropertyChanged(Action<Series, ISeriesModel> map, bool animate = false)
        {
            return (o, args) =>
            {
                var wpfSeries = o as Series;
                if (wpfSeries == null) return;

                map(wpfSeries, wpfSeries.Model);

                wpfSeries.Model.Chart.Update(animate);
            };
        }
        #endregion
    }
}
