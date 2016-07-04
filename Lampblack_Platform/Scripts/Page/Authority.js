$(function() {
    $('.parent-permission').on('click', function () {
        var value = $(this).val();
        $('.child-permission[data-parent=' + value + ']').prop('checked', $(this).prop('checked')).prop('disabled', !$(this).prop('checked'));
    });
})