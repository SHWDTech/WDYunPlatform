$(function () {
    $('#RegisterDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'L'
    });

    slideUp.append($('#cateringEdit'));
    $('#cateringEdit').show();

    $('#add').on('click', function() {
        slideUp.show();
    });

    $('#pageSize').on("change", function () {
        $('#catering').submit();
    });
})