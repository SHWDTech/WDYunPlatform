$(function () {
    $('#ProductionDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'YYYY-MM-DD'
    });

    $('#reset').on('click', function () {
        resetValidation();
        $('#deviceEdit').find('input[type=text]').val('');
        $('#deviceEdit').find('select').val('');
    });

    $('#ProjectId').select2();
})