﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class OrderLinesController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: OrderLines
        public ActionResult Index(int productId, string orderSatsus)
        {
            ViewBag.productId = productId;
            ViewBag.OrderStatusSelected = orderSatsus;

            var orderLineList = from o in db.Order
                                group o by o.OrderStatus into g
                                select g.Key;
            ViewBag.orderSatsus = new SelectList(orderLineList);
            
            var orderLine = db.OrderLine.Where(p => p.ProductId == productId);
            if (!string.IsNullOrEmpty(orderSatsus))
            {
                orderLine = orderLine.Where(p => p.Order.OrderStatus == orderSatsus);
            }
            return PartialView("Index",orderLine.ToList());
        }

        // GET: OrderLines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLine orderLine = db.OrderLine.Find(id);



            if (orderLine == null)
            {
                return HttpNotFound();
            }
            return View(orderLine);
        }

        // GET: OrderLines/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Order, "OrderId", "OrderStatus");
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName");
            return View();
        }

        // POST: OrderLines/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,LineNumber,ProductId,Qty,LineTotal")] OrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                db.OrderLine.Add(orderLine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Order, "OrderId", "OrderStatus", orderLine.OrderId);
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName", orderLine.ProductId);
            return View(orderLine);
        }

        // GET: OrderLines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLine orderLine = db.OrderLine.Find(id);
            if (orderLine == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Order, "OrderId", "OrderStatus", orderLine.OrderId);
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName", orderLine.ProductId);
            return View(orderLine);
        }

        // POST: OrderLines/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,LineNumber,ProductId,Qty,LineTotal")] OrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderLine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Order, "OrderId", "OrderStatus", orderLine.OrderId);
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName", orderLine.ProductId);
            return View(orderLine);
        }

   
   
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int OrderId, int LineNumber, string orderSatsus)
        {
            OrderLine ol = db.OrderLine.Find(OrderId,LineNumber);
            db.OrderLine.Remove(ol);
            db.SaveChanges();

            return Index(ol.ProductId, orderSatsus);

            //TODO 先笨一點，將Index中的方法拉進來
            //ViewBag.productId = ol.ProductId;
            //ViewBag.OrderStatusSelected = orderSatsus;

            //var orderLineList = from o in db.Order
            //                    group o by o.OrderStatus into g
            //                    select g.Key;
            //ViewBag.orderSatsus = new SelectList(orderLineList);

            //var orderLine = db.OrderLine.Where(p => p.ProductId == ol.ProductId);
            //if (!string.IsNullOrEmpty(orderSatsus))
            //{
            //    orderLine = orderLine.Where(p => p.Order.OrderStatus == orderSatsus);
            //}
            //return PartialView("index",orderLine.ToList());


            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
