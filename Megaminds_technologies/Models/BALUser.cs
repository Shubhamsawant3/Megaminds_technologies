using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace Megaminds_technologies.Models
{
	public class BALUser
	{
		SqlConnection conn = new SqlConnection("MegaMindsCrudDB");
		private readonly User _user;
		public BALUser(User objUser)
		{
			_user = objUser;
		}

		public DataSet List()
		{
			conn.Open();
			SqlCommand cmd = new SqlCommand("UserMindsProcedure", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Flag","ListData");
			SqlDataAdapter adpt  = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			adpt.Fill(ds);
			return ds;
		}
		public void Savedata()
		{
			conn.Open();
			SqlCommand cmd = new SqlCommand("UserMindsProcedure", conn);
			cmd.CommandType =CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Flag","Insertdata");
			cmd.Parameters.AddWithValue("@Name", _user.Name);
			cmd.Parameters.AddWithValue("@Email", _user.Email);
			cmd.Parameters.AddWithValue("@Phone", _user.Phone);
			cmd.Parameters.AddWithValue("@Address", _user.Address);
			cmd.Parameters.AddWithValue("@State", _user.State);
			cmd.Parameters.AddWithValue("@Flag", _user.City);
			cmd.ExecuteNonQuery();
			conn.Close();
		}
	}
}