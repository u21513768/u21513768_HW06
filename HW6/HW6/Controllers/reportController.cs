using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW6.Models;

namespace HW6.Controllers
{
    public class reportController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: report
        public ActionResult Index()
        {
            var orders = from o in db.orders select o;
            
            Report newReport = new Report();
            newReport.Totals = new List<int>();

            List<order> Order = orders.ToList();

            for (int i = 1; i <= 12; i++)
            {
                int totalTemp = 0;
                var orderItemTemp = Order.Where(x => x.order_date.Month == i).Select(x => x.order_items);

                foreach (var order in orderItemTemp)
                {
                    var productTemp = order.Where(x => x.product.category.category_name == "Mountain Bikes").Select(x => x.quantity).Sum();
                    totalTemp += productTemp;
                }

                newReport.Totals.Add(totalTemp);
            }

            db.Configuration.ProxyCreationEnabled = false;
            return View(newReport);
        }
    }
}

//newReport.Years = db.orders.Select(r => r.order_date.Year).Distinct();
//newReport.Months = (new System.Globalization.CultureInfo("en-US")).DateTimeFormat.MonthNames;
//newReport.Totals = new List<List<int>>();
//foreach(var year in newReport.Years)
//{
//    List<int> totalTemp = new List<int>();
//    for(int i = 1; i < 13; i++)
//    {
//        var total = 0;
//        var orderItemTemp = newReport.Order.Where(x => x.order_date.Year == year && x.order_date.Month == i).Select(x => x.order_items);
//        //var total = orderItemTemp.Select(c => c.)
//        foreach(var ord in orderItemTemp)
//        {
//            foreach(var item in ord)
//            {
//                total += item.quantity;
//            }
//        }
//        totalTemp.Add(total);
//    }
//    newReport.Totals.Add(totalTemp);
//}