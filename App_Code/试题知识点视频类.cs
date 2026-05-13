using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace 试题知识点视频
{
    public class 试题知识点视频类
    {
        public Dictionary<int, object> ConvertToJsStructure(书类 book)
        {
            var dataSet = new Dictionary<int, object>();

            foreach (var zhang in book.zhangs)
            {
                var sectionsDict = new Dictionary<int, object>();

                foreach (var jie in zhang.jies)
                {
                    var pagesDict = new Dictionary<int, object>();

                    foreach (var ye in jie.yes)
                    {
                        var questionsList = new List<object>();

                        foreach (var shiti in ye.shitis)
                        {
                            questionsList.Add(new
                            {
                                id = shiti.id,
                                title = shiti.试题名,
                                //zsd=shiti.知识点,
                                shitiid = shiti.试题ID
                                //,
                                //tm=shiti.试题html,
                                //daan=shiti.答案html
                                // 这里需要提取试题内容，当前结构中没有
                                //content = $"{shiti.试题ID} - {shiti.知识点}"
                            });
                        }

                        // 修改点：为每个页对象添加name属性
                        pagesDict.Add(ye.id, new
                        {
                            name = ye.页名, // 添加name属性
                            questions = questionsList,
                            knowledgePoints = new List<object>
                            {
                                new { id = "K-"+ye.id, title = "示例知识点", duration = "10:45" }
                            }
                        });
                    }

                    sectionsDict.Add(jie.id, new
                    {
                        name = jie.节名,
                        pages = pagesDict
                    });
                }

                dataSet.Add(zhang.id, new
                {
                    name = zhang.章名,
                    sections = sectionsDict
                });
            }

            return dataSet;
        }
      
        public string 分解(string 图书id )
        {
            DataTable dt = 数据库类.返回DataTable("select * from 图书试题表 where 图书id="+图书id);
            书类 book = new 书类();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string[] parts = dr["相对目录"].ToString().Trim().Split('\\'); // 分割输入字符串为行
                if (parts.Length < 4) continue;

                string 章名 = parts[0].Trim();
                string 节名 = parts[1].Trim();
                string 页名 = parts[2].Trim();
                string 试题名 = parts[3].Trim();

                // 处理章
                章类 zhang = book.zhangs.FirstOrDefault(z => z.章名 == 章名);
                if (zhang == null)
                {
                    zhang = new 章类
                    {
                        id = book.zhangs.Count + 1,
                        章名 = 章名
                    };
                    book.zhangs.Add(zhang);
                }

                // 处理节
                节类 jie = zhang.jies.FirstOrDefault(j => j.节名 == 节名);
                if (jie == null)
                {
                    jie = new 节类
                    {
                        id = zhang.jies.Count + 1,
                        节名 = 节名
                    };
                    zhang.jies.Add(jie);
                }

                // 处理页
                页类 ye = jie.yes.FirstOrDefault(y => y.页名 == 页名);
                if (ye == null)
                {
                    ye = new 页类
                    {
                        id = jie.yes.Count + 1,
                        页名 = 页名
                    };
                    jie.yes.Add(ye);
                }


                string str1 = dr["试题路径"].ToString().Replace("D:\\9013\\", "http://localhost:9013/") + 试题名 + ".html";
                ye.shitis.Add(new 试题类
                {
                    id = ye.shitis.Count + 1,
                    试题ID = dr["图书试题id"].ToString(),
                    试题名 = 试题名,
                    知识点 = dr["试题知识点"].ToString(),
                    试题html = str1,
                    答案html = str1.Replace("-题目.", "-答案.")
                });
            }
            // 转换为JS结构
            var jsStructure = ConvertToJsStructure(book);

            // 序列化为JSON
            string json = JsonConvert.SerializeObject(jsStructure, Formatting.Indented);
            //return book;
            return json;
        }
         
    }

    public class 书类
    {
        public List<章类> zhangs = new List<章类>(); 
    }

    public class 章类
    {
        public int id = 0;
        public string 章名 = "";
        public List<节类> jies = new List<节类>();
    }

    public class 节类
    {
        public int id = 0;
        public string 节名 = "";
        public List<页类> yes = new List<页类>();
    }

    public class 页类
    {
        public int id = 0;
        public string 页名 = "";
        public List<试题类> shitis = new List<试题类>();
    }

    public class 试题类
    {
        public int id = 0;
        public string 试题ID = "";
        public string 试题名 = "";
        public string 试题html = "";
        public string 答案html = "";
        public string 知识点 = "";
        public string http = "";
    }
    public class 知识点类
    {
        public int id = 0;
        public string title = "";
        public string duration = "";
        public string http = "";
    }


}
