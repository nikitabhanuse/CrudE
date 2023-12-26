using CrudE.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CrudE.Models
{
    public class RegModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }

        public string SaveTest(HttpPostedFileBase fb, RegModel model)
        {
            string msg = "Submit";
            CrudEEntities db = new CrudEEntities();

            string filepath = "";
            string fileName = "";
            string sysFileName = "";
            if (fb != null && fb.ContentLength > 0)
            {
                filepath = HttpContext.Current.Server.MapPath("../Content/Photo");
                DirectoryInfo di = new DirectoryInfo(filepath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fb.FileName;
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb.FileName);
                fb.SaveAs(filepath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fb.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("/Content/Photo") + "/" + sysFileName;
                }
            }

            if (model.Id == 0)
            {

                var Imgdata = new tblReg()
                {
                    Id = model.Id, 
                    Name = model.Name,
                    Photo = sysFileName,
                    Address = model.Address,
                    Age = model.Age,

                };
                db.tblRegs.Add(Imgdata);
                db.SaveChanges();
                msg = "Data Saved";
            }
            else
            {
                var Imgdata = db.tblRegs.Where(p => p.Id == model.Id).FirstOrDefault();
                if (Imgdata != null)
                {
                    Imgdata.Id = model.Id;
                    Imgdata.Photo = sysFileName;
                    Imgdata.Name = model.Name;
                    Imgdata.Address = model.Address;
                    Imgdata.Age = model.Age;



                };
                db.SaveChanges();
                msg = "Updated Successfully";
            }
            return msg;
        }

        public List<RegModel> getTestList(String Searchtxt)
        {
            CrudEEntities db = new CrudEEntities();
            List<RegModel> lstReg = new List<RegModel>();
            var Pdata = db.tblRegs.Where(p =>(p.Name.Contains(Searchtxt))).ToList();
            if (Pdata != null)
            {
                foreach (var reg in Pdata)
                {
                    lstReg.Add(new RegModel()
                    {
                        Id = reg.Id,
                        Name = reg.Name,
                        Photo = reg.Photo,
                        Address = reg.Address,
                        Age = reg.Age
                    });
                }
            }
            return lstReg;


        }
        public string deleteTest(int Id)
        {
            CrudEEntities db = new CrudEEntities();
            var msg = "Deleted";
            var deleteP = db.tblRegs.Where(p => p.Id == Id).FirstOrDefault();
            if (deleteP != null)
            {
                db.tblRegs.Remove(deleteP);
                db.SaveChanges();

            }
            return msg;
        }

        public RegModel editTest(int Id)
        {
            RegModel model= new RegModel();
            CrudEEntities db = new CrudEEntities();
            var editTest = db.tblRegs.Where( p => p.Id == Id).FirstOrDefault();
            if(editTest != null)
            {
                model.Id = editTest.Id;
                model.Name = editTest.Name;
                model.Photo = editTest.Photo;
                model.Address = editTest.Address;
                model.Age = editTest.Age;
            }
            return model;

        }



    }
}