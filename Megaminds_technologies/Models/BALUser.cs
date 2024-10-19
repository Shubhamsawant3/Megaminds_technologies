using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace Megaminds_technologies.Models
{
	public class BALUser
	{
		static string _connectionString = ConfigurationManager.ConnectionStrings["MegaMindsCrudDB"].ConnectionString;
		public DataSet List()
		{
			SqlConnection conn = new SqlConnection(_connectionString); 
			SqlCommand cmd = new SqlCommand("UserMindsProcedure",conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Flag","ListData");
			SqlDataAdapter adpt  = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			adpt.Fill(ds);
			return ds;
		}
		public SqlDataReader GetStates()
		{
			SqlConnection conn = new SqlConnection(_connectionString);
			SqlCommand cmd = new SqlCommand("UserMindsProcedure", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Flag", "GetStates");
			conn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			return dr;
		}
		public SqlDataReader GetCities(int stateid)
		{
			User objUser = new User();
			SqlConnection conn = new SqlConnection(_connectionString);
			SqlCommand cmd = new SqlCommand("UserMindsProcedure", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Flag", "GetCities");
			cmd.Parameters.AddWithValue("@StateId", stateid);
			conn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			return dr;
		}
		public void Savedata(User _user)
		{
			SqlConnection conn = new SqlConnection(_connectionString);
			SqlCommand cmd = new SqlCommand("UserMindsProcedure",conn);
			cmd.CommandType =CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Flag", "InsertData");
			cmd.Parameters.AddWithValue("@Name", _user.Name);
			cmd.Parameters.AddWithValue("@Email", _user.Email);
			cmd.Parameters.AddWithValue("@Phone", _user.Phone);
			cmd.Parameters.AddWithValue("@Address", _user.Address);
			cmd.Parameters.AddWithValue("@StateId", _user.StateId);
			cmd.Parameters.AddWithValue("@CityId", _user.CityId);
			conn.Open();
			cmd.ExecuteNonQuery();
		}
		public DataSet FetchUserdata(int userid)
		{
			User objUser = new User();
			SqlConnection conn = new SqlConnection(_connectionString);
			SqlCommand cmd = new SqlCommand("UserMindsProcedure", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Flag", "Fetchuserdata");
			cmd.Parameters.AddWithValue("@UserId", userid);
			SqlDataAdapter adpt = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			adpt.Fill(ds);
			return ds;
		}
		public void UpdateData(User _user)
		{
			SqlConnection conn = new SqlConnection(_connectionString);
			SqlCommand cmd = new SqlCommand("UserMindsProcedure", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Flag", "Updatedata");
			cmd.Parameters.AddWithValue("@UserId", _user.Id);
			cmd.Parameters.AddWithValue("@Name", _user.Name);
			cmd.Parameters.AddWithValue("@Email", _user.Email);
			cmd.Parameters.AddWithValue("@Phone", _user.Phone);
			cmd.Parameters.AddWithValue("@Address", _user.Address);
			cmd.Parameters.AddWithValue("@StateId", _user.StateId);
			cmd.Parameters.AddWithValue("@CityId", _user.CityId);
			conn.Open();
			cmd.ExecuteNonQuery();
		}
		public void Deletedata(int id)
		{
			SqlConnection conn = new SqlConnection(_connectionString);
			SqlCommand cmd = new SqlCommand("UserMindsProcedure", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Flag", "Deletedata");
			cmd.Parameters.AddWithValue("@UserId", id);
			conn.Open();
			cmd.ExecuteNonQuery();
		}
	}
}