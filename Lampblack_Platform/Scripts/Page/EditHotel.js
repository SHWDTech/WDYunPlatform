$(function () {
    var load = document.createElement("script");
    load.src = "http://api.map.baidu.com/api?v=1.4&callback=init_EditHotel()";
    document.body.appendChild(load);

    $('#RegisterDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'L'
    });

    $('#OpeningDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'HH:mm'
    });

    $('#StopDateTime').datetimepicker({
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
});

function init_EditHotel() {
    $('#getLocation').on('click', function () {
        var geo = new window.BMap.Geocoder();
        geo.getPoint($('#AddressDetail').val(), function (point) {
            if (point) {
                $('#Longitude').val(point.lng);
                $('#Latitude').val(point.lat);
            } else {
                Msg('获取经纬度信息失败，请填写有效的详细地址！', { title: '提示！' });
            }
        });
    });
}