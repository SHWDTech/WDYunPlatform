var getTable = function () {
    $.get("/Management/EditHotel", function (obj) {
        slideUp.append(obj);
        slideUp.show();
    });
}

$(function () {
    $('#add').on('click', function () {
        getTable();
    });

    $('#pageSize').on("change", function () {
        $('#hotel').submit();
    });

    slideUp.Set({ 'top': '5%' });
});