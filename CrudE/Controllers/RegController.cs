using CrudE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace CrudE.Controllers
{
    public class RegController : Controller
    {
        // GET: Reg
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult detailIndex(int Id)
        {
            ViewBag.Id= Id;

            return View();
        }

        public ActionResult SaveTest(RegModel model)
        {
            try
            {
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];
                }
                return Json(new {message= new RegModel().SaveTest(fb,model)},JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message },JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getTestList(string Searchtxt)
        {
            try
            {
                return Json(new { model = (new RegModel().getTestList(Searchtxt))}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new {model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult deleteTest(int Id) 
        {
            try
            {
                return Json(new { model = new RegModel().deleteTest(Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult editTest(int Id)
        {
            try
            {
                return Json(new { model = (new RegModel().editTest(Id)) }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { ex.Message },JsonRequestBehavior.AllowGet);
            }
        }
    }
}
