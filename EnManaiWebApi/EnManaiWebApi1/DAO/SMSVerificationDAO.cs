using Dapper;
using EnManaiWebApi.Model;
using EnManaiWebApi.Model.Request;
using System.Data;
using System.Data.SqlClient;

namespace EnManaiWebApi.DAO
{
    public interface ISMSVerificationDAO
    {
        public SMSVerification CreateOrUpdateSMSVerification(SMSVerification smsVerification, string connStr, int BlockAfterAttempt, int BlockForDays);
        public SMSVerification GetSMSVerification(SMSVerification smsVerification, string connStr);
        public SMSVerification UpdateSMSVerification(SMSVerification smsVerification, string connStr);
        public SMSVerification VerifySMSCode(SMSCodeVerificationRequest smsVerification, string connStr);
    }

    public class SMSVerificationDAO : ISMSVerificationDAO
    {
        /// <summary>
        /// Create or update smsverification table
        /// </summary>
        /// <param name="sms"> SMS Verification Object</param>
        /// <param name="connStr"> Connection string</param>
        /// <param name="BlockAfterAttempt">to check wether user need block further sms verification </param>
        /// <param name="BlockForDays">No of days to block when user attains maximum of number times</param>
        /// <returns></returns>
        public SMSVerification CreateOrUpdateSMSVerification(SMSVerification sms, string connStr, int BlockAfterAttempt, int BlockForDays)
        {
            IDbConnection db = null;
            SMSVerification smsVerification = null;
            try
            {
                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    Dictionary<string, object> dic = new Dictionary<string, object>() {
                        { "@Username",sms.Username},
                        { "@LoginId",sms.LoginId},
                        { "@Phonenumber",sms.Phonenumber},
                        { "@SMSSendDateTime",sms.SMSSendDateTime},
                        { "@SMSExpiryDateTime",sms.SMSExpiryDateTime},
                        { "@Status",sms.Status},
                        { "@NoOfVerificationPerDay",sms.NoOfVerificationPerDay},
                        { "@BlockForDays",sms.BlockForDays},
                        { "@BlockAfterAttempt",sms.BlockAfterAttempt},
                        { "@CreatedOn",$"{sms.CreatedOn.Month}-{sms.CreatedOn.Day}-{sms.CreatedOn.Year}"},
                        { "@CreatedBy",sms.CreatedBy},
                        { "@ModifiedOn",sms.ModifiedOn},
                        { "@ModifiedBy",sms.ModifiedBy},
                        { "@TotalNoAttempt",sms.TotalNoAttempt},
                        { "@SMSCode",sms.SMSCode},
                    };
                    int totalAttempt = GetUserTotalAttempt(dic, connStr);
                    dic["@TotalNoAttempt"] = (totalAttempt + 1).ToString();
                    dic["@Status"] = "Active";
                    //SELECT* FROM SMSVerification WHERE CAST(createdon as DATE) = cast('01-09-2023' as date)
                    string sql = "SELECT * FROM SMSVerification where username = @username and loginid = @loginid and CAST(createdon as DATE) = cast(@createdon as date) and phonenumber = @phonenumber";
                    smsVerification = db.QuerySingleOrDefault<SMSVerification>(sql, dic);

                    if (smsVerification == null)
                    {
                        dic["@NoOfVerificationPerDay"] = 1;
                        string insertSql = "INSERT INTO SMSVERIFICATION (Username, LoginId, PhoneNumber, SMSSendDateTime, SMSExpiryDateTime, Status," +
                            " NoOfVerificationPerDay, BlockForDays,TotalNoAttempt, BlockAfterAttempt, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy, SMSCode) values(@Username, @LoginId," +
                            "@Phonenumber, @SMSSendDateTime, @SMSExpiryDateTime, @Status, @NoOfVerificationPerDay, " +
                            "@BlockForDays, @TotalNoAttempt, @BlockAfterAttempt, @CreatedOn, @CreatedBy, @ModifiedOn, @ModifiedBy, @SMSCode) SELECT SCOPE_IDENTITY ()";

                        int id = db.ExecuteScalar<int>(insertSql, dic);

                        Dictionary<string, object> idDic = new Dictionary<string, object>()
                       {
                           {"@id",id }
                       };
                        smsVerification = db.QuerySingleOrDefault<SMSVerification>("SELECT * FROM SMSVERIFICATION WHERE ID=@id", idDic);
                    }
                    else
                    {
                        dic["@NoOfVerificationPerDay"] = (smsVerification.NoOfVerificationPerDay + 1).ToString();
                        string updateSql = "UPDATE SMSVERIFICATION set SMSSendDateTime = @SMSSendDateTime, SMSExpiryDateTime = @SMSExpiryDateTime, Status = @Status," +
                            "NoOfVerificationPerDay = @NoOfVerificationPerDay, BlockForDays = @BlockForDays, TotalNoAttempt = @TotalNoAttempt, BlockAfterAttempt = @BlockAfterAttempt, CreatedBy = @CreatedBy, ModifiedOn = @ModifiedOn, SMSCode = @SMSCode, ModifiedBy = @ModifiedBy WHERE CAST(createdon as DATE) = cast(@createdon as date) and  LoginId = @LoginId " +
                            " and Phonenumber = @Phonenumber ";
                        db.Execute(updateSql, dic);
                        smsVerification = db.QuerySingle<SMSVerification>("SELECT * FROM SMSVERIFICATION WHERE username = @username and loginid = @loginid and CAST(createdon as DATE) = cast(@createdon as date) and phonenumber = @phonenumber", dic);
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
            return smsVerification;
        }
        /// <summary>
        /// To get total no. of attempt till date
        /// </summary>
        /// <param name="dic">Contains all the user sms verification data</param>
        /// <param name="conn">connection object</param>
        /// <returns></returns>
        private int GetUserTotalAttempt(Dictionary<string, object> dic, string connStr)
        {
            IDbConnection db = null;
            int totalAttempt = 0;
            using (db = new SqlConnection(connStr))
            {
                string sql = "SELECT totalnoattempt FROM SMSVerification where username = @username and loginid = @loginid";
                //Dictionary<string, object> dic = new Dictionary<string, object>() { 
                //    { "@loginid", LoginId },
                //    { "@username", userName }
                //};
                
                if (db != null)
                {
                    totalAttempt = db.ExecuteScalar<int>(sql, dic);
                }
            }
            return totalAttempt;
        }

        /// <summary>
        /// To get today smsverfication record for user so if exists update or insert.
        /// </summary>
        /// <param name="smsVerification">sms verification object</param>
        /// <param name="connStr">connection string</param>
        /// <returns></returns>
        public SMSVerification GetSMSVerification(SMSVerification smsVerification, string connStr)
        {
            IDbConnection db = null;
            try
            {
                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    Dictionary<string, object> dic = new Dictionary<string, object>() {
                        { "@Username",smsVerification.Username},
                        { "@LoginId",smsVerification.LoginId},
                        { "@Phonenumber",smsVerification.Phonenumber},
                        { "@CreatedOn",$"{smsVerification.CreatedOn.Month}-{smsVerification.CreatedOn.Day}-{smsVerification.CreatedOn.Year}"}
                    };

                    //SELECT* FROM SMSVerification WHERE CAST(createdon as DATE) = cast('01-09-2023' as date)
                    string sql = "SELECT * FROM SMSVerification where username = @username and loginid = @loginid and CAST(createdon as DATE) = cast(@createdon as date) and phonenumber = @phonenumber";
                    smsVerification = db.QuerySingleOrDefault<SMSVerification>(sql, dic);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return smsVerification;
        }

        public SMSVerification UpdateSMSVerification(SMSVerification smsVerification, string connStr)
        {
            IDbConnection db = null;
            SMSVerification sms = null;
            try
            {
                using (db = new SqlConnection(connStr))
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>() {
                        { "@Username",smsVerification.Username},
                        { "@LoginId",smsVerification.LoginId},
                        { "@Phonenumber",smsVerification.Phonenumber},
                        //{ "@Status",smsVerification.Status},
                        { "@CreatedOn",$"{smsVerification.CreatedOn.Month}-{smsVerification.CreatedOn.Day}-{smsVerification.CreatedOn.Year}"},
                        { "@TotalNoAttempt",smsVerification.TotalNoAttempt},
                    };
                    string updateSql = "Update SMSVerification set Status='Blocked', TotalNoAttempt=@TotalNoAttempt where username = @username and loginid = @loginid and CAST(createdon as DATE) = cast(@createdon as date) and phonenumber = @phonenumber";
                    db.ExecuteScalar(updateSql, dic);
                }
            }catch (Exception ex)
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
            
            return sms;
        }

        public SMSVerification UpdateSMSStatus(SMSVerification smsVerification, string connStr, bool Status)
        {
            IDbConnection db = null;
            SMSVerification sms = null;
            try
            {
                using (db = new SqlConnection(connStr))
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>() {
                        { "@LastSMSSendStatus",Status}
                    };
                    string updateSql = "Update SMSVerification set LastSMSSendStatus=@LastSMSSendStatus, where username = @username and loginid = @loginid and CAST(createdon as DATE) = cast(@createdon as date) and phonenumber = @phonenumber";
                    db.ExecuteScalar(updateSql, dic);
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

            return sms;
        }

        public SMSVerification VerifySMSCode(SMSCodeVerificationRequest smsVerification, string connStr)
        {
            IDbConnection db = null;
            SMSVerification sms = null;
            try { 

            using (db = new SqlConnection(connStr))
            {
                db.Open();
                Dictionary<string, object> dic = new Dictionary<string, object>() {
                        { "@Username",smsVerification.Username},
                        { "@LoginId",smsVerification.LoginId},
                        { "@Phonenumber",smsVerification.Phonenumber},
                        //{ "@Status",smsVerification.Status},
                        { "@CreatedOn",$"{smsVerification.CreatedOn.Month}-{smsVerification.CreatedOn.Day}-{smsVerification.CreatedOn.Year}"},
                    };
                string VerifySQL = "SELECT * FROM SMSVerification where username = @username and loginid = @loginid and CAST(createdon as DATE) = cast(@createdon as date) and phonenumber = @phonenumber";
                sms = db.QuerySingleOrDefault<SMSVerification>(VerifySQL, dic);
            }
            }catch(Exception ex)
            {
                throw ;
            }
            finally
            {
                if(db != null)
                {
                    db.Close();
                }
            }
            return sms;
        }
    }
}

