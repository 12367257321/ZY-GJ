using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// 教师管理类 的摘要说明
/// </summary>
public class 教师管理类
{
    public 教师管理类()
    { 
    }
    public static string 登陆(HttpContext context)
    {
        string json = "{\"status\":\"false\",\"message\":\"登录失败：账号或密码不正确\"}";
        try
        {
            string name = context.Request["name"];
            string mima = context.Request["mima"];

            string strsql = "select * from 教师表 where 教师账号='" + name + "' and 教师密码='" + mima + "'";
            DataTable dt = 数据库类.返回DataTable(strsql);
            if (dt != null && dt.Rows.Count > 0)
            {
                JObject job = new JObject();
                job.Add("status", "success");
                job.Add("教师姓名", dt.Rows[0]["教师姓名"].ToString());
                //var responseData = new { status = "success", message = "登录成功！" };
                //JavaScriptSerializer serializer = new JavaScriptSerializer();// 序列化为JSON
                //json = serializer.Serialize(responseData);
                json = job.ToString();
            }

        }
        catch (Exception ex) { }

        return json;
    }
    public static string 查看作业(HttpContext context)
    {
        string json = "{\"status\":\"success\",\"message\":\"成功!\"}";

        try
        {
            string myUserID = context.Request["userID"];
            string strsql = "SELECT dbo.上传作业表.上传作业id, dbo.上传作业表.用户id, dbo.上传作业表.图书id, dbo.上传作业表.教师id, " +
                "dbo.上传作业表.作业名称, dbo.上传作业表.描述, dbo.上传作业表.作业名称头部, dbo.上传作业表.作业存储路径, " +
                "dbo.上传作业表.上传时间, dbo.上传作业表.评判时间, dbo.上传作业表.评语, dbo.图书表.图书名称, dbo.图书表.学年段, " +
                "dbo.图书表.科目, dbo.图书表.第几学期, dbo.图书表.学期名称 FROM      dbo.上传作业表 INNER JOIN     dbo.图书表 ON dbo.上传作业表.图书id = dbo.图书表.图书id  where 用户id=" + myUserID + "  order by 上传时间 desc";
            DataTable dt = 数据库类.返回DataTable(strsql);
            JArray ja = new JArray();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                JObject job = new JObject();
                job.Add("id", Convert.ToInt32(dr["上传作业id"].ToString()));
                job.Add("type", "word");
                job.Add("name", dr["图书名称"].ToString());
                job.Add("subject", dr["科目"].ToString());

                job.Add("date", dr["上传时间"].ToString().Replace("/", "-"));
                job.Add("status", 1);
                job.Add("description", dr["评语"].ToString());
                ja.Add(job);
            }
            json = ja.ToString();
            json = "[" + json.Substring(1, json.Length - 2) + "]";
        }
        catch (Exception ex)
        {
            return "失败: " + ex.Message;
        }
        //id: 1,
        //            name: "数学三角函数作业",
        //            type: "pdf",
        //            subject: "数学",
        //            date: "2025-09-15", 
        //            description: "完成了三角函数所有题，最后两题有部分错误需注意", 
        return json;
    }
}