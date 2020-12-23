using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NJU足球赛程管理系统
{
    /* 第三版本，后面还有两个版本，被我淘汰了，最终采用了这个！
       通过参数化实现预防Sql注入攻击
       与第二版本的区别是：函数最后一个参数改变成长度可变参数（C#语言特性）来简化
       具体地：形参设置为params+一个数组，则会将实参中多余的项打包成一个数组来传参
     */
    class SqlHelper
    {
        //这里是数据库操作助手类，封装了数据库的链接、打开，执行命令等操作

        //1. 编写数据库连接串
        //string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;User ID=payiz;Password=payiz"; //数据库用户名和密码登录
        //string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;Integrated Security=SSPI"; //windows本地身份登录

        //这里是更高级的用法，将数据库连接串放到配置文件App.config中，这样更加方便
        private static string connStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;

        //2. 数据库操作
        //方法1，执行对数据表的增加、删除、修改操作
        public static int ExcuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            //创建SqlConnection的实例，using 关键字是用来自动释放资源
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //打开数据库连接
                conn.Open();
                //创建并执行数据库命令
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    //foreach (SqlParameter param in parameters)
                    //{
                    //    cmd.Parameters.Add(param);
                    //}

                    //更高级
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        //方法2，返回查询结果中第 1 行第 1 列的值
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    //更高级
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        //上面函数的升级版：因为平常查询结果只返回一个表
        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    //更高级
                    cmd.Parameters.AddRange(parameters);
                    //SqlDataAdapter是一个帮我们将SqlCommand查询到的结果填充到DataSet中的类
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //DataSet是一个本地非常复杂的集合/容器（类似于List）
                    DataSet dataset = new DataSet();
                    //执行填充
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }
    }


    //class SqlHelper
    //{
    //    //这里是数据库操作助手类，封装了数据库的链接、打开，执行命令等操作

    //    //1. 编写数据库连接串
    //    //string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;User ID=payiz;Password=payiz"; //数据库用户名和密码登录
    //    //string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;Integrated Security=SSPI"; //windows本地身份登录

    //    //这里是更高级的用法，将数据库连接串放到配置文件App.config中，这样更加方便
    //    private static string connStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;

    //    //2. 数据库操作
    //    //方法1，执行对数据表的增加、删除、修改操作
    //    public static int ExcuteNonQuery(string sql)
    //    {
    //        //创建SqlConnection的实例，using 关键字是用来自动释放资源
    //        using (SqlConnection conn = new SqlConnection(connStr))
    //        {
    //            //打开数据库连接
    //            conn.Open();
    //            //创建并执行数据库命令
    //            using (SqlCommand cmd = new SqlCommand())
    //            {
    //                cmd.Connection = conn;
    //                cmd.CommandText = sql;
    //                return cmd.ExecuteNonQuery();
    //            }
    //        }
    //    }
    //    //方法2，返回查询结果中第 1 行第 1 列的值
    //    public static object ExecuteScalar(string sql)
    //    {
    //        using (SqlConnection conn = new SqlConnection(connStr))
    //        {
    //            conn.Open();
    //            using (SqlCommand cmd = new SqlCommand())
    //            {
    //                cmd.Connection = conn;
    //                cmd.CommandText = sql;
    //                return cmd.ExecuteScalar();


    //            }
    //        }
    //    }
    //    //适用于数据查询结果比较少的情况
    //    //public static DataSet ExecuteDataSet(string sql)
    //    //{
    //    //    using (SqlConnection conn = new SqlConnection(connStr))
    //    //    {
    //    //        conn.Open();
    //    //        using (SqlCommand cmd = new SqlCommand())
    //    //        {
    //    //            cmd.Connection = conn;
    //    //            cmd.CommandText = sql;
    //    //            //SqlDataAdapter是一个帮我们将SqlCommand查询到的结果填充到DataSet中的类
    //    //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
    //    //            //DataSet是一个本地非常复杂的集合/容器（类似于List）
    //    //            DataSet dataset = new DataSet();
    //    //            //执行填充
    //    //            adapter.Fill(dataset);
    //    //            return dataset;
    //    //            ////调用实例举例：
    //    //            //DataSet ds = SqlHelper.ExecuteDataSet(select * from xxx);
    //    //            //foreach(row )

    //    //        }
    //    //    }
    //    //}

    //    //上面函数的升级版：因为平常查询结果只返回一个表
    //    public static DataTable ExecuteDataTable(string sql)
    //    {
    //        using (SqlConnection conn = new SqlConnection(connStr))
    //        {
    //            conn.Open();
    //            using (SqlCommand cmd = new SqlCommand())
    //            {
    //                cmd.Connection = conn;
    //                cmd.CommandText = sql;
    //                //SqlDataAdapter是一个帮我们将SqlCommand查询到的结果填充到DataSet中的类
    //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
    //                //DataSet是一个本地非常复杂的集合/容器（类似于List）
    //                DataSet dataset = new DataSet();
    //                //执行填充
    //                adapter.Fill(dataset);
    //                return dataset.Tables[0];
    //                ////调用实例举例：
    //                //DataSet ds = SqlHelper.ExecuteDataSet(select * from xxx);
    //                //foreach(row )

    //            }
    //        }
    //    }
    //}


    // /* 第二版本，通过参数化实现预防Sql注入攻击*/
    //class SqlHelper
    //{
    //    //这里是数据库操作助手类，封装了数据库的链接、打开，执行命令等操作

    //    //1. 编写数据库连接串
    //    //string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;User ID=payiz;Password=payiz"; //数据库用户名和密码登录
    //    //string connStr = "Data source=LAPTOP-AGT30UAM\\SQLEXPRESS;Initial Catalog=NJU_FOOTBALL;Integrated Security=SSPI"; //windows本地身份登录

    //    //这里是更高级的用法，将数据库连接串放到配置文件App.config中，这样更加方便
    //    private static string connStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;

    //    //2. 数据库操作
    //    //方法1，执行对数据表的增加、删除、修改操作
    //    public static int ExcuteNonQuery(string sql,SqlParameter[] parameters)
    //    {
    //        //创建SqlConnection的实例，using 关键字是用来自动释放资源
    //        using (SqlConnection conn = new SqlConnection(connStr))
    //        {
    //            //打开数据库连接
    //            conn.Open();
    //            //创建并执行数据库命令
    //            using (SqlCommand cmd = new SqlCommand())
    //            {
    //                cmd.Connection = conn;
    //                cmd.CommandText = sql;
    //                //foreach (SqlParameter param in parameters)
    //                //{
    //                //    cmd.Parameters.Add(param);
    //                //}

    //                //更高级
    //                cmd.Parameters.AddRange(parameters);
    //                return cmd.ExecuteNonQuery();
    //            }
    //        }
    //    }
    //    //方法2，返回查询结果中第 1 行第 1 列的值
    //    public static object ExecuteScalar(string sql,SqlParameter[] parameters)
    //    {
    //        using (SqlConnection conn = new SqlConnection(connStr))
    //        {
    //            conn.Open();
    //            using (SqlCommand cmd = new SqlCommand())
    //            {
    //                cmd.Connection = conn;
    //                cmd.CommandText = sql;
    //                //更高级
    //                cmd.Parameters.AddRange(parameters);
    //                return cmd.ExecuteScalar();


    //            }
    //        }
    //    }    

    //    //上面函数的升级版：因为平常查询结果只返回一个表
    //    public static DataTable ExecuteDataTable(string sql, SqlParameter[] parameters)
    //    {
    //        using (SqlConnection conn = new SqlConnection(connStr))
    //        {
    //            conn.Open();
    //            using (SqlCommand cmd = new SqlCommand())
    //            {
    //                cmd.Connection = conn;
    //                cmd.CommandText = sql;
    //                //更高级
    //                cmd.Parameters.AddRange(parameters);
    //                //SqlDataAdapter是一个帮我们将SqlCommand查询到的结果填充到DataSet中的类
    //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
    //                //DataSet是一个本地非常复杂的集合/容器（类似于List）
    //                DataSet dataset = new DataSet();
    //                //执行填充
    //                adapter.Fill(dataset);
    //                return dataset.Tables[0];
    //                ////调用实例举例：
    //                //DataSet ds = SqlHelper.ExecuteDataSet(select * from xxx);
    //                //foreach(row )

    //            }
    //        }
    //    }
    //}


}
