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

    $('#linkage_table').bootstrapTable({
        url: '/Query/LinkageRateTable',
        queryParams: function (params) {
            params.Area = $('#AreaGuid').val();
            params.Street = $('#StreetGuid').val();
            params.Address = $('#AddressGuid').val();
            params.QueryDateTime = $('#QueryDateTime').val();
            params.Name = $('#queryName').val();
            return params;
        },
        height: $('#running_time').parents('.float-card').height() - 150
    });

    $('#linkageRateQuery').on('click', function () {
        $('#linkage_table').bootstrapTable('refresh',
            {
                url: '/Query/LinkageRateTable'
            });
    });
});