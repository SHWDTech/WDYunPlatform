var trendChart = null;

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

    trendChart = echarts.init(document.getElementById('trendChart'));

    $('#query').on('click', function () {
        debugger;
        var form = $('form').serialize();
        form.StartDateTime = $('#StartDateTime').val();
        form.DueDateTime = $('#DueDateTime').val();
        base.AjaxGet('/CommonAjax/GetTrendAnalysis', form, function (ret) {
            var option = Echart_Tools.getOption();
            option.title.text = "趋势分析";
            option.legend.data = ["趋势分析"];
            option.series[0].name = "趋势分析";
            var xAxisData = [];
            var seriesData = [];
            $.each(ret, function () {
                xAxisData.push($(this)[0].Date);
                seriesData.push($(this)[0].Linkage);
            });

            option.xAxis.data = xAxisData;
            option.series[0].data = seriesData;

            trendChart.clear();
            trendChart.setOption(option);
        });
    });
});