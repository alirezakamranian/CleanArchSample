using Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ArticleAggregate
{
    public class Article
    {
        private Article() { }
        public Article(string title, string content, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));

            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be empty.", nameof(content));

            Title = title;
            Content = content;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }

        //UserRel
        public Guid UserId { get; private set; }
        public ApplicationUser User { get; private set; }

        public static Article Create() => new();
        public void UpdateTitle(string title) => Title = title;
        public void UpdateContent(string content) => Content = content;
    }
}
