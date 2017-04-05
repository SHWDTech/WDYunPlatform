$(function () {
    $('#StartDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'L'
    });

    $('#EndDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'L'
    });

    $('#query').on('click', function() {
        $('#history_data').bootstrapTable({
            url: '/Query/HistoryDataTable'
        });
    });

    var historyParams = function (params) {
        params.StartDateTime = $('#StartDateTime').val();
        params.EndDateTime = $('#EndDateTime').val();
        return params;
    };

    $('#history_data').bootstrapTable({
        url: '/Query/HistoryDataTable',
        queryParams: historyParams
    });

    $('#AddressGuid').select2();
});