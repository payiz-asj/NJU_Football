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
    public partial class SearchWindow : Window
    {
        FootballMatch match1 = new FootballMatch();

        void connect_sql_server()
        {
            //这里是测试连接按钮点击事件
            //编写数据库连接串
            //string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;User ID=payiz;Password=payiz"; //数据库用户名和密码登录
            string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;Integrated Security=SSPI"; //windows本地身份登录

            //创建SqlConnection的实例
            try
            {
                //using 关键字是用来自动释放资源
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    //打开数据库连接
                    conn.Open();
                    //MessageBox.Show("数据库连接成功！开始探索吧！", "连接状态通报", MessageBoxButton.OK, MessageBoxImage.Information);  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接失败！\n程序将无法提供正常服务，请谅解！\n\n您也可以按以下信息排查错误：\n" + ex.Message, "连接状态通报", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public SearchWindow()
        {   
            //这是窗口初始化函数，不能删哦
            InitializeComponent();             
        }
        
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("hello");
            connect_sql_server();
            label1.Content = "hello world!";
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            match1.Height += 1;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            match1.match_type = "南超";
            textbox_01.DataContext = match1;
            match1.Count = 2;
            label1.DataContext = match1;
            textbox1.DataContext = match1;
            match1.Height = 178.78;

        }
    }
}
