$(function () {
    $('#StartDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'YYYY-MM-DD'
    });

    $('#EndDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'YYYY-MM-DD'
    });

    var getDistricts = function (id, select) {
        base.AjaxGet('/CommonAjax/GetAreaList', { id: id }, function (ret) {
            $(select).empty();
            $(select).select2({
                data: ret.Result
            });
            $(select).trigger("select2:select");
        });
    }
    $.get('CommonAjax/UserDistrictSelections', null, function (ret) {
        var selecter = $('#AreaGuid').select2({
            data: ret.Result
        });
        $(selecter).on("select2:select", function (e) {
            getDistricts(e.params.data.id, $('#StreetGuid'));
        });
    });

    $('#StreetGuid').select2();
    $('#StreetGuid').on("select2:select", function (e) {
        if (e.params != null) {
            getDistricts(e.params.data.id, $('#AddressGuid'));
        } else {
            getDistricts(0, $('#AddressGuid'));
        }
    });
    $('#AddressGuid').select2();

    $('#cleanRate_table').bootstrapTable({
        url: '/Query/CleanRateTable',
        queryParams: function (params) {
            params.Area = $('#AreaGuid').val();
            params.Street = $('#StreetGuid').val();
            params.Address = $('#AddressGuid').val();
            params.StartDate = $('#StartDateTime').val();
            params.EndDate = $('#EndDateTime').val();
            return params;
        },
        height: $('#cleanRate_table').parents('.float-card').height() - 150
    });

    $('#cleanRateQuery').on('click', function () {
        $('#cleanRate_table').bootstrapTable('refresh',
            {
                url: '/Query/CleanRateTable'
            });
    });

    $('#cleanRate_table').on('load-success.bs.table', function (e, data) {
        var idx = 0;
        while (idx < data.merge.length) {
            $('#cleanRate_table').bootstrapTable('mergeCells', { index: data.merge[idx].index, field: 'ProjectName', rowspan: data.merge[idx].count });
            idx++;
        }
    });
});