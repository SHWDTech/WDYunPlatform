$(function () {
    $('#reset').on('click', function () {
        resetValidation();
        $('#roleEdit').find('input[type=text]').val('');
        $('#roleEdit').find('select').val('');
    });
})