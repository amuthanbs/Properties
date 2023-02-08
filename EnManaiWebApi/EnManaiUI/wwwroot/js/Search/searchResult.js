
var rentalData = null;
var user = null;
var userType = 'UnRegistered';
$(document).ready(function () {
    //debugger;
    //console.log('Seach JS');
    //console.log('rentalDetails');
    var s = window.location.href;

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
//function MyProfile() {
//    console.log("My Profile");
//    $.ajax({
//        type: "POST",
//        url: 'https://localhost:7041/api/Home/LandingPage?id=' + usr.logins[0].id,
//        //data: "city=" + search + "&encryptedUserCode=uiuoi" ,
//        contentType: 'application/json; charset=utf-8',
//        headers: { "Authorization": 'Bearer ' + usr.accessToken.token },
//        success: function (result, xhr, settings) {
//            console.log('Result:', result);
//            alert('Search Result Success')
//            rentalDetails = event.rentalDetails;
//            //console.log(rentalDetails);
//            //SetUpHouseDetail();
//            window.location.href = 'https://localhost:7059/Home/SearchResult?city=' + search;

//        },
//        error: function (result, xhr, settings) {
//            alert('API call is not made');
//        }
//    });
//}
//function SearchByCity() {
//    console.log('Search Text:');
//    var search = $('#search-input').val();
//    alert(search);

//    $.ajax({
//        type: "POST",
//        url: 'https://localhost:7041/api/Home/SearchResult?city=' + search + '&encryptedUserCode=' + encodeURIComponent(''),
//        //data: "city=" + search + "&encryptedUserCode=uiuoi" ,
//        contentType: 'application/json; charset=utf-8',
//        headers: { "Authorization": 'Bearer ' + usr.accessToken.token },
//        success: function (result, xhr, settings) {
//            console.log('Result:', result);
//            alert('Search Result Success')
//            rentalDetails = event.rentalDetails;
//            //console.log(rentalDetails);
//            //SetUpHouseDetail();
//            window.location.href = 'https://localhost:7059/Home/SearchResult?city=' + search;
//            loadRentalData();
//        },
//        error: function (result, xhr, settings) {
//            alert('API call is not made');
//        }
//    });
//}

function loadRentalData() {
    console.log(test);
    //$('.rentals').append(rentalDetails);
    $.each(rentalData, function (i, v) {
        var v1;
        if (userType === 'UnRegistered') {
            //v1 = rentalDetails;
            v1 = unregisterted;
            v2 = v1.replace('--CITY--', v.city).replace('--BHK--', v.bhk).replace('--DEPOSIT--', 5000).replace('--PINCODE--', v.pincode);
            $('#petsAllowed').prop('checked', v.petsAllowed);
            $('.rentals').append(v2);
        } else if (userType === 'Logged') {
            //v1 = rentalDetails;
            v1 = unregisterted;
            v2 = v1.replace('--CITY--', v.city).replace('--BHK--', v.bhk).replace('--DEPOSIT--', 5000).replace('--PINCODE--', v.pincode);
            $('#petsAllowed').prop('checked', v.petsAllowed);
            $('.rentals').append(v2);
        } else if (userType === 'Paid') {
            v1 = null;
        }
        
        console.log(v.city, v.pincode);
        
    });

}
/*style =\"content: \" \"; display: table; clear: both;\" */
//padding - bottom: 20px; border: 2px solid lightgrey; margin: 3px

var test = "<div id=\"header_idx\" style=\"width: 500px; background: linear - gradient(90deg, rgba(30, 30, 241, 1) 0 %, rgba(0, 212, 255, 1) 35 %, rgba(255, 255, 255, 1) 100 %);\" >1 BHK Rent in Palani - 624601 </div > ";
var unregisterted = "<div class=\"searchResultContent ma-10\"> <div style=\"padding-bottom: 10px;\" ><div style=\"float: left; width: 33.33%;\"><span id=\"buildarea\">--SQRT--</span></div><div style=\"float: left; width: 33.33%;\"><span id=\"bhkType\">--BHK--</span></div><div style=\"float: left; width: 33.33%;\"><span id=\"deposit\">--DEPOSIT--</span></div></div style=\"margin-top: 30px;\" ><div><div class=\"pa-10\" style=\"float: left; width: 40%;height:230px;;border: 2px solid lightgrey\"><div id=\"houseImage\">Image</div></div><div class=\"ma-10\" style=\"float: left; width: 60%;\"><div id=\"city\">--CITY--</div><div id=\"pincode\">--PINCODE--</div><div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"cooperationWater\"><label class=\"form-check-label\" for=\"cooperationWater\">Co-operation Water</label></div><div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"bachelor\"><label class=\"form-check-label\" for=\"Bachelor\">Bachelor</label></div><div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"petsAllowed\"><label class=\"form-check-label\" for=\"petsAllowed\">Pets Allowed</label></div></div></div></div></div >"
    var r = "< div id = \"header_idx\" style = \"width: 500px; background: linear-gradient(90deg, rgba(30,30,241,1) 0%, rgba(0,212,255,1) 35%, rgba(255,255,255,1) 100%); \" >        1 BHK Rent in Palani - 624601</div >"    