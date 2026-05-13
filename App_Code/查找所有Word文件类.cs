using System;
using System.Collections.Generic;
using System.IO;

public class 查找所有word文件
{
    public static List<string> GetAllDocxFiles(string rootDirectory)
    {
        var docxFiles = new List<string>();

        if (!Directory.Exists(rootDirectory))
        {
            throw new DirectoryNotFoundException("Directory not found: {rootDirectory}");
        }

        try
        {
            //// 递归搜索所有 .docx 文件（忽略权限不足的目录）
            //foreach (var file in Directory.EnumerateFiles(
            //             rootDirectory,
            //             "*.docx",
            //             new EnumerationOptions
            //             {
            //                 IgnoreInaccessible = true,
            //                 RecurseSubdirectories = true
            //             }))
            //{
            //    docxFiles.Add(file);
            //}
        }
        // 处理根目录的访问权限异常
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Access denied to directory: {rootDirectory}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error scanning directory: {ex.Message}");
        }

        return docxFiles;
    }
    static void Main1()
    {
        List<string> dd = 查找所有word文件.GetAllDocxFiles(@"C:\Users\Admini\Desktop\aaa");
    }
}
