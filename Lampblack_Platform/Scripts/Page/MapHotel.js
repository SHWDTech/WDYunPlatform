$(function() {
    $('#AreaGuid').on('change', function () {
        if (IsNullOrEmpty($('#AreaGuid').val())) {
            $('#street').empty();
            $('#street').append('<option value="">全部</option>');
            return;
        }
        base.AjaxGet('/Management/GetAreaList', { id: $('#AreaGuid').val() }, function (ret) {
            $('#street').empty();
            $('#street').append('<option value="">全部</option>');
            $(ret).each(function () {
                $('#street').append('<option value=' + this.Id + '>' + this.ItemValue + '</option>');
            });
            if (ret.length > 0) {
                $('#street').val(ret[0].Id).change();
            }
        });
    });
})