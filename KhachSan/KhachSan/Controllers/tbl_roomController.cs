using KhachSan.Models.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ImageResizer;

namespace HotelManagmentSystem.Controllers
{
    public class tbl_roomController : Controller
    {
        private KSEntities db = new KSEntities();

        // GET: tbl_room
        public ActionResult Index()
        {
            var tbl_room = db.tbl_room.Include(t => t.tbl_booking_status).Include(t => t.tbl_room_type);
            return View(tbl_room.ToList());
        }

        // GET: tbl_room/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_room tbl_room = db.tbl_room.Find(id);
            if (tbl_room == null)
            {
                return HttpNotFound();
            }
            return View(tbl_room);
        }

        // GET: tbl_room/Create
        public ActionResult Create()
        {
            ViewBag.booking_status_id = new SelectList(db.tbl_booking_status, "booking_status_id", "booking_status");
            ViewBag.room_type_id = new SelectList(db.tbl_room_type, "room_type_id", "room_name");
            return View();
        }

        // POST: tbl_room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "room_id,room_number,room_description,room_type_id,booking_status_id,is_Active,room_image")] tbl_room tbl_room,HttpPostedFileBase room_image)
        {
            if (ModelState.IsValid)
            {
                if (room_image != null)
                {
                    var fileName = Path.GetFileName(room_image.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/ViewContent/img/rooms"), fileName);
                    tbl_room.room_image = fileName;

                    room_image.SaveAs(path);
                }

                db.tbl_room.Add(tbl_room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.booking_status_id = new SelectList(db.tbl_booking_status, "booking_status_id", "booking_status", tbl_room.booking_status_id);
            ViewBag.room_type_id = new SelectList(db.tbl_room_type, "room_type_id", "room_name", tbl_room.room_type_id);
            return View(tbl_room);
        }

        // GET: tbl_room/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_room tbl_room = db.tbl_room.Find(id);
            if (tbl_room == null)
            {
                return HttpNotFound();
            }
            ViewBag.booking_status_id = new SelectList(db.tbl_booking_status, "booking_status_id", "booking_status", tbl_room.booking_status_id);
            ViewBag.room_type_id = new SelectList(db.tbl_room_type, "room_type_id", "room_name", tbl_room.room_type_id);
            return View(tbl_room);
        }

        // POST: tbl_room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "room_id,room_number,room_description,room_type_id,booking_status_id,is_Active,room_image")] tbl_room tbl_room,HttpPostedFileBase room_image)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tbl_room).State = EntityState.Modified;
                var phong = db.tbl_room.FirstOrDefault(p => p.room_id ==tbl_room.room_id);
                if (phong != null)
                {
                    phong.room_number = tbl_room.room_number;
                    phong.room_description = tbl_room.room_description;
                    phong.room_type_id = tbl_room.room_type_id;
                    phong.booking_status_id = tbl_room.booking_status_id;
                    phong.is_Active = tbl_room.is_Active;
                    if (room_image != null)
                    {
                        //Lấy tên file của hình được up lên
                        var fileName = Path.GetFileName(room_image.FileName);
                        //Tạo đường dẫn tới file
                        var path = Path.Combine(Server.MapPath("~/Content/ViewContent/img/rooms"),fileName);
                        //Lưu tên
                        phong.room_image = fileName;
                        //Save vào Images Folder
                        room_image.SaveAs(path);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.booking_status_id = new SelectList(db.tbl_booking_status, "booking_status_id", "booking_status", tbl_room.booking_status_id);
            ViewBag.room_type_id = new SelectList(db.tbl_room_type, "room_type_id", "room_name", tbl_room.room_type_id);
            return View(tbl_room);
        }

        // GET: tbl_room/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_room tbl_room = db.tbl_room.Find(id);
            if (tbl_room == null)
            {
                return HttpNotFound();
            }
            return View(tbl_room);
        }

        // POST: tbl_room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_room tbl_room = db.tbl_room.Find(id);
            db.tbl_room.Remove(tbl_room);
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
