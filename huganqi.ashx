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

                case "renlianshibie":
                    context.Response.Write(互感器类.读人脸识别数据 (context));
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