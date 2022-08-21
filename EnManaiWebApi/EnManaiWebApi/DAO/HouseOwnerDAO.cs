using EnManaiWebApi.Model;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace EnManaiWebApi.DAO
{
    public interface IHouseOwnerDAO
    {
        public List<HouseOwner> GetAll(string connStr, ILogger _logger);
        public HouseOwner GetById(int id,string connStr);
        public HouseOwner Save(HouseOwner houseOwner, string connStr);
        public HouseOwner Create(HouseOwner houseOwner, string connStr);
        public HouseOwner Delete(int id, string connStr);
    }
    public class HouseOwnerDAO : IHouseOwnerDAO
    {
        public HouseOwner Create(HouseOwner houseOwner)
        {
            throw new NotImplementedException();
        }

        public HouseOwner Create(HouseOwner houseOwner, string connStr)
        {
            IDbConnection db = null;
            try
            {
                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    var dic = new Dictionary(string, object)
                    {
                        { "Name",houseOwner.Name},
                        { "LastName",houseOwner.LastName},
                        { "FatherName",houseOwner.FatherName},
                        { "MotherName",houseOwner.MotherName},
                        { "MotherName",houseOwner.MotherName},
                        { "AadharNo",houseOwner.AadharNo},
                        { "PhonePrimary",houseOwner.PhonePrimary},
                        { "Phone2",houseOwner.Phone2},
                        { "LandLine1",houseOwner.LandLine1},
                        { "LandLine2",houseOwner.LandLine2},
                        { "EmailAddress",houseOwner.EmailAddress},
                        { "Address1",houseOwner.Address1},
                        { "CIty",houseOwner.CIty},
                        { "District",houseOwner.District},
                        { "State",houseOwner.State},
                        { "Pincode",houseOwner.Pincode},
                        { "ResidingAddress",houseOwner.ResidingAddress}
                    };
                    String sql = @"Insert into houseowner 
                                (
                                Name,
                                LastName,
                                FatherName, 
                                MotherName,
                                AadharNo, 
                                PhonePrimary, 
                                Phone2, 
                                LandLine1,
                                LandLine2, 
                                EmailAddress, 
                                Address1, 
                                Address2, 
                                CIty, 
                                District, 
                                State, 
                                Pincode, 
                                ResidingAddress) 
                                values(@name,
                                @LastName,
                                @FatherName,
                                @MotherName,
                                @AadharNo,
                                @PhonePrimary,
                                @Phone2,
                                @LandLine1,
                                @LandLine2,
                                @EmailAddress,
                                @Address1,
                                @Address2,
                                @CIty,
                                @District,
                                @State,
                                @Pincode,
                                @ResidingAddress)";
                    holist = db.Query<HouseOwner>(@"SELECT * FROM HOUSEOWNER").AsList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"House Owner Get All : Exception Message:{ex.Message} - Inner Message:{ex.InnerException}");
                throw;
            }
            finally
            {
                if (db != null) { db.Close(); }
            }

            return holist;
        }

        public HouseOwner Delete(int id, string connStr)
        {
            throw new NotImplementedException();
        }

        public List<HouseOwner> GetAll(string connStr, ILogger _logger)
        {
            IDbConnection db = null;
            List<HouseOwner> holist;
            try
            {
                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    holist = db.Query<HouseOwner>(@"SELECT * FROM HOUSEOWNER").AsList();
                }
            }catch(Exception ex)
            {
                _logger.LogInformation($"House Owner Get All : Exception Message:{ex.Message} - Inner Message:{ex.InnerException}");
                throw;
            }
            finally
            {
                if (db != null) { db.Close(); }
            }
            
            return holist;
        }

        public HouseOwner GetById(int id, string connStr)
        {
            IDbConnection db = null;
            List<HouseOwner> holist;
            try
            {
                var dic = new Dictionary<string, object>
                {
                    { "id",id}
                };
                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    holist = db.Query<HouseOwner>(@"SELECT * FROM HOUSEOWNER where id = @id",dic).AsList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db != null) { db.Close(); }
            }

            return holist.FirstOrDefault();
        }

        public HouseOwner Save(HouseOwner houseOwner, string connStr)
        {
            throw new NotImplementedException();
        }
    }
}
