﻿namespace Blog.API.Models.Entities
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public string BlogSummary { get; set; }
        public string UrlHandle { get; set; }
        public string BlogImageUrl { get; set; }
        public bool Visible { get; set; }
        public string BlogAuthor { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EditedDate { get; set; }
    }
}
