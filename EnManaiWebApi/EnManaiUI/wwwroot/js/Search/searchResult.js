
var rentalData = null;
var user = null;
var userType = 'UnRegistered';
var currentPageNumber = getLocalPageNumber('PageNumber');
$(document).ready(function () {
    var s = window.location.href;
    setSearchInput(s);
    var rentalDataTemp = getLocalRentalData("rentaldetails");
    var userTemp = GetLocalData("user");
    if (!isNull(userTemp)) {
        user = JSON.parse(userTemp);
        userType = 'Logged';
    }
    if (!isNull(rentalDataTemp) && (rentalDataTemp !== 'null')) {
        rentalData = JSON.parse(rentalDataTemp);
        $('#main').removeClass('searchPageBorderPadding');
        loadRentalData();
        $('#btnLoadMore').removeClass('hidden');
    } else {
        $('.rentals').empty();
        $('#main').addClass('searchPageBorderPadding');
        $('#btnLoadMore').addClass('hidden');
    }
});
function setSearchInput(s) {
    if (s.split('?')[1] !== undefined) {
        if (s.split('?')[1].split('=')[1] !== undefined) {
            console.log(s.split('?')[1].split('=')[1]);
            $('#search-input').val(s.split('?')[1].split('=')[1]);
            return;
        }
    }
}
function loadMore() {
    nextPageCount = parseInt(currentPageNumber) + 1;
    
        console.log('Search Text:');
        var search = $('#search-input').val();
        if (userType === "UnRegistered") {
            $.ajax({
                type: "POST",
                url: APIUrl + '/api/App/NonRegisteredSearchResult?city=' + search + '&encryptedUserCode=' + encodeURIComponent(''),
                contentType: 'application/json; charset=utf-8',
                success: function (result, xhr, settings) {
                    console.log('Result:', result);
                    rentalDetails = result.rentalDetails;
                    if (result.rentalDetails.length > 0) {
                        setLocalPageNumber('PageNumber', nextPageCount);
                    }
                    setLocalRentalData("rentaldetails", rentalDetails);
                    window.location.href = 'https://localhost:7059/Home/SearchResult?city=' + search;
                },
                error: function (result, xhr, settings) {
                    alert('API call is not made');
                }
            });
        } else {
            $.ajax({
                type: "POST",
                url: APIUrl + '/api/App/SearchResult?city=' + search + '&encryptedUserCode=' + encodeURIComponent('') + '&id=' + usr.logins[0].id + '&start=0&end=' + (NoOfRecordsPerPage * nextPageCount),
                contentType: 'application/json; charset=utf-8',
                headers: { "Authorization": 'Bearer ' + usr.accessToken.token },
                success: function (result, xhr, settings) {
                    console.log('Result:', result);
                    rentalDetails = result.rentalDetails;
                    setLocalRentalData("rentaldetails", rentalDetails);
                    if (result.rentalDetails.length > 0) {
                        setLocalPageNumber('PageNumber', nextPageCount);
                    }
                    window.location.href = 'https://localhost:7059/Home/LoggedSearch?city=' + search;

                },
                error: function (result, xhr, settings) {
                    alert('API call is not made');
                }
            });
        }
}
function GetPhoneNumber(e) {
    console.log('Phone Number:');
    rentalId = e.attributes['data-idx'].value;
    var giveCall = false;
    if (jQuery.inArray(rentalId, user.logins[0].nonPaidContactList.split(',')) > -1) {
        giveCall = true;
    } else if (user.logins[0].nonPaidedContactViewed <= user.logins[0].noOfNonPaidedContact)  {
        giveCall = true;
    }
    if (giveCall) {
        //GetPhoneNumber
        $.ajax({
            type: "POST",
            url: APIUrl + '/api/App/GetPhoneNumber?id=' + usr.logins[0].id + '&rentalid=' + rentalId,
            contentType: 'application/json; charset=utf-8',
            headers: { "Authorization": 'Bearer ' + usr.accessToken.token },
            success: function (result, xhr, settings) {
                console.log('Result:', result);
                user.logins.pop(0);
                user.logins.push(result.rentalHouseDetailPhoneNumber.login);
                SetLocalData("user", user);
                phoneList = result.rentalHouseDetailPhoneNumber.landLineNumber + "," + result.rentalHouseDetailPhoneNumber.phoneNumber + ',' + result.rentalHouseDetailPhoneNumber.phoneNumberPrimary;
                $('#' + rentalId + '_lblPhoneNumber').text(phoneList);
                //rentalDetails = result.rentalDetails;
                //setLocalRentalData("rentaldetails", rentalDetails);
                //if (result.rentalDetails.length > 0) {
                //    setLocalPageNumber('PageNumber', nextPageCount);
                //}
                //window.location.href = 'https://localhost:7059/Home/LoggedSearch?city=' + search;
            },
            error: function (result, xhr, settings) {
                alert('Get Phone API call is not made');
            }
        });
    } else {
        alert('Pay to get house details');
    }
    
}
function loadRentalData() {
    console.log(test);
    $.each(rentalData, function (i, v) {
        var v1;
        if (userType === 'UnRegistered') {
            v1 = unregisterted;
            v2 = v1.replace('--CITY--', v.city).replace('--BHK--', v.bhk).replace('--DEPOSIT--', 5000).replace('--PINCODE--', v.pincode);
            $('#petsAllowed').prop('checked', v.petsAllowed);
            $('.rentals').append(v2);
        } else if (userType === 'Logged') {
            v1 = loggedHtml;
            v2 = v1.replace('--CITY--', v.city).replace('--BHK--', v.bhk).replace('--DEPOSIT--', v.deposit).replace('--PINCODE--', v.pincode).replace('--FLOOR--', v.floor).replace('--AREAORNAGAR--', v.areaOrNagar).replace('--RENTFROM--', v.rentFrom).replace('--RENTO--', v.rentTo);
            v2 = v2.replaceAll('--idx--', v.id);
            $('.rentals').append(v2);
            $('#' + v.id + '_petsAllowed').prop('checked', v.petsAllowed);
            $('#' + v.id + `_cooperationWater`).prop('checked', v.coOperationWater);
            $('#' + v.id + `_bachelor`).prop('checked', v.bachelor);
            $('#' + v.id + `_TwoWheelerParking`).prop('checked', v.twoWheelerParking);
            $('#' + v.id + `_FourWheelerParking`).prop('checked', v.fourWheelerParking);
            $('#' + v.id + `_SeparateHouse`).prop('checked', v.separateHouse);
            $('#' + v.id + `_NonVeg`).prop('checked', v.nonVeg);
            $('#lblPhoneNumber').hide()
        } else if (userType === 'Paid') {
            v1 = null;
        }
        console.log(v.city, v.pincode);
    });
}

var test = "<div id=\"header_idx\" style=\"width: 500px; background: linear - gradient(90deg, rgba(30, 30, 241, 1) 0 %, rgba(0, 212, 255, 1) 35 %, rgba(255, 255, 255, 1) 100 %);\" >1 BHK Rent in Palani - 624601 </div > ";
var unregisterted = "<div class=\"searchResultContent ma-10\"> <div style=\"padding-bottom: 10px;\" ><div style=\"float: left; width: 33.33%;\"><span id=\"buildarea\">--SQRT--</span></div><div style=\"float: left; width: 33.33%;\"><span id=\"bhkType\">--BHK--</span></div><div style=\"float: left; width: 33.33%;\"><span id=\"deposit\">--DEPOSIT--</span></div></div style=\"margin-top: 30px;\" ><div><div class=\"pa-10\" style=\"float: left; width: 40%;height:230px;;border: 2px solid lightgrey\"><div id=\"houseImage\">Image</div></div><div class=\"ma-10\" style=\"float: left; width: 60%;\"><div id=\"city\">--CITY--</div><div id=\"pincode\">--PINCODE--</div><div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"cooperationWater\"><label class=\"form-check-label\" for=\"cooperationWater\">Co-operation Water</label></div><div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"bachelor\"><label class=\"form-check-label\" for=\"Bachelor\">Bachelor</label></div><div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"petsAllowed\"><label class=\"form-check-label\" for=\"petsAllowed\">Pets Allowed</label></div></div></div></div></div >"

var loggedHtml = '<div class="searchResultContent ma-10">' +
    '<div style="padding-bottom: 10px;" >' +
    '	<div style="float: left; width: 33.33%;"><span id="--idx--_buildarea">--SQRT--</span></div>' +
    '	<div style="float: left; width: 33.33%;"><span id="--idx--_bhkType">--BHK--</span></div>' +
    '	<div style="float: left; width: 33.33%;"><span id="--idx--_deposit">--DEPOSIT--</span></div>' +
    '	</div style="margin-top: 30px;\" >' +
    '<div>' +
    '	<div class="pa-10" style="float: left; width: 40%;height:230px;;border: 2px solid lightgrey">' +
    '	<div id="--idx--_houseImage">Image</div>' +
    '	</div>' +
    '<div class="ma-10" style="float: left; width: 60%;">'+
        '<div style="float: left; width: 50%;height:230px;">' +
            '<div style="padding-left:10px" id="--idx--_areaornagar">Area Or Nagar:--AREAORNAGAR--</div>' +
            '<div style="padding-left:10px" id="--idx--_city">City:--CITY--</div>' +
            '<div style="padding-left:10px" id="--idx--_pincode">Pincode:--PINCODE--</div>' +
    
            '<div style="padding-left:10px" id="--idx--_floor">Floor:--FLOOR--</div>'+
    //'</div>' +
            '<div class="form-check">' +
            '    <input style="padding-left:10px" class="form-check-input" type="checkbox" value="" onclick="return false" id="--idx--_cooperationWater">' +
            '        <label class="form-check-label" for="cooperationWater">Co-operation Water</label>' +
            '</div>' +
            '<div class="form-check">' +
            '    <input style="padding-left:10px" class="form-check-input" type="checkbox" value="" onclick="return false" id="--idx--_bachelor">' +
            '        <label class="form-check-label" for="Bachelor">Bachelor</label>' +
            '</div>' +
            '<div class="form-check">' +
            '    <input style="padding-left:10px" class="form-check-input" type="checkbox" value="" onclick="return false" id="--idx--_petsAllowed">' +
            '        <label class="form-check-label" for="petsAllowed">Pets Allowed</label>' +
            '</div>' +
        '</div>' +
    '<div style="float: right; width: 50%;height:230px;">' +
    '    <div class="form-check">' +
    '    <input style="padding-left:10px" class="form-check-input" type="checkbox" value="" onclick="return false" id="--idx--_TwoWheelerParking">' +
    '    <label class="form-check-label" for="Two Wheeler Parking">Two Wheeler Parking</label>' +
    '    </div>' +
    '    <div class="form-check">' +
    '    <input style="padding-left:10px" class="form-check-input" type="checkbox" value="" onclick="return false" id="--idx--_FourWheelerParking">' +
    '    <label class="form-check-label" for="Four Wheeler Parking">Four Wheeler Parking</label>' +
    '    </div>' +
    '    <div class="form-check">' +
    '    <input style="padding-left:10px" class="form-check-input" type="checkbox" value="" onclick="return false" id="--idx--_SeparateHouse">' +
    '    <label class="form-check-label" for="Separate House">Separate House</label>' +
    '    </div>' +
    '    <div style="padding-left:10px" id="--idx--_RentFrom">Rent From:--RENTFROM--</div>' +
    '    <div style="padding-left:10px" id="--idx--_RentTo">Rent To:--RENTO--</div>' +
    //'    </div>' +
    '    <div class="form-check">' +
    '    <input style="padding-left:10px" class="form-check-input" type="checkbox" onclick="return false" value="" id="--idx--_NonVeg">' +
    '    <label class="form-check-label" for="Non Veg">Non Veg</label>' +
    '    <div><button type="button" data-idx="--idx--" id="--idx--_btnPhoneNumber" onclick="GetPhoneNumber(this)" class="btn btn-primary">GetPhoneNumber</button>' +
    '    <span id="--idx--_lblPhoneNumber" class="label label-primary">Primary Label</span></div>' +
    '    </div>' +
    '</div>' +
    '</div>' +
    //'<div class="ma-10" style="float: left; width: 60%;">' +
    //'	<div id="areaornagar">--AreaOrNagar--</div>' +
    //'	<div id="city">--CITY--</div>' +
    //'	<div id="pincode">--PINCODE--</div>' +
    //'	<div class="form-check">' +
    //'		<input class="form-check-input" type="checkbo" value="" id="cooperationWater">' +
    //'		<label class="form-check-label" for="cooperationWater">Floor</label>' +
    //'	</div>' +
    //'	<div class="form-check">' +
    //'		<input class="form-check-input" type="checkbox" value="" id="cooperationWater">' +
    //'		<label class="form-check-label" for="cooperationWater">Co-operation Water</label>' +
    //'	</div>' +
    //'	<div class="form-check">' +
    //'		<input class="form-check-input" type="checkbox" value="" id="bachelor">' +
    //'		<label class="form-check-label" for="Bachelor">Bachelor</label>' +
    //'	</div>' +
    //'	<div class="form-check">' +
    //'		<input class="form-check-input" type="checkbox" value="" id="petsAllowed">' +
    //'		<label class="form-check-label" for="petsAllowed">Pets Allowed</label>' +
    //'	</div>' +
    //'	<div class="form-check">' +
    //'		<input class="form-check-input" type="checkbox" value="" id="TwoWheelerParking">' +
    //'		<label class="form-check-label" for="Two Wheeler Parking">Two Wheeler Parking</label>' +
    //'	</div>' +
    //'	<div class="form-check">' +
    //'		<input class="form-check-input" type="checkbox" value="" id="FourWheelerParking">' +
    //'		<label class="form-check-label" for="Four Wheeler Parking">Four Wheeler Parking</label>' +
    //'	</div>' +
    //'	<div class="form-check">' +
    //'		<input class="form-check-input" type="checkbox" value="" id="SeparateHouse">' +
    //'		<label class="form-check-label" for="Separate House">Separate House</label>' +
    //'	</div>' +
    //'	<div class="form-check">' +
    //'		<input class="form-check-input" type="checkbox" value="" id="Rent From">' +
    //'		<label class="form-check-label" for="Rent From">Rent From</label>' +
    //'	</div>' +
    //'	<div class="form-check">' +
    //'		<input class="form-check-input" type="checkbox" value="" id="RentTo">' +
    //'		<label class="form-check-label" for="Rent To">Rent To</label>' +
    //'	</div>' +
    //'	<div class="form-check">' +
    //'		<input class="form-check-input" type="checkbox" value="" id="NonVeg">' +
    //'		<label class="form-check-label" for="Non Veg">Non Veg</label>' +
    //'	</div>' +
    //'</div>' +
    '</div>' +
    '</div>' +
    '</div>';




var r = "< div id = \"header_idx\" style = \"width: 500px; background: linear-gradient(90deg, rgba(30,30,241,1) 0%, rgba(0,212,255,1) 35%, rgba(255,255,255,1) 100%); \" >        1 BHK Rent in Palani - 624601</div >"    