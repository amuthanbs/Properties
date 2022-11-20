using EnManaiWebApi.Model;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using EnManaiWebApi.Model.Response;

namespace EnManaiWebApi.DAO
{
    public interface ISearchDAO
    {
        public List<RentalHouseDetail> BasicSearch(int id,string city, string connStr);
        //public HouseOwner GetById(int id, string connStr);
        //public HouseOwner Save(HouseOwner houseOwner, string connStr);
        //public HouseOwner Create(HouseOwner houseOwner, string connStr);
        //public HouseOwner Delete(int id, string connStr);
    }
    public class SearchDAO : ISearchDAO
    {
        public List<RentalHouseDetail> BasicSearch(int id=0,string city="", string connStr="")
        {
            //string[] splt = city.Split(',');
            //if (splt.Length == 1)
            {
                IDbConnection db = null;
                List<RentalHouseDetail> rentalHouselist = new List<RentalHouseDetail>();
                try
                {
                    var dic = new Dictionary<string, object>
                        {
                            { "id",null},
                            { "HouseOwnerId" , null },
                            {"AreaOrNagar",null},
                            { "City", "Coimbatore"},
                            { "District", null},
                            {"State", null },
                            {"Pincode" , null },
                            {"Floor", null },
                            {"Vasuthu", 0 },
                            {"CoOperationWater",0 },
                            {"BoreWater" , 0 },
                            { "SeparateED" , 0 },
                            {"TwoWheelerParking",0 },
                            {"FourWheelerParking", 0 },
                            { "SeparateHouse",0 },
                            {"HouseResidingInSameBuilding" , 0 },
                            {"apartment", 0 },
                            {"ResultType", "Search" },
                            {"action" ,0 },
                            {"RentFrom" ,null },
                            {"RentTo",null },
                            {"PetsAllowed" ,0 },
                        {"PaymentActive",true }
                        };
                    using (db = new SqlConnection(connStr))
                    {
                        db.Open();
                        IEnumerable<dynamic> result = db.Query("EM_RentalHouseDetails", dic,null,true,null, CommandType.StoredProcedure);
                        foreach (dynamic item in result)
                        {

                            RentalHouseDetail rd = ConvertToObject(item);
                            rd.PaymentActive = GetPaymentStatus(rd.Id, rd.HouseOwnerId, connStr);
                            rentalHouselist.Add(rd);
                        }
                        //holist = db.Query<HouseOwner>(@"SELECT * FROM HOUSEOWNER where id = @id", dic).AsList();
                    }

                    return rentalHouselist;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (db != null) { db.Close(); }
                }
            }
        }

        private bool GetPaymentStatus(int id, int houseOwnerId, string connStr)
        {

            IDbConnection db = null;
            List<RentalHouseDetail> rentalHouselist = new List<RentalHouseDetail>();
            try
            {
                using (db = new SqlConnection(connStr))
                {
                    var dic = new Dictionary<string, object>
                    {
                        { "id", id },
                        { "houseOwnerId",houseOwnerId}
                    };
                    string sql = @"SELECT count(*) FROM [PaymentForRent] WHERE RentHouseId = @id and houseownerid = @houseOwnerId";
                    int? count = db.Query<int>(sql, dic).FirstOrDefault();
                    if (count != null && count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        private RentalHouseDetail ConvertToObject(dynamic item)
        {

            RentalHouseDetail rhd = new RentalHouseDetail();
            rhd.Id = item.Id;
            rhd.HouseOwnerId = item.HouseOwnerId;
            rhd.FlatNoOrDoorNo = item.FlatNoOrDoorNo;
            rhd.Address1 = item.Address1;
            rhd.Address2 = item.Address2;
            rhd.Address3 = item.Address3;
            rhd.AreaOrNagar = item.AreaOrNagar;
            rhd.City = item.City;
            rhd.District = item.District;
            rhd.State = item.State;
            rhd.Pincode = item.Pincode;
            rhd.Floor = item.Floor;
            rhd.Vasuthu = item.Vasuthu;
            rhd.CoOperationWater = item.CoOperationWater;
            rhd.BoreWater = item.BoreWater;
            rhd.SeparateEB = item.SeparateEB;
            rhd.TwoWheelerParking = item.TwoWheelerParking;
            rhd.FourWheelerParking = item.FourWheelerParking;
            rhd.SeparateHouse = item.SeparateHouse;
            rhd.HouseOwnerResidingInSameBuilding = item.HouseOwnerResidingInSameBuilding;
            rhd.RentalOccupied = item.RentalOccupied;
            rhd.Apartment = item.Apartment;
            rhd.ApartmentFloor = item.ApartmentFloor;
            rhd.RentFrom = item.RentFrom;
            rhd.RentTo = item.RentTo;
            rhd.PetsAllowed = item.PetsAllowed;
            rhd.PaymentActive = item.PaymentActive;

            return rhd;
        }
    }

}
