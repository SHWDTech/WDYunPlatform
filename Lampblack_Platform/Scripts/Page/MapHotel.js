$(function() {
    $('#AreaGuid').on('change', function () {
        if (IsNullOrEmpty($('#AreaGuid').val())) {
            $('#StreetGuid').empty();
            return;
        }
        base.AjaxGet('/Management/GetAreaList', { id: $('#AreaGuid').val() }, function (ret) {
            $('#StreetGuid').empty();
            $('#StreetGuid').append('<option value="">全部</option>');
            $(ret).each(function () {
                $('#StreetGuid').append('<option value=' + this.ItemValue + '>' + this.Id + '</option>');
            });
        });
    });
})