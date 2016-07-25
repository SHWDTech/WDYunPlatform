$(function () {
    $('#pageSize').on("change", function () {
        $('#history').submit();
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