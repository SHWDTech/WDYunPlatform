//地图容器名称
var containerName = null;
//初始定位地址
var centerPosition = '上海';
//初始放大倍数
var zoom = null;
//地图对象
var map = null;

function map_init(func) {
    map = new BMap.Map(containerName);
    map.centerAndZoom(centerPosition, zoom);
    map.enableScrollWheelZoom();    //启用滚轮放大缩小，默认禁用
    map.enableContinuousZoom();    //启用地图惯性拖拽，默认禁用
    if (func != null) {
        func();
    }
};

var add_MapPoint = function (point, status, extroInfo) {
    var iconAddr = '';
    switch (status) {
        case LampblackStatus.dirty:
            iconAddr = '/Images/Site/marker_red_sprite2.png';
            break;
        case LampblackStatus.clean:
            iconAddr = '/Images/Site/marker_red.png';
            break;
        case LampblackStatus.unknow:
            iconAddr = '/Images/Site/marker_gray_sprite1.png';
            break;
    }
    var icon = new BMap.Icon(iconAddr, new BMap.Size(20, 32), {//是引用图标的名字以及大小，注意大小要一样
        anchor: new BMap.Size(10, 25)//这句表示图片相对于所加的点的位置
    });

    var marker = new BMap.Marker(new BMap.Point(point.longitude, point.latitude), {
        icon: icon
    });

    map.addOverlay(marker);

    if (extroInfo != null) {
        marker.addEventListener('mouseover', function (e) {
            markerShowView(e, extroInfo);
        });

        marker.addEventListener('mouseout', markerHideView);
    }
};

var markerShowView = function (event, statInfo) {
    var ul = $('.floatInfo').find('ul');

    $(ul).append('<li class="title">' + statInfo.Name + '</li>');
    $(ul).append('<li><span class="text pm">颗粒物</span><span class="num safe">' + statInfo.AvgTp + '(mg/m³)</span></li>');
    $(ul).append('<li><span class="text db">噪音</span><span class="num safe">' + statInfo.AvgDb + '(dB)</span></li>');
    $(ul).append('<li><span class="date">' + statInfo.UpdateTime + '</span></li>');
    $('.floatInfo').append(ul);

    $('.floatInfo').css('top', event.clientY).css('left', event.clientX + 20).show();
}

var markerHideView = function () {
    $('.floatInfo ul').html('');
    $('.floatInfo').hide();
}