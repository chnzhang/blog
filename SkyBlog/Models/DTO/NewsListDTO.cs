using System;
using System.Collections.Generic;
using SkyBlog.Models.DbModel;

namespace SkyBlog.Models.DTO
{
    public class NewsListDTO
    {
        public int id{get;set;}
        public string title{get;set;}
        public string image{get;set;}
        public string description{get;set;}
        public string userName{get;set;}
        public string categoryName{get;set;}
        public DateTime createDateTime{get;set;}
        public int number{get;set;}
        public int goodNumber{get;set;}
        public List<tag> tag{get;set;}
    }
}