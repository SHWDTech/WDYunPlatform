//地图容器名称
var containerName = null;
//初始定位地址
var centerPosition = '上海';
//初始放大倍数
var zoom = null;
//地图对象
var map = null;
//地图标记对象
var markers = [];

function map_init(func) {
    window.addEventListener('mousedown', function () {
        $('#map-Marker').hide();
    });
    window.addEventListener('mousewheel', function () {
        $('#map-Marker').hide();
    });

    map = new BMap.Map(containerName);
    map.centerAndZoom(centerPosition, zoom);
    map.enableScrollWheelZoom();    //启用滚轮放大缩小，默认禁用
    map.enableContinuousZoom();    //启用地图惯性拖拽，默认禁用
    if (func != null) {
        func();
    }
};

var add_MapPoint = function (item) {
    var iconAddr = '';
    switch (item.status) {
        case LampblackStatus.clean:
            iconAddr = '/Resources/Images/Site/marker_red_sprite2.png';
            break;
        case LampblackStatus.dirty:
            iconAddr = '/Resources/Images/Site/marker_red.png';
            break;
        case LampblackStatus.noData:
            iconAddr = '/Resources/Images/Site/marker_gray_sprite1.png';
            break;
    }
    var icon = new BMap.Icon(iconAddr, new BMap.Size(20, 32), {//是引用图标的名字以及大小，注意大小要一样
        anchor: new BMap.Size(10, 25)//这句表示图片相对于所加的点的位置
    });

    var point = new BMap.Point(item.Point.Longitude, item.Point.Latitude);
    var marker = new BMap.Marker(point, {
        icon: icon
    });

    map.addOverlay(marker);

    marker.addEventListener('click', function (e) {
        markerShowView(e, item.Id);
    });

    markers.push({id: item.Id, marker: marker, point: point});
};

var markerShowView = function (event, id) {
    base.AjaxGet("/Monitor/GetMapHotelInfo", { hotelGuid: id }, function (ret) {
        $('#name').html(ret.Name);
        $('#chargeMan').html(ret.ChargeMan);
        $('#address').html(ret.Address);
        $('#phone').html(ret.Phone);
        $('#current').html(ret.Current / 100 + 'mA');
        $('#cleanerStatus').html(ret.CleanerStatus);
        $('#fanStatus').html(ret.FanStatus);
        $('#lampblackIn').html(ret.LampblackIn + 'mg/m³');
        $('#lampblackOut').html(ret.LampblackOut + 'mg/m³');
        $('#cleanRate').html(ret.CleanRate);

        if (event != null) {
            $('#map-Marker').css('top', event.clientY - 55).css('left', event.clientX - 180).show();
        } else {
            var marker = $.grep(markers, function (e) { return e.id === id })[0];
            var position = $(marker.marker.B).position();
            map.centerAndZoom(marker.point, zoom);
            $('#map-Marker').css('top', position.top).css('left', position.left).show();
        }
        
    });
}

var markerHideView = function () {
    $('.floatInfo ul').html('');
    $('.floatInfo').hide();
}

var getHotelInfo = function () {
    base.AjaxGet("/Monitor/GetHotelInfo", null, function (ret) {
        $(ret).each(function(index, item) {
            add_MapPoint(item);
        });
    });
};