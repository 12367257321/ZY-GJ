<%@ WebHandler Language="C#" Class="MyAction" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Script.Serialization;

public class MyAction : IHttpHandler
{    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/json";
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        try
        { 
            string method = context.Request["action"];            
            
            //string sss=context.Request.Params .ToString();
            //sss = sss.Substring(sss.IndexOf("&")).ToUpper();
            switch (method)
            { 
                case "zygj-denglu":
                        // url:   "http://tcyc.vip:9012/zygj.ashx?action=zygj-denglu&userID="+myUserID+"&tikuid="+tikuid ,
                    context.Response.Write(作业管家类.登陆(context));
                    break; 
                case "zygj-daan": 
                    context.Response.Write(作业管家类.单册答案(context));
                    break; 
                case "zygj-chakandaan": 
                    context.Response.Write(作业管家类.查看所有答案(context));
                    break; 
                case "zygj-chakandance": 
                    context.Response.Write(作业管家类.查找单册答案(context));
                    break; 
                case "zygj-shipinshuku": 
                    context.Response.Write(作业管家类.视频书库(context));
                    break; 
                case "zygj-shitizhishidian": 
                    context.Response.Write(作业管家类.试题的题目答案知识点获得(context));
                    break; 
                case "zygj-bookid": 
                    context.Response.Write(作业管家类.图书章节页题(context));
                    break; 
                case "zygj-mybooks": 
                    context.Response.Write(作业管家类.我的视频书库(context));
                    break; 
                case "zygj-uploadHomework": 
                    context.Response.Write(作业管家类.上传作业(context));
                    break; 
                case "zygj-lookHomeworks": 
                    context.Response.Write(作业管家类.查看作业(context));
                    break; 
                case "zygj-homework": 
                    context.Response.Write(作业管家类.查看单次作业(context));
                    break; 
                case "zygj-download": 
                    context.Response.Write(作业管家类.下载中心(context));
                    break; 
                default:
                    break;
            }
              
        }
        catch
        {
        }
  
    }
  
    
    public bool IsReusable {
        get {
            return false;
        }
    } 
}