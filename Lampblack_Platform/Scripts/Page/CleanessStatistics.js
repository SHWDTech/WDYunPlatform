﻿var cleanessChart = null;

$(function () {
    var getDistricts = function (id, select) {
        base.AjaxGet('/CommonAjax/GetAreaList', { id: id }, function (ret) {
            $(select).empty().append('<option value="none">全部</option>');
            $(ret).each(function () {
                $(select).append('<option value=' + this.Id + '>' + this.ItemValue + '</option>');
            });
        });
    }

    $('#AreaGuid').on('change', function () {
        $('#AddressGuid').empty();
        getDistricts($(this).val(), $('#StreetGuid'));
    });

    $('#StreetGuid').on('change', function () {
        getDistricts($(this).val(), $('#AddressGuid'));
    });

    $('#DueDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'YYYY-MM',
        viewMode: 'months'
    });

    cleanessChart = echarts.init(document.getElementById('cleanessChart'));
});