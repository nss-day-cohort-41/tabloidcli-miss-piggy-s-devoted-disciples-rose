using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TabloidCLI.Models;

namespace TabloidCLI.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public override string ToString()
        {
            return $"{Title} ({Url})";
        }
    }
}