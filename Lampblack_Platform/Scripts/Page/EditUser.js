$(function () {
    $('#reset').on('click', function () {
        resetValidation();
        $('#userEdit').find('input[type=text]').val('');
        $('#userEdit').find('select').val('');
        $('#userEdit').find('input[type=checkbox]').prop('checked', false);
    });
});

var EncrypPassword = function (jqXhr, settings) {
    // ReSharper disable once InconsistentNaming
    var hashObj = new jsSHA('SHA-256', 'TEXT', 1);
    hashObj.update($('#Password').val());
    $('#Password').val(hashObj.getHash('HEX'));

    var form = $('#userEdit');
    settings.data = form.serialize();

    return true;
};