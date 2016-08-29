var getTable = function () {
    base.AjaxGet("/System/EditRole", null, function (obj) {
        slideUp.append(obj);
        slideUp.show();
    });
}

$(function () {
    $('#add').on('click', function () {
        getTable();
    });

    $('#pageSize').on("change", function () {
        $('#role').submit();
    });

    slideUp.Set({ 'top': '5%' });
});