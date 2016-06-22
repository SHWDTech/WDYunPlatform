$(function () {
    $('.main-menu-item').on('click', function () {
        if ($(this)[0].actived === true) {
            $('.sub-menu').removeClass('active');
            $.each($('.main-menu-item'), function () {
                $(this).removeClass('active');
                $(this)[0].actived = false;
            });
        } else {
            $.each($('.main-menu-item'), function () {
                $(this).removeClass('active');
                $(this)[0].actived = false;
            });
            $(this).addClass('active');
            $('.sub-menu').removeClass('active');
            $($(this)[0].nextElementSibling).addClass('active');
            $(this)[0].actived = true;
        }
    });
});