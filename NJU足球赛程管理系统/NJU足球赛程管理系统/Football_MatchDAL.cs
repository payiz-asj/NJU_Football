using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

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

        public static object ToDBValue(DateTime? value)
        {
            //这是处理NULL的值插入时出错的情况
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                DateTime d = (DateTime)value;//这里日期是非null，但是我只想要日期，不要时间
                return d.Date;
            }
                
        }

        //插入一条数据,因为列很多，所以形参为一个FootballMatch类
        public void Insert(FootballMatch one_match)
        {
            SqlHelper.ExcuteNonQuery(@"insert into T_match(match_type,match_order,match_day,match_time,match_ground,team_one,team_two) 
                    values(@match_type,@match_order,@match_day,@match_time,@match_ground,@team_one,@team_two)",
                    new SqlParameter("@match_type", one_match.match_type),
                    new SqlParameter("@match_order", one_match.match_order),
                    new SqlParameter("@match_day", ToDBValue(one_match.match_day)),
                    new SqlParameter("@match_time", one_match.match_time),
                    new SqlParameter("@match_ground", one_match.match_ground),
                    new SqlParameter("@team_one", one_match.team_one),
                    new SqlParameter("@team_two", one_match.team_two)
                    );
        }

        //修改更新
        public void Update(FootballMatch one_match)
        {
            SqlHelper.ExcuteNonQuery(@"UPDATE T_match
                    SET match_type = @match_type,
                    match_order = @match_order,
                    match_day = @match_day,
                    match_time = @match_time,
                    match_ground = @match_ground,
                    team_one = @team_one,
                    team_two =@team_two 
                    where ID=@Id",
                    new SqlParameter("@match_type",one_match.match_type),
                    new SqlParameter("@match_order", one_match.match_order),
                    new SqlParameter("@match_day", ToDBValue(one_match.match_day)),
                    new SqlParameter("@match_time", one_match.match_time),
                    new SqlParameter("@match_ground", one_match.match_ground),
                    new SqlParameter("@team_one", one_match.team_one),
                    new SqlParameter("@team_two", one_match.team_two),
                    new SqlParameter("@Id", one_match.ID)
                    );
        }
        //将返回结果复制到类
        private static FootballMatch To_Football(DataRow row)
        {
            FootballMatch one_match = new FootballMatch();
            one_match.ID = (long)row["ID"];
            one_match.match_type = (string)row["match_type"];
            one_match.match_order = (string)row["match_order"];
            one_match.match_time = (string)row["match_time"];
            one_match.match_ground = (string)row["match_ground"];
            one_match.team_one = (string)row["team_one"];
            one_match.team_two = (string)row["team_two"];
            if (row["match_day"] == DBNull.Value)
                one_match.match_day = null;
            else
            {
                DateTime d = (DateTime)row["match_day"];//这里日期是非null，但是我只想要日期，不要时间
                one_match.match_day = d.Date;
            }
            return one_match;
        }


        //查找
        public FootballMatch Get_ByID(long id)
        {
            DataTable table= SqlHelper.ExecuteDataTable("select * from T_match where ID=@Id",
                new SqlParameter("@Id", id));
            if(table.Rows.Count<=0)
            {
                return null;//没找到
            }
            else if(table.Rows.Count>1)
            {
                throw new Exception("ID重复！请检查数据库是否损坏！");
            }
            else
            {
                DataRow row = table.Rows[0];
                return To_Football(row);
            }
        }
        //获取所有数据
        public FootballMatch[] GetAll()
        {
            DataTable table = SqlHelper.ExecuteDataTable("select * from T_match");
            FootballMatch[] all = new FootballMatch[table.Rows.Count];
            for(int i=0;i<table.Rows.Count;++i)                           
            {
                DataRow row = table.Rows[i];
                all[i]=To_Football(row);
            }
            return all;
        }
        //获取与实参非空属性匹配的数据
        public FootballMatch[] Get_ByNoEmpty(FootballMatch one_match)
        {            
            //这个函数能够从数据库多条件查询，这些条件来自形参中不为空的项
            //下面两个数组记录查询的条件列表
            string[] conditions = new string[99];
            SqlParameter[] param_list = new SqlParameter[99];
            int count = 0;            
            bool is_where = false;
            //查询语句包含match_type
            if (one_match.match_type != "")
            {
                is_where = true;
                conditions[count]="match_type = @match_type";
                param_list[count] = new SqlParameter("@match_type", one_match.match_type);
                count++;
            }
            if(one_match.match_order!="")
            {
                is_where = true;
                conditions[count] = "match_order = @match_order";
                param_list[count] = new SqlParameter("@match_order", one_match.match_order);
                count++;
            }
            if (one_match.match_order != "")
            {
                is_where = true;
                conditions[count] = "match_order = @match_order";
                param_list[count] = new SqlParameter("@match_order", one_match.match_order);
                count++;
            }

            if (one_match.match_day != null)
            {
                is_where = true;
                conditions[count] = "match_day = @match_day";
                param_list[count] = new SqlParameter("@match_day", ToDBValue(one_match.match_day));
                count++;
            }
            if (one_match.match_ground != "")
            {
                is_where = true;
                conditions[count] = "match_ground = @match_ground";
                param_list[count] = new SqlParameter("@match_ground", one_match.match_ground);
                count++;
            }
            if (one_match.team_one != "")
            {
                is_where = true;
                conditions[count] = "team_one = @team_one";
                param_list[count] = new SqlParameter("@team_one", one_match.team_one);
                count++;
            }
            if (one_match.team_two != "")
            {
                is_where = true;
                conditions[count] = "team_two = @team_two";
                param_list[count] = new SqlParameter("@team_two", one_match.team_two);
                count++;
            }


            //开始创建数据库查询语句
            string sql = "";
            if (is_where)
            {
                sql = "select * from T_match where ";
                for (int i=0;i<count;++i)
                    sql += i == 0 ? conditions[i] : " and " + conditions[i];                
            }
            else
                sql = "select * from T_match ";
           
            DataTable table = SqlHelper.ExecuteDataTable_2(sql,param_list,count);
            FootballMatch[] all = new FootballMatch[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; ++i)
            {
                DataRow row = table.Rows[i];
                all[i] = To_Football(row);
            }
            return all;
        }


    }
}
