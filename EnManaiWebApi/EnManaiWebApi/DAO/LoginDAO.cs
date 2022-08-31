using EnManaiWebApi.Model.Request;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data.SqlClient;
using System.Data;
using EnManaiWebApi.Model;
using System.Collections.Generic;
using System.Collections;
using Dapper;

namespace EnManaiWebApi.DAO
{
    public interface ILoginDAO
    {
        List<Login> GetAll();
        Login GetLogin(int id, string username);
        int InsertLogin(Login login,string connStr);
    }
    public class LoginDAO : ILoginDAO
    {
        public List<Login> GetAll()
        {
            throw new NotImplementedException();
        }

        public Login GetLogin(int id, string username)
        {
            throw new NotImplementedException();
        }
        public int InsertLogin(Login login,string connStr)
        {
            int count = 0;
            IDbConnection db = null;
            try
            {
                string @sql = @"INSERT INTO LOGIN(username,
                password,
                phonenumber,
                emailid,
                createddate,
                modifieddate,
                createdby,
                modifiedby) values(@username,
                @password,
                @phonenumber,
                @emailid,
                @createddate,
                @modifieddate,
                @createdby,
                @modifiedby)";

                var dic = new Dictionary<string,object>();

                dic.Add("UserName", login.UserName);
                dic.Add("Password", login.Password);
                dic.Add("CreatedDate", login.CreatedDate); 
                dic.Add("ModifiedDate", login.ModifiedDate); 
                dic.Add("CreatedBy", login.CreatedBy); 
                dic.Add("ModifiedBy", login.ModifiedBy); 
                dic.Add("PhoneNumber", login.PhoneNumber);
                dic.Add("Emailid", login.EMailId);

                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    count = db.Execute(sql, dic);

                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally{
                if (db != null)
                {
                    db.Close();
                }
            }
        }
    }
}
