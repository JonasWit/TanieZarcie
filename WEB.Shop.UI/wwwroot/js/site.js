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

//Smooth scrolling 

$(document).ready(function () {
    // Add smooth scrolling to all links
    $("a").on('click', function (event) {

        // Make sure this.hash has a value before overriding default behavior
        if (this.hash !== "") {
            // Prevent default anchor click behavior
            event.preventDefault();

            // Store hash
            var hash = this.hash;

            // Using jQuery's animate() method to add smooth page scroll
            // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 800, function () {

                // Add hash (#) to URL when done scrolling (default click behavior)
                window.location.hash = hash;
            });
        } // End if
    });
});









// Scroll down:
function scrollWin() {
    window.scrollBy(0, 1100);
}


// Scroll up:
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


//Index page counters

const counters = document.querySelectorAll('.counter');
const speed = 100; // The lower the slower

counters.forEach(counter => {
    const updateCount = () => {
        const target = +counter.getAttribute('data-target');
        const count = +counter.innerText;

        // Lower inc to slow and higher to slow
        const inc = target / speed;

        // console.log(inc);
        // console.log(count);

        // Check if target is reached
        if (count < target) {
            // Add inc to count and output in counter
            counter.innerText = count + inc;
            // Call function every ms
            setTimeout(updateCount, 1);
        } else {
            counter.innerText = target;
        }
    };

    updateCount();
});