//清洁度饼图
var pieChart = null;

$(function () {
    pieChart = echarts.init(document.getElementById('cleannessMap'));
    setChart();

    $('#tab-nav a').on('click', function () {
        $('.clean-list').hide();
        $('#tab-nav li').removeClass('active');
        $('#' + $(this).attr('data-target')).show();
        $(this).parent().addClass('active');
    });
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