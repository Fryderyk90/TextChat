//
//
let login = document.getElementById('loginCard');
let register = document.getElementById('registerCard');
window.onload = function (register) {
    document.getElementById('registerCard').style.display = 'none';
};

function renderRegisterPartial() {
    document.getElementById('loginCard').style.display = 'none';
    document.getElementById('registerCard').style.display = 'block';
}


function renderLoginPartial() {
    document.getElementById('registerCard').style.display = 'none';
    document.getElementById('loginCard').style.display = 'block';
}