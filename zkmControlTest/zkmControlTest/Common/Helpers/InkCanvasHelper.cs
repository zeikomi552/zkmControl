using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using zkmControlTest.Common.Enums;

namespace zkmControlTest.Common.Helpers
{
    public static class InkCanvasHelper
    {
        #region StrokeColor
        /// <summary>
        /// ViewModelから制御するための依存関係プロパティ
        /// </summary>
        public static readonly DependencyProperty StrokeColorProperty =
            DependencyProperty.RegisterAttached(
                "StrokeColor",
                typeof(ColorsEnum?),
                typeof(InkCanvasHelper),
                new PropertyMetadata((d, e) =>
                {
                    var inkcanvas = d as InkCanvas;
                    if (inkcanvas != null)
                    {
                        //////// 直接変更したいプロパティの処理を書く
                        ColorsEnum newValue = (ColorsEnum)e.NewValue;

                        var color = System.Drawing.Color.FromName(newValue.ToString());
                        var swmcolor
                            = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);

                        inkcanvas.DefaultDrawingAttributes.Color = swmcolor;
                    }
                }));

        /// <summary>
        /// Xamlから添付プロパティとして設定させるためのメソッド
        /// </summary>
        public static void SetStrokeColor(InkCanvas target, bool? value)
        {
            target.SetValue(StrokeColorProperty, value);
        }
        #endregion

        #region StrokeSize
        /// <summary>
        /// ViewModelから制御するための依存関係プロパティ
        /// </summary>
        public static readonly DependencyProperty StrokeSizeProperty =
            DependencyProperty.RegisterAttached(
                "StrokeSize",
                typeof(double),
                typeof(InkCanvasHelper),
                new PropertyMetadata((d, e) =>
                {
                    var inkcanvas = d as InkCanvas;
                    if (inkcanvas != null)
                    {
                        //////// 直接変更したいプロパティの処理を書く
                        double newValue = (double)e.NewValue;
                        inkcanvas.DefaultDrawingAttributes.Width = newValue;
                        inkcanvas.DefaultDrawingAttributes.Height = newValue;

                    }
                }));

        /// <summary>
        /// Xamlから添付プロパティとして設定させるためのメソッド
        /// </summary>
        public static void SetStrokeSize(InkCanvas target, double value)
        {
            target.SetValue(StrokeSizeProperty, value);
        }
        #endregion
    }
}
