﻿using System;
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
using System.Configuration;

namespace NJU足球赛程管理系统
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //公开属性,用来标识数据库能否正常连接
        public bool is_connected_to_sql_server { get; set; }
        public MainWindow()
        {
            InitializeComponent();  
            
        }

        private void test_connection(bool is_Message_box=false) 
        {
            //目的是检测数据库连接是否正常,参数is_Message_box选择是否通过消息框通知成功状态，默认为不通知（注意：出现错误时无论如何都会通知）

            //1. 编写数据库连接串
            //string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;User ID=payiz;Password=payiz"; //数据库用户名和密码登录
            //string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;Integrated Security=SSPI"; //windows本地身份登录

            //这里是更高级的用法，将数据库连接串放到配置文件App.config中，这样更加方便
            string connStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;

            //2. 检测数据库连接是否正常
            try
            {
                //创建SqlConnection的实例，using 关键字是用来自动释放资源
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    //打开数据库连接
                    conn.Open();
                    is_connected_to_sql_server = true;
                    if (is_Message_box)
                        MessageBox.Show("数据库连接成功！开始探索吧！", "连接状态通报", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                is_connected_to_sql_server = false;
                MessageBox.Show("数据库连接失败！\n程序将无法提供正常服务，请谅解！\n\n您也可以按以下信息排查错误：\n" + ex.Message, "连接状态通报", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        private void Button_Click_connect_test(object sender, RoutedEventArgs e)
        {
            //这个按钮的作用是测试数据库连接是否成功，并用消息框通知状态
            test_connection(true);//这里是用户主动测试数据库连接
        }
        private void Button_Click_enter(object sender, RoutedEventArgs e)
        {
            //这里是查询按钮点击事件
            //跳转到其他页面
            ListWindow sw = new ListWindow();
            test_connection(false);//这里是程序自动测试数据库连接
            sw.is_connected_to_sql_server = this.is_connected_to_sql_server;
            this.Hide();
            sw.ShowDialog();
            this.Show();
        }
    }
}
