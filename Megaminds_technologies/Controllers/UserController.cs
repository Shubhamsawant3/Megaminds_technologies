using Megaminds_technologies.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Megaminds_technologies.Controllers
{
    public class UserController : Controller
    {
		User _user = new User();
		BALUser _baluser = new BALUser();

		[HttpGet]
        public ActionResult List()
        {
			DataSet ds = _baluser.List();
            List<User> datalist = new List<User>();
			foreach (DataRow dr in ds.Tables[0].Rows)
            {
                _user.Name = dr["Name"].ToString();
                _user.Email = dr["Email"].ToString();
                _user.Phone = dr["Phone"].ToString();
                datalist.Add(_user);
            }
            return View(datalist);
        }
    }
}