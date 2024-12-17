using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TX2.Models;

namespace TX2.Controllers
{
    public class HocSinhsController : Controller
    {
        private HocSinhDb db = new HocSinhDb();

        // GET: HocSinhs
        public async Task<ActionResult> Index()
        {
            var hocSinh = db.HocSinh.Include(h => h.LopHoc);
            return View(await hocSinh.ToListAsync());
        }

        // GET: HocSinhs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = await db.HocSinh.FindAsync(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // GET: HocSinhs/Create
        public ActionResult Create()
        {
            ViewBag.malop = new SelectList(db.LopHoc, "malop", "tenlop");
            return View();
        }

        // POST: HocSinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "sbd,hoten,anhduthi,malop,diemthi")] HocSinh hocSinh)
        {
            if (ModelState.IsValid)
            {
                db.HocSinh.Add(hocSinh);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.malop = new SelectList(db.LopHoc, "malop", "tenlop", hocSinh.malop);
            return View(hocSinh);
        }

        // GET: HocSinhs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = await db.HocSinh.FindAsync(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.malop = new SelectList(db.LopHoc, "malop", "tenlop", hocSinh.malop);
            return View(hocSinh);
        }

        // POST: HocSinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "sbd,hoten,anhduthi,malop,diemthi")] HocSinh hocSinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hocSinh).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.malop = new SelectList(db.LopHoc, "malop", "tenlop", hocSinh.malop);
            return View(hocSinh);
        }

        // GET: HocSinhs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = await db.HocSinh.FindAsync(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // POST: HocSinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            HocSinh hocSinh = await db.HocSinh.FindAsync(id);
            try
            {
                db.HocSinh.Remove(hocSinh);
                await db.SaveChangesAsync();
                TempData["Message"] = "Xóa học sinh thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Lỗi khi xóa học sinh: " + ex.Message;
                return RedirectToAction("Index");
            }
            
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        // Định nghĩa là GET method
        public async Task<ActionResult> GetHocSinhs(string tukhoa = "")
        {
            IQueryable<HocSinh> hocSinhs = db.HocSinh.Include(h => h.LopHoc);

            if (!string.IsNullOrEmpty(tukhoa))
            {
                hocSinhs = hocSinhs.Where(s => s.hoten.Contains(tukhoa) || s.sbd.Contains(tukhoa));
            }

            List<HocSinh> result = await hocSinhs.ToListAsync();

            if (result == null || result.Count == 0)
            {
                return HttpNotFound(); // Trả về 404 Not Found nếu không tìm thấy
            }

            return View(result); // Trả về 200 OK với dữ liệu
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
