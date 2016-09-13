//清洁度饼图
var pieChart = null;
//当前状态值
var currentGauge = null;
//当前状态值图表选项
var gaugeOption = null;
//就点名
var hotel = null;

$(function () {
    pieChart = echarts.init(document.getElementById('cleannessMap'));
    currentGauge = echarts.init(document.getElementById('currentStatus'));
    gaugeOption = Echart_Tools.getGaugeOption();
    gaugeOption.title.text = '当前电流值';
    gaugeOption.series[0].min = 0;
    gaugeOption.series[0].max = 1000;
    setChart();

    $('#tab-nav a').on('click', function () {
        $('.clean-list').hide();
        $('#tab-nav li').removeClass('active');
        $('#' + $(this).attr('data-target')).show();
        $(this).parent().addClass('active');
    });

    var getHotels = function () {
        base.AjaxGet('/CommonAjax/Hotels', { area: $('#areas').val(), street: $('#street').val(), address: $('#address').val() }, function (ret) {
            $('#hotels').empty();
            if (!IsNullOrEmpty(ret.Result)) {
                $(ret.Result).each(function(index, hotel) {
                    $('#hotels').append('<span class="wd-card" value=' + hotel.Id + '>' + hotel.Name + '</span>');
                });
            }
            $('.wd-card').on('click', function () {
                hotel = $(this).html();
                currentGauge.showLoading();
                base.AjaxGet('/Home/HotelCurrentStatus', { hotelGuid: $(this).attr('value') }, function (ret) {
                    currentGauge.hideLoading();
                    var record = ret.Result;
                    $('#current').html(record.Current / 100 + 'mA');
                    $('#cleanerStatus').html(record.CleanerStatus);
                    $('#fanStatus').html(record.FanStatus);
                    $('#lampblackIn').html(record.LampblackIn + 'mg/m³');
                    $('#lampblackOut').html(record.LampblackOut + 'mg/m³');
                    $('#cleanRate').html(record.CleanRate);
                    $('#removeRate').html();
                    $('#cleanerRunTime').html(record.CleanerRunTime);
                    $('#fanRunTime').html(record.FanRunTime);

                    gaugeOption.series[0].data = { name: hotel, value: record.Current / 100 };
                    currentGauge.setOption(gaugeOption);
                });
            });
        });
    }

    var getDistricts = function (id, select) {
        base.AjaxGet('/CommonAjax/GetAreaList', { id: id }, function (ret) {
            $(select).empty().append('<option value="none">全部</option>');
            $(ret.Result).each(function () {
                $(select).append('<option value=' + this.Id + '>' + this.ItemValue + '</option>');
            });

            getHotels();
        });
    }

    $('#areas').on('change', function () {
        $('#address').empty();
        getDistricts($(this).val(), $('#street'));
    });

    $('#street').on('change', function () {
        getDistricts($(this).val(), $('#address'));
    });

    $('#address').on('change', function() {
        getHotels();
    });

    $('#areas').change();


});

var setChart = function () {
    var option = Echart_Tools.getPieOption();
    option.title.text = '实时清洁度分布';
    option.title.subtext = '实时清洁度一览';

    option.legend.data = ['无数据', '失效', '较差', '合格', '良好'];
    option.series[0].name = '实时清洁度';

    option.series[0].data = [
        { value: parseInt($('input[type=hidden]').attr('data-nodata')), name: '无数据' },
        { value: parseInt($('input[type=hidden]').attr('data-faild')), name: '失效' },
        { value: parseInt($('input[type=hidden]').attr('data-worse')), name: '较差' },
        { value: parseInt($('input[type=hidden]').attr('data-qualified')), name: '合格' },
        { value: parseInt($('input[type=hidden]').attr('data-good')), name: '良好' }];

    pieChart.setOption(option);
};