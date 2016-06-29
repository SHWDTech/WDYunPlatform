var catringEnterprise = [];

var getTable = function () {
    $('#edit-container').html('');
    $.get("/Management/EditCateringEnterprise", function (obj) {
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