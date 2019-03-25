function openTab(evt, cityName, init) {
    var i;
    var x = document.getElementsByClassName("tab");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    var tablinks = document.getElementsByClassName("tablink");

    for (i = 0; i < x.length; i++) {
        if (init === false) {
            tablinks[i].className = tablinks[i].className.replace(" w3-border-red", "");
        } else {
            tablinks[i].className += " w3-border-red";
            init = false;
        }
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.firstElementChild.className += " w3-border-red";


}


$(document).ready(function () {
        openTab($('#init-tab'), "Search", true);

});