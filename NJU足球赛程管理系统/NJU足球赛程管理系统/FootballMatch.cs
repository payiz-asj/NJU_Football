using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NJU足球赛程管理系统
{
    class FootballMatch:INotifyPropertyChanged
    {
        //对于可空列，要注意int?的问题
        public long id { get; set; }
        public string match_type { get; set; }
        public string match_order { get; set; }
        public DateTime? match_day { get; set; }
        public string match_time { get; set; }
        public string match_ground { get; set; }
        public string team_one { get; set; }
        public string team_two { get; set; }




        //public string match_type
        //{
        //    get;
        //    set;
        //}
        //private int count;
        //public int Count
        //{
        //    get{ return count; }
        //    set
        //    {
        //        this.count = value;
        //        if(PropertyChanged !=null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs("Count"));
        //        }
        //    }
        //}
        //private double height;
        //public double Height
        //{
        //    get { return height; }
        //    set
        //    {
        //        this.height = value;
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs("Height"));
        //        }
        //    }
        //}
        public event PropertyChangedEventHandler PropertyChanged;
    }


}
