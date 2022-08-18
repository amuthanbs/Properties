namespace EnManaiWebApi.Model
{
    public class RentalHouseDetails  
{  
    public int Id { get; set; }
    public int HouseOwnerId { get; set; }
    public string FlatNoOrDoorNo { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
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
public char RentalOccupied { get; set; }  
}
}
