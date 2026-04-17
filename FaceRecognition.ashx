 

<%@ WebHandler Language="C#" Class="FaceRecognitionHandler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Script.Serialization;
public class FaceRecognitionHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        try
        {
            // 检查是否包含文件
            if (context.Request.Files.Count < 1)
            {
                SendResponse(context, false, "未上传人脸图片");
                return;
            }

            // 获取表单数据
            string name = context.Request.Form["name"];
            string phone = context.Request.Form["phone"];
            string gender = context.Request.Form["gender"];
            string role = context.Request.Form["role"];
            string remarks = context.Request.Form["remarks"];

            // 基本验证
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone))
            {
                SendResponse(context, false, "姓名和手机号为必填项");
                return;
            }

            // 获取上传的文件
            HttpPostedFile imageFile = context.Request.Files["imageFile"];
            if (imageFile == null || imageFile.ContentLength == 0)
            {
                SendResponse(context, false, "上传的图片文件为空");
                return;
            }

            string fileName = System.IO.Path.GetFileName(imageFile.FileName);
            string fileExtension = System.IO.Path.GetExtension(fileName).ToLower();

            // 验证文件类型
            if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
            {
                SendResponse(context, false, "仅支持JPG、JPEG和PNG格式的图片");
                return;
            }

            // 创建保存目录（确保目录存在）
            string saveDirectory = context.Server.MapPath("~/FaceImages/");
            if (!System.IO.Directory.Exists(saveDirectory))
            {
                System.IO.Directory.CreateDirectory(saveDirectory);
            }

            // 生成唯一文件名
            string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            string savePath = System.IO.Path.Combine(saveDirectory, uniqueFileName);
            imageFile.SaveAs(savePath);

            // 注意：以下是人脸识别的模拟步骤，实际项目中需要替换为真实的识别API调用
            // 模拟人脸识别处理
            // ...

            // 构建响应数据
                        
           // int nnn3 = access数据库类.执行SQL指令("insert into 人脸识别临时表(姓名,性别,手机,职务,人脸图片路径,备注,唯一标识,时间,已处理)values('"+name+"','"+gender+"','"+phone+"','"+role+"','"+savePath+"','"+remarks+"','"+uniqueFileName.Substring(0,uniqueFileName.IndexOf('.'))+"','"+DateTime.Now.ToString()+"',0)");


            int nnn = access数据库类.执行SQL指令("insert into 人脸识别临时表(姓名,性别,手机,职务,人脸图片路径,备注,唯一标识,时间)values('"+name+"','"+gender+"','"+phone+"','"+role+"','"+savePath+"','"+remarks+"','"+uniqueFileName.Substring(0,uniqueFileName.IndexOf('.'))+"','"+DateTime.Now.ToString()+"')");

            //int nnn = access数据库类  .执行SQL指令("insert into aa(字段1)values('a' )");
            if (nnn <= 0)
            {

            }
            var response = new
            {
                success = true,
                message = "人脸信息提交成功",
                userData = new
                {
                    name,
                    phone,
                    gender,
                    role,
                    remarks,
                    facePoints = "128个面部特征点", // 模拟数据
                    imageUrl = "http://www.tcyc.vip/FaceImages/"+uniqueFileName // 在网站上访问该图片的URL
                }
            };

            // 发送成功响应
            var serializer = new JavaScriptSerializer();
            context.Response.Write(serializer.Serialize(response));
        }
        catch (Exception ex)
        {
            // 异常处理
            SendResponse(context, false,"");// $"处理出错: {ex.Message}");
        }
    }

    private void SendResponse(HttpContext context, bool success, string message)
    {
        var response = new
        {
            success,
            message
        };
        var serializer = new JavaScriptSerializer();
        context.Response.Write(serializer.Serialize(response));
    }

    public bool IsReusable { get { return false; } }
}
