$(function () {
    $('#MaintenanceDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'L'
    });

    $('#reset').on('click', function () {
        resetValidation();
        $('#deviceMaintenceEdit').find('input[type=text]').val('');
        $('#deviceMaintenceEdit').find('select').val('');
    });
})