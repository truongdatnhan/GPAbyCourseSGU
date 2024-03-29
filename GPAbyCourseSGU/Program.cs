﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GPAbyCourseSGU
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<Student> list = new List<Student>();

            String courseLink = "http://thongtindaotao.sgu.edu.vn/Default.aspx?page=danhsachsvtheonhomhoc&malop=DCT1184&madk=84130703%20%2001";
            HtmlWeb web = new HtmlWeb();
            web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0";
            var doc = web.Load(courseLink);
            var nodesTotal = doc.DocumentNode.SelectNodes("//table").ToList();
            var nodes = nodesTotal[4].SelectNodes("tr[position()>1]").ToList();
            foreach (var node in nodes)
                {
                    var student = new Student
                    {
                        MSSV = node.SelectSingleNode("td[2]/span").InnerText,
                        FirstName = node.SelectSingleNode("td[3]/span").InnerText,
                        LastName = node.SelectSingleNode("td[4]/span").InnerText,
                        Class = node.SelectSingleNode("td[5]/span").InnerText
                    };

                    list.Add(student);
               }
           
            foreach(var student in list)
            {
                String gpaLink = $"http://thongtindaotao.sgu.edu.vn/Default.aspx?page=xemdiemthi&id={student.MSSV}";
                doc = web.Load(gpaLink);
                var nodesGpaTotal = doc.DocumentNode.SelectNodes("//table").ToList();
                var nodesGpa = nodesGpaTotal[4].SelectNodes("tr[@class='row-diemTK']").ToList();
                student.LastedGPA = nodesGpa[nodesGpa.Count - 6].SelectSingleNode("td/span[2]").InnerText;
                student.TotalGPA = nodesGpa[nodesGpa.Count - 4].SelectSingleNode("td/span[2]").InnerText;
            }

            list.ForEach(x => Console.WriteLine(x.StudentInfo));
            File.WriteAllLines(@"D:\List.txt", list.Select(x => x.StudentInfo).ToList());
        }
    }
}
