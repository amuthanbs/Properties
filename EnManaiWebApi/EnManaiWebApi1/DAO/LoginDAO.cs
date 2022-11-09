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
        List<Login> GetAll(string connStr);
        Login GetLogin(int id, string username, string password, string connStr);
        int InsertLogin(Login login, string connStr);
        int updateLogin(Login login, string connStr);
        public Login getUserDetails(int userID, string connStr);
        public PaymentForTenant GetPayment(int id, string username, string connStr);
    }
    public class LoginDAO : ILoginDAO
    {
        public List<Login> GetAll(string connStr)
        {
            IDbConnection db = null;
            try
            {
                string @sql = @"SELECT * FROM LOGIN";

                List<Login> count = new List<Login>();

                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    count = db.Query<Login>(sql).AsList();
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
        }

        public Login GetLogin(int id, string username, string password,string connStr)
        {
            IDbConnection db = null;
            try
            {
                string @sql = @"SELECT * FROM LOGIN WHERE username=@username and password=@password";
                var dic = new Dictionary<string, object>()
                {
                    {"username",username },
                    {"password",password }
                };
                //var dicPayment = new Dictionary<string, object>()
                //{
                //    {"id",username }
                //};
                List<Login> count = new List<Login>();

                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    count = db.Query<Login>(sql, dic).AsList();
                }
                //int id = count.FirstOrDefault().ID;
                return count.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
        }
        public PaymentForTenant GetPayment(int id, string username, string connStr)
        {
            PaymentForTenant paymentForTenant;
            IDbConnection db = null;
            try
            {
                string @sql = @"SELECT * FROM PaymentForTenant WHERE TenantId=@id";
                var dic = new Dictionary<string, object>()
                {
                    {"id",id}
                };
                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    paymentForTenant = db.Query<PaymentForTenant>(sql, dic).AsList().FirstOrDefault();

                }
                if(paymentForTenant != null)
                {
                    UpdatePaymentStatus(paymentForTenant, connStr);
                }
                
                //int id = count.FirstOrDefault().ID;
                return paymentForTenant;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
        }
        private PaymentForTenant UpdatePaymentStatus(PaymentForTenant pft, string connStr)
        {
            IDbConnection db = null;
            try
            {
                int it = DateTime.Now.Date.CompareTo(pft.PaymentExpiryDate.Date);

                if (DateTime.Compare(DateTime.Now, pft.PaymentExpiryDate) > 0)
                {
                    using (db = new SqlConnection(connStr))
                    {
                        db.Open();
                        string sSql = @"UPDATE PAYMENTFORTENANT SET PaymentExpired=1 WHERE ID=@id";
                        var dic = new Dictionary<string, object>()
                    {
                        {"id",pft.Id}
                    };
                        var count = db.Query<Scheme>(sSql, dic).AsList().FirstOrDefault();
                        //(scheme == null )? return scheme : return null;
                        pft.PaymentExpired = true;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
            return pft;
        }
        public int InsertLogin(Login login, string connStr)
        {
            int count = 0;
            IDbConnection db = null;
            try
            {
                var dic = new Dictionary<string, object>();

                dic.Add("UserName", login.UserName);
                dic.Add("Password", login.Password);
                dic.Add("CreatedDate", login.CreatedDate);
                dic.Add("ModifiedDate", login.ModifiedDate);
                dic.Add("CreatedBy", login.CreatedBy);
                dic.Add("ModifiedBy", login.ModifiedBy);
                dic.Add("PhoneNumber", login.PhoneNumber);
                dic.Add("Emailid", login.EMailId);


                Login l = GetLogin(0, dic.FirstOrDefault(x => x.Key == "UserName").Value.ToString(),"", connStr);
                if (l != null)
                {
                    throw new Exception($"Username already exists:{login.UserName}");
                }

                string sql = @"INSERT INTO LOGIN(username,
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
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
        }

        public Login getUserDetails(int userID, string connStr)
        {

            IDbConnection db = null;
            try
            {
                string @sql = @"SELECT * FROM LOGIN WHERE ID=@Id";
                var dic = new Dictionary<string, object>()
                {
                    {"id",@userID }
                };
                Login login = new Login();

                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    login = db.Query<Login>(sql, dic).FirstOrDefault();
                }
                return login;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
        }

        public int updateLogin(Login login, string connStr)
        {
            int count = 0;
            IDbConnection db = null;
            try
            {
                string @sql = @"Update LOGIN SET username = @username,
                password = @password,
                phonenumber = @phonenumber,
                emailid = @emailid,
                houseownerid = @houseownerid,
                tenantid = @tenantid,
                phonenumberverified = @phonenumberverified,
                emailverified = @emailverified,
                reverificationtime = @reverificationtime,
                phonenumberverifieddate = @phonenumberverifieddate,
                emailverifieddate = @emailverifieddate,
                MandatoryVerification = @MandatoryVerification,
                ReVerification = @ReVerification,
                createddate = @createddate,
                modifieddate = @modifieddate,
                createdby = @createdby,
                modifiedby) values(@username,
                @password,
                @phonenumber,
                @emailid,
                @houseownerid,
                @tenantid,
                @phonenumberverified,
                @emailverified,
                @reverificationtime,
                @phonenumberverifieddate,
                @emailverifieddate,
                @MandatoryVerification,
                @ReVerification,
                @createddate,
                @modifieddate,
                @createdby,
                @modifiedby)";

                var dic = new Dictionary<string, object>();

                dic.Add("UserName", login.UserName);
                dic.Add("Password", login.Password);
                dic.Add("CreatedDate", login.CreatedDate);
                dic.Add("ModifiedDate", login.ModifiedDate);
                dic.Add("CreatedBy", login.CreatedBy);
                dic.Add("ModifiedBy", login.ModifiedBy);
                dic.Add("PhoneNumber", login.PhoneNumber);
                dic.Add("Emailid", login.EMailId);
                dic.Add("houseownerid", login.HouseOwnerId);
                dic.Add("tenantid", login.TenantId);
                dic.Add("phonenumberverified", login.PhoneNumberVerified);
                dic.Add("emailverified", login.EmailVerified);
                dic.Add("reverificationtime", login.ReverficationTime);
                dic.Add("phonenumberverifieddate", login.PhoneNumberVerifiedDate);
                dic.Add("emailverifieddate", login.EmailIdVerifiedDate);
                dic.Add("MandatoryVerification", login.MandatoryVerification);
                dic.Add("ReVerification", login.ReVerification);
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
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
        }
    }
}
