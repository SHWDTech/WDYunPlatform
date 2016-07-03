var getTable = function () {
    $.get("/System/EditRole", function (obj) {
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