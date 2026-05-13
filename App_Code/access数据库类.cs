using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class access数据库类
{
    static object obj = new object();
    //static string strpath= @"C:\互感器网站\App_Data\huganqi.accdb;";
    static string strpath= @"C:\huganqi\App_Data\huganqi.accdb;";
   public access数据库类()
	{ 	}
    public static DataTable 返回DataTable(string sql指令 )
    {
        try
        {
            lock (obj)
            {
                string connStr = @"Provider= Microsoft.ACE.OLEDB.12.0;Jet OLEDB:DataBase Password=888139; Data Source=" + strpath;
                OleDbConnection conn = new OleDbConnection(); //创建OleDb连接对象 
                conn.ConnectionString = connStr;// oleString.ToString(); //将生成的字符串传入 
                conn.Open(); //打开数据库 
                OleDbCommand mycmd = new OleDbCommand(); //创建sql命令对象 
                mycmd.Connection = conn; //设置连接 
                mycmd.CommandText = sql指令;// "select * from 用户表 where 用户注册名称='n0120200101'";// "Insert into Users(用户名,密码,家庭地址) values(@name,@pwd,@address)"; //并且用sql参数形式插入数据 

                OleDbDataReader dr = mycmd.ExecuteReader();
                DataTable dt = new DataTable();
                if (dr.HasRows)
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        dt.Columns.Add(dr.GetName(i));
                    }
                    dt.Rows.Clear();
                }
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        row[i] = dr[i];
                    }
                    dt.Rows.Add(row);
                }
                conn.Close(); //最后不要忘了关数据库 
                mycmd.Dispose();
                return dt;
            }        
        }
        catch { return null; }
            
    }
    public static int 执行SQL指令(string sql指令)//返回影响的行数
    { 
        lock(obj)
        {
            string connStr = @"Provider= Microsoft.ACE.OLEDB.12.0;Jet OLEDB:DataBase Password=888139; Data Source=" + strpath;

            //string connStr = @"Provider= Microsoft.ACE.OLEDB.12.0;Jet OLEDB:DataBase Password=888139; Data Source=" + System.Environment.CurrentDirectory + "\\ctbxt.accdb;";
            OleDbConnection conn = new OleDbConnection(); //创建OleDb连接对象 
            conn.ConnectionString = connStr;
            conn.Open(); //打开数据库 
            OleDbCommand mycmd = new OleDbCommand(); //创建sql命令对象 
            mycmd.Connection = conn; //设置连接 
            mycmd.CommandText = sql指令;         
            int num = mycmd.ExecuteNonQuery(); //执行插入语句 
            conn.Close(); //最后不要忘了关数据库 
            mycmd.Dispose();
            return num;
        }

    }
}
