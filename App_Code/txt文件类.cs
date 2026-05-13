using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
public  class txt文件类
{
    public static  void Write(string str文件名, string str内容)
    {
        File.Delete(str文件名);
        FileStream fs = new FileStream(str文件名, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        //开始写入
        sw.Write(str内容);//  Encoding.GetEncoding("GB2312"));
        //清空缓冲区
        sw.Flush();
        //关闭流
        sw.Close();
        fs.Close();
    }

    public static string read(string str文件名)
    {
        StreamReader sr = new StreamReader(str文件名, Encoding.Default);//UTF8 Default
        string strReturn = sr.ReadToEnd();
        sr.Close();
        if(strReturn.Contains("?"))
        {
            sr = new StreamReader(str文件名, Encoding.UTF8);//UTF8 Default
            strReturn = sr.ReadToEnd();
            sr.Close();

        }
        return strReturn;
    }



}

