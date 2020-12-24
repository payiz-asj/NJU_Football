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
        //公开属性,用来标识数据库能否正常连接
        public bool is_connected_to_sql_server { get; set; }

        public ListWindow()
        {   
            //这是窗口初始化函数，不能删哦
            InitializeComponent();             
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //窗口加载函数
            if (is_connected_to_sql_server)
            {
                Football_MatchDAL dal = new Football_MatchDAL();
                grid_items.ItemsSource = dal.GetAll();
            }
            else
            {
                MessageBox.Show("再次提醒一下\n数据库连接失败状态下是无法正常使用本程序，\n温馨提示：您可以在本程序App.config文件里修改数据库连接字段，\n祝您好运！" , "连接状态通报", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            EditWindow ed = new EditWindow();
            ed.cmd_type = 1;
            if (ed.ShowDialog() == true)
            {
                //实现查询并更新列表
                FootballMatch one_match = new FootballMatch();
                one_match.match_type = ed.temp_strings[0];
                one_match.match_order = ed.temp_strings[1];
                one_match.match_day = ed.temp_date;
                one_match.match_time = ed.temp_strings[2];
                one_match.match_ground = ed.temp_strings[3];
                one_match.team_one = ed.temp_strings[4];
                one_match.team_two = ed.temp_strings[5];
                //这里才是查询真正被调用的地方，调用了多条件查询
                grid_items.ItemsSource=new Football_MatchDAL().Get_ByNoEmpty(one_match);
               
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EditWindow ed = new EditWindow();
            ed.cmd_type = 2;
            if (ed.ShowDialog() == true)
            {
                //更新列表
                Football_MatchDAL dal = new Football_MatchDAL();
                grid_items.ItemsSource = dal.GetAll();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            FootballMatch chosen_one = (FootballMatch)grid_items.SelectedItem;           
            if (chosen_one == null)
            {
                MessageBox.Show("请选择要编辑的一行","提醒", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            EditWindow ed = new EditWindow();
            ed.cmd_type = 3;
            ed.EditingID = chosen_one.ID;
            if (ed.ShowDialog() == true)
            {
                //更新列表
                Football_MatchDAL dal = new Football_MatchDAL();
                grid_items.ItemsSource = dal.GetAll();
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            FootballMatch chosen_one = (FootballMatch)grid_items.SelectedItem;
            if(chosen_one==null)
            {
                MessageBox.Show("请选择要删除的一行","提醒",MessageBoxButton.OK,MessageBoxImage.Information);
                return;
            }         
            if (MessageBox.Show("确认删除这条数据吗？","提醒",MessageBoxButton.YesNo, MessageBoxImage.Information) ==MessageBoxResult.Yes)
            {
                Football_MatchDAL.DeleteById(chosen_one.ID);
                //更新列表
                Football_MatchDAL dal = new Football_MatchDAL();
                grid_items.ItemsSource = dal.GetAll();
            }
        } 

    }
}
