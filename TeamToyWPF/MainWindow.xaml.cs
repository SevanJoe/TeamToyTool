using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TeamToyWPF.Data;

namespace TeamToyWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int mSelectedMonth;
        private string mFilePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void setDisplayMonth(int month)
        {
            mSelectedMonth = month;
            if (mSelectedMonthText != null)
            {
                mSelectedMonthText.Text = mSelectedMonth + "月";
            }
        }

        private void mCalendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            setDisplayMonth(mCalendar.DisplayDate.Month);
        }

        private void mCalendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            if (mCalendar.DisplayMode != CalendarMode.Year)
            {
                mCalendar.DisplayMode = CalendarMode.Year;
            }
        }

        private void mCalendar_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured is CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            setDisplayMonth(DateTime.Now.Month);
            mStatusText.Text = "文件尚未读取";
        }

        private void mButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Excel 文件|*.xls;*.xlsx";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mStatusText.Text = "文件读取成功，文件导出中...";

                mFilePath = openFileDialog.FileName;

                Thread thread = new Thread(exportExcel);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void exportExcel()
        {
            SqlManager sqlManager = new SqlManager();
            if (sqlManager.execute(mSelectedMonth, mFilePath))
            {
                mStatusText.Dispatcher.Invoke(new Action(() =>
                {
                    mStatusText.Text = "导出成功！";
                    new MessageManager().showMessage("导出成功！", "Excel 导出");
                }));
            }
            else
            {
                mStatusText.Dispatcher.Invoke(new Action(() =>
                {
                    mStatusText.Text = "导出失败！";
                    new MessageManager().showError("导出失败！");
                }));
            }
        }

    }
}
