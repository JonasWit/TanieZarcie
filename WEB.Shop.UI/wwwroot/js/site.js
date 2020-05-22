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

$(document).ready(function () {

    var stack1 = $('#stack1');

    stack1.hammer().on('tap', function (event) {
        stack1.addClass('hover');
        event.stopPropagation();
    });

    $(document).hammer().on('tap', function (event) {
        stack1.removeClass('hover');
        $('.card-front').removeClass('hover');
    });

    $('.card-front').hammer().on('tap', function (event) {
        $('.card-front').removeClass('hover');
        $(this).addClass('hover');
    });
});

$(document).ready(function () {

    var stack1 = $('#stack1');

    stack1.removeClass('start');

    stack1.hammer().on('tap', function (event) {
        stack1.addClass('hover');
        event.stopPropagation();
    });


    $(document).hammer().on('tap', function (event) {
        stack1.removeClass('hover');
        $('.card').removeClass('hover');
    });

    $('.card').hammer().on('tap', function (event) {
        $('.card').removeClass('hover');
        $(this).addClass('hover');
    });
});
