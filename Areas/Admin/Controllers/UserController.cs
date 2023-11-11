using ASP.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ASP.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        WebSiteBanHangEntities obj = new WebSiteBanHangEntities();
        // GET: Admin/Users
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstUser = new List<User>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstUser = obj.Users.Where(n => n.FirstName.Contains(SearchString)).ToList();
            }
            else
            {
                lstUser = obj.Users.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstUser = lstUser.OrderByDescending(n => n.Id).ToList();
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            //this.LoadData();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            //this.LoadData();
            if (ModelState.IsValid)
            {
                var check = obj.Users.FirstOrDefault(s => s.Email == user.Email);
                if (check == null)
                {
                    user.Password = GetMD5(user.Password);
                    user.IsAdmin = false; //IsAdmin = false = người dùng
                    obj.Configuration.ValidateOnSaveEnabled = false;
                    // add user
                    obj.Users.Add(user);
                    //lưu thông tin lại
                    obj.SaveChanges();
                    // trả về trang chủ
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";


                    return View();
                }

            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var Users = obj.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(Users);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var Users = obj.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(Users);
        }
        [HttpPost]
        public ActionResult Delete(User objuser)
        {
            var objUser = obj.Users.Where(n => n.Id == objuser.Id).FirstOrDefault();
            obj.Users.Remove(objUser);
            obj.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //this.LoadData();

            var users = obj.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(users);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, User objUser)
        {
            objUser.Password = GetMD5(objUser.Password);
            obj.Entry(objUser).State = EntityState.Modified;
            obj.SaveChanges();

            return RedirectToAction("Index");
        }
        // mã hóa pass word
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;

        }
    }
}