$(function () {
    $('#AreaGuid').on('change', function () {
        if (IsNullOrEmpty($('#AreaGuid').val())) {
            $('#StreetGuid').empty();
            return;
        }
        base.AjaxGet('/Management/GetAreaList', { id: $('#AreaGuid').val() }, function (ret) {
            $('#StreetGuid').empty();
            $('#StreetGuid').append('<option value="">全部</option>');
            $(ret.Result).each(function () {
                $('#StreetGuid').append('<option value=' + this.Id + '>' + this.ItemValue + '</option>');
            });
        });
    });

    $('.td-nav').on('click', function () {
        window.markerShowView($(this).attr('id'), 16);
    });
})