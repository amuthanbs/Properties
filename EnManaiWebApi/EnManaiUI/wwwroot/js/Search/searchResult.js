
var rentalData = null;
$(document).ready(function () {
    console.log('Seach JS');
    console.log('rentalDetails');
    rentalData = JSON.parse(getLocalRentalData("rentaldetails"));
    loadRentalData();
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
    debugger;
    console.log(test);
    //$('.rentals').append(rentalDetails);
    $.each(rentalData, function (i, v){
        var v1 = rentalDetails;
        console.log(v.city, v.pincode);
        v2 = v1.replace('--CITY--', v.city).replace('--BHK--', v.bhk).replace('--DEPOSIT--', 5000).replace('--PINCODE--', v.pincode);
        $('#petsAllowed').prop('checked', v.petsAllowed);
        $('.rentals').append(v2);
    });

}
var test = "<div id=\"header_idx\" style=\"width: 500px; background: linear - gradient(90deg, rgba(30, 30, 241, 1) 0 %, rgba(0, 212, 255, 1) 35 %, rgba(255, 255, 255, 1) 100 %);\" >1 BHK Rent in Palani - 624601 </div > ";
var rentalDetails = "<div class=\"searchResultContent\"> <div style=\"content: \" \"; display: table; clear: both;\" ><div style=\"float: left; width: 33.33%;\"><span id=\"buildarea\">--SQRT--</span></div><div style=\"float: left; width: 33.33%;\"><span id=\"bhkType\">--BHK--</span></div><div style=\"float: left; width: 33.33%;\"><span id=\"deposit\">--DEPOSIT--</span></div></div ><div style=\"content: \" \"; display: table; clear: both;\" ><div style=\"float: left; width: 60%;\"><div id=\"houseImage\">Image</div></div><div style=\"float: left; width: 40%;\"><div id=\"city\">--CITY--</div><div id=\"pincode\">--PINCODE--</div><div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"cooperationWater\"><label class=\"form-check-label\" for=\"cooperationWater\">Co-operation Water</label></div><div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"bachelor\"><label class=\"form-check-label\" for=\"Bachelor\">Bachelor</label></div><div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"petsAllowed\"><label class=\"form-check-label\" for=\"petsAllowed\">Pets Allowed</label></div></div></div></div></div >"
    var r = "< div id = \"header_idx\" style = \"width: 500px; background: linear-gradient(90deg, rgba(30,30,241,1) 0%, rgba(0,212,255,1) 35%, rgba(255,255,255,1) 100%); \" >        1 BHK Rent in Palani - 624601</div >"    