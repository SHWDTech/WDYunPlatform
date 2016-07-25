$(function () {
    $('#pageSize').on("change", function () {
        $('#linkAge').submit();
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