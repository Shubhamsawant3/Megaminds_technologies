﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Megaminds_technologies.Models
{
	public class User
	{
        public int Id { get; set; }
        public int StateId { get; set; }
		public int CityId { get; set; }
		public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string StateName { get; set; }
        public string CityName {  get; set; }
    }
}