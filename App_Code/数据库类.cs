using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using System.Linq;
using System.Web;
using System.Security;
using System.Security.Cryptography;
using System.Text;
/// <summary>
/// 数据库类 的摘要说明
/// </summary>
public class 数据库类
{
    static string connectString = "Data Source=LAPTOP-890QPIOQ\\SQLEXPRESS;Initial Catalog=zygj;UID=sa;Pwd=1538845922Zyl!";

    //static string connectString = @"Data Source=iZegogfowrq9ktZ\\JIWUHUI;Initial Catalog=xcs; UId=sa;Pwd=1538845922Zyl!";
    static MD5 md5 = MD5.Create(); //实例化一个md5对像
    
    public static string 判断入侵(string ssss)
    {
        string str = ssss.ToUpper();
        if (str.Contains("DELE") || str.Contains("UPDA") || str.Contains("INSE") || str.Contains("SELE"))
            return "0";
        else return "1";
    }
    public static string GetConnectString()
    {
       
            return connectString;
    }
   
	public 数据库类()
	{ 	}
    public static DataTable 返回DataTable(string sql指令)
    {
        if (sql指令.Trim() == "") return null;
        SqlConnection sqlCnt = new SqlConnection(GetConnectString());   
        //强大的SqlDataAdapter 
        System.Data.SqlClient.SqlDataAdapter adapter = new SqlDataAdapter(sql指令, sqlCnt);
        DataSet ds = new DataSet();
        //Fill 方法会执行一系列操作 connection.open command.reader 等等
        //反正到最后就把 sql语句执行一遍,然后把结果集插入到 ds 里.
        adapter.Fill(ds);
        DataTable dt = ds.Tables[0];
        sqlCnt.Close();
        return dt;
    }
    public static int 执行SQL指令(string sql指令)//返回影响的行数
    {
        SqlConnection sqlCnt = new SqlConnection(GetConnectString());
        sqlCnt.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = sqlCnt;
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = sql指令; 
        int number = command.ExecuteNonQuery();		//执行SQL，返回一个“流”
        sqlCnt.Close();
        return number;
    }
    public static string 返回指定列数据(string sql语句, string 列名)
    {
        SqlConnection sqlCnt = new SqlConnection(GetConnectString());
        sqlCnt.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = sqlCnt;
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = sql语句; 
        SqlDataReader reader = command.ExecuteReader();		//执行SQL，返回一个“流”        

        string str = "";
        if (reader.Read())
        {
            str = "1";
            //try
            //{
            //    str = reader[列名].ToString().Trim(); // 打印出每个用户的用户名 
            //    sqlCnt.Close();
            //}
            //catch 
            //{ 
            //    sqlCnt.Close(); 
            //}
            //return str;
        }
        else str = "0";
        sqlCnt.Close();
        return str;
    }
    public static string MyEncrypt(string password)
    {
        //MD5 md5 = MD5.Create(); //实例化一个md5对像
        // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
        byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password)); 
        return  Convert.ToBase64String(s);
    }
    /// <summary>
    /// /////////////////////////////////////////
    /// </summary>
    /// <param name="commandText"></param>
    /// <returns></returns>
    //public SqlDataAdapter SqlDataAdapter函数(string commandText)
    //{
    //    SqlDataAdapter sda = new SqlDataAdapter(commandText, sqlCnt);
    //    return sda;
    //}
    //public SqlDataReader SqlDataReader对象(string commandText)
    //{        

    //    //SqlDataReader对象
    //    SqlCommand command = new SqlCommand();
    //    command.Connection = sqlCnt;
    //    command.CommandType = System.Data.CommandType.Text;
    //    command.CommandText = commandText;// "Select * from 企业用户表";
    //    SqlDataReader reader = command.ExecuteReader();		//执行SQL，返回一个“流”         
    //    return reader;
    //    //数据库类 d = new 数据库类();
    //    //SqlDataReader reader = d.SqlDataReader对象("Select * from 企业用户表");
    //    //while (reader.Read())
    //    //{
    //    //    string str = reader["注册用户名"].ToString().Trim(); // 打印出每个用户的用户名           
    //    //}
    //    //d.sqlCnt.Close();     
    //}

    //public void SqlCommand对象()
    //{
    //    //SqlCommand command = new SqlCommand();
    //    //command.Connection = sqlCnt;            // 绑定SqlConnection对象
    //    //常用方法：
    //    //command.ExecuteNonQuery(): 返回受影响函数，如增、删、改操作；
    //    //command.ExecuteScalar()：执行查询，返回首行首列的结果；
    //    //command.ExecuteReader()：返回一个数据流（SqlDataReader对象）。
                
    //    //执行SQL
    //    SqlCommand cmd = sqlCnt.CreateCommand();              //创建SqlCommand对象
    //    cmd.CommandType = CommandType.Text;
    //    cmd.CommandText = "select * from products = @ID";   //sql语句
    //    //cmd.Parameters.Add("@ID", SqlDbType.Int);
    //    //cmd.Parameters["@ID"].Value = 1;                    //给参数sql语句的参数赋值

    //    //调用存储过程
    //    //SqlCommand cmd = conn.CreateCommand();                      
    //    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //    //cmd.CommandText = "存储过程名";

    //    //整张表
    //    //SqlCommand cmd = conn.CreateCommand();    
    //    //cmd.CommandType = System.Data.CommandType.TableDirect;
    //    //cmd.CommandText = "表名


    //}


    //public void DataSet对象()
    //{
    //    SqlDataAdapter myDataAdapter = new SqlDataAdapter("select * from product", sqlCnt);
       
    //    //属性和方法
    //    //myDataAdapter.SelectCommand属性：SqlCommand变量，封装Select语句；
    //    //myDataAdapter.InsertCommand属性：SqlCommand变量，封装Insert语句；
    //    //myDataAdapter.UpdateCommand属性：SqlCommand变量，封装Update语句；
    //    //myDataAdapter.DeleteCommand属性：SqlCommand变量，封装Delete语句。
    //    //myDataAdapter.fill()：将执行结果填充到Dataset中，会隐藏打开SqlConnection并执行SQL等操作。
    //}
#region

//    6、DataSet对象

//6.1 SqlDataAdapter;

//命名空间：System.Data.SqlClient.SqlDataAdapter;

//SqlDataAdapter是SqlCommand和DataSet之间的桥梁，实例化SqlDataAdapter对象：

//SqlConnection sqlCnt = new SqlConnection(connectString);
//sqlCnt.Open();

//// 创建SqlCommand
//SqlCommand mySqlCommand = new SqlCommand();
//mySqlCommand.CommandType = CommandType.Text;
//mySqlCommand.CommandText = "select * from product";
//mySqlCommand.Connection = sqlCnt;

//// 创建SqlDataAdapter
//SqlDataAdapter myDataAdapter = new SqlDataAdapter();
//myDataAdapter.SelectCommand = mySqlCommand;	// 为SqlDataAdapter对象绑定所要执行的SqlCommand对象
//上述SQL可以简化为

//SqlConnection sqlCnt = new SqlConnection(connectString);
//sqlCnt.Open();
//// 隐藏了SqlCommand对象的定义，同时隐藏了SqlCommand对象与SqlDataAdapter对象的绑定
//SqlDataAdapter myDataAdapter = new SqlDataAdapter("select * from product", sqlCnt);
//属性和方法

//myDataAdapter.SelectCommand属性：SqlCommand变量，封装Select语句；
//myDataAdapter.InsertCommand属性：SqlCommand变量，封装Insert语句；
//myDataAdapter.UpdateCommand属性：SqlCommand变量，封装Update语句；
//myDataAdapter.DeleteCommand属性：SqlCommand变量，封装Delete语句。
//myDataAdapter.fill()：将执行结果填充到Dataset中，会隐藏打开SqlConnection并执行SQL等操作。
//6.2 SqlCommandBuilder;

//命名空间：System.Data.SqlClient.SqlCommandBuilder。

//对DataSet的操作（更改、增加、删除）仅是在本地修改，若要提交到“数据库”中则需要SqlCommandBuilder对象。用于在客户端编辑完数据后，整体一次更新数据。具体用法如下：

//SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(myDataAdapter);  // 为myDataAdapter赋予SqlCommandBuilder功能
//myDataAdapter.Update(myDataSet, "表名");                   // 向数据库提交更改后的DataSet，第二个参数为DataSet中的存储表名，并非数据库中真实的表名（二者在多数情况下一致）。
//6.3 DataSet

//命名空间：System.Data.DataSet。

//数据集，本地微型数据库，可以存储多张表。

//使用DataSet第一步就是将SqlDataAdapter返回的数据集（表）填充到Dataset对象中：

//SqlDataAdapter myDataAdapter = new SqlDataAdapter("select * from product", sqlCnt);
//DataSet myDataSet = new DataSet();		// 创建DataSet
//myDataAdapter.Fill(myDataSet, "product");	// 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
//① 访问DataSet中的数据

//SqlDataAdapter myDataAdapter = new SqlDataAdapter("select * from product", sqlCnt);
//DataSet myDataSet = new DataSet();
//myDataAdapter.Fill(myDataSet, "product");

//DataTable myTable = myDataSet.Tables["product"];
//foreach (DataRow myRow in myTable.Rows) {
//    foreach (DataColumn myColumn in myTable.Columns) {
//        Console.WriteLine(myRow[myColumn]);	//遍历表中的每个单元格
//    }
//}
//② 修改DataSet中的数据

//SqlDataAdapter myDataAdapter = new SqlDataAdapter("select * from product", sqlCnt);
//DataSet myDataSet = new DataSet();
//myDataAdapter.Fill(myDataSet, "product");

//// 修改DataSet
//DataTable myTable = myDataSet.Tables["product"];
//foreach (DataRow myRow in myTable.Rows) {
//    myRow["name"] = myRow["name"] + "商品";
//}

//// 将DataSet的修改提交至“数据库”
//SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(myDataAdapter);
//myDataAdapter.Update(myDataSet, "product");
//注意：在修改、删除等操作中表product必须定义主键，select的字段中也必须包含主键，否则会提示“对于不返回任何键列信息的 SelectCommand,不支持 UpdateCommand 的动态 SQL 生成。”错误

//③ 增加一行

//SqlDataAdapter myDataAdapter = new SqlDataAdapter("select * from product", sqlCnt);
//DataSet myDataSet = new DataSet();
//myDataAdapter.Fill(myDataSet, "product");
//DataTable myTable = myDataSet.Tables["product"];

//// 添加一行
//DataRow myRow = myTable.NewRow();
//myRow["name"] = "捷安特";
//myRow["price"] = 13.2;
////myRow["id"] = 100; id若为“自动增长”，此处可以不设置，即便设置也无效
//myTable.Rows.Add(myRow);

//// 将DataSet的修改提交至“数据库”
//SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(myDataAdapter);
//myDataAdapter.Update(myDataSet, "product");
//④ 删除一行

//SqlDataAdapter myDataAdapter = new SqlDataAdapter("select * from product", sqlCnt);
//DataSet myDataSet = new DataSet();
//myDataAdapter.Fill(myDataSet, "product");

//// 删除第一行
//DataTable myTable = myDataSet.Tables["product"];
//myTable.Rows[0].Delete();

//SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(myDataAdapter);
//myDataAdapter.Update(myDataSet, "product");
//属性

//Tables：获取包含在DataSet中的表的集合。
//Relations：获取用于将表链接起来并允许从父表浏览到子表的关系的集合。
//HasEroors：表明是否已经初始化DataSet对象的值。
//方法

//Clear清除DataSet对象中所有表的所有数据。
//Clone复制DataSet对象的结构到另外一个DataSet对象中，复制内容包括所有的结构、关系和约束，但不包含任何数据。
//Copy复制DataSet对象的数据和结构到另外一个DataSet对象中。两个DataSet对象完全一样。
//CreateDataReader为每个DataTable对象返回带有一个结果集的DataTableReader，顺序与Tables集合中表的显示顺序相同。
//Dispose释放DataSet对象占用的资源。
//Reset将DataSet对象初始化。

//7、释放资源

//资源使用完毕后应及时关闭连接和释放，具体方法如下：

//myDataSet.Dispose();        // 释放DataSet对象
//myDataAdapter.Dispose();    // 释放SqlDataAdapter对象
//myDataReader.Dispose();     // 释放SqlDataReader对象
//sqlCnt.Close();             // 关闭数据库连接
//sqlCnt.Dispose();           // 释放数据库连接对象（默认释放连接时会先释放存在绑定之的Command，DataReader等对象）
 
#endregion
 
}