var getTable = function () {
    base.AjaxGet("/System/EditUser", null, function (obj) {
        slideUp.append(obj);
        slideUp.show();
    });
}

$(function () {
    $('#add').on('click', function () {
        getTable();
    });

    $('#pageSize').on("change", function () {
        $('#user').submit();
    });

    slideUp.Set({ 'top': '5%' });
});