using EnManaiWebApi.Model;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace EnManaiWebApi.DAO
{
    public interface IRentalDetailsDAO
    {
        public List<RentalHouseDetail> GetAll(string connStr, ILogger _logger);
        public List<RentalHouseDetail> GetOwnerRentalHouse(int houseOwnerID, string connStr);
        public RentalHouseDetail GetById(int id, string connStr);
        public int Save(RentalHouseDetail rentalHoseDetail, string connStr);
        public RentalHouseDetail Create(HouseOwner houseOwner, string connStr);
        public RentalHouseDetail Delete(int id, string connStr);
    }
    public class RentalDetailsDAO : IRentalDetailsDAO
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
            IDbConnection db = null;
            RentalHouseDetail rental=null;
            try
            {
                var dic = new Dictionary<string, object>
                        {
                            { "id",id}
                        };
                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    rental = db.Query<RentalHouseDetail>(@"SELECT * FROM RentalHouseDetails where id = @id", dic).FirstOrDefault();
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

            return rental;
        }

        public List<RentalHouseDetail> GetOwnerRentalHouse(int houseOwnerID, string connStr)
        {
            IDbConnection db = null;
            List<RentalHouseDetail> rentalList;
            try
            {
                var dic = new Dictionary<string, object>
                        {
                            { "id",houseOwnerID}
                        };
                using (db = new SqlConnection(connStr))
                {
                    db.Open();
                    rentalList = db.Query<RentalHouseDetail>(@"SELECT * FROM RentalHouseDetails where HouseOwnerId = @id", dic).AsList();
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

            return rentalList;
        }

        public int Save(RentalHouseDetail rentalHouseDetail, string connStr)
        {
            IDbConnection db = null;
            int updateRentalStatus = 0;
            try
            {
                var dic = new Dictionary<string, object>
                        {
                            { "id",rentalHouseDetail.Id},
                            { "flatNoOrDoorNo",rentalHouseDetail.FlatNoOrDoorNo},
                            { "address1",rentalHouseDetail.Address1},
                            { "address2",rentalHouseDetail.Address2},
                            { "address3",rentalHouseDetail.Address3},
                            { "areaOrNagar",rentalHouseDetail.AreaOrNagar},
                            { "city",rentalHouseDetail.City},
                            { "district",rentalHouseDetail.District},
                            { "state",rentalHouseDetail.State},
                            { "pincode",rentalHouseDetail.Pincode},
                            { "floor",rentalHouseDetail.Floor},
                            { "vasuthu",rentalHouseDetail.Vasuthu},
                            { "coOperationWater",rentalHouseDetail.CoOperationWater},
                            { "boreWater",rentalHouseDetail.BoreWater},
                            { "separateEB",rentalHouseDetail.SeparateEB},
                            { "twoWheelerParking",rentalHouseDetail.TwoWheelerParking},
                            { "fourWheelerParking",rentalHouseDetail.FourWheelerParking},
                            { "separateHouse",rentalHouseDetail.SeparateHouse},
                            { "houseOwnerResidingInSameBuilding",rentalHouseDetail.HouseOwnerResidingInSameBuilding},
                            { "rentalFrom",rentalHouseDetail.RentFrom},
                            { "rentalTo",rentalHouseDetail.RentTo},
                            { "petsAllowed",rentalHouseDetail.PetsAllowed},
                            { "paymentActive",rentalHouseDetail.PaymentActive}
                        };


                if (rentalHouseDetail.Id == -1)
                {
                    using (db = new SqlConnection(connStr))
                    {
                        //Insert new Rental Details
                        db.Open();
                        updateRentalStatus = db.Execute(@"INSERT INTO RentalHouseDetails(
                            HouseOwnerId,
                            FlatNoOrDoorNo,
                            Address1,
                            Address2,
                            Address3, 
                            AreaOrNagar, 
                            City, 
                            District, 
                            State, 
                            Pincode,
                            Floor, 
                            Vasuthu, 
                            CoOperationWater, 
                            BoreWater,
                            SeparateEB, 
                            TwoWheelerParking, 
                            FourWheelerParking, 
                            SeparateHouse,
                            HouseOwnerResidingInSameBuilding, 
                            RentFrom, 
                            RentTo,
                            PetsAllowed, 
                            PaymentActive
                            ) values( 
                            '1',
                            @flatNoOrDoorNo,
                            @address1,
                            @address2,
                            @address3,
                            @areaOrNagar,
                            @city,
                            @district,
                            @state,
                            @pincode,
                            @floor,
                            @vasuthu,
                            @coOperationWater,
                            @boreWater,
                            @separateEB,
                            @twoWheelerParking,
                            @fourWheelerParking,
                            @separateHouse,
                            @houseOwnerResidingInSameBuilding,
                            @rentalFrom,
                            @rentalTo,
                            @petsAllowed,
                            @paymentActive)
                            ", dic);
                    }
                }
                else if (rentalHouseDetail.Id > 0)
                {
                    //Update Rental Details
                    using (db = new SqlConnection(connStr))
                    {
                        db.Open();
                        updateRentalStatus = db.Execute(@"UPDATE RentalHouseDetails SET 
                            FlatNoOrDoorNo=@flatNoOrDoorNo,
                            Address1=@address1,
                            Address2=@address2,
                            Address3=@address3,
                            AreaOrNagar=@areaOrNagar,
                            City=@city,
                            District=@district,
                            State=@state,
                            Pincode=@pincode,
                            Floor=@floor,
                            Vasuthu=@vasuthu,
                            CoOperationWater=@coOperationWater,
                            BoreWater=@boreWater,
                            SeparateEB=@separateEB,
                            TwoWheelerParking=@twoWheelerParking,
                            FourWheelerParking=@fourWheelerParking,
                            SeparateHouse=@separateHouse,
                            HouseOwnerResidingInSameBuilding=@houseOwnerResidingInSameBuilding,
                            RentFrom=@rentalFrom,
                            RentTo=@rentalTo,
                            PetsAllowed=@petsAllowed,
                            PaymentActive=@paymentActive
                            where id = @id", dic);
                    }
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

            return updateRentalStatus;
        }
    }
}
