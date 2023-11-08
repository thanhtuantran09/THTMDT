using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THTMDT.Models;
namespace THTMDT.Controllers
{
    public class TinTucController : Controller
    {
        BaodientuEntities db = new BaodientuEntities();
        // GET: TinTuc

        public ActionResult TinTuc(int? size, int? page, string currentFilter, string searchString)
        {
            var email = Session["Email"] as string;
            if (email == null)
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("DangNhapTacGia", "DangNhap");
            }

            var nguoidung = db.TacGias.FirstOrDefault(kh => kh.Email == email);
            if (nguoidung != null) // Kiểm tra xem người dùng có tồn tại hay không
            {
                // Lấy thông tin của cửa hàng nếu tồn tại, sau đó lưu ID của cửa hàng vào sản phẩm
                var maTG = nguoidung.MaTG;

                // Lấy danh sách bài báo của tác giả (người dùng đăng nhập)
                var baiBaos = db.BaiBaos
                    .Where(bb => bb.MaTG == maTG)
                    .OrderByDescending(bb => bb.NgayTao);

                // Thực hiện tìm kiếm nếu có chuỗi tìm kiếm
                if (!string.IsNullOrEmpty(searchString))
                {

                    baiBaos = (IOrderedQueryable<BaiBao>)baiBaos.Where(p => p.TieuDe.Contains(searchString));
                }

                // Lưu trạng thái tìm kiếm hiện tại
                currentFilter = searchString;

                var pageNumber = page ?? 1;
                int pageSize = size ?? 10;
                var pagedBaiBaos = baiBaos
                    .ToPagedList(pageNumber, pageSize);

                return View(pagedBaiBaos);
            }

            return RedirectToAction("ErrorPage"); // Xử lý khi không tìm thấy người dùng
        }

        public ActionResult TaoTinTuc()
        {

            BaiBao pro = new BaiBao();
            return View(pro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult TaoTinTuc(BaiBao model)
        {
            if (ModelState.IsValid)
            {
                var email = Session["Email"] as string;
                if (email == null)
                {
                    // Nếu chưa đăng nhập, chuyển hướng đến trang đăng nhập
                    return RedirectToAction("DangNhapTacGia", "DangNhap");
                }
                // Lấy thông tin của người dùng trong cơ sở dữ liệu
                var nguoidung = db.TacGias.FirstOrDefault(kh => kh.Email == email);

                if (nguoidung != null) // Kiểm tra xem người dùng có tồn tại hay không
                {
                    // Lấy thông tin của cửa hàng nếu tồn tại, sau đó lưu ID của cửa hàng vào sản phẩm

                    model.MaTG = nguoidung.MaTG;
                    model.NgayTao = DateTime.Now;
                    model.MaTT = 1;
                }
                // Lưu ảnh vào thư mục ~/Content/Images/ 
                if (model.UploadImage1 != null)
                {
                    string filename1 = Path.GetFileNameWithoutExtension(model.UploadImage1.FileName);
                    string extension1 = Path.GetExtension(model.UploadImage1.FileName);
                    filename1 = filename1 + extension1;
                    model.HinhAnh = "~/Content/img/" + filename1;
                    model.UploadImage1.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), filename1));
                    // gan cac du lieu vao cai lay len 

                }
                db.BaiBaos.Add(model);
                db.SaveChanges();
                return RedirectToAction("TinTuc");
            }
            return View(model);
        }
        public ActionResult CapNhatTinTuc(int id)
        {
            return View(db.BaiBaos.Where(s => s.MaBB == id).FirstOrDefault());
        }

        // POST: Admin/Hang/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatTinTuc(BaiBao model)
        {
            if (ModelState.IsValid)
            {
                var tt = db.BaiBaos.Find(model.MaBB);

                if (model.UploadImage1 != null)
                {
                    string filename1 = Path.GetFileNameWithoutExtension(model.UploadImage1.FileName);
                    string extension1 = Path.GetExtension(model.UploadImage1.FileName);
                    filename1 = filename1 + extension1;
                    model.HinhAnh = "~/Content/img/" + filename1;
                    model.UploadImage1.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), filename1));
                    // gan cac du lieu vao cai lay len 

                }

                tt.TieuDe = model.TieuDe;
                tt.NoiDung = model.NoiDung;
                tt.HinhAnh = model.HinhAnh;


                db.SaveChanges();
                return RedirectToAction("TinTuc", "TinTuc");
            }

            return View(model);
        }
        public ActionResult ChonDanhMuc()
        {
            Danhmuc danhhmuc = new Danhmuc();
            danhhmuc.ListDM = db.Danhmucs.ToList<Danhmuc>();
            return PartialView(danhhmuc);
        }
        public ActionResult ChonPheDuyet()
        {
            TTBB pheduyet = new TTBB();
            pheduyet.ListPD = db.TTBBs.ToList<TTBB>();
            return PartialView(pheduyet);
        }

        public ActionResult QLTinTuc(int? size, int? page, string currentFilter, string searchString)
        {
            // Kiểm tra quyền truy cập theo CHUCVUID 

            var email = Session["Email"] as string;
            var admin = db.ADs.FirstOrDefault(c => c.Email == email);
          
                var thongtin = new List<BaiBao>();
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                if (!string.IsNullOrEmpty(searchString))
                {
                    //lấy ds tên nv theo tu khóa tim kiếm 
                    thongtin = db.BaiBaos.Where(n => n.TieuDe.Contains(searchString)).ToList();

                }
                else
                {
                    //lấy ds tên nv theo bảng NV 
                    thongtin = db.BaiBaos.ToList();
                }
                ViewBag.CurrenFilter = searchString;
                // 1. Tạo list pageSize để người dùng có thể chọn xem để phân trang 
                // Bạn có thể thêm bớt tùy ý  
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "5", Value = "5" });
                items.Add(new SelectListItem { Text = "10", Value = "10" });
                items.Add(new SelectListItem { Text = "20", Value = "20" });


                // 1.1. Giữ trạng thái kích thước trang được chọn trên DropDownList 
                foreach (var item in items)
                {
                    if (item.Value == size.ToString()) item.Selected = true;
                }

                // 1.2. Tạo các biến ViewBag 
                ViewBag.size = items; // ViewBag DropDownList 
                ViewBag.currentSize = size; // tạo biến kích thước trang hiện tại 

                // 2. Nếu page = null thì đặt lại là 1. 
                page = page ?? 1; //if (page == null) page = 1; 

                // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy 
                // theo LinkID mới có thể phân trang. 
                thongtin = thongtin.OrderBy(n => n.MaBB).ToList();

                // 4. Tạo kích thước trang (pageSize), mặc định là 5. 
                int pageSize = (size ?? 5);

                // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn 
                // nếu page = null thì lấy giá trị 1 cho biến pageNumber. 
                int pageNumber = (page ?? 1);
                //4.2 Lấy tổng số record chia cho kích thuốc để biết bao nhiêu trang 
                int checkTotal = (int)(thongtin.ToList().Count / pageSize) + 1;
                //Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tống số trang 
                if (pageNumber > checkTotal) pageNumber = checkTotal;

                // 5. Trả về các Link được phân trang theo kích thước và số trang. 
                return View(thongtin.ToPagedList(pageNumber, pageSize));
          
        }
        public ActionResult PheDuyetTinTuc(int id)
        {
            return View(db.BaiBaos.Where(s => s.MaBB == id).FirstOrDefault());
        }

        // POST: Admin/Hang/Edit/5
        [HttpPost]
        public ActionResult PheDuyetTinTuc(BaiBao model)
        {
            try
            {
                var ad = db.BaiBaos.Find(model.MaBB);
                // TODO: Add update logic here
                ad.MaTT = model.MaTT;
               

                db.SaveChanges();
                return RedirectToAction("QLTinTuc");
            }
            catch
            {
                return View();
            }
        }

    }
}

