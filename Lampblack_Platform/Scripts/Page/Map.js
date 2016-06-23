$(function() {
    containerName = 'mapContainer';

    zoom = 12;
    var load = document.createElement("script");
    load.src = "http://api.map.baidu.com/api?v=1.4&callback=map_init()";
    document.body.appendChild(load);
})