$(function () {
    $('#reset').on('click', function () {
        resetValidation();
        $('#userEdit').find('input[type=text]').val('');
        $('#userEdit').find('select').val('');
    });
})