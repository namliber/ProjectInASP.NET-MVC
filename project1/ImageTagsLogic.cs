using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ImageTagsLogic
    {
        public List<string> GetTags(string path)
        {
            List<string> Result = new List<string>();

            ImageContent DrugImage = new ImageContent(path);

            ImageAnalysis dal = new ImageAnalysis();

            dal.GetTags(DrugImage);

            var Threshold = 50.0;

            foreach (var item in DrugImage.Details)
            {
                if (item.Value > Threshold)
                {
                    Result.Add(item.Key);
                }
                else
                {
                    break;
                }
            }

            return Result;
        }

        public List<string> DrugsTags = new List<string>
        {
            "prescription drug",
            "bottle",
            "pill bottle",
            "pill",
            "pills",
            "medicine",
            "container"

        };
    }
}
