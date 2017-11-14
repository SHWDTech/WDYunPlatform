var mainChart = null;

$(function () {
    window.statusFormatter = function (value) {
        if (value) {
            return '<img src="/Resources/Images/Site/CleanRate/RUN.png" />';
        } else {
            return '<img src="/Resources/Images/Site/CleanRate/STOP.png" />';
        }
    }

    mainChart = echarts.init(document.getElementById('historyBar'));

    $('#StartDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'YYYY-MM-DD HH:mm'
    });

    $('#EndDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'YYYY-MM-DD HH:mm'
    });

    var hdqp = {
        Hotel: $('#DistrictHotels').val(),
        StartDate: $('#StartDateTime').val(),
        EndDate: $('#EndDateTime').val()
    }

    var table;

    $('#query').on('click', function () {
        hdqp = {
            Hotel: $('#DistrictHotels').val(),
            StartDate: $('#StartDateTime').val(),
            EndDate: $('#EndDateTime').val()
        }
        table = $('#history_data').bootstrapTable('refresh',
            {
                url: '/Query/HistoryDataTable'
            });
        $('#export').attr('href', '/Query/HistoryQeryExport?StartDate=' + $('#StartDateTime').val() + '&EndDate=' + $('#EndDateTime').val() + '&Hotel=' + $('#DistrictHotels').val());
    });

    $('#history_data').bootstrapTable({
        url: '/Query/HistoryDataTable',
        queryParams: function (params) {
            params.Hotel = hdqp.Hotel;
            params.StartDate = hdqp.StartDate;
            params.EndDate = hdqp.EndDate;
            return params;
        },
        height: $('#history_data').parents('.float-card').height() - 100
    });

    var getDistrictHotels = function (id) {
        base.AjaxGet('/CommonAjax/GetDistrictHotel', { id: id }, function (ret) {
            $('#DistrictHotels').empty();
            $('#DistrictHotels').select2({
                data: ret.Result
            });
        });
    }

    base.AjaxGet('/CommonAjax/GetDistrictHotel', { id: '00000000-0000-0000-0000-000000000000' }, function (ret) {
        $('#DistrictHotels').empty();
        $('#DistrictHotels').select2({
            data: ret.Result
        });
    });

    $.get('CommonAjax/UserDistrictSelections', null, function (ret) {
        var selecter = $('#AreaGuid').select2({
            data: ret.Result
        });
        $(selecter).on("select2:select", function (e) {
            getDistrictHotels(e.params.data.id, $('#StreetGuid'));
        });
    });

    $('#btnHourLine').on('click', function() {
        $.get('/Query/HistoryLineChart',
            { Hotel: $('#DistrictHotels').val(), DataType: 0, EndDate: $('#EndDateTime').val() },
            function (ret) {
                mainChart.clear();
                var option = Echart_Tools.getOption();
                option.series = [];
                option.legend.data = ['油烟浓度'];
                option.yAxis = [{ type: 'value', name: 'mg/m³', axisLabel: { formatter: '{value}' } }];
                option.xAxis.data = ret.Result.UpdateTimes;
                var series = Echart_Tools.getSeries('油烟浓度', 'line', null, ret.Result.Values);
                option.series.push(series);
                mainChart.setOption(option);
                $('#chartModal').modal();
            });
    });

    $('#btnDayLine').on('click', function () {
        $.get('/Query/HistoryLineChart',
            { Hotel: $('#DistrictHotels').val(), DataType: 1, EndDate: $('#EndDateTime').val() },
            function (ret) {
                mainChart.clear();
                var option = Echart_Tools.getOption();
                option.series = [];
                option.legend.data = ['油烟浓度'];
                option.yAxis = [{ type: 'value', name: 'mg/m³', axisLabel: { formatter: '{value}' } }];
                option.xAxis.data = ret.Result.UpdateTimes;
                var series = Echart_Tools.getSeries('油烟浓度', 'line', null, ret.Result.Values);
                option.series.push(series);
                mainChart.setOption(option);
                $('#chartModal').modal();
            });
    });

    $('#btnMonthLine').on('click', function () {
        $.get('/Query/HistoryLineChart',
            { Hotel: $('#DistrictHotels').val(), DataType: 2, EndDate: $('#EndDateTime').val() },
            function (ret) {
                mainChart.clear();
                var option = Echart_Tools.getOption();
                option.series = [];
                option.legend.data = ['油烟浓度'];
                option.yAxis = [{ type: 'value', name: 'mg/m³', axisLabel: { formatter: '{value}' } }];
                option.xAxis.data = ret.Result.UpdateTimes;
                var series = Echart_Tools.getSeries('油烟浓度', 'line', null, ret.Result.Values);
                option.series.push(series);
                mainChart.setOption(option);
                $('#chartModal').modal();
            });
    });
});