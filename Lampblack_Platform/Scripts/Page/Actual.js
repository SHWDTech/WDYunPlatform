$(function () {
    window.statusFormatter = function (value) {
        if (value) {
            return '<img src="/Resources/Images/Site/CleanRate/RUN.png" />';
        } else {
            return '<img src="/Resources/Images/Site/CleanRate/STOP.png" />';
        }
    }

    window.cleanerFormatter = function(value) {
        switch (value) {
        case LampblackStatus.Fail:
            return '<img src="/Resources/Images/Site/CleanRate/F_3232.png" title="失效"/>';
        case LampblackStatus.noData:
            return '<img src="/Resources/Images/Site/CleanRate/N_3232.png" title="无数据"/>';
        case LampblackStatus.Worse:
            return '<img src="/Resources/Images/Site/CleanRate/W_3232.png" title="较差"/>';
        case LampblackStatus.Qualified:
            return '<img src="/Resources/Images/Site/CleanRate/Q_3232.png" title="合格"/>';
        case LampblackStatus.Good:
            return '<img src="/Resources/Images/Site/CleanRate/G_3232.png" title="良好"/>';
        }
        return '';
    };

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
        $('#actual_status').bootstrapTable('refresh',
            {
                url: '/Monitor/ActualTable'
            });
    });

    $('#actual_status').on('load-success.bs.table', function (e, data) {
        var idx = 0;
        while (idx < data.merge.length) {
            $('#actual_status').bootstrapTable('mergeCells', { index: data.merge[idx].index, field: 'ProjectName', rowspan: data.merge[idx].count });
            idx++;
        }
    });
});