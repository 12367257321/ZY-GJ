<%@ WebHandler Language="C#" Class="FileProcessor" %>

using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Web.Script.Serialization;

public class FileProcessor : IHttpHandler {
    
    public void ProcessRequest(HttpContext context) {
        context.Response.ContentType = "application/json";
        var response = new Dictionary<string, object> {
            { "success", false },
            { "message", "" }
        };
        
        try {
            // 验证请求类型
            if (!context.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase)) {
                response["message"] = "只接受POST请求";
                WriteJsonResponse(context, response);
                return;
            }
            
            // 获取表单数据
            string phone = context.Request.Form["phone"];
            string requirement = context.Request.Form["requirement"];
            
            // 验证手机号
            if (string.IsNullOrWhiteSpace(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^1[3-9]\d{9}$")) {
                response["message"] = "请输入有效的11位手机号码";
                WriteJsonResponse(context, response);
                return;
            }
            
            // 验证处理需求
            if (string.IsNullOrWhiteSpace(requirement)) {
                response["message"] = "请填写处理需求";
                WriteJsonResponse(context, response);
                return;
            }
            
            // 验证文件
            if (context.Request.Files.Count == 0) {
                response["message"] = "请至少上传一个文件";
                WriteJsonResponse(context, response);
                return;
            }
            
            // 创建处理ID和存储目录
            string processId = Guid.NewGuid().ToString("N").Substring(0, 16);
            string savePath = context.Server.MapPath(  "~/App_Data/Uploads/"+processId);
            Directory.CreateDirectory(savePath);
            
            // 保存上传的文件
            List<string> savedFiles = new List<string>();
            for (int i = 0; i < context.Request.Files.Count; i++) {
                HttpPostedFile file = context.Request.Files[i];
                if (file != null && file.ContentLength > 0) {
                    // 检查文件大小（最大100MB）
                    if (file.ContentLength > 100 * 1024 * 1024) {
                        response["message"] = "文件 "+file.FileName+" 超过100MB限制";
                        WriteJsonResponse(context, response);
                        return;
                    }
                    
                    // 保存文件
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(savePath, fileName);
                    file.SaveAs(filePath);
                    savedFiles.Add(fileName);
                }
            }
            
            // 实际处理逻辑应该在这里
            // 这里只是模拟处理过程
            System.Threading.Thread.Sleep(2000); // 模拟处理时间
            
            // 设置成功响应
            response["success"] = true;
            response["message"] = "成功处理了 "+context.Request.Files.Count+" 个文件";
            response["processId"] = processId;
            response["fileCount"] = context.Request.Files.Count;
            
        } catch (Exception ex) {
            response["message"] = "服务器错误: "+ex.Message+"";
        }
        
        WriteJsonResponse(context, response);
    }
    
    private void WriteJsonResponse(HttpContext context, Dictionary<string, object> data) {
        var serializer = new JavaScriptSerializer();
        context.Response.Write(serializer.Serialize(data));
    }
    
    public bool IsReusable {
        get { return false; }
    }
}
