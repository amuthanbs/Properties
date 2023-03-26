var loginrequest = {
    'userName': '',
    'password': '',
    'forgetpassword': ''
}

$(document).ready(function () {
    console.log('hello. Welcome by Amuthan');
    var login = null;
    usr = GetLocalData("user");
    if (!isNull(usr)) {
        usr = JSON.parse(usr);
        userType = 'Logged';
        login = usr.logins[0];
        //if (login.status === 'Active') {
        //    redirect(reHost() + '/Home/LoggedSearch');
        //}
    }
    setLocalRentalData('rentaldetails', null);
});




function Submit() {
    loginrequest.userName = $('#txtUserName').val();
    loginrequest.password = $('#txtPassword').val();
    loginrequest.forgetpassword = false;

    console.log(JSON.stringify(loginrequest));

    $.ajax({
        type: "POST",
        url: 'https://localhost:7041/api/App/GetLogin',
        //headers: { "Authorization": 'Bearer ' + usr.accessToken.token },
        data: JSON.stringify(loginrequest),
        dataType: 'json',
        async:false,
        contentType: 'application/json',
        success: function (result, xhr, settings) {
            login = result.logins[0];
            console.log(result);
            SetLocalData("user", result);
            if (login !== undefined) {
                if (result.status === 'Success') {
                        if (login.phoneNumberVerified) {
                            //window.location.href = 'https://localhost:7059/Home/Search';
                            window.location.href = 'https://localhost:7059/Home/LoggedSearch';
                        } else {
                            alert('Complete your phone verification');
                            window.location.href = 'https://localhost:7059/Home/SMSVerification';
                        }
                }
            } else {
                alert(result.errorMessage);
                window.location.href = 'https://localhost:7059/Home/Search';
            }
        },
        error: function (result  , xhr, settings) {
            alert('500 internal server error' );
        }
    });
}
