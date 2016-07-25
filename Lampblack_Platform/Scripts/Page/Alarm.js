$(function () {
    $('#pageSize').on("change", function () {
        $('#alarm').submit();
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