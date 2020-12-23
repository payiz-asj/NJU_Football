using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace NJU足球赛程管理系统
{
    /// <summary>
    /// SearchWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ListWindow : Window
    {
        FootballMatch match1 = new FootballMatch();

       

        public ListWindow()
        {   
            //这是窗口初始化函数，不能删哦
            InitializeComponent();             
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //窗口加载函数

        }
    }
}
