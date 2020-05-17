function activateSideBarAdminButton(buttonId) {
    var buttons = document.querySelectorAll('[id ^= "sidebar-button-"]');

    for (var i = 0; i < buttons.length; i++) {
        if (buttonId === buttons[i].id) {
            if (!buttons[i].classList.contains('tza-current')) {
                buttons[i].classList.add('tza-current');
            }  
        }
        else {
            if (buttons[i].classList.contains('tza-current')) {
                buttons[i].classList.remove('tza-current');
            }   
        }
    }      
}

$(document).ready(function () {
    $('.myTip').tooltip()
});