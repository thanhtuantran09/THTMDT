using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THTMDT.Models;
namespace THTMDT.Controllers
{
    public class DangNhapController : Controller
    {
        BaodientuEntities db=new BaodientuEntities();
        // GET: DangNhap
        public ActionResult DangNhapTacGia()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhapTacGia(TacGia tacgia)
        {
            var objUserGet = db.TacGias.Where(n => n.Email.Equals(tacgia.Email) && n.Matkhau.Equals(tacgia.Matkhau)).FirstOrDefault();

            if (objUserGet == null)
            {
                ViewBag.error = "Email hay Mật khẩu không đúng vui lòng nhập lại!";
                return View();
            }
            else
             Session["Email"] = objUserGet.Email;
            Session["Ten"] = objUserGet.TenTG;
           
            return RedirectToAction("TinTuc", "TinTuc");


        }
        public ActionResult DangXuat()
        {
            Session["Email"] = null;
            return RedirectToAction("DangNhapTacGia", "DangNhap");
        }

        public ActionResult DangNhapAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhapAdmin(AD ad)
        {
            var objUserGet = db.ADs.Where(n => n.Email.Equals(ad.Email) && n.Pass.Equals(ad.Pass)).FirstOrDefault();

            if (objUserGet == null)
            {
                ViewBag.error = "Email hay Mật khẩu không đúng vui lòng nhập lại!";
                return View();
            }
            else
            Session["Email"] = objUserGet.Email;
            Session["Ten"] = objUserGet.TenAdmin;

            return RedirectToAction("QLTinTuc", "TinTuc");


        }
        public ActionResult DangXuatAdmin()
        {
            Session["Email"] = null;
            return RedirectToAction("DangNhapAdmin", "DangNhap");
        }
    }
}