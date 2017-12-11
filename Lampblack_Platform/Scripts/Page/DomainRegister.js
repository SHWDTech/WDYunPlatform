$(function () {
    $('#reset').on('click', function () {
        resetValidation();
        $('#userEdit').find('input[type=text]').val('');
        $('#userEdit').find('select').val('');
        $('#userEdit').find('input[type=checkbox]').prop('checked', false);
    });
});

var EncrypPassword = function (jqXhr, settings) {
    if ($('#ManagerPassword').val() !== "") {
        // ReSharper disable once InconsistentNaming
        var hashObj = new jsSHA('SHA-256', 'TEXT', 1);
        hashObj.update($('#ManagerPassword').val());
        $('#ManagerPassword').val(hashObj.getHash('HEX'));

        var form = $('#domainRegister');
        settings.data = form.serialize();

        return true;
    }
    return true;
};