using Dapper;
using EnManaiWebApi.Model;
using System.Data;
using System.Data.SqlClient;

namespace EnManaiWebApi.DAO
{
    public interface IRentalHouseDetails
    {
        public List<RentalHouseDetail> GetAll(string connStr, ILogger _logger);
        public RentalHouseDetail GetById(int id, string connStr);
        public RentalHouseDetailPhoneNumber GetPhoneNumberById(int id, string connStr);
        public RentalHouseDetail Save(HouseOwner houseOwner, string connStr);
        public RentalHouseDetail Create(HouseOwner houseOwner, string connStr);
        public RentalHouseDetail Delete(int id, string connStr);

    }
    public class RentalHouseDetailsDAO : IRentalHouseDetails
    {
        public RentalHouseDetail Create(HouseOwner houseOwner, string connStr)
        {
            throw new NotImplementedException();
        }

        public RentalHouseDetail Delete(int id, string connStr)
        {
            throw new NotImplementedException();
        }

        public List<RentalHouseDetail> GetAll(string connStr, ILogger _logger)
        {
            throw new NotImplementedException();
        }

        public RentalHouseDetail GetById(int id, string connStr)
        {
            throw new NotImplementedException();
        }

        public RentalHouseDetailPhoneNumber GetPhoneNumberById(int id, string connStr)
        {
            IDbConnection db = null;
            RentalHouseDetailPhoneNumber rhd = new RentalHouseDetailPhoneNumber();
            try
            {
                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    var dic = new Dictionary<string, object>
                        {
                            { "id",id}
                    };
                    dynamic result = db.QueryFirstOrDefault("SELECT * FROM RentalHouseDetails where ID = @id", dic);
                    if(result != null) { 
                    rhd.Id = result.Id;
                    rhd.PhoneNumber = result.PhoneNumber;
                    rhd.PhoneNumberPrimary = result.PhoneNumberPrimary;
                    rhd.LandLineNumber = result.LandLineNumber;
                    }else
                    {
                        rhd = null;
                    }
                }

                return rhd;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public RentalHouseDetail Save(HouseOwner houseOwner, string connStr)
        {
            throw new NotImplementedException();
        }
    }
}
