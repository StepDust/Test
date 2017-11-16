using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace EBuy.Areas.WebFunction.Controllers {
    public class CrawlerController : Controller {
        // GET: WebFunction/Crawler
        public ActionResult Index(ReqData<News> data)
        {
            return View(data);
        }

        [HttpPost]
        public ActionResult Index(string url)
        {
            Crawler crawler = new Crawler();

            crawler.OnCompleted += Crawler_OnCompleted;

            crawler.Start(url, "<a[^>]+?href=[\"']?https://www.ithome.com/html/([^\"']+)[\"']?[^>]*>(.+?)</a>").Wait();

            return View();
        }

        private void Crawler_OnCompleted(object sender, OnCompletedEventArgs e)
        {
            // e.PageSource = DataCheck.RepTrim(e.PageSource);

            string[] str = DataCheck.GetRegStrArr(e.PageSource, e.Regex);

        }

    }

    public class News {
        public string Title { get; set; }
        public string Url { get; set; }
    }

}