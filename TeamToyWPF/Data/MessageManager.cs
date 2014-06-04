using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TeamToyWPF.Data
{
    class MessageManager
    {
        public void showError(string content)
        {
            MessageBox.Show(content, "错误", MessageBoxButton.OK);
        }

        public void showMessage(string content, string title)
        {
            MessageBox.Show(content, title, MessageBoxButton.OK);
        }

    }
}
