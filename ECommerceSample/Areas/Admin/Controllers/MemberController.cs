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
        MemberViewModel mvm = new MemberViewModel();
        MemberRep mr = new MemberRep();
        ResultInstance<Member> result = new ResultInstance<Member>();
        

        // GET: Admin/Member
        public ActionResult List()
        {
            result.resultList = mr.List();
            return View(result.resultList.ProccessResult);
        }
        //roleName lerin modele gönderilerek kayıt isleminin yapılması icin metot olusturdum.
        private void RoleList(List<SelectListItem> roleList,int id)
        {
            foreach (var item in mr.RoleList().ProccessResult)
            {
                roleList.Add(new SelectListItem
                {
                    Value = item.RoleID.ToString(),
                    Text = item.RoleName,
                    Selected=(item.RoleID==id)
                });
                
            }

        }
        public ActionResult AddMember()
        {
            List<SelectListItem> roleList = new List<SelectListItem>();
            RoleList(roleList,-1);
            mvm.roles = roleList;
            mvm.members = null;
            return View(mvm);
        }

        [HttpPost]
        public ActionResult AddMember(MemberViewModel model, HttpPostedFileBase photoPath)
        {
            string photoName = "";
            if (photoPath != null)
            {
                photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/Member/" + photoName);
                model.members.Photo = photoName;
                photoPath.SaveAs(path);
            }
            result.resultint = mr.Insert(model.members);
            if (result.resultint.IsSucceded)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        public ActionResult EditMember(int id)
        {
            List<SelectListItem> roleList = new List<SelectListItem>();
            result.resultT = mr.GetById(id);
            int roleId = result.resultT.ProccessResult.UserRole.RoleID;
            RoleList(roleList,roleId);
            mvm.members = result.resultT.ProccessResult;
            mvm.roles = roleList;
            if (result.resultT.IsSucceded)
            {
                return View(mvm);
            }
            return RedirectToAction("List");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditMember(MemberViewModel model, HttpPostedFileBase photoPath)
        {
            string photoName = model.members.Photo;
            if (photoPath != null)
            {
                photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/Member/" + photoName);
                string fullpath = Request.MapPath("~/Upload/Member/" + model.members.Photo);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                model.members.Photo = photoName;
                photoPath.SaveAs(path);
            }
            result.resultint = mr.Update(model.members);
            return RedirectToAction("List");
        }
    }
}