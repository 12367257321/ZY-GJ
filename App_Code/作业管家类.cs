using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using 试题知识点视频;
/// <summary>
/// 作业管家类 的摘要说明
/// </summary>
public class 作业管家类
{
    public 作业管家类()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static string 登陆(HttpContext context)
    {
        string json = "{\"status\":\"false\",\"message\":\"登录失败：账号或密码不正确\"}";
        try
        {
            string name= context.Request["name"]; 
            string mima= context.Request["mima"];

            string strsql = "select * from 用户表 where 用户账号='"+name+"' and 用户密码='"+mima+"'";
            DataTable dt=数据库类.返回DataTable(strsql); 
            if(dt!=null&& dt.Rows.Count>0)
            {
                var responseData =new { status= "success", message="登录成功！" }; 
                JavaScriptSerializer serializer = new JavaScriptSerializer();// 序列化为JSON
                json = serializer.Serialize(responseData); 
            }

        }
        catch (Exception ex) { }

        return json;
    }
    public static string 单册答案(HttpContext context)
    {
        string json = "{\"status\":\"false\",\"message\":\"登录失败：账号或密码不正确\"}";
        var responseData = new { status = "success", message = "登录成功！" };
        JavaScriptSerializer serializer = new JavaScriptSerializer();// 序列化为JSON
        json = serializer.Serialize(responseData);
        try
        {
            string bookid = context.Request["id"];
            string name = context.Request["name"];
            string mima = context.Request["mima"];

            string strsql = "select * from 用户表 where 用户账号='" + name + "' and 用户密码='" + mima + "'";
            DataTable dt = 数据库类.返回DataTable(strsql);
            if (dt != null && dt.Rows.Count > 0)
            {
                  responseData = new { status = "success", message = "登录成功！" };
                  serializer = new JavaScriptSerializer();// 序列化为JSON
                json = serializer.Serialize(responseData);
            }

        }
        catch (Exception ex) { }

        return json;
    }
    public static string 查看所有答案(HttpContext context)
    {
        string json = "{\"status\":\"false\",\"message\":\"登录失败：账号或密码不正确\"}";
        var responseData = new { status = "success", message = "登录成功！" };
        JavaScriptSerializer serializer = new JavaScriptSerializer();// 序列化为JSON
        json = serializer.Serialize(responseData);
        try
        {
            string bookid = context.Request["id"];
            string name = context.Request["name"];
            string mima = context.Request["mima"];

            //string strsql = "select * from 用户表 where 用户账号='" + name + "' and 用户密码='" + mima + "'";
            //DataTable dt = 数据库类.返回DataTable(strsql);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //      responseData = new { status = "success", message = "登录成功！" };
            //      serializer = new JavaScriptSerializer();// 序列化为JSON
            //    json = serializer.Serialize(responseData);
            //}
            JArray ja = new JArray();
            DataTable dt = 数据库类.返回DataTable("select * from 图书表 where 使用中=1 ");//图书id,图书名称,图书答案网页路径
            for(int i=0;i<dt.Rows.Count;i++)
            {
                DataRow dr= dt.Rows[i];
                JObject jobj = new JObject();
                jobj.Add("id", dr["图书id"].ToString());
                jobj.Add("title", dr["图书名称"].ToString());
                jobj.Add("subject", dr["科目"].ToString());
                jobj.Add("level", dr["学年段"].ToString());
                jobj.Add("cover", dr["封皮网页路径"].ToString().Replace("http://tcyc.vip:9012", "http://localhost:9012") );// "http://localhost:9012//books/胜券在握物理8年级上/bookbmp.png?fit=crop&w=400&h=500");
                jobj.Add("pages", dr["答案页数"].ToString());
                jobj.Add("isPopular", "true");
                jobj.Add("isNew", "true");
                //  jobj["id"] = i;
                ja.Add(jobj); 
            }
            // for (int i = 1; i < 10; i++)
            // {
            //   JObject jobj = new JObject();
            // jobj.Add("id",i);
            // jobj.Add("title", "初中数学核心解题方法");
            // jobj.Add("subject", "数学");
            // jobj.Add("level", "初中");
            // jobj.Add("cover", "http://tcyc.vip:9012//books/胜券在握物理8年级上/bookbmp.png?fit=crop&w=400&h=500");
            // jobj.Add("pages", "124");
            // jobj.Add("isPopular", "true");
            // jobj.Add("isNew", "true");
            // //  jobj["id"] = i;
            //     ja.Add(jobj);
            // json = ja.ToString();
            //}
            json = ja.ToString();
        }
        catch (Exception ex) { }
        
        return json;
    }
    public static string 查找单册答案(HttpContext context)
    {
        string json = "{\"status\":\"false\",\"message\":\"登录失败：账号或密码不正确\"}";
        var responseData = new { status = "success", message = "登录成功！" };
        JavaScriptSerializer serializer = new JavaScriptSerializer();// 序列化为JSON
        json = serializer.Serialize(responseData);
        try
        {
            string bookid = context.Request["id"];  
            DataTable dt = 数据库类.返回DataTable("select * from 图书表 where 使用中=1 and 图书id= "+ bookid);//图书id,图书名称,图书答案网页路径
            JObject jobj = new JObject();
            if(dt.Rows.Count>0)
            {
                DataRow dr = dt.Rows[0];
                jobj.Add("status", "success");
                jobj.Add("id", dr["图书id"].ToString()); 
                jobj.Add("imagepath", dr["图书答案网页路径"].ToString().Replace("http://tcyc.vip:9012", "http://localhost:9012"));// "http://localhost:9012//books/胜券在握物理8年级上/bookbmp.png?fit=crop&w=400&h=500");
                jobj.Add("pages", dr["答案页数"].ToString());
                jobj.Add("grade", dr["学期名称"].ToString());
                jobj.Add("subject", dr["科目"].ToString());
                jobj.Add("title", dr["图书名称"].ToString());
                jobj.Add("edition", dr["出版社"].ToString()); 
            }           
            json = jobj.ToString();
        }
        catch (Exception ex) { }
        return json;
    } 
    public static string 图书章节页题(HttpContext context)
    {
        string json = "{\"status\":\"false\",\"message\":\"没有此书\"}";
        try
        { 
            string  bookid1= context.Request["bookid"]; 
            试题知识点视频类 ff = new 试题知识点视频类();
            json= ff.分解(bookid1); 
        }
        catch (Exception ex) { }
        return json;
    }
  
    public static string 试题的题目答案知识点获得(HttpContext context)
    {
        string json = "{\"status\":\"false\",\"message\":\"没有此题\"}";
        try
        { 
            string  id1= context.Request["id"];

            

            DataTable dt = 数据库类.返回DataTable(" SELECT dbo.图书试题表.*, dbo.图书表.科目  FROM    dbo.图书表 INNER JOIN dbo.图书试题表 ON dbo.图书表.图书id = dbo.图书试题表.图书id where 图书试题id=" + id1);
            if(dt.Rows.Count>0)
            {
                DataRow dr = dt.Rows[0];
                string 科目 = dr["科目"].ToString().Trim();
                string 试题名称 = dr["试题名称"].ToString().Trim();
                string 试题路径 = dr["试题路径"].ToString().Replace(@"D:\9013\", "http://localhost:9013/").Trim() + "/" + 试题名称.Replace(".docx", ".html");
                string 试题答案路径 = dr["试题路径"].ToString().Replace(@"D:\9013\", "http://localhost:9013/").Trim() + "/" + 试题名称.Replace(".docx", ".html").Replace("题目","答案") ;
                string[] 知识点s = dr["试题知识点"].ToString().Trim().Split(',');
                JObject job = new JObject();
                job.Add("status", "success");
                job.Add("tm", 试题路径);
                job.Add("daan", 试题答案路径);
                job.Add("kemu", 科目);
                JArray ja = new JArray();
                for (int i = 0; i < 知识点s.Length; i++)
                {
                    if (知识点s[i].Trim() != "")
                    {
                        JObject job2 = new JObject();
                        job2.Add("title", 知识点s[i].Trim()); 
                        job2.Add("duration", "10:00");
                        ja.Add(job2);
                    }
                }
              
                job.Add("zhishidian", ja);            
                json = JsonConvert.SerializeObject(job);
                //"zhishidian": [{ "id": "K-9",   "title": "整式基本概念",  "duration": "15:00",  "tags": ["单项式", "多项式"] }, { "id": "K-9", "title": "整式基本概念",  "duration": "15:00",   "tags": ["单项式", "多项式"]}]
            }
        }
        catch (Exception ex) { }
        return json;
    }
    public static string 视频书库(HttpContext context)
    {
        string json = "{\"status\":\"false\",\"message\":\"登录失败：账号或密码不正确\"}"; 
        try
        {
            Dictionary<int, VideoBook> videoBooks = new Dictionary<int, VideoBook>();
            DataTable dt = 数据库类.返回DataTable("select * from 图书表 where 使用中=1");
            for(int i=0;i<dt.Rows.Count;i++)
            {
                DataRow dr = dt.Rows[i];
                videoBooks.Add((i + 1), new VideoBook
                {
                    id = dr["图书id"].ToString(),
                    title = dr["图书名称"].ToString(),
                    category = dr["学年段"].ToString(),
                    type = dr["分类"].ToString(),
                    image = dr["封皮网页路径"].ToString()
                }) ; 
            }
            json = JsonConvert.SerializeObject(videoBooks);



        }
        catch (Exception ex) { }
        return json;
    }
    public static string 我的视频书库(HttpContext context)
    {
        string json = "{\"status\":\"false\",\"message\":\"登录失败：账号或密码不正确\"}";
        try
        {

           
            string userID = context.Request["userID"];  
            string strsql =   "SELECT dbo.用户表.用户账号, dbo.用户图书汇总表.用户id, dbo.用户图书汇总表.图书id, dbo.用户图书汇总表.截止日期, dbo.图书表.图书名称, dbo.图书表.封皮网页路径, dbo.图书表.科目, dbo.图书表.学年段, dbo.图书表.学期名称"
                +" FROM      dbo.用户表 INNER JOIN  dbo.用户图书汇总表 ON dbo.用户表.用户id = dbo.用户图书汇总表.用户id INNER JOIN  dbo.图书表 ON dbo.用户图书汇总表.图书id = dbo.图书表.图书id " +
                " where 使用中=1 and 用户账号='" + userID + "' and 截止日期 > GETDATE();"; 
            DataTable dt = 数据库类.返回DataTable(strsql);
            JArray ja = new JArray();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i]; 
                JObject job = new JObject();
                job.Add("id", dr["图书id"].ToString());
                job.Add("title", dr["图书名称"].ToString() + "-" + dr["科目"].ToString() + "-" + dr["学年段"].ToString() + "-" + dr["学期名称"].ToString());
                job.Add("http", "http://localhost:9012/a试题知识点视频.html?bookId=" + dr["图书id"].ToString() + "&k=1&name="+ dr["图书名称"].ToString());
                job.Add("cover", dr["封皮网页路径"].ToString());
                ja.Add(job);
            }  
            json = ja.ToString();
        }
        catch (Exception ex) { }
        return json;
    }
    public static string 上传作业(HttpContext context)
    {
        try
        {
            //txt文件类.Write("d:\\a.txt", "fff");
            // 创建作业目录（如不存在）
            string uploadPath = context.Server.MapPath("~/Homework/");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string myUserID = context.Request.Form["myUserID"];
            string bookid = context.Request.Form["bookid"];
            // 获取表单数据 
            //string className = context.Request.Form["className"];
            string 图书名称 = context.Request.Form["subject"];

            string userDirectory = Path.Combine(uploadPath, myUserID);
            if (!Directory.Exists(userDirectory))
                Directory.CreateDirectory(userDirectory);

            // 创建基于科目的子目录
            string subjectDirectory = Path.Combine(userDirectory, 图书名称);
            if (!Directory.Exists(subjectDirectory))
                Directory.CreateDirectory(subjectDirectory);

            List<string> savedFiles = new List<string>();
            string strsqls = "";
            string timestamp = DateTime.Now.ToString("yyMMddHHmmssfff");
            // 处理上传的所有文件
            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                HttpPostedFile file = context.Request.Files[i];

                if (file != null && file.ContentLength > 0)
                {
                    // 防止文件名冲突
                    string fileName = Path.GetFileName(file.FileName).Replace(" ","");
                    string newfileName = timestamp + fileName;
                    string filePath = Path.Combine(subjectDirectory, newfileName);
                    // 保存文件
                    file.SaveAs(filePath); 
                    string newfileName2 = timestamp +"p"+ fileName;
                    string filePath2 = Path.Combine(subjectDirectory, newfileName2);
                    // 保存文件
                    file.SaveAs(filePath2);


                    savedFiles.Add(fileName);
                    strsqls += fileName.Trim() + ";";
                }
            }

            if (strsqls.Length > 0)
            {
                strsqls = strsqls.Substring(0, strsqls.Length - 1);
                string strsql = "insert into 上传作业表(用户id,图书id,作业存储路径,作业名称,作业名称头部,上传时间)values(" 
                    + myUserID + "," + bookid + ",'" + subjectDirectory + "','" + strsqls + "','" + timestamp + "','"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"') ";

                int num = 数据库类.执行SQL指令(strsql);
                //txt文件类.Write(Path.Combine(subjectDirectory, "a.txt"), strsql);
                //txt文件类.Write(  "d:\\a.txt" , strsql);
                return "成功上传{savedFiles.Count}个文件：" + string.Join(", ", savedFiles);

            }
            return "上传失败:0个文件 ";

            //context.Response.Write("成功上传{savedFiles.Count}个文件：" + string.Join(", ", savedFiles));
        }
        catch (Exception ex)
        {
            return "上传失败: " + ex.Message;
            //context.Response.Write("上传失败: " + ex.Message);
        }


        //string json = "{\"status\":\"success\",\"message\":\"上传成功!\"}";
        //return json;
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
                "dbo.图书表.科目, dbo.图书表.第几学期, dbo.图书表.学期名称 FROM      dbo.上传作业表 INNER JOIN     dbo.图书表 ON dbo.上传作业表.图书id = dbo.图书表.图书id  where 使用中=1 and 用户id=" + myUserID+ "  order by 上传时间 desc";
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
                DateTime dt3 = Convert.ToDateTime(dr["上传时间"].ToString());
                job.Add("date",  dt3.ToString("yyyy-MM-dd"));
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
    public static string 查看单次作业(HttpContext context)
    {
        string json = "{\"status\":\"success\",\"message\":\"成功!\"}";

        try
        {
            string homeid = context.Request["homeid"];
            string strsql = "SELECT dbo.上传作业表.上传作业id, dbo.上传作业表.用户id, dbo.上传作业表.图书id, dbo.上传作业表.教师id, " +
                "dbo.上传作业表.作业名称, dbo.上传作业表.描述, dbo.上传作业表.作业名称头部, dbo.上传作业表.作业存储路径,dbo.上传作业表.错题本路径,dbo.上传作业表.类题推送路径, " +
                "dbo.上传作业表.上传时间, dbo.上传作业表.评判时间, dbo.上传作业表.评语, dbo.图书表.图书名称, dbo.图书表.学年段, " +
                "dbo.图书表.科目, dbo.图书表.第几学期, dbo.图书表.学期名称 FROM      dbo.上传作业表 INNER JOIN     dbo.图书表 ON dbo.上传作业表.图书id = dbo.图书表.图书id  where 使用中=1 and 上传作业id=" + homeid;
            DataTable dt = 数据库类.返回DataTable(strsql);
             
            if( dt.Rows.Count>0)
            {
                DataRow dr = dt.Rows[0];
                JObject job = new JObject();
                job.Add("id", Convert.ToInt32(dr["上传作业id"].ToString()));
                string[] str作业名称s = dr["作业名称"].ToString().Split(';');
                string str作业头部 = dr["作业名称头部"].ToString().Trim();
                string str作业存储路径=dr["作业存储路径"].ToString().Trim();
                string str类题推送路径 = dr["类题推送路径"].ToString().Replace("C:\\huganqi\\", "");
                string str错题本路径 = dr["错题本路径"].ToString().Replace("C:\\huganqi\\", "");
                
                string[] result = Regex.Split(str作业存储路径, "Homework", RegexOptions.IgnoreCase);

                JArray ja = new JArray();
                for(int i=0;i<str作业名称s.Length;i++)
                {
                    string filename ="http://localhost:9012/Homework"+result[1]+ "\\"+str作业头部+"p"+str作业名称s[i];
                    ja.Add(filename);
                }
                job.Add("imgs", ja);
                if (str类题推送路径 == "")
                    job.Add("类题推送路径", "");
                else
                    job.Add("类题推送路径", "http://localhost:9012/" + dr["类题推送路径"].ToString().Replace("C:\\huganqi\\", ""));
                if (str错题本路径 == "")
                    job.Add("错题本路径", "");
                else
                    job.Add("错题本路径", "http://localhost:9012/" + dr["错题本路径"].ToString().Replace("C:\\huganqi\\", ""));
                job.Add("科目", dr["科目"].ToString());
                job.Add("学期名称", dr["学期名称"].ToString());
                job.Add("图书名称", dr["图书名称"].ToString());
                job.Add("评语", dr["评语"].ToString());
                job.Add("时间", dr["上传时间"].ToString());
                job.Add("评判",  "作业已判");

                json = job.ToString();
            }  
        }
        catch (Exception ex)
        {
            return "失败: " + ex.Message;
        }
        
        return json;
    }
    public static string 下载中心(HttpContext context)
    {
        string json = "{\"status\":\"success\",\"message\":\"成功!\"}";

        try
        {
            string userID = context.Request["userID"];
            string strsql = "select * from 下载中心表 where 用户id= " + userID+ " order by 生成时间 desc";
            DataTable dt = 数据库类.返回DataTable(strsql);
               
            JArray ja = new JArray();
            for(int i=0;i<dt.Rows.Count;i++)
            {
                DataRow dr = dt.Rows[i];
                JObject job = new JObject();
                // 下载id, 文件类型, 下载文件名称, 下载路径, 科目, 生成时间, 下载时间, 详情, 上传作业id 
                job.Add("id", Convert.ToInt32(dr["下载id"].ToString())); 
                job.Add("type", dr["文件类型"].ToString());
                job.Add("name", dr["下载文件名称"].ToString());
                job.Add("path", dr["下载路径"].ToString());
                job.Add("subject", dr["科目"].ToString());
                DateTime dt3 = Convert.ToDateTime(dr["生成时间"].ToString());
                job.Add("date", dt3.ToString("yyyy-MM-dd"));
                //job.Add("date", dr["生成时间"].ToString().Replace("/","-"));
                job.Add("description", dr["详情"].ToString() ); 
                ja.Add(job);


            //    {
            //        "id": 9,
            //    "type": "word",
            //    "name": "刷题",
            //    "subject": "数学",
            //    "date": "2025-10-25 18:19:24",
            //    "status": 1,
            //    "description": ""
            //}

                 
            }
            json = ja.ToString();
        }
        catch (Exception ex)
        {
            return "失败: " + ex.Message;
        }
        return json;
    }
    public class VideoBook
    {
        public string id { get; set; }
        public string title { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public string image { get; set; }
    }
    

}