var catringEnterprise = [];

var getTable = function () {
    $('#edit-container').html('');
    $.get("/Management/EditCateringEnterprise", function (obj) {
        $('#edit-container').html(obj);
        slideUp.show();
    });
}

$(function () {
    slideUp.append($('#edit-container'));
    $('#edit-container').show();

    $('#add').on('click', function () {
        getTable();
    });

    $('#pageSize').on("change", function () {
        $('#catering').submit();
    });
});