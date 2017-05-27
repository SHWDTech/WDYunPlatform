$(function () {
    $('#StartDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'YYYY-MM-DD'
    });

    $('#EndDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'YYYY-MM-DD'
    });

    $('#alarm_table').bootstrapTable({
        url: '/Query/AlarmDataTable',
        height: $('#alarm_table').parents('.float-card').height() - 100
    });

    $('#query').on('click', function () {
        $('#alarm_table').bootstrapTable('refresh' ,{
            url: '/Query/AlarmDataTable'
        });
    });
});