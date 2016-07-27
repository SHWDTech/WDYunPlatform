$(function () {
    var getDistricts = function (id, select) {
        base.AjaxGet('/CommonAjax/GetAreaList', { id: id }, function (ret) {
            $(select).empty().append('<option value="none">全部</option>');
            $(ret).each(function () {
                $(select).append('<option value=' + this.Id + '>' + this.ItemValue + '</option>');
            });
        });
    }

    $('#AreaGuid').on('change', function () {
        $('#AddressGuid').empty();
        getDistricts($(this).val(), $('#StreetGuid'));
    });

    $('#StreetGuid').on('change', function () {
        getDistricts($(this).val(), $('#AddressGuid'));
    });

    $('#StartDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'YYYY-MM',
        viewMode: 'months'
    });
    $('#DueDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'YYYY-MM',
        viewMode: 'months'
    });

    $('input[name=ReportType]').on('click', function () {
        $('#StartDateTime').data("DateTimePicker").destroy();
        $('#DueDateTime').data("DateTimePicker").destroy();
        if ($(this).attr('id') !== 'Month') {
            $('#StartDateTime')
                .datetimepicker({
                    locale: 'zh-cn',
                    format: 'YYYY',
                    viewMode: 'years'
                });
            $('#DueDateTime')
                .datetimepicker({
                    locale: 'zh-cn',
                    format: 'YYYY',
                    viewMode: 'years'
                });
        } else {
            $('#StartDateTime').datetimepicker({
                locale: 'zh-cn',
                format: 'YYYY-MM',
                viewMode: 'months'
            });
            $('#DueDateTime').datetimepicker({
                locale: 'zh-cn',
                format: 'YYYY-MM',
                viewMode: 'months'
            });
        }

        $('.extraSelect').hide();
        if ($(this).attr('id') === 'Season') {
            $('#StartSeason').show();
            $('#EndSeason').show();
        }

        if ($(this).attr('id') === 'Halfyear') {
            $('#StartHalfYear').show();
            $('#EndHalfYear').show();
        }
    });

    $('#query').on('click', function () {
        base.AjaxGet('/CommonAjax/GetTrendAnalysis', $('form').serialize(), function (ret) {
            debugger;
            alert(ret);
        });
    });
});