var getTable = function () {
    base.AjaxGet("/Management/EditHotel", null, function (obj) {
        slideUp.append(obj);
        slideUp.show();
    });
}

window.hotelOperateFormatter = function () {
    return [
        '<a class="update" href="javascript:void(0)" title="编辑酒店">编辑</a>',
        '<a class="delete" href="javascript:void(0)" title="删除删除" style="margin-left: 10px;">删除</a>'
    ].join('');
}

window.hotelEvents = {
    'click .update': function (e, value, row) {
        base.AjaxGet("/Management/EditHotel", { guid: row.Id }, function (obj) {
            slideUp.append(obj);
            slideUp.show();
        });
    },
    'click .delete': function (e, value, row) {
        if (confirm("确定删除吗？")) {
            base.AjaxGet("/Management/DeleteHotel", { guid: row.Id }, function (ret) {
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

    var hotelqp = {
        Area: $('#AreaGuid').val(),
        Street: $('#StreetGuid').val(),
        Address: $('#AddressGuid').val(),
        QueryName: $('#queryName').val()
    }

    $('#hotel_table').bootstrapTable({
        url: '/Management/HotelTable',
        queryParams: function (params) {
            params.Area = hotelqp.Area;
            params.Street = hotelqp.Street;
            params.Address = hotelqp.Address;
            params.QueryName = hotelqp.QueryName;
            return params;
        },
        height: $('#hotel_table').parents('.float-card').height() - 100
    });

    $('#hotelQuery').on('click', function () {
        hotelqp = {
            Area: $('#AreaGuid').val(),
            Street: $('#StreetGuid').val(),
            Address: $('#AddressGuid').val(),
            QueryName: $('#queryName').val()
        }
        $('#hotel_table').bootstrapTable('refresh',
            {
                url: '/Management/HotelTable'
            });
    });

    slideUp.Set({ 'top': '5%' });
});