$(function () {
    var getDistricts = function (id, select) {
        base.AjaxGet('/CommonAjax/GetAreaList', { id: id }, function (ret) {
            $(select).empty().append('<option value="none">全部</option>');
            $(ret.Result).each(function () {
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
        format: 'L'
    });
});