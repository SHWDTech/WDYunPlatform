﻿$(function () {
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

    var rtqp = {
        Area: $('#AreaGuid').val(),
        Street: $('#StreetGuid').val(),
        Address: $('#AddressGuid').val(),
        StartDate: $('#StartDateTime').val(),
        EndDate: $('#EndDateTime').val(),
        Name: $('#queryName').val()
    }

    $('#running_time').bootstrapTable({
        url: '/Query/RunningTimeTable',
        queryParams: function (params) {
            params.Area = rtqp.Area;
            params.Street = rtqp.Street;
            params.Address = rtqp.Address;
            params.StartDate = rtqp.StartDate;
            params.EndDate = rtqp.EndDate;
            params.Name = rtqp.Name;
            return params;
        },
        height: $('#running_time').parents('.float-card').height() - 150
    });

    $('#runningTimeQuery').on('click', function () {
        rtqp = {
            Area: $('#AreaGuid').val(),
            Street: $('#StreetGuid').val(),
            Address: $('#AddressGuid').val(),
            StartDate: $('#StartDateTime').val(),
            EndDate: $('#EndDateTime').val(),
            Name: $('#queryName').val()
        }
        $('#running_time').bootstrapTable('refresh',
            {
                url: '/Query/RunningTimeTable'
            });
    });

    $('#running_time').on('load-success.bs.table', function (e, data) {
        var idx = 0;
        while (idx < data.merge.length) {
            $('#running_time').bootstrapTable('mergeCells', { index: data.merge[idx].index, field: 'ProjectName', rowspan: data.merge[idx].count });
            idx++;
        }
    });
});