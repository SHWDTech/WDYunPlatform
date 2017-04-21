var getTable = function () {
    base.AjaxGet("/Management/EditDevice", null, function (obj) {
        slideUp.append(obj);
        slideUp.show();
    });
}

window.deviceOperateFormatter = function () {
    return [
        '<a class="update" href="javascript:void(0)" title="编辑设备">编辑</a>',
        '<a class="delete" href="javascript:void(0)" title="删除设备" style="margin-left: 10px;">删除</a>'
    ].join('');
}

window.deviceEvents = {
    'click .update': function (e, value, row) {
        base.AjaxGet("/Management/EditDevice", { guid: row.Id }, function (obj) {
            slideUp.append(obj);
            slideUp.show();
        });
    },
    'click .delete': function (e, value, row) {
        if (confirm("确定删除吗？")) {
            base.AjaxGet("/Management/DeleteDevice", { guid: row.Id }, function (ret) {
                base.Msg(ret.Message);
            });
        }
    }
};

$(function () {
    $('#add').on('click', function () {
        getTable();
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

    var devqp = {
        Area: $('#AreaGuid').val(),
        Street: $('#StreetGuid').val(),
        Address: $('#AddressGuid').val(),
        QueryName: $('#queryName').val()
    }

    $('#device_table').bootstrapTable({
        url: '/Management/DeviceTable',
        queryParams: function (params) {
            params.Area = devqp.Area;
            params.Street = devqp.Street;
            params.Address = devqp.Address;
            params.QueryName = devqp.QueryName;
            return params;
        },
        height: $('#device_table').parents('.float-card').height() - 100
    });

    $('#deviceQuery').on('click', function () {
        devqp = {
            Area: $('#AreaGuid').val(),
            Street: $('#StreetGuid').val(),
            Address: $('#AddressGuid').val(),
            QueryName: $('#queryName').val()
        }
        $('#device_table').bootstrapTable('refresh',
            {
                url: '/Management/DeviceTable'
            });
    });

    slideUp.Set({ 'top': '-5%' });
});