$(function() {
    $('.form-control').on('focus', function() {
                $(this).parents('.textbox-wrap').addClass("focuesd");
            });

    $('.form-control').on('blur', function() {
                $(this).parents('.textbox-wrap').removeClass("focuesd");
            });
});


var EncrypSubmit = function() {
// ReSharper disable once InconsistentNaming
    var hashObj = new jsSHA('SHA-256', 'TEXT', 1);
    hashObj.update($('#Password').val());
    $('#Password').val(hashObj.getHash('HEX'));
    document.getElementById('loginform').submit();
};