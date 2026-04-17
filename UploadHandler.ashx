<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.IO;
using System.Web;
using System.Collections.Generic;

public class UploadHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        try
        {
            // 创建作业目录（如不存在）
            string uploadPath = context.Server.MapPath("~/Homework/");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // 获取表单数据
            string studentName = context.Request.Form["studentName"];
            //string className = context.Request.Form["className"];
            string subject = context.Request.Form["subject"];

            //// 创建基于班级和姓名的子目录
            //string userDirectory = Path.Combine(uploadPath, className, studentName);
            //if (!Directory.Exists(userDirectory))
            //    Directory.CreateDirectory(userDirectory);

            string userDirectory = Path.Combine(uploadPath,  studentName);
            if (!Directory.Exists(userDirectory))
                Directory.CreateDirectory(userDirectory);

            // 创建基于科目的子目录
            string subjectDirectory = Path.Combine(userDirectory, subject);
            if (!Directory.Exists(subjectDirectory))
                Directory.CreateDirectory(subjectDirectory);

            List<string> savedFiles = new List<string>();

            // 处理上传的所有文件
            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                HttpPostedFile file = context.Request.Files[i];

                if (file != null && file.ContentLength > 0)
                {
                    // 防止文件名冲突
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(subjectDirectory, fileName);

                    //// 如果文件已存在，添加时间戳
                    //if (File.Exists(filePath))
                    //{
                    //    string timestamp = DateTime.Now.ToString("yyMMddHHmmssfff");
                    //    fileName = Path.GetFileNameWithoutExtension(file.FileName)+"-"+timestamp+"\\"+Path.GetExtension(file.FileName);
                    //    filePath = Path.Combine(subjectDirectory, fileName);
                    //}

                    // 保存文件
                    file.SaveAs(filePath);
                    savedFiles.Add(fileName);
                }
            }

            context.Response.Write("成功上传{savedFiles.Count}个文件：" + string.Join(", ", savedFiles));
        }
        catch (Exception ex)
        {
            context.Response.Write("上传失败: " + ex.Message);
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
