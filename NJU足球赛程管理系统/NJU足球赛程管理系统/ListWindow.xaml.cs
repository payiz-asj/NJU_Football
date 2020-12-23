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
            Football_MatchDAL dal = new Football_MatchDAL();
            grid_items.ItemsSource = dal.GetAll();


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EditWindow ed = new EditWindow();
            ed.Isinsert = true;
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
            ed.Isinsert = false;
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //FootballMatch chosen_one = (FootballMatch)grid_items.SelectedItem;
            //if (chosen_one == null)
            //{
            //    MessageBox.Show("请选择要删除的一行", "提醒", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}
            //if (MessageBox.Show("确认删除这条数据吗？", "提醒", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            //{
            //    Football_MatchDAL.DeleteById(chosen_one.ID);
            //    //更新列表
            //    Football_MatchDAL dal = new Football_MatchDAL();
            //    grid_items.ItemsSource = dal.GetAll();
            //}
        }
        


    }
}
