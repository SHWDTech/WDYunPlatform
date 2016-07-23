$(function () {
    $('#pageSize').on("change", function () {
        $('#cleanRate').submit();
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