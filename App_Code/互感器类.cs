using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Class1 的摘要说明
/// </summary>
public class 互感器类
{
    public 互感器类()
    { 
    }

    public static string 读人脸识别数据(HttpContext context)
    {
        string a = context.Request["a"].ToString().Trim();
        string b = context.Request["b"].ToString().Trim();
        JArray xxx = new JArray();
        string strsql1 = "select * from 人脸识别临时表 where 已处理=0";
        DataTable dt= access数据库类.返回DataTable(strsql1);
        if(dt!=null)
            for(int i=0;i<dt.Rows.Count;i++)
            {
                //ID 姓名   性别 手机  职务 人脸图片路径  备注 唯一标识    时间 已处理
                JObject jo1 = new JObject();
                jo1["ID"] = dt.Rows[i]["ID"].ToString();
                jo1["姓名"] = dt.Rows[i]["姓名"].ToString();
                jo1["性别"] = dt.Rows[i]["性别"].ToString();
                jo1["手机"] = dt.Rows[i]["手机"].ToString();
                jo1["职务"] = dt.Rows[i]["职务"].ToString();
                jo1["人脸图片路径"] = dt.Rows[i]["人脸图片路径"].ToString();
                jo1["备注"] = dt.Rows[i]["备注"].ToString();
                //jo1["唯一标识"] = dt.Rows[i]["唯一标识"].ToString();
                jo1["时间"] = dt.Rows[i]["时间"].ToString();
                jo1["已处理"] = dt.Rows[i]["已处理"].ToString();
                xxx.Add(jo1);
            }

        access数据库类.执行SQL指令("update 人脸识别临时表 set 已处理=1");
        string str = xxx.ToString();
        return str;
    }
}
