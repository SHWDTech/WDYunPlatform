$(function () {
    $('#QueryDateTime').datetimepicker({
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

    var lkrqp = {
        Area: $('#AreaGuid').val(),
        Street: $('#StreetGuid').val(),
        Address: $('#AddressGuid').val(),
        QueryDateTime: $('#QueryDateTime').val(),
        Name: $('#queryName').val()
    }

    $('#linkage_table').bootstrapTable({
        url: '/Query/LinkageRateTable',
        queryParams: function(params) {
            params.Area = lkrqp.Area;
            params.Street = lkrqp.Street;
            params.Address = lkrqp.Address;
            params.QueryDateTime = lkrqp.QueryDateTime;
            params.Name = lkrqp.Name;
            return params;
        },
        height: $('#linkage_table').parents('.float-card').height() - 150
    });

    $('#linkageRateQuery').on('click', function () {
        lkrqp = {
            Area: $('#AreaGuid').val(),
            Street: $('#StreetGuid').val(),
            Address: $('#AddressGuid').val(),
            QueryDateTime: $('#QueryDateTime').val(),
            Name: $('#Name').val()
        }

        $('#linkage_table').bootstrapTable('refresh',
            {
                url: '/Query/LinkageRateTable'
            });
    });

    $('#linkage_table').on('load-success.bs.table', function (e, data) {
        var idx = 0;
        while (idx < data.merge.length) {
            $('#linkage_table').bootstrapTable('mergeCells', { index: data.merge[idx].index, field: 'ProjectName', rowspan: data.merge[idx].count });
            idx++;
        }
    });
});