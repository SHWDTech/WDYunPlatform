var getTable = function () {
    base.AjaxGet("/System/EditDepartment", function (obj) {
        slideUp.append(obj);
        slideUp.show();
    });
}

$(function () {
    $('#add').on('click', function () {
        getTable();
    });

    $('#pageSize').on("change", function () {
        $('#department').submit();
    });

    slideUp.Set({ 'top': '5%' });
});