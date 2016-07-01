$(function () {
    $('#ProductionDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'L'
    });

    $('#reset').on('click', function () {
        resetValidation();
        $('#deviceEdit').find('input[type=text]').val('');
        $('#deviceEdit').find('select').val('');
    });
})