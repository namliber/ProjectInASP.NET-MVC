using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ImageContent
    {
        public string ImagePath { get; set; }

        public Dictionary<string, double> Details { get; set; }

        public ImageContent() { }
        public ImageContent(string ImagePath)
        {
            this.ImagePath = ImagePath;
            Details = new Dictionary<string, double>();
        }
    }
}
