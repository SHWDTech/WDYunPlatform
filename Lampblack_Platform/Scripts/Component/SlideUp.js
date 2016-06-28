var slideUp = {};

$(function () {
    slideUp = $('#slide-up');
    slideUp.content = slideUp.find('.slide-up-content');
    $('.close-span').on('click', function() {
        slideUp.hide();
    });

    slideUp.append = function(target) {
        slideUp.content.append(target);
    };

    slideUp.show = function() {
        slideUp.removeClass('slide-up-hide');
        $('#mask').show();
    };

    slideUp.hide = function() {
        slideUp.addClass('slide-up-hide');
        $('#mask').hide();
    }

    slideUp.Set = function (options) {
        for (var key in options) {
            if (options.hasOwnProperty(key)) {
                slideUp.css(key, options[key]);
            }
        }
    }
})