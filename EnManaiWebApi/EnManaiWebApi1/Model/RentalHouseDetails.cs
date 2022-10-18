﻿namespace EnManaiWebApi.Model
{
    public class RentalHouseDetail
    {
        public int Id { get; set; }
        public int HouseOwnerId { get; set; }
        public string FlatNoOrDoorNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string AreaOrNagar { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Floor { get; set; }
        public bool Vasuthu { get; set; }
        public bool CoOperationWater { get; set; }
        public bool BoreWater { get; set; }
        public bool SeparateEB { get; set; }
        public bool TwoWheelerParking { get; set; }
        public bool FourWheelerParking { get; set; }
        public bool SeparateHouse { get; set; }
        public bool HouseOwnerResidingInSameBuilding { get; set; }
        public bool RentalOccupied { get; set; }
        public bool PetsAllowed { get; set; }
        public bool Apartment { get; set; }
        public int ApartmentFloor { get; set; }
        public bool PaymentActive { get; set; }
        public int RentFrom { get; set; }
        public int RentTo { get; set; }
        public int BHK { get; set; }
        public bool Bachelor { get; set; }
        public bool NonVeg { get; set; }
    }
}
