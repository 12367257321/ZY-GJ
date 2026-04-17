<%@ WebHandler Language="C#" Class="jszygj" %>

using System;
using System.Web;

public class jszygj : IHttpHandler {

    public void ProcessRequest (HttpContext context) {              
        context.Response.ContentType = "text/json";
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        try
        {
            string method = context.Request["action"];

            switch (method)
            {
                case "js-denglu":
                    // url:   "http://tcyc.vip:9012/js/jszygj.ashx?action=zygj-denglu&userID="+myUserID+"&tikuid="+tikuid ,
                    context.Response.Write(作业管家类.登陆(context));
                    break;                     
                case "js-lookhomework": 
                    context.Response.Write(作业管家类.查看作业(context));
                    break;
            }
        }
        catch (Exception err){ }
    }


    public bool IsReusable {
        get {
            return false;
        }
    }

}

