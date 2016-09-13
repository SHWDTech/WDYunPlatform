//Echart工具
var Echart_Tools = {};

Echart_Tools.getSeries = function (name, type, color, data, stack) {
    var series = {
        itemStyle: {
            normal: {
                color: '#1abc9c'
            }
        },
        name: '',
        type: 'bar',
        data: [],
        stack: '',
        markPoint: {
            data: []
        },
        markLine: {
            data: []
        }
    };

    if (name) {
        series.name = name;
    }
    if (type) {
        series.type = type;
    }
    if (color) {
        series.itemStyle.normal.color = color;
    }
    if (data) {
        series.data = data;
    }
    if (stack) {
        series.stack = stack;
    }

    return series;
};

Echart_Tools.getGaugeOption = function () {
    var option = {
        title: {
            text: ''
        },
        tooltip: {
            formatter: "{a} <br/>{b} : {c}"
        },
        toolbox: {
            show: true,
            feature: {
                saveAsImage: {
                    type: 'png',
                    backgroundColor: 'auto',
                    excludeComponents: ['toolbox'],
                    show: true,
                    title: '保存为图片'
                }
            }
        },
        series: [
            {
                name: "",
                type: "gauge",
                min: 0,
                max: 2,
                axisLine: {            // 坐标轴线
                    lineStyle: {       // 属性lineStyle控制线条样式
                        width: 18,
                        color: [[0.05, '#d9534f'], [0.2, '#f0ad4e'], [0.5, '#5bc0de'], [1, '#5cb85c']]
                    }
                },
                splitLine: {           // 分隔线
                    length: 24,         // 属性length控制线长
                    lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                        color: 'auto'
                    }
                },
                title: {
                    show: true,
                    offsetCenter: [
                        0,
                        90
                    ],
                    textStyle: {
                        color: "#333",
                        fontSize: 16
                    }
                },
                detail: {
                    formatter: "{value}",
                    offsetCenter: [0, '100%'],
                    height: 40
                },
                data: [
                    {
                        name: "",
                        value: ""
                    }
                ]
            }
        ]
    };

    return option;
};

Echart_Tools.getOption = function () {
    var option = {
        title: {
            text: ''
        },
        tooltip: {},
        legend: {
            data: ['']
        },
        xAxis: {
            data: []
        },
        yAxis: {},
        toolbox: {
            show: true,
            feature: {
                saveAsImage: {
                    type: 'png',
                    backgroundColor: 'auto',
                    excludeComponents: ['toolbox'],
                    show: true,
                    title: '保存为图片'
                },
                magicType: {
                    show: true,
                    type: ['line', 'bar'],
                    title: {
                        line: '切换为折线图',
                        bar: '切换为柱状图'
                    }
                }
            }
        },
        series: [
            {
                itemStyle: {
                    normal: {
                        color: '#1abc9c'
                    }
                },
                name: '',
                type: 'bar',
                data: [],
                markPoint: {
                    data: []
                },
                markLine: {
                    data: []
                }
            }
        ]
    };

    return option;
};

Echart_Tools.getStackLineOption = function () {
    var option = {
        title: {
            text: ''
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: []
        },
        toolbox: {
            feature: {
                saveAsImage: {}
            }
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: [
            {
                type: 'category',
                boundaryGap: false,
                data: []
            }
        ],
        yAxis: [
            {
                type: 'value'
            }
        ],
        series: []
    };

    return option;
};

Echart_Tools.getPieOption = function() {
    var option = {
        title: {
            text: '',
            subtext: '',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: []
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: {
                    show: true,
                    type: ['pie', 'funnel']
                },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        series: [
            {
                name: '',
                type: 'pie',
                radius: '50%',
                center: ['50%', '65%'],
                label: {
                    normal: {
                        show: false
                    },
                    emphasis: {
                        show: true
                    }
                },
                lableLine: {
                    normal: {
                        show: false
                    },
                    emphasis: {
                        show: true
                    }
                },
                data: []
            }
        ]
    };

    return option;
};