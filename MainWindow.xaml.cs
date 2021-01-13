using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;

namespace ReadingRulerSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// タスクトレイのアイコン
        /// </summary>
        private System.Windows.Forms.NotifyIcon _notifyIcon;

        #region win32api関連

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(
            IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();
        /// <summary>
        /// WPFとWin32の相互運用ヘルパー
        /// </summary>
        private System.Windows.Interop.WindowInteropHelper windowInteropHelper; 
        
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //Windowハンドラ用のヘルパーを生成
            windowInteropHelper = new System.Windows.Interop.WindowInteropHelper(this);

            //タスクトレイアイコンを初期化する
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Text = "リーディングルーラーサンプル";
            _notifyIcon.Icon = new System.Drawing.Icon("app.ico");
            //タスクトレイに表示する
            _notifyIcon.Visible = true;

            //アイコンにコンテキストメニュー「終了」を追加する
            ContextMenuStrip menuStrip = new ContextMenuStrip();
            ToolStripMenuItem exitItem = new ToolStripMenuItem();
            exitItem.Text = "終了";
            menuStrip.Items.Add(exitItem);
            exitItem.Click += new EventHandler(exitItem_Click);
            
            _notifyIcon.ContextMenuStrip = menuStrip;

            //タスクトレイアイコンのクリックイベントハンドラを登録する
            _notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(_notifyIcon_MouseClick);

            //見た目を整える
            this.Opacity = 0.7;
            var brush = new SolidColorBrush(Color.FromRgb(0xFF,0xFF,0x88));
            this.Background = brush;
            //マウスイベントの定義
            this.MouseDown += new System.Windows.Input.MouseButtonEventHandler(Window_MouseDown);
        }

        /// <summary>
        /// MouseDownイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender,
             System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //マウスのキャプチャを解除
                ReleaseCapture();
                //タイトルバーでマウスの左ボタンが押されたことにする
                SendMessage(windowInteropHelper.Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, IntPtr.Zero);
            }
        }

        /// <summary>
        /// 終了メニューのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitItem_Click(object sender, EventArgs e)
        {
            try
            {
                _notifyIcon.Dispose();
                System.Windows.Application.Current.Shutdown();
            }
            catch { }
        }

        /// <summary>
        /// タスクトレイアイコンのクリックのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    //ウィンドウを可視化
                    Visibility = System.Windows.Visibility.Visible;
                    WindowState = System.Windows.WindowState.Normal;
                }
            }
            catch { }
        }
    }
}
