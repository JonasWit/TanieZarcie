// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
document.addEventListener('DOMContentLoaded', () => {

    // Get all "navbar-burger" elements
    const $navbarBurgers = Array.prototype.slice.call(document.querySelectorAll('.navbar-burger'), 0);

    // Check if there are any navbar burgers
    if ($navbarBurgers.length > 0) {

        // Add a click event on each of them
        $navbarBurgers.forEach(el => {
            el.addEventListener('click', () => {

                // Get the target from the "data-target" attribute
                const target = el.dataset.target;
                const $target = document.getElementById(target);

                // Toggle the "is-active" class on both the "navbar-burger" and the "navbar-menu"
                el.classList.toggle('is-active');
                $target.classList.toggle('is-active');

            });
        });
    }
});



//Cards
$(document).ready(function () {

    var stack1 = $('#stack1');

    stack1.removeClass('start');

    stack1.hammer().on('tap', function (event) {
        stack1.addClass('hover');
        event.stopPropagation();
    });


    $(document).hammer().on('tap', function (event) {
        stack1.removeClass('hover');
        $('.card-index').removeClass('hover');
    });

    $('.card-index').hammer().on('tap', function (event) {
        $('.card-index').removeClass('hover');
        $(this).addClass('hover');
    });
});

    //Cards 2
$(document).ready(function () {

    var stack1 = $('#stack2');

    stack1.removeClass('start');

    stack1.hammer().on('tap', function (event) {
        stack1.addClass('hover');
        event.stopPropagation();
    });


    $(document).hammer().on('tap', function (event) {
        stack1.removeClass('hover');
        $('.card-index-1').removeClass('hover');
    });

    $('.card-index-1').hammer().on('tap', function (event) {
        $('.card-index-1').removeClass('hover');
        $(this).addClass('hover');
    });
});







// Scroll down:
function scrollWin() {
    window.scrollBy(0, 900);
}


//Get the button:
mybutton = document.getElementById("myBtn");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
}