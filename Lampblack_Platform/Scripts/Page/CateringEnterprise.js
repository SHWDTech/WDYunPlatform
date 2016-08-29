var getTable = function () {
    base.AjaxGet("/Management/EditCateringEnterprise", null, function (obj) {
        slideUp.append(obj);
        slideUp.show();
    });
}

$(function () {
    $('#add').on('click', function () {
        getTable();
    });

    $('#pageSize').on("change", function () {
        $('#catering').submit();
    });
});