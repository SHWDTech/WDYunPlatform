$(function () {
    $('#reset').on('click', function () {
        resetValidation();
        $('#departmentEdit').find('input[type=text]').val('');
        $('#departmentEdit').find('select').val('');
    });
})