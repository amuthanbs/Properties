var loginrequest = {
    'userName': '',
    'password': '',
    'forgetpassword': ''
}
$(document).ready(function () {
    console.log('hello. Welcome by Amuthan');
   

    
})
var logins;
function Submit() {
    debugger;
    loginrequest.userName = $('#txtUserName').val();
    loginrequest.password = $('#txtPassword').val();
    loginrequest.forgetpassword = false;

    console.log(JSON.stringify(loginrequest));
    
    $.ajax({
        type: "POST",
        url: 'https://localhost:7041/api/Home/GetLogin',
        data: JSON.stringify(loginrequest),
        dataType: 'json',
        contentType: 'application/json',
        success: function (event, xhr, settings) {
            debugger;
            logins = event.logins;
            console.log(logins);
        }
    });
}