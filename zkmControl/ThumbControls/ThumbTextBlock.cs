using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace zkmControl.ThumbControls
{
    public class ThumbTextBlock : Thumb
    {
        private const string BASE_CANVAS_NAME = "canvas";

        public Canvas BaseCanvas { get; set; } = new Canvas();
        public TextBlock TextBlock { get; set; } = new TextBlock();
        public Border Border { get; set; } = new Border();

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ThumbTextBlock()
        {
            ControlTemplate templete = new(typeof(Thumb));
            templete.VisualTree = new System.Windows.FrameworkElementFactory(typeof(Canvas), BASE_CANVAS_NAME);
            this.Template = templete;

            // テンプレートの再構築
            this.ApplyTemplate();

            // Canvas
            this.BaseCanvas = (Canvas)this.Template.FindName(BASE_CANVAS_NAME, this);
            this.BaseCanvas.Background = Brushes.White;
        }
        #endregion

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            var border = new Border();
            this.Border.Child = this.TextBlock;
            this.BaseCanvas.Children.Add(this.Border);
        }


        #region Drag and Dropを有効にするフラグ[EnableDrag]依存プロパティ
        /// <summary>
        /// Drag and Dropを有効にするフラグ[EnableDrag]依存プロパティ
        /// </summary>
        [Category("カスタムプロパティ")]
        [Browsable(true)]
        public static readonly DependencyProperty EnableDragProperty =
        DependencyProperty.Register(
        "EnableDrag",     // プロパティ名
        typeof(bool),    // プロパティの型
        typeof(ThumbTextBlock),  // コントロールの型
        new FrameworkPropertyMetadata(   // メタデータ
                    false,
                    new PropertyChangedCallback(EnableDragChanged)));


        /// <summary>
        /// 依存プロパティが変更された際の処理
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private static void EnableDragChanged(
        DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ThumbTextBlock? userControl = obj as ThumbTextBlock;
            if (userControl != null)
            {
                //////// 直接変更したいプロパティの処理を書く
                bool newValue = (bool)e.NewValue;

                userControl.DragStarted -= Thumb_DragStarted;
                userControl.DragCompleted -= Thumb_DragCompleted;
                userControl.DragDelta -= Thumb_DragDelta;

                if (newValue)
                {
                    userControl.DragStarted += Thumb_DragStarted;
                    userControl.DragCompleted += Thumb_DragCompleted;
                    userControl.DragDelta += Thumb_DragDelta;
                }
            }
        }

        /// <summary>
        /// 依存プロパティのラッパー
        /// </summary>
        public bool EnableDrag
        {
            get { return (bool)GetValue(EnableDragProperty); }
            set { SetValue(EnableDragProperty, value); }
        }
        #endregion

        #region 選択状態を示すフラグ(true:選択状態 false:非選択状態)[SelectedF]依存プロパティ
        /// <summary>
        /// 選択状態を示すフラグ(true:選択状態 false:非選択状態)[SelectedF]依存プロパティ
        /// </summary>
        [Category("カスタムプロパティ")]
        [Browsable(true)]
        public static readonly DependencyProperty SelectedFProperty =
        DependencyProperty.Register(
        "SelectedF",     // プロパティ名
        typeof(bool),    // プロパティの型
        typeof(ThumbTextBlock),  // コントロールの型
        new FrameworkPropertyMetadata(   // メタデータ
                    false,
                    new PropertyChangedCallback(SelectedFChanged)));


        /// <summary>
        /// 依存プロパティが変更された際の処理
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private static void SelectedFChanged(
        DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ThumbTextBlock? userControl = obj as ThumbTextBlock;
            if (userControl != null)
            {
                //////// 直接変更したいプロパティの処理を書く
                bool newValue = (bool)e.NewValue;

                if (newValue)
                {
                    userControl.Border.BorderBrush = Brushes.AliceBlue;
                    userControl.Border.BorderThickness = new Thickness(3);
                }
                else
                {
                    userControl.Border.BorderBrush = null;
                    userControl.Border.BorderThickness = new Thickness(0);
                }
            }
        }

        /// <summary>
        /// 依存プロパティのラッパー
        /// </summary>
        public bool SelectedF
        {
            get { return (bool)GetValue(SelectedFProperty); }
            set { SetValue(SelectedFProperty, value); }
        }
        #endregion

        #region X座標位置[Left]依存プロパティ
        /// <summary>
        /// X座標位置[Left]依存プロパティ
        /// </summary>
        [Category("カスタムプロパティ")]
        [Browsable(true)]
        public static readonly DependencyProperty LeftProperty =
        DependencyProperty.Register(
        "Left",     // プロパティ名
        typeof(double),    // プロパティの型
        typeof(ThumbTextBlock),  // コントロールの型
        new FrameworkPropertyMetadata(   // メタデータ
                    (double)0.0, new PropertyChangedCallback(LeftChanged)));


        /// <summary>
        /// 依存プロパティが変更された際の処理
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private static void LeftChanged(
        DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ThumbTextBlock? thumb = obj as ThumbTextBlock;
            if (thumb != null)
            {
                //////// 直接変更したいプロパティの処理を書く
                double newValue = (double)e.NewValue;
                Canvas.SetLeft(thumb, newValue);
            }
        }

        /// <summary>
        /// 依存プロパティのラッパー
        /// </summary>
        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }
        #endregion

        #region Y座標位置[Top]依存プロパティ
        /// <summary>
        /// Y座標位置[Top]依存プロパティ
        /// </summary>
        [Category("カスタムプロパティ")]
        [Browsable(true)]
        public static readonly DependencyProperty TopProperty =
        DependencyProperty.Register(
        "Top",     // プロパティ名
        typeof(double),    // プロパティの型
        typeof(ThumbTextBlock),  // コントロールの型
        new FrameworkPropertyMetadata(   // メタデータ
                    (double)0.0, new PropertyChangedCallback(TopChanged)));

        /// <summary>
        /// 依存プロパティが変更された際の処理
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private static void TopChanged(
        DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ThumbTextBlock? thumb = obj as ThumbTextBlock;
            if (thumb != null)
            {
                //////// 直接変更したいプロパティの処理を書く
                double newValue = (double)e.NewValue;
                Canvas.SetTop(thumb, newValue);
            }
        }

        /// <summary>
        /// 依存プロパティのラッパー
        /// </summary>
        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }
        #endregion

        #region テキスト[Text]依存プロパティ
        /// <summary>
        /// テキスト[Text]依存プロパティ
        /// </summary>
        [Category("カスタムプロパティ")]
        [Browsable(true)]
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
        "Text",     // プロパティ名
        typeof(string),    // プロパティの型
        typeof(ThumbTextBlock),  // コントロールの型
        new FrameworkPropertyMetadata(   // メタデータ
                    "Sample", new PropertyChangedCallback(TextChanged)));

        /// <summary>
        /// 依存プロパティが変更された際の処理
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private static void TextChanged(
        DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ThumbTextBlock? userControl = obj as ThumbTextBlock;
            if (userControl != null)
            {
                //////// 直接変更したいプロパティの処理を書く
                string newValue = (string)e.NewValue;
                userControl.TextBlock.Text = newValue;
            }
        }

        /// <summary>
        /// 依存プロパティのラッパー
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region ドラッグ開始処理
        /// <summary>
        /// ドラッグ開始処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            try
            {
                ThumbTextBlock? thumb = sender as ThumbTextBlock;

                if (thumb != null)
                {
                    thumb.SelectedF = true;
                }
            }
            catch { }
        }
        #endregion

        #region ドラッグ終了処理
        /// <summary>
        /// ドラッグ終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            try
            {
                ThumbTextBlock? thumb = sender as ThumbTextBlock;
                if (thumb != null)
                {
                    thumb.SelectedF = false;
                }
            }
            catch { }
        }
        #endregion

        #region ドラッグ中の処理
        /// <summary>
        /// ドラッグ中の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            try
            {
                ThumbTextBlock? thumb = sender as ThumbTextBlock;

                if (thumb != null)
                {
                    var x = Canvas.GetLeft(thumb) + e.HorizontalChange;
                    var y = Canvas.GetTop(thumb) + e.VerticalChange;

                    var canvas = thumb.Parent as Canvas;

                    if (canvas != null)
                    {
                        x = Math.Max(x, 0);
                        y = Math.Max(y, 0);
                        x = Math.Min(x, canvas.ActualWidth - thumb.ActualWidth);
                        y = Math.Min(y, canvas.ActualHeight - thumb.ActualHeight);
                    }
                    thumb.Left = x;
                    thumb.Top = y;
                }
            }
            catch { }
        }
        #endregion
    }
}
