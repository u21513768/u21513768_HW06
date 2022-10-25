using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW6.Models;
using PagedList;

namespace HW6.Controllers
{
    public class ordersController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: orders
        public ActionResult Index(string currentFilterTextbox, string DueDate, int? page)
        {
            //var products = db.products.Include(p => p.brand).Include(p => p.category);
            if (DueDate != null)
            {
                page = 1;
            }
            else
            {
                DueDate = currentFilterTextbox;
            }

            ViewBag.CurrentFilterTextbox = DueDate;

            //var orders = db.orders.Include(o => o.customer).Include(o => o.staff).Include(o => o.store);
            var ordersTemp = from o in db.orders
                         select o;
            var productsTemp = from p in db.products
                            select p;
            var orderItemsTemp = from oi in db.order_items
                              select oi;

            var result = (from a in ordersTemp
                          join b in orderItemsTemp on a.order_id equals b.order_id into tempTable01
                          from b in tempTable01.ToList()
                          join c in productsTemp on b.product_id equals c.product_id into tempTable02
                          from c in tempTable02.ToList()
                          select new OrderDetails
                          {
                              Order = a,
                              OrderItem = b,
                              Product = c
                          });
            result = result.GroupBy(x => x.Order).Select(g => g.FirstOrDefault());

            if (!String.IsNullOrEmpty(DueDate))
            {
                var tempDate = Convert.ToDateTime(DueDate);
                result = result.Where(xx => xx.Order.order_date == tempDate);
            }

            result = result.OrderBy(p => p.Order.order_id);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
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
