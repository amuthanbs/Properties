var user = null;
var logins = null;
var rentalDetails = [];

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

function GetLocalData(name) {
    if (!isNull(name))
        return localStorage.getItem(name);
    return null;
}

function isNull(value) {
    if ((value === undefined) || (value === ''))
        return true;
    return false;
}