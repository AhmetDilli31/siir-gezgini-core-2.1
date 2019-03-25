function search() {
    var key = $('#txt-search').val();
    if (key==="" || key==="undefined") {
        return;
    }
    window.location = "/home/search?search=" + key;
}

function keyPressListener(e) {
    if (e.keyCode == 13) {
        search();
    }
}
