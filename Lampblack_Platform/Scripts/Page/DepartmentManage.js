var getTable = function () {
    $.get("/System/EditDepartment", function (obj) {
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