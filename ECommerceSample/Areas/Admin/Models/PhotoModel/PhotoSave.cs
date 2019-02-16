using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Entity;
using ECommerce.Common;
using ECommerce.Repository;
using System.Web.Mvc;

namespace ECommerceSample.Areas.Admin.Models.PhotoModel
{
    public class PhotoSave:Controller
    {
        public void AddPhoto(string PhotoName,HttpPostedFileBase PhotoPath)
        {
            // inserting photo
            if (PhotoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + PhotoName);
                PhotoPath.SaveAs(path);
            }
        }

        public void UpdatePhoto(string PhotoName, HttpPostedFileBase PhotoPath)
        {
            string fullPath = Request.MapPath("~/Upload/" + PhotoName);
            if (PhotoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + PhotoName);
                //img deleting
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                PhotoPath.SaveAs(path);
            }
        }
    }
}