using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Common;
using ECommerce.Repository;
using ECommerceSample.Areas.Admin.Models.ResultModel;
using ECommerceSample.Areas.Admin.Models.ViewModel;

namespace ECommerceSample.Areas.Admin.Controllers
{
    public class MemberController : Controller
    {
        MemberRep mr = new MemberRep();
        ResultInstance<Member> result = new ResultInstance<Member>();

        // GET: Admin/Member
        public ActionResult List()
        {
            result.resultList = mr.List();
            return View(result.resultList.ProccessResult);
        }

        public ActionResult AddMember()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMember(Member model, HttpPostedFileBase photoPath)
        {
            string photoName = "";
            if (photoPath != null)
            {
                photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + photoName);
                model.Photo = photoName;
                photoPath.SaveAs(path);
            }
            model.RoleID = 1;
            result.resultint = mr.Insert(model);
            if (result.resultint.IsSucceded)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        public ActionResult EditMember(int id)
        {
            result.resultT = mr.GetById(id);
            return View(result.resultT.ProccessResult);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditMember(Member model, HttpPostedFileBase photoPath)
        {
            model.RoleID = 1;
            string photoName = model.Photo;
            string fullpath = Request.MapPath("~/Upload/" + model.Photo);
            if (photoPath != null)
            {
                photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + photoName);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                model.Photo = photoName;
                photoPath.SaveAs(path);
            }
            result.resultint = mr.Update(model);
            return RedirectToAction("List");
        }
    }
}