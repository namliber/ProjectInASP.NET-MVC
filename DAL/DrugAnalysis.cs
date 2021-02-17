using BE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DrugAnalysis
    {

        private DrugContent GetDrugContent<DrugContent>(string url)
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance
                return JsonConvert.DeserializeObject<DrugContent>(json_data);
            }
        }

        public string GetDrugsResult(string[] drugsNDCs)
        {
            List<string> drugsRx = new List<string>();
            foreach (var drug in drugsNDCs)
            {
                string myurl = "https://rxnav.nlm.nih.gov/REST/rxcui.json?idtype=NDC&id=" + drug;
                DrugContent content = GetDrugContent<DrugContent>(myurl);
                drugsRx.Add(content.idGroup.rxnormId[0]);
            }

            //the url go get the json from
            string RxURL = string.Empty;
            foreach (var drug in drugsRx)
            {
                RxURL += drug + "+";
            }
            RxURL= RxURL.Substring(0, RxURL.Length - 1); //remove the last +


            //Use the REST API for list of drags rxcui
            
            string url = "https://rxnav.nlm.nih.gov/REST/interaction/list.json?rxcuis=" + RxURL;
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class
                var results = JsonConvert.DeserializeObject<DrugsInteractionContent>(json_data);
                //return the results
                if (results.fullInteractionTypeGroup == null) return "No risks";
                string answer="";
                try
                {
                    answer = results.fullInteractionTypeGroup[0].fullInteractionType[0].interactionPair[0].description;
                    answer += "\n Severity: " + results.fullInteractionTypeGroup[0].fullInteractionType[0].interactionPair[0].severity;
                }
                catch (Exception) { }
                return answer;
            }

        }
    }
}
