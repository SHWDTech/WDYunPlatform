﻿var getTable = function () {
    $.get("/System/EditUser", function (obj) {
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