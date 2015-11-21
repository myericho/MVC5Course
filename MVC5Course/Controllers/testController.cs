﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.ActionFillters;

namespace MVC5Course.Controllers
{
    [全站共用viewBag的titleAttribute]
    public class testController : BaseController
    {
        // GET: test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult testPartial()
        {
            return PartialView();
        }

        public ActionResult ContentIndex()
        {
            return Content("<h1>hello world</h1>", "text/plain");
        }

        public string ContentString()
        {
            return "<h1>hello world</h1>";
        }

        public ActionResult FileTest()
        {
            string filePath = Server.MapPath(@"~/Content/344774.jpg");
            string contentType = "image/jepg";
            return File(filePath, contentType);
        }

        public ActionResult FileTestRename()
        {
            string filePath = Server.MapPath(@"~/Content/344774.jpg");
            string contentType = "image/jepg";
            return File(filePath, contentType, "我自定的檔名.jpg");
        }

        public ActionResult JsonIndex()
        {
            return View();
        }

        public ActionResult JsonData()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var data = db.Product.Take(5).ToList();

            return Json(data);

            //return Json(new
            //{
            //    id = 1,
            //    name = "hello",
            //    test = "lock"

            //});
        }


        public ActionResult RedirecToIndex()
        {
            return RedirectPermanent("Index");
        }


        public ActionResult testRazor(bool enable=true)
        {
            ViewBag.IsEnbled = enable;
            int[] data = new int[] { 1, 2, 3, 4, 5 };
            return View(data);
        }
    }
}