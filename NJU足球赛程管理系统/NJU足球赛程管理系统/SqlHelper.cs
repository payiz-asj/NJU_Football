using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NJU足球赛程管理系统
{
    class SqlHelper
    {
        //这里是数据库操作助手类，封装了数据库的链接、打开，执行命令等操作

        //1. 编写数据库连接串
        //string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;User ID=payiz;Password=payiz"; //数据库用户名和密码登录
        //string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;Integrated Security=SSPI"; //windows本地身份登录

        //这里是更高级的用法，将数据库连接串放到配置文件App.config中，这样更加方便
        private static string connStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;

        //2. 数据库操作
        public static int ExcuteNonQuery(string sql)
        {
            //创建SqlConnection的实例，using 关键字是用来自动释放资源
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //打开数据库连接
                conn.Open();
                //执行数据库命令
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static object ExecuteScalar(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    return cmd.ExecuteScalar();
                }
            }
        }



    }
}
