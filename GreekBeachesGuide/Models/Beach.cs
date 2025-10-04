using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreekBeachesGuide.Models
{
    internal class Beach
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Region { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }
        public string? ImagePath { get; set; }
        public string? AudioPath { get; set; }
        public string? VideoPath { get; set; }
        public int Rating { get; set; }
        public bool IsTop { get; set; }
    }
}
