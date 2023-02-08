var user = null;
var logins = null;
var rentalDetails = [];
var currentNoRecords = 0;
//$(window).unload(function () {
//    alert('Clearing local cache');
//    localStorage.clear();
//});
function reHost() {
    host = window.location.origin;
    return host;
}

function Signout() {
    ClearLocalStorage();
    redirect(reHost());
}

function setLocalRentalData(name, value) {
    localStorage.setItem(name, JSON.stringify(value));
    console.log(localStorage.getItem(name));
}

function getLocalRentalData(name) {
    if (!isNull(name))
        return localStorage.getItem(name);
    return null;
}


function SetLocalData(name, value) {
    localStorage.setItem(name, JSON.stringify(value));
    console.log(localStorage.getItem(name));
}
function ClearLocalStorage() {
    localStorage.clear();
}
function GetLocalData(name) {
    if (!isNull(name))
        return localStorage.getItem(name);
    return null;
}

function isNull(value) {
    if ((value === undefined) || (value === '') || (value === null))
        return true;
    return false;
}

function redirect(value) {
    window.location.href = value;
}

