using BE;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class ImageAnalysis
    {
        public void GetTags(ImageContent CurrentImage)
        {
            string apiKey = "acc_fdcae2250dc93ab";
            string apiSecret = "1dd2c07fff743949a6e68f47cfadf5e9";
            string image = CurrentImage.ImagePath;

            string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", apiKey, apiSecret)));

            var client = new RestClient("https://api.imagga.com/v2/tags");
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", String.Format("Basic {0}", basicAuthValue));
            request.AddFile("image", image);

            IRestResponse response = client.Execute(request);
            Root TagList = JsonConvert.DeserializeObject<Root>(response.Content);

            foreach (var item in TagList.result.tags)
            {
                CurrentImage.Details.Add(item.tag.en, item.confidence);
            }

        }
    }

    public class Tag2
    {
        public string en { get; set; }
    }

    public class Tag
    {
        public double confidence { get; set; }
        public Tag2 tag { get; set; }
    }

    public class Result
    {
        public List<Tag> tags { get; set; }
    }

    public class Status
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Root
    {
        public Result result { get; set; }
        public Status status { get; set; }
    }
}
