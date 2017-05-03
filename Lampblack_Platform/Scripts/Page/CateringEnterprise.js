var getTable = function () {
    base.AjaxGet("/Management/EditCateringEnterprise", null, function (obj) {
        slideUp.append(obj);
        slideUp.show();
    });
}

window.cateringEnterpriseFormatter = function () {
    return [
        '<a class="update" href="javascript:void(0)" title="编辑餐饮企业">编辑</a>',
        '<a class="delete" href="javascript:void(0)" title="删除餐饮企业" style="margin-left: 10px;">删除</a>'
    ].join('');
}

window.cateringEnterpriseEvents = {
    'click .update': function (e, value, row) {
        base.AjaxGet("/Management/EditCateringEnterprise", { guid: row.Id }, function (obj) {
            slideUp.append(obj);
            slideUp.show();
        });
    },
    'click .delete': function (e, value, row) {
        if (confirm("确定删除吗？")) {
            base.AjaxGet("/Management/DeleteCateringEnterprise", { guid: row.Id }, function (ret) {
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

    var ceqp = {
        QueryName: $('#queryName').val()
    }

    $('#cateringEnterprise_table').bootstrapTable({
        url: '/Management/CateringEnterpriseTable',
        queryParams: function (params) {
            params.QueryName = ceqp.QueryName;
            return params;
        },
        height: $('#cateringEnterprise_table').parents('.float-card').height() - 100
    });

    $('#cateringEnterpriseQuery').on('click', function () {
        ceqp = {
            QueryName: $('#queryName').val()
        }
        $('#cateringEnterprise_table').bootstrapTable('refresh',
            {
                url: '/Management/CateringEnterpriseTable'
            });
    });
});