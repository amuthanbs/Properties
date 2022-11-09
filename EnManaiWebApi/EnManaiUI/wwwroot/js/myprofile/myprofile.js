//import { logins } from '..login/login.js';
var rentalDetails = null;
var login = null, houseOwnerId = null;
var usr = null;
$(document).ready(function () {
    usr = JSON.parse(GetLocalData("user"));
    GetProfileDetail();
    GetRentalDetails();
    console.log(usr);
});
function GetRentalDetails() {
    debugger;
    $.ajax({
        type: "POST",
        url: 'https://localhost:7041/api/App/GetRentalDetail?',
        data: '? id = ' + usr.logins[0].houseOwnerId,
        headers: { "Authorization": 'Bearer ' + usr.accessToken.token },
        //dataType: 'json',
        contentType: 'application/json',
        success: function (event, xhr, settings) {
            rentalDetails = event.rentalDetails;
            console.log(rentalDetails);
            SetUpHouseDetail();
            //window.location.href = 'https://localhost:7059/Home/Search';

        }
    });
}
function GetProfileDetail() {

    var loginRequest = {
        "userName": "amuthan",
        "password": "12345678",
        "forgetPassword": true
    };
    if ((usr === null) || (usr === '')) {
        $.ajax({
            type: "POST",
            url: 'https://localhost:7041/api/Home/GetLogin',
            data: JSON.stringify(loginRequest),
            //headers: { "Authorization": 'Bearer ' + usr.accessToken.token },
            dataType: 'json',
            contentType: 'application/json',
            success: function (event, xhr, settings) {
                debugger;
                login = event.logins;
                houseOwnerId = login[0].houseOwnerId;
                console.log(houseOwnerId);
                SetUpProfilePage();
                //window.location.href = 'https://localhost:7059/Home/LandingPage';

            }
        });
    } else {
        SetUpProfilePage();
    }
}
function SetUpProfilePage() {
    console.log(login);
    $('#txtUserName').val(login[0].userName);
    $('#txtPhoneNumber').val(login[0].phoneNumber);
    $('#txtEmailId').val(login[0].eMailId);
    $('#chkPhoneNumberVerified').prop('checked', login[0].phoneNumberVerified);
    $('#chkEmailVerified').prop('checked', login[0].emailVerified);
    $('#txtReverification').val(login[0].reverificationTime);
    $('#txtPhoneNumberVerifiedDate').val(login[0].phoneNumberVerifiedDate);
    $('#txtEmailVerifiedDate').val(login[0].emailIdVerifiedDate);
    $('#chkMandatoryVerification').prop('checked', login[0].mandatoryVerification);
    $('#chkReVerification').prop('checked', login[0].reVerification);

}
function SetUpHouseDetail() {
    $('#txtRecordId').val(rentalDetails.houseOwner.id);
    $('#txtFirstName').val(rentalDetails.houseOwner.name);
    $('#txtFirstName').attr("data-id", rentalDetails.houseOwner.id);
    $('#txtLastName').val(rentalDetails.houseOwner.lastName);
    $('#txtFatherName').val(rentalDetails.houseOwner.fatherName);
    $('#txtMotherName').val(rentalDetails.houseOwner.motherName);
    //$('#txtMotherName').val(rentalDetails.houseOwner.motherName);
    $('#txtPrimaryPhone').val(rentalDetails.houseOwner.phonePrimary);
    $('#txtPhone2').val(rentalDetails.houseOwner.phone2);
    $('#txtlandLine1').val(rentalDetails.houseOwner.landLine1);
    $('#txtlandLine2').val(rentalDetails.houseOwner.landLine2);
    $('#txtEmailAddress').val(rentalDetails.houseOwner.emailAddress);
    $('#txtAddress1').val(rentalDetails.houseOwner.address1);
    $('#txtAddress2').val(rentalDetails.houseOwner.address2);
    $('#txtCity').val(rentalDetails.houseOwner.cIty);
    $('#txtDistrict').val(rentalDetails.houseOwner.district);
    $('#txtState').val(rentalDetails.houseOwner.state);
    $('#txtPincode').val(rentalDetails.houseOwner.pincode);

    $.each(rentalDetails.rentalHouseDetails, function (i, v) {
        //debugger;
        //console.log(v);
        $('#tblRentalHouse tbody').append(
            rentalTableRow.replace('--FlatNoOrFloor--', v.flatNoOrDoorNo)
                .replace('--Address1--', v.address1)
                .replace('--Address2--', v.address2)
                .replace('--Address3--', v.address3)
                .replace('--AreaOrNagar--', v.areaOrNagar)
                .replace('--City--', v.city)
                .replace('--District--', v.district)
                .replace('--State--', v.state)
                .replace('--ID--', v.id)
                .replace('--HouseOwnerId--', v.houseOwnerId)
        );
    });
    var table = $('#tblRentalHouse').DataTable({
        destroy: true,
        columnDefs: [
            {
                target: 0,
                visible: false,
                searchable: false,
            },
            {
                target: 1,
                visible: false,
                searchable: false,
            }
        ],
    });
    $('#tblRentalHouse tbody').on('click', 'tr', function () {
        debugger;
        var data = table.row(this).data();
        //console.log('data:', data);
        var col = rentalDetails.rentalHouseDetails.find(x => x.id === (data[0] - 0));
        //col.houseOwnerId = data[1] - 0;
        console.log(col);
        setUpRentalHouseModel(col);
        $('#rentalHouseModal').modal({ backdrop: "static ", keyboard: false });
        $('#rentalHouseModal').modal('show');
        //$('#exampleModal').on('shown.bs.modal', function () {
        //    $('#myInput').trigger('focus')
        //})
        //alert('You clicked on ' + data[0] + "'s row");
    });
}
rentalTableRow = '<tr> <td>--ID--</td><td>--HouseOwnerId--</td> <td>--FlatNoOrFloor--</td> <td>--Address1--</td> <td>--Address2--</td> <td>--Address3--</td> <td>--AreaOrNagar--</td> <td>--City--</td> <td>--District--</td> <td>--State--</td> </tr>';
function SaveHouseDetails() {
    alert('Save House Details');
}
function addNewRentalHouseDetails() {
    setUpRentalHouseModel(-1);
    $('#txtHouseOwnerId').val(rentalDetails.rentalHouseDetails[0].houseOwnerId);
    $('#rentalHouseModal').modal({ backdrop: "static ", keyboard: false });
    $('#rentalHouseModal').modal('show');
}
function setUpRentalHouseModel(col) {
    if (col !== -1) {
        $('#txtRentalId').val(col.id);
        //$('#txtHouseOwnerId').val(col.houseOwnerId);
        $('#txtRentalFlatNoOrDoorNo').val(col.flatNoOrDoorNo);
        $('#txtRentalAddress1').val(col.address1);
        $('#txtRentalAddress2').val(col.address2);
        $('#txtRentalAddress3').val(col.address3);
        $('#txtRentalareaOrNagar').val(col.areaOrNagar);
        $('#txtRentalCity').val(col.city);
        $('#txtRentalDistrict').val(col.district);
        $('#txtRentalState').val(col.state);
        $('#txtRentalPincode').val(col.pincode);
        $('#txtRentalFloor').val(col.floor);
        $('#chkRentalcoOperationWater').prop('checked', col.coOperationWater);
        $('#chkRentalVasuthu').prop('checked', col.vasuthu);
        $('#chkRentalboreWater').prop('checked', col.boreWater);
        $('#chkRentalseparateEB').prop('checked', col.separateEB);
        $('#chkRentaltwoWheelerParking').prop('checked', col.twoWheelerParking);
        $('#chkRentalfourWheelerParking').prop('checked', col.fourWheelerParking);
        $('#chkRentalrentalOccupied').prop('checked', col.rentalOccupied);
        $('#chkRentalhouseOwnerResidingInSameBuilding').prop('checked', col.houseOwnerResidingInSameBuilding);
        $('#chkRentalseparateHouse').prop('checked', col.separateHouse);
        $('#chkRentalpetsAllowed').prop('checked', col.petsAllowed);
        $('#chkRentalApartment').prop('checked', col.apartment);
        $('#txtRentalRentalAmountFrom').val(col.rentFrom);
        $('#txtrentalApartmentFloor').val(col.apartmentFloor);
        $('#txtRentalRentalAmountTo').val(col.rentTo);
    } else {
        $('#txtRentalId').val(-1);
        $('#txtRentalFlatNoOrDoorNo').val();
        $('#txtRentalAddress1').val();
        $('#txtRentalAddress2').val();
        $('#txtRentalAddress3').val();
        $('#txtRentalareaOrNagar').val();
        $('#txtRentalCity').val();
        $('#txtRentalDistrict').val();
        $('#txtRentalState').val();
        $('#txtRentalPincode').val();
        $('#txtRentalFloor').val();
        $('#chkRentalcoOperationWater').prop('checked', false);
        $('#chkRentalVasuthu').prop('checked', false);
        $('#chkRentalboreWater').prop('checked', false);
        $('#chkRentalseparateEB').prop('checked', false);
        $('#chkRentaltwoWheelerParking').prop('checked', false);
        $('#chkRentalfourWheelerParking').prop('checked', false);
        $('#chkRentalrentalOccupied').prop('checked', false);
        $('#chkRentalhouseOwnerResidingInSameBuilding').prop('checked', false);
        $('#chkRentalseparateHouse').prop('checked', false);
        $('#chkRentalpetsAllowed').prop('checked', false);
        $('#chkRentalApartment').prop('checked', false);
        $('#txtRentalRentalAmountFrom').val();
        $('#txtrentalApartmentFloor').val();
        $('#txtRentalRentalAmountTo').val();
    }
}

function SaveProfileDetails() {
    alert('Save Profile Details');
}

function modalClose() {
    $('#rentalHouseModal').modal('hide');
}

function saveRentalHouseDetails() {
    console.log('House Owner Id:', $('#txtHouseOwnerId').val())
    saveRentalHouseDetail = {
        "id": $('#txtRentalId').val(),
        "houseOwnerId": $('#txtHouseOwnerId').val(),
        "flatNoOrDoorNo": $('#txtRentalFlatNoOrDoorNo').val(),
        "address1": $('#txtRentalAddress1').val(),
        "address2": $('#txtRentalAddress2').val(),
        "address3": $('#txtRentalAddress2').val(),
        "areaOrNagar": $('#txtRentalareaOrNagar').val(),
        "city": $('#txtRentalCity').val(),
        "district": $('#txtRentalDistrict').val(),
        "state": $('#txtRentalState').val(),
        "pincode": $('#txtRentalPincode').val(),
        "floor": $('#txtRentalFloor').val(),
        "vasuthu": $('#chkRentalVasuthu').is(':checked'),
        "coOperationWater": $('#chkRentalcoOperationWater').is(':checked'),
        "boreWater": $('#chkRentalboreWater').is(':checked'),
        "separateEB": $('#chkRentalseparateEB').is(':checked'),
        "twoWheelerParking": $('#chkRentaltwoWheelerParking').is(':checked'),
        "fourWheelerParking": $('#chkRentalfourWheelerParking').is(':checked'),
        "separateHouse": $('#chkRentalseparateHouse').is(':checked'),
        "houseOwnerResidingInSameBuilding": $('#chkRentalhouseOwnerResidingInSameBuilding').is(':checked'),
        "rentalOccupied": $('#chkRentalrentalOccupied').is(':checked'),
        "petsAllowed": $('#chkRentalpetsAllowed').is(':checked'),
        "apartment": $('#chkRentalApartment').is(':checked'),
        "apartmentFloor": $('#txtrentalApartmentFloor').val(),
        "paymentActive": false,
        "rentFrom": $('#txtRentalRentalAmountFrom').val(),
        "rentTo": $('#txtRentalRentalAmountTo').val()
    }
    //console.log('Save Rental Details:', saveRentalHouseDetail);

    $.ajax({
        type: "POST",
        url: 'https://localhost:7041/api/App/UpdateRentalDetail',
        data: JSON.stringify(saveRentalHouseDetail),
        dataType: 'json',
        contentType: 'application/json',
        success: function (event, xhr, settings) {
            modalClose();
            console.log(event);
            GetRentalDetails();
            if (event.status === 'Success') {
                if (event.count > 0) {
                    alert('Update Rental details is Success');
                } else {
                    alert(event.errorMessage);
                }
            }
        },
        error: function (result, xhr, settings) {
            alert('500 internal server error');

        }
    });
}