$(function () {
    if ($('#UpdateSuccessed').val() === 'ture') {
        $('#alertSuccess').show();
    }
});

var EncrypPassword = function (jqXhr, settings) {
    if ($('#Password').val() !== $('#CheckPassword').val()) {
        $('#CheckPassword').parent().addClass('has-error');
        $('#CheckPassword').popover('show');
        return false;
    }
    if ($('#Password').val() !== "") {
        // ReSharper disable once InconsistentNaming
        var hashObj = new jsSHA('SHA-256', 'TEXT', 1);
        hashObj.update($('#Password').val());
        $('#Password').val(hashObj.getHash('HEX'));
        hashObj.update($('#CheckPassword').val());
        $('#CheckPassword').val(hashObj.getHash('HEX'));

        var form = $('#setup');
        settings.data = form.serialize();

        return true;
    }
    return true;
};

var AlertResult = function () {
    debugger;
    if ($('#UpdateSuccessed').val() === 'True') {
        $('#alertSuccess').show();
    }
}