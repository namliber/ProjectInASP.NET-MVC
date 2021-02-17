using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medical
{
    public static class ExtendHtmlHelper
    {
        public static IHtmlString DisplayImage(this HtmlHelper helper, string ImageUrl, int size)
        {
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("src", ImageUrl);
            tb.Attributes.Add("style", $"width:{size}px");
            return new MvcHtmlString(tb.ToString());
        }
    }
}