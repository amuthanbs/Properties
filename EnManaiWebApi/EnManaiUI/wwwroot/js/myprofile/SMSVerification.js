var user = null;
var login = null;
var host = null;
$(document).ready(function () {
    user = JSON.parse(GetLocalData("user"));
    if (isNull(user)) {
        redirect(host + 'Home/LandingPage');
    }
    console.log('Login User:', user);
    login = user.logins[0];
    $('#txtPhonenumber').val(login.phoneNumber);
    host = window.location.origin;
    console.log(host);
});
var SMSVerifyCodeRequest = {
    "username": "",
    "loginId": "",
    "phonenumber": "",
    "createdOn": "",
    "smsCode": ""
};
function VerifiyBySMSCode(e) {
    alert('SMS Code Verification');
    SMSVerifyCodeRequest.loginId = login.id + "";
    SMSVerifyCodeRequest.phonenumber = login.phoneNumber;
    SMSVerifyCodeRequest.username = login.userName;
    var d = new Date();
    month = d.getMonth() < 10 ? ('0' + (d.getMonth() + 1)) : (d.getMonth() + 1);
    SMSVerifyCodeRequest.createdOn = d.getFullYear() + '-' + month + '-' + d.getDate();
    SMSVerifyCodeRequest.smsCode = $('#txtVerificationCode').val();
    $.ajax({
        type: "POST",
        url: 'https://localhost:7041/api/Verification/VerifySMSCode',
        //url: host+'/api/Verification/VerifySMSCode',
        data: JSON.stringify(SMSVerifyCodeRequest),
        headers: { "Authorization": 'Bearer ' + user.accessToken.token },
        //dataType: 'json',
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (result, xhr, settings) {
            if (result.status === 'Success') {  
                alert('Success update');
                //e.preventDefault();
                //1.Update login table updated in smsverification.
                //2.redirect to logged page.
                redirect(host + '/Home/LoggedSearch');
            } else {
                alert(result.errorMessage);
                e.preventDefault();
                return false;
            }
            /// Update the Login table.
        },
        error: function (result, xhr, settings) {
            console.log(result);
            //Verification/SendSMS
            //window.location.href = 'https://localhost:7059/Home/LandingPage';
        }
    });
}

function ReSendSMSCode(e) {
    alert('Resend SMS');
    if (!isNull(user) && (login.status === 'Active')) {
        console.log('Triggering SMS');

        alert('SMS Code Verification');
        SMSVerifyCodeRequest.loginId = login.id + "";
        SMSVerifyCodeRequest.phonenumber = login.phoneNumber;
        SMSVerifyCodeRequest.username = login.userName;
        var d = new Date();
        month = d.getMonth() < 10 ? ('0' + (d.getMonth() + 1)) : (d.getMonth() + 1);
        SMSVerifyCodeRequest.createdOn = d.getFullYear() + '-' + month + '-' + d.getDate();
        SMSVerifyCodeRequest.smsCode = $('#txtVerificationCode').val();
        $.ajax({
            type: "POST",
            url: 'https://localhost:7041/api/Verification/SendSMS',
            //url: host+'/api/Verification/VerifySMSCode',
            data: JSON.stringify(SMSVerifyCodeRequest),
            headers: { "Authorization": 'Bearer ' + user.accessToken.token },
            //dataType: 'json',
            async: false,
            contentType: 'application/json; charset=utf-8',
            success: function (result, xhr, settings) {
                if (result.status === 'Success') {
                    alert('Success update');
                    //e.preventDefault();
                    //1.Update login table updated in smsverification.
                    //2.redirect to logged page.
                    //redirect(host + '/Home/LoggedSearch');
                } else {
                    alert(result.errorMessage);
                    ClearLocalStorage();
                    redirect(host + '/Home')
                }
                /// Update the Login table.
            },
            error: function (result, xhr, settings) {
                console.log(result);
                //Verification/SendSMS
                //window.location.href = 'https://localhost:7059/Home/LandingPage';
            }
        });

    } else {
        redirect(host + '/Home/LandingPage');
    }
}