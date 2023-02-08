
var usr;
var userType = 'UnRegistered';

$(document).ready(function () {
    console.log('Seach JS');
    console.log('rentalDetails');
    usr = GetLocalData("user");
    if (!isNull(usr)) {
        usr = JSON.parse(usr);
        userType = 'Logged';
        login = usr.logins[0];
        //if (login.status === 'Active') {
        //    redirect(reHost() + '/Home/LoggedSearch');
        //}
    } else {
        userType = 'UnRegistered';
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

function loadMore() {
    alert(currentNoRecords);
}
function SearchByCity() {
    console.log('Search Text:');
    var search = $('#search-input').val();
  //  alert(search);
    //NonRegisteredSearchResult
    if (userType === "UnRegistered") {
        $.ajax({
            type: "POST",
            url: 'https://localhost:7041/api/App/NonRegisteredSearchResult?city=' + search + '&encryptedUserCode=' + encodeURIComponent(''),
            contentType: 'application/json; charset=utf-8',
            success: function (result, xhr, settings) {
                console.log('Result:', result);
                rentalDetails = result.rentalDetails;
                currentNoRecords = result.rentalDetails.length;
                alert(currentNoRecords);
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
            url: 'https://localhost:7041/api/App/SearchResult?city=' + search + '&encryptedUserCode=' + encodeURIComponent('') + '&id=' + usr.logins[0].id,
            //data: "city=" + search + "&encryptedUserCode=uiuoi" ,  
            contentType: 'application/json; charset=utf-8',
            headers: { "Authorization": 'Bearer ' + usr.accessToken.token },
            success: function (result, xhr, settings) {
                console.log('Result:', result);
                // alert('Search Result Success')
                rentalDetails = result.rentalDetails;
                currentNoRecords = result.rentalDetails.length;
                setLocalRentalData("rentaldetails", rentalDetails);
                //console.log(rentalDetails);
                //SetUpHouseDetail();
                window.location.href = 'https://localhost:7059/Home/LoggedSearch?city=' + search;
                
            },
            error: function (result, xhr, settings) {
                alert('API call is not made');
            }
        });
    }
    
}
//function loadRentalData() {
//    console.log(rentalDetails);
//}
var rentalDetails = "<div class=\"searchResultContent\">< div id = \"header_idx\" style = \"width: 500px; background: linear-gradient(90deg, rgba(30,30,241,1) 0%, rgba(0,212,255,1) 35%, rgba(255,255,255,1) 100%); \" >        1 BHK Rent in Palani - 624601</div >    <div style=\"content: \" \"; display: table; clear: both;\" ><div style=\"float: left; width: 33.33%;\"><span id=\"buildarea\">600 sqft</span></div><div style=\"float: left; width: 33.33%;\"><span id=\"bhkType\">1 BHK</span></div><div style=\"float: left; width: 33.33%;\"><span id=\"deposit\">50000 Deposit</span></div></div ><div style=\"content: \" \"; display: table; clear: both;\" ><div style=\"float: left; width: 60%;\"><div id=\"houseImage\">Image</div></div><div style=\"float: left; width: 40%;\"><div id=\"city\">City</div><div id=\"pincode\">Pincode</div><div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"cooperationWater\"><label class=\"form-check-label\" for=\"cooperationWater\">Co-operation Water</label></div><div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"bachelor\"><label class=\"form-check-label\" for=\"Bachelor\">Bachelor</label></div><div class=\"form-check\"><input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"petsAllowed\"><label class=\"form-check-label\" for=\"petsAllowed\">Pets Allowed</label></div></div></div></div></div > "