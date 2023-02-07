using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ProjectUserRegistration.Controllers
{
    public class HomeController : Controller
    {
        FirstWebEntities db = new FirstWebEntities();

        public ActionResult Index ()
        {
            return View();
        }

        public ActionResult Registration ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration (tbl_User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.tbl_User.Add(user);
                    db.SaveChanges();
                    TempData["Msg"] = "Registration Completed SuccessFully!!";
                    return RedirectToAction("Registration");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Failed To Register" + e.Message;
                return RedirectToAction("Registration");
            }
        }

        public ActionResult Login ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login (tbl_User objuser)
        {
            var user = db.tbl_User.Where(x => x.UserName == objuser.UserName && x.Password == objuser.Password).Count();
            if (user > 0)
            {
                return RedirectToAction("Showuser");
            }
            else
            {
                return View();
            }
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Showuser (string SortOrder, string SortBy, string searchText, int PageNumber = 1)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.SortBy = SortBy;
            var list = db.tbl_User.ToList();

            if (searchText != null)
            {
                list = db.tbl_User.Where(x => x.UserName.Contains(searchText) || x.Email.Contains(searchText) || x.Gender.Contains(searchText) || x.Password.Contains(searchText)).ToList();
                list = ApplySorting(SortOrder, SortBy, list);
                list = ApplyPaginition(list, PageNumber);
            }
            else
            {
                list = ApplySorting(SortOrder, SortBy, list);
                list = ApplyPaginition(list, PageNumber);
            }
            return View(list);
        }

        public ActionResult Delete (int id)
        {
            var user = db.tbl_User.Find(id);
            db.tbl_User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Showuser");
        }

        [HttpGet]
        public ActionResult Userprofile (int id)
        {
            var user = db.tbl_User.Find(id);
            user.DobStr = Convert.ToDateTime(user.DOB).ToString("dd/MM/yyyy");
            user.IsIntrestedIncSharp = (user.IsIntrestedIncSharp == null) ? false : user.IsIntrestedIncSharp;
            user.IsIntrestedInJava = (user.IsIntrestedInJava == null) ? false : user.IsIntrestedInJava;
            user.IsIntrestedInPython = (user.IsIntrestedInPython == null) ? false : user.IsIntrestedInPython;

            var cityList = db.tbl_City.ToList();
            user.CityList = new SelectList(cityList, "CityId", "CityName");

            if (user.ImagePath == null)
            {
                user.ImagePath = "~/Images/no_image.png";
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Userprofile (tbl_User user, string cSharp, string java, string python)
        {
            DateTime d = DateTime.MinValue;
            if (DateTime.TryParse(user.DobStr.ToString(), out d))
            {
                user.DOB = d;
            }
            //else
            //{

            //}

            //user.DOB = Convert.ToDateTime(user.DobStr);
            user.IsIntrestedIncSharp = (cSharp == "true") ? true : false;
            user.IsIntrestedInJava = (java == "true") ? true : false;
            user.IsIntrestedInPython = (python == "true") ? true : false;

            if (user.UserImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(user.UserImageFile.FileName);
                string extension = Path.GetExtension(user.UserImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                user.ImagePath = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                user.UserImageFile.SaveAs(fileName);
            }

            if (user.ImagePath == "~/Images/no_image.png")
            {
                user.ImagePath = null;
            }

            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Userprofile", new { id = user.UserId });
        }
        public List<tbl_User> ApplySorting (string SortOrder, string SortBy, List<tbl_User> list)
        {
            switch (SortBy)
            {
                case "UserName":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    list = list.OrderBy(x => x.UserName).ToList();
                                    break;
                                }
                            case "Dsc":
                                {
                                    list = list.OrderByDescending(x => x.UserName).ToList();
                                    break;
                                }
                            default:
                                {
                                    list = list.OrderBy(x => x.UserName).ToList();
                                    break;
                                }
                        }

                        break;
                    }
                case "Email":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    list = list.OrderBy(x => x.Email).ToList();
                                    break;
                                }
                            case "Dsc":
                                {
                                    list = list.OrderByDescending(x => x.Email).ToList();
                                    break;
                                }
                            default:
                                {
                                    list = list.OrderBy(x => x.Email).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                case "Password":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    list = list.OrderBy(x => x.Password).ToList();
                                    break;
                                }
                            case "Dsc":
                                {
                                    list = list.OrderByDescending(x => x.Password).ToList();
                                    break;
                                }
                            default:
                                {
                                    list = list.OrderBy(x => x.Password).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                case "Gender":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    list = list.OrderBy(x => x.Gender).ToList();
                                    break;
                                }
                            case "Dsc":
                                {
                                    list = list.OrderByDescending(x => x.Gender).ToList();
                                    break;
                                }
                            default:
                                {
                                    list = list.OrderBy(x => x.Gender).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        list = list.OrderBy(x => x.UserName).ToList();
                        break;
                    }
            }
            return list;
        }

        public List<tbl_User> ApplyPaginition (List<tbl_User> list, int PageNumber = 1)
        {
            ViewBag.TotalPages = Math.Ceiling(list.Count() / 40.0);
            ViewBag.PageNumber = PageNumber;
            list = list.Skip((PageNumber - 1) * 40).Take(40).ToList();
            return list;
        }
    }
}