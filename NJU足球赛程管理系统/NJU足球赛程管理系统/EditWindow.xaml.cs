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

namespace NJU足球赛程管理系统
{
    /// <summary>
    /// EditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditWindow : Window
    {
        //是:新增,否：修改
        public bool Isinsert { get; set; }
        //是否修改
        public long EditingID { get; set; }

        public EditWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Isinsert)
            {

            }
            else
            {
                FootballMatch one_match = new Football_MatchDAL().Get_ByID(EditingID);
                txt_type.Text = one_match.match_type;
                txt_order.Text = one_match.match_order;
                date_date.SelectedDate = (DateTime)one_match.match_day;
                txt_time.Text = one_match.match_time;
                txt_ground.Text = one_match.match_ground;
                txt_team_one.Text = one_match.team_one;
                txt_team_two.Text = one_match.team_two;
            }
        }

        private void Button_Click_save(object sender, RoutedEventArgs e)
        {
            if(Isinsert)
            {
                FootballMatch one_match = new FootballMatch();
                one_match.match_type = txt_type.Text;
                one_match.match_order = txt_order.Text;
                one_match.match_day = date_date.SelectedDate;
                one_match.match_time = txt_time.Text;
                one_match.match_ground = txt_ground.Text;
                one_match.team_one= txt_team_one.Text;
                one_match.team_two = txt_team_two.Text;
                new Football_MatchDAL().Insert(one_match);
            }
            else
            {
                FootballMatch one_match = new Football_MatchDAL().Get_ByID(EditingID);
                one_match.match_type = txt_type.Text;
                one_match.match_order = txt_order.Text;
                one_match.match_day = date_date.SelectedDate;
                one_match.match_time = txt_time.Text;
                one_match.match_ground = txt_ground.Text;
                one_match.team_one = txt_team_one.Text;
                one_match.team_two = txt_team_two.Text;
                new Football_MatchDAL().Update(one_match);
            }
            DialogResult = true;
        }

        private void Button_Click_cansel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
