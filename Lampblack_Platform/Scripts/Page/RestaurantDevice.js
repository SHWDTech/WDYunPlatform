var getTable = function () {
    base.AjaxGet("/Management/EditDevice", null, function (obj) {
        slideUp.append(obj);
        slideUp.show();
    });
}

$(function () {
    $('#add').on('click', function () {
        getTable();
    });

    $('#pageSize').on("change", function () {
        $('#device').submit();
    });

    slideUp.Set({ 'top': '-5%' });
});