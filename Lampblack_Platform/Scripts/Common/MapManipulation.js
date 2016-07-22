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
//提示框内元素
var infoPanel = {};

function map_init(func) {
    map = new BMap.Map(containerName);
    map.centerAndZoom(centerPosition, zoom);
    map.enableScrollWheelZoom();    //启用滚轮放大缩小，默认禁用
    map.enableContinuousZoom();    //启用地图惯性拖拽，默认禁用

    infoPanel.body = $('#map-Marker');
    infoPanel.name = $('#name');
    infoPanel.chargeMan = $('#chargeMan');
    infoPanel.address = $('#address');
    infoPanel.telephone = $('#telephone');
    infoPanel.current = $('#current');
    infoPanel.cleanerStatus = $('#cleanerStatus');
    infoPanel.fanStatus = $('#fanStatus');
    infoPanel.lampblackIn = $('#lampblackIn');
    infoPanel.lampblackOut = $('#lampblackOut');
    infoPanel.cleanRate = $('#cleanRate');

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

    marker.addEventListener('click', function () {
        markerShowView(item.Id);
    });

    markers.push({id: item.Id, marker: marker, point: point});
};

var markerShowView = function (id, zoom) {
    base.AjaxGet("/Monitor/GetMapHotelInfo", { hotelGuid: id }, function (ret) {
        $(infoPanel.name).html(ret.Name);
        $(infoPanel.chargeMan).html(ret.ChargeMan);
        $(infoPanel.address).html(ret.Address);
        $(infoPanel.telephone).html(ret.Phone);
        $(infoPanel.current).html(ret.Current / 100 + 'mA');
        $(infoPanel.cleanerStatus).html(ret.CleanerStatus === true ? '开启' : '关闭');
        $(infoPanel.fanStatus).html(ret.FanStatus === true ? '开启' : '关闭');
        $(infoPanel.lampblackIn).html(ret.LampblackIn + 'mg/m³');
        $(infoPanel.lampblackOut).html(ret.LampblackOut + 'mg/m³');
        $(infoPanel.cleanRate).html(ret.CleanRate);

        var point = $.grep(markers, function (e) { return e.id === id })[0].point;
        var infoWindow = new BMap.InfoWindow(infoPanel.body[0], { width: 400, height: 320, title: '<h4 class="text-center text-main-reverse">酒店当前状况</h4>' });
        if (zoom) {
            map.centerAndZoom(point, zoom);
        }
        map.openInfoWindow(infoWindow, point);
    });
}

var getHotelInfo = function () {
    base.AjaxGet("/Monitor/GetHotelInfo", null, function (ret) {
        $(ret).each(function(index, item) {
            add_MapPoint(item);
        });
    });
};