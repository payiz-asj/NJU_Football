using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NJU足球赛程管理系统
{
    class Football_MatchDAL
    {
        //数据访问类，DAL(Data Access Layer)
        //这里封装了针对NJU_FOOTBALL数据库中T_match表，常用的操作
        //获取数据总条数
        public static Int32 Get_Count()
        {
            return (Int32)SqlHelper.ExecuteScalar("select count(*) from T_match",new SqlParameter[0]);
        }

        //删除指定id的数据
        public static void DeleteById(long id)
        {
            SqlHelper.ExcuteNonQuery("delete from T_match where ID=@Id",
                new SqlParameter("@Id", id));
        }

        //插入一条数据,因为列很多，所以直接插入一个类
        public static void Insert(FootballMatch one_match)
        {
            SqlHelper.ExcuteNonQuery(@"insert into(match_type,match_order,match_day,match_time,match_ground,team_one,team_two) 
                    values(@match_type,@match_order,@match_day,@match_time,@match_ground,@team_one,@team_two)",
                    new SqlParameter("@match_type", one_match.match_type),
                    new SqlParameter("@match_order", one_match.match_order),
                    new SqlParameter("@match_day", one_match.match_day),
                    new SqlParameter("@match_time", one_match.match_time),
                    new SqlParameter("@match_ground", one_match.match_ground),
                    new SqlParameter("@team_one", one_match.team_one),
                    new SqlParameter("@team_two", one_match.team_two)
                    );



        }



    }
}
