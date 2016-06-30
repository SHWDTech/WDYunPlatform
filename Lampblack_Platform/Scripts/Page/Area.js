//区域信息
var AreaInfo = [];

$(function () {
    base.AjaxGet('/Management/GetAreaInfo',
        null,
        function (obj) {
            AreaInfo = obj;
            $('input[type=text][item-level=0]').node = '';
            $('ul[item-level=0]').attr('parentNode', '');
            if (AreaInfo.length > 0) {
                $(AreaInfo)
                    .each(function () {
                        if (this.ItemLevel !== 0) return;
                        var li = $('<li class="list-group-item"><span>' +
                            this.ItemValue +
                            '</span><span class="glyphicon glyphicon-remove delete-mark"></span><span class="glyphicon glyphicon-pencil edit-mark"></span></li>');
                        $('ul[item-level=0]').append(li);
                        var area = this;
                        li.on('click', '.edit-mark', { area: area, li: li }, editArea);
                        li.on('click', '.delete-mark', { area: area, li: li }, removeArea);
                        li.on('click',
                            function () {
                                active(0, li, area);
                            });
                    });
            }
        });

    slideUp.Set({ 'width': '40%', 'left': '30%' });
    slideUp.append($('#areaEdit'));
    $('#areaEdit').show();
    $('#cancel').on('click', function () { slideUp.hide(false); });

    $('.area-input')
        .keydown(function (e) {
            if (e.which === 13) {
                $(this)[0].nextElementSibling.focus();
            }
        });

    //删除指定区域信息
    function removeArea(event) {
        event.stopPropagation();
        var area = event.data.area;
        var li = event.data.li;
        Msg('确定要删除【' + area.ItemValue + '】吗？',
        { title: '确认信息', confirm: '确定', callback: doRemoveArea, param: { area, li } });
        };

    //执行删除操作
    function doRemoveArea(param) {
        var area = param.area;
        var li = param.li;
        base.AjaxGet('Management/DeleteArea',
            { 'Id': area.Id },
            function () {
                AreaInfo = AreaInfo.filter(function (obj) {
                    return !(obj.Id === area.Id ||
                        obj.ParentNode === area.Id ||
                        (obj.Parent != null && obj.Parent.ParentNode === area.Id));
                });
                $(li).addClass('removed');
                setTimeout(function () {
                    if ($(li).is('.active')) {
                        var itemLevel = parseInt($(li).parent('ul').attr('item-level'));
                        $('div[item-level=' + (itemLevel + 1) + ']').addClass('float-card-hide');
                        if (itemLevel === 0) {
                            $('div[item-level=' + (itemLevel + 2) + ']').addClass('float-card-hide');
                        }
                    };
                    $(li).remove();
                },
                    500);
            });
    };

    //编辑区域信息
    function editArea(event) {
        event.stopPropagation();
        var area = event.data.area;
        var li = event.data.li;
        $('#areaEdit').find('input[type=text]').val(area.ItemValue);
        $('#areaEdit').attr('itemId', area.Id);
        slideUp.show();
        $('#confirm').off();
        $('#confirm')
            .on('click',
                function () {
                    slideUp.hide(false);
                    base.AjaxGet('/Management/EditAreaInfo',
                        { itemId: area.Id, editName: $('#areaEdit').find('input[type=text]').val() },
                        function (obj) {
                            var item = AreaInfo.filter(function (item) { return item.Id === obj.Id });
                            item[0].ItemValue = obj.ItemValue;
                            li.find('span')[0].innerHTML = obj.ItemValue;
                        });
                });
    };


    //激活当前选中区域信息
    function active(itemLevel, item, area) {
        var ul = $('ul[item-level=' + itemLevel + ']');
        var target = $('div[item-level=' + (itemLevel + 1) + ']');
        ul.children('li').removeClass('active');
        $(item).addClass('active');
        target.addClass('float-card-hide').find('input[type=text]').val('');
        if (itemLevel === 0) {
            $('div[item-level=' + (itemLevel + 2) + ']').addClass('float-card-hide');
        }
        target.find('ul').attr('parentNode', area.Id).empty();

        var areas = AreaInfo.filter(function (obj) {
            return obj.ItemLevel === (itemLevel + 1) && obj.ParentNode === area.Id
        });
        setTimeout(function () {
            target.removeClass('float-card-hide');
            if (areas.length > 0) {
                $(areas)
                    .each(function () {
                        var li = $('<li class="list-group-item"><span>' +
                            this.ItemValue +
                            '</span><span class="glyphicon glyphicon-remove delete-mark"></span><span class="glyphicon glyphicon-pencil edit-mark"></span></li>');
                        $('ul[item-level=' + (itemLevel + 1) + ']').append(li);
                        var area = this;
                        li.on('click', '.edit-mark', { area: area, li: li }, editArea);
                        li.on('click', '.delete-mark', { area: area, li: li }, removeArea);
                        li.on('click',
                            function () {
                                active(itemLevel + 1, li, area);
                            });
                    });
            }
        },
            300);
    }

    //添加按钮失去焦点时，清空提示信息。
    $('.area-insert-btn')
        .on('blur',
            function () {
                $('.area-input').parent().removeClass('has-error');
                $('.area-input').popover('hide');
            });

    //添加按钮click事件
    $('.area-insert-btn')
        .on('click',
            function () {
                var relatedInput = $('input[type=text][item-level=' + $(this).attr('item-level') + ']');
                var itemLevel = parseInt($(this).attr('item-level'));
                var areaName = relatedInput.val();
                var parentNode = $('ul[item-level=' + itemLevel + ']').attr('parentNode');

                //判断当前输入的区域是否已经存在
                var currentAreas = AreaInfo.filter(function (obj) {
                    return obj.ItemLevel === itemLevel && obj.ParentNode === parentNode
                });
                if (IsNullOrEmpty(areaName) || !currentAreas.every(obj => obj.ItemValue !== areaName)) {
                    relatedInput.parent().addClass('has-error');
                    relatedInput.popover('show');
                    return;
                }

                //获取添加结果
                base.AjaxGet('/Management/AddAreaInfo',
                    { 'areaName': areaName, 'itemLevel': itemLevel, 'parentNode': parentNode },
                    function (obj) {
                        var area = {
                            Id: obj.Id,
                            ItemKey: obj.ItemKey,
                            ItemValue: obj.ItemValue,
                            ItemLevel: obj.ItemLevel,
                            ParentNode: obj.ParentNode,
                            Parent: AreaInfo.filter(function (obj) { return obj.Id === parentNode })
                        };
                        AreaInfo.push(area);
                        var li = $('<li class="list-group-item onCreate"><span>' +
                            obj.ItemValue +
                            '</span><span class="glyphicon glyphicon-remove delete-mark"></span><span class="glyphicon glyphicon-pencil edit-mark"></span></li>');
                        $('ul[item-level=' + itemLevel + ']').append(li);
                        li.one('mouseenter',
                            function () {
                                $(this).removeClass('onCreate');
                            });
                        li.on('click',
                            function () {
                                active(itemLevel, li, area);
                            });
                        li.on('click', '.edit-mark', { area: area, li: li }, editArea);
                        li.on('click', '.delete-mark', { area: area, li: li }, removeArea);
                    });
            });
});