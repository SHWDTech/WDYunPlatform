$(function () {
    window.statusFormatter = function (value) {
        if (value) {
            return '<img src="/Resources/Images/Site/CleanRate/RUN.png" />';
        } else {
            return '<img src="/Resources/Images/Site/CleanRate/STOP.png" />';
        }
    }

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

    var qp = {
        Area: $('#AreaGuid').val(),
        Street: $('#StreetGuid').val(),
        Address: $('#AddressGuid').val(),
        Name: $('#queryName').val()
    };

    var queryParams = function (params) {
        params.Area = qp.Area;
        params.Street = qp.Street;
        params.Address = qp.Address;
        params.Name = qp.Name;
        return params;
    }

    $('#actual_status').bootstrapTable({
        url: '/Monitor/ActualTable',
        queryParams: queryParams,
        height: $('#actual_status').parents('.float-card').height() - 100
    });

    $('#actualQuery').on('click', function () {
        qp = {
            Area: $('#AreaGuid').val(),
            Street: $('#StreetGuid').val(),
            Address: $('#AddressGuid').val(),
            Name: $('#queryName').val()
        };
        $('#actual_status').bootstrapTable('refresh');
    });

    $('#actual_status').on('load-success.bs.table', function (e, data) {
        var idx = 0;
        while (idx < data.merge.length) {
            $('#actual_status').bootstrapTable('mergeCells', { index: data.merge[idx].index, field: 'ProjectName', rowspan: data.merge[idx].count });
            idx++;
        }
    });
});