var loginrequest = {
    'userName': '',
    'password': '',
    'forgetpassword': ''
}

$(document).ready(function () {
    console.log('hello. Welcome by Amuthan');

    //export { logins };
    var login = null;
    
})

function Submit() {
    debugger;
    loginrequest.userName = $('#txtUserName').val();
    loginrequest.password = $('#txtPassword').val();
    loginrequest.forgetpassword = false;

    console.log(JSON.stringify(loginrequest));

    $.ajax({
        type: "POST",
        url: 'https://localhost:7041/api/Home/GetLogin',
        //headers: { "Authorization": 'Bearer ' + usr.accessToken.token },
        data: JSON.stringify(loginrequest),
        dataType: 'json',
        contentType: 'application/json',
        success: function (result, xhr, settings) {
            login = result.logins[0];
            console.log(result);
            SetLocalData("user",result);
            if (result.status === 'Success') {
                if (login.mandatoryVerification) {
                    if (!login.reVerification) {
                        if (login.phoneNumberVerified) {
                            window.location.href = 'https://localhost:7059/Home/Search';
                        } else {
                            alert('Complete your phone verification');
                            window.location.href = 'https://localhost:7059/';
                            return;
                        }
                    } else {
                        alert('do your phone verification');
                        window.location.href = 'https://localhost:7059';
                        return;
                    }
                }
            } else {
                alert('Login Failed', result.errorMessage);
                window.location.href = 'https://localhost:7059/';
            }
        },
        error: function (result  , xhr, settings) {
            alert('500 internal server error' );
        }
    });
}
