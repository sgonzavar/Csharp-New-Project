using ApiWeb.Models;
using ApiWeb.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiWeb.Controllers
{
    public class OrderController : Controller
    {

        // GET: Order
        public ActionResult Index()
        {

            try
            {
                xyzContext db = new xyzContext();

                //List<tbl_order> lista =  db.tbl_order.ToList();

                return View(db.tbl_order.ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        #region "Add"
        public ActionResult add() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add(tbl_order order)
        {
            Rule rules = new Rule(order.day, order.request, order.typeVehicle, order.quantity);

            if (!ModelState.IsValid)
                return View();

            try
            {

                using (var db = new xyzContext())
                {
                    db.tbl_order.Add(order);            
                    db.SaveChanges();                  
                }

                using (var db1 = new xyzContext())
                {
                    tbl_order order1 = new tbl_order();
                    
                    rules.order();
                    order.day = rules.AssignedDay;
                    order.quantity = rules.QuantityAvalible;
                    order.request = rules.Request;
                    order.typeVehicle = rules.TypeVehicle;

                    order1 = order;
                    db1.tbl_order.Add(order1);
                
                    db1.SaveChanges();
                }

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al crear", ex.Message);
                return View();
            }                                               
        }

        #endregion

        #region "Edit"
        public ActionResult edit(int id) 
        {
            try
            {
                using (var db = new xyzContext())
                {
                    //tbl_order order = db.tbl_order.Where(o => o.id == id).FirstOrDefault();
                    tbl_order order1 = db.tbl_order.Find(id);
                    return View(order1);
                }
            }
            catch (Exception)
            {
                throw;
            }           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edit( tbl_order order ) 
        {
            try
            {
                using (var db = new xyzContext())
                {
                    tbl_order order1 = db.tbl_order.Find(order.id);
                    order1.day = order.day;
                    order1.request = order.request;
                    order1.typeVehicle = order.typeVehicle;
                    order1.quantity = order.quantity;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }                
            }
            catch (Exception)
            {
                throw;
            }

            
        }

        #endregion

        #region "Delete"

        public ActionResult delete(int id)
        {
            using (var db = new xyzContext())
            {
                tbl_order order1 = db.tbl_order.Find(id);
                db.tbl_order.Remove(order1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        #endregion





    }
}