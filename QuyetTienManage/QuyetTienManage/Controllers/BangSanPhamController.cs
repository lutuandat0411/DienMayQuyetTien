using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuyetTienManage.Models;
using System.Transactions;
namespace QuyetTienManage.Controllers
{
    public class BangSanPhamController : Controller
    {
        private CS4PEEntities db = new CS4PEEntities();

        // GET: /BangSanPham/
        public ActionResult Index()
        {
            var bangsanphams = db.BangSanPhams.Include(b => b.LoaiSanPham);
            return View(bangsanphams.ToList());
        }

        // GET: /BangSanPham/Details/5
        public FileResult Details(String id)
        {
            var path = Server.MapPath("~/App_Data/" + id);
            return File(path, "HinhAnh");
        }

        // GET: /BangSanPham/Create
        public ActionResult Create()
        {
            ViewBag.Loai_id = new SelectList(db.LoaiSanPhams, "id", "TenLoai");
            return View();
        }

        // POST: /BangSanPham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,MaSP,TenSP,Loai_id,GiaBan,GiaGoc,GiaGop,SoLuongTon")] BangSanPham model)
        {
            CheckBangSanPham(model);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.BangSanPhams.Add(model);
                    db.SaveChanges();

                    var path = Server.MapPath("~/App_Data");
                    path = path + "/" + model.id;
                    if (Request.Files["HinhAnh"] != null &&
                       Request.Files["HinhAnh"].ContentLength > 0)
                    {
                        Request.Files["HinhAnh"].SaveAs(path);

                        scope.Complete(); // approve for transaction
                        return RedirectToAction("Index");
                    }
                    else
                        ModelState.AddModelError("HinhAnh", "Chưa có hình sản phẩm!");   
                }

            }

            ViewBag.Loai_id = new SelectList(db.LoaiSanPhams, "id", "TenLoai", model.Loai_id);
            return View(model);
        }

        private void CheckBangSanPham(BangSanPham model) 
        {
            if (model.GiaGoc < 0)
                ModelState.AddModelError("GiaGoc", "Giá Gốc phải lớn hơn 0!");
            if (model.GiaBan < model.GiaGoc)
                ModelState.AddModelError("GiaBan", "Giá Bán phải lớn hơn giá gốc");
            if (model.GiaGop < model.GiaBan)
                ModelState.AddModelError("GiaGop", "Giá Góp phải lớn hơn giá bán");

        }
        // GET: /BangSanPham/Edit/5
        public ActionResult Edit(int id)
        {
       
            BangSanPham model = db.BangSanPhams.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.Loai_id = new SelectList(db.LoaiSanPhams, "id", "TenLoai", model.Loai_id);
            return View(model);
        }

        // POST: /BangSanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,MaSP,TenSP,Loai_id,GiaBan,GiaGoc,GiaGop,SoLuongTon")] BangSanPham model)
        {
            CheckBangSanPham(model);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    var path = Server.MapPath("~/App_Data");
                    path = path + "/" + model.id;
                    if (Request.Files["HinhAnh"] != null &&
                       Request.Files["HinhAnh"].ContentLength > 0)
                    {
                        Request.Files["HinhAnh"].SaveAs(path);
                    }
                        scope.Complete(); // approve for transaction
                        return RedirectToAction("Index");
                    
                }
            }
            ViewBag.Loai_id = new SelectList(db.LoaiSanPhams, "id", "TenLoai", model.Loai_id);
            return View(model);
        }

        // GET: /BangSanPham/Delete/5
        public ActionResult Delete(int? id)
        {
           if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangSanPham model = db.BangSanPhams.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        
        }

        // POST: /BangSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BangSanPham model = db.BangSanPhams.Find(id);
            db.BangSanPhams.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
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
