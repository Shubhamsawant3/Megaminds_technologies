using Megaminds_technologies.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Megaminds_technologies.Controllers
{
    public class UserController : Controller
    {
		

		[HttpGet]
        public ActionResult List()
        {
			
			BALUser _baluser = new BALUser();
			DataSet ds = _baluser.List();
            List<User> datalist = new List<User>();
			foreach (DataRow dr in ds.Tables[0].Rows)
            {
				User _user = new User();
				_user.Id =Convert.ToInt32(dr["id"]);
				_user.Name = dr["Name"].ToString();
                _user.Email = dr["Email"].ToString();
                _user.Phone = dr["Phone"].ToString();
                datalist.Add(_user);
            }
            return View(datalist);
        }
        [HttpGet]
        public ActionResult Create()
        {
			BALUser _baluser = new BALUser();
            SqlDataReader dr = _baluser.GetStates();
			List<User> states = new List<User>();
			while (dr.Read())
			{
				states.Add(new User
				{
					StateId = Convert.ToInt32(dr["Id"]),
					StateName = dr["StateName"].ToString()
				});
			}
			ViewBag.StatesBag = new SelectList(states, "StateId", "StateName");
			return View();
		}
		[HttpPost]
		public JsonResult GetCities(int stateId)
		{
			BALUser _baluser = new BALUser();
			SqlDataReader dr = _baluser.GetCities(stateId);
			List<User> cities = new List<User>();

			while (dr.Read())
			{
				cities.Add(new User
				{
					CityId = Convert.ToInt32(dr["Id"]),
					CityName = dr["CityName"].ToString()
				});
			}

			return Json(cities, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
        public ActionResult Create(User _objUser)
        {
			BALUser _baluser = new BALUser();
            _baluser.Savedata(_objUser);
			return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
			User _user = new User();
			BALUser _baluser = new BALUser();
			DataSet ds = _baluser.FetchUserdata(id);
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				_user.Id = Convert.ToInt32(dr["UserId"].ToString());
				_user.Name = dr["Name"].ToString();
				_user.Email = dr["Email"].ToString();
				_user.Address = dr["Address"].ToString();
				_user.Phone = dr["Phone"].ToString();
				_user.StateId = Convert.ToInt32(dr["StateId"]);
				_user.StateName = dr["CityName"].ToString();
				_user.CityId = Convert.ToInt32(dr["CityId"]);
				_user.CityName = dr["CityName"].ToString();
			}
			Create();
			// Reuse existing function to get cities for the selected state
			SqlDataReader drCities = _baluser.GetCities(_user.StateId);
			List<User> cities = new List<User>();
			while (drCities.Read())
			{
				cities.Add(new User
				{
					CityId = Convert.ToInt32(drCities["Id"]),
					CityName = drCities["CityName"].ToString()
				});
			}
			ViewBag.CitiesBag = cities;
			return View(_user);
        }
        [HttpPost]
        public ActionResult Edit(User _objUser)
        {
			BALUser _objbalUser = new BALUser();
			_objbalUser.UpdateData(_objUser);
            return RedirectToAction("List");
        }
		[HttpGet]
		public ActionResult Delete(int id)
		{
			BALUser _user = new BALUser();
			_user.Deletedata(id);
			return RedirectToAction("List");
		}
    }
}