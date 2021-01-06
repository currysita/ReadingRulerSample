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

namespace ReadingRulerSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// タスクトレイの
        /// </summary>
        private System.Windows.Forms.NotifyIcon _notifyIcon;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //ここに画面初期化処理を組み込みます

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
        /// 
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
