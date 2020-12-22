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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;



namespace NJU足球赛程管理系统
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //这里是测试连接按钮点击事件
            //编写数据库连接串
            //string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;User ID=payiz;Password=payiz"; //数据库用户名和密码登录
            string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;Integrated Security=SSPI"; //windows本地身份登录

            //创建SqlConnection的实例
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    //打开数据库连接
                    conn.Open();
                    MessageBox.Show("数据库连接成功！开始探索吧！","连接状态通报", MessageBoxButton.OK,MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接失败！\n程序将无法提供正常服务，请谅解！\n\n您也可以按以下信息排查错误：\n" + ex.Message, "连接状态通报", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //这里是增加按钮点击事件
            //跳转到其他页面
            AddWindow aw = new AddWindow();
            this.Hide();
            aw.ShowDialog();
            this.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //这里是查询按钮点击事件
            //跳转到其他页面
            SearchWindow sw = new SearchWindow();            
            this.Hide();
            sw.ShowDialog();
            this.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //这里是修改按钮点击事件
            //跳转到其他页面
            ModifyWindow mw = new ModifyWindow();
            this.Hide();
            mw.ShowDialog();
            this.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //这里是删除按钮点击事件
            //跳转到其他页面
            DeleteWindow dw = new DeleteWindow();
            this.Hide();
            dw.ShowDialog();
            this.Show();
        }
    }
}
