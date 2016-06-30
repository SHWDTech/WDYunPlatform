$(function () {
    $('#RegisterDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'L'
    });

    $('#OpeningDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'HH:mm'
    });

    $('#StopDateTIme').datetimepicker({
        locale: 'zh-cn',
        format: 'HH:mm'
    });

    $('#reset').on('click', function () {
        resetValidation();
        $('#hotelEdit').find('input[type=text]').val('');
        $('#hotelEdit').find('select').val('');
    });

    var getDistricts = function (id, select) {
        base.AjaxGet('/Management/GetAreaList', { id: id }, function (ret) {
            $(select).empty();
            $(ret).each(function () {
                $(select).append('<option value=' + this.Id + '>' + this.ItemValue + '</option>');
            });
            if (ret.length > 0) {
                $(select).val(ret[0].Id).change();
            }
        });
    }

    $('#DistrictId').on('change', function () {
        $('#AddressId').empty();
        getDistricts($(this).val(), $('#StreetId'));
    });

    $('#StreetId').on('change', function () {
        getDistricts($(this).val(), $('#AddressId'));
    });

    $('#DistrictId').change();
})