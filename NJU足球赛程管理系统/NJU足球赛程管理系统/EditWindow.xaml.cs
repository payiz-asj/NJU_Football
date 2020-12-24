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
        //命令的类型:  1.查询select   2. 新增insert  3：修改update
        public int cmd_type { get; set; }
        //是否修改
        public long EditingID { get; set; }
        //查询到的数据
        //public list_of_FootballMatch all_found = new list_of_FootballMatch();    //这里出问题了，只能退而求次用下面的方法
        //具体原因是无法将一个FootballMatch类的数组设置为Public属性，或者用public函数返回
        //要查询的几个字段，用来在外面ListWindow里再调用数据库查询！！！（我真的没其他办法了）
        public string[] temp_strings = new string[6];
        public DateTime? temp_date = new DateTime();

        public EditWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(cmd_type==1)
            {
                //查询，不用显示
            }
            else if(cmd_type==2)
            {
                //新增，不用显示
            }
            else
            {
                //修改，需要显示
                FootballMatch one_match = new Football_MatchDAL().Get_ByID(EditingID);
                txt_type.Text = one_match.match_type;
                txt_order.Text = one_match.match_order;
                if (one_match.match_day == null)
                {
                    date_date.SelectedDate = null;
                }
                else
                    date_date.SelectedDate = (DateTime)one_match.match_day;

                txt_time.Text = one_match.match_time;
                txt_ground.Text = one_match.match_ground;
                txt_team_one.Text = one_match.team_one;
                txt_team_two.Text = one_match.team_two;
            }
        }

        private void Button_Click_save(object sender, RoutedEventArgs e)
        {
            if(cmd_type == 1)
            {
                //FootballMatch one_match = new FootballMatch();
                //one_match.match_type = txt_type.Text;
                //one_match.match_order = txt_order.Text;
                //one_match.match_day = date_date.SelectedDate;
                //one_match.match_time = txt_time.Text;
                //one_match.match_ground = txt_ground.Text;
                //one_match.team_one = txt_team_one.Text;
                //one_match.team_two = txt_team_two.Text;
                //all_found.AddRange(new Football_MatchDAL().Get_ByNoEmpty(one_match));
                temp_strings[0] = txt_type.Text;
                temp_strings[1] = txt_order.Text;
                temp_strings[2] = txt_time.Text;
                temp_strings[3] = txt_ground.Text;
                temp_strings[4] = txt_team_one.Text;
                temp_strings[5] = txt_team_two.Text;
                temp_date= date_date.SelectedDate;
                //真正的查询将在外面的ListWindow中实现
            }
            else if(cmd_type==2)
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
