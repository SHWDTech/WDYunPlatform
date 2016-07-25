$(function () {
    $('#pageSize').on("change", function () {
        $('#runningTime').submit();
    });

    $('#StartDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'L'
    });

    $('#EndDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'L'
    });
});