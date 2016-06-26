//区域信息
var AreaInfo = [];

$(function () {
    base.AjaxGet('/Management/GetAreaInfo', null, function (obj) {
        AreaInfo = obj;
        $('input[type=text][item-level=0]').node = '';
        if (AreaInfo.length > 0) {
            $(AreaInfo).each(function () {
                if (this.ItemLevel !== 0) return;
                var li = $('<li class="list-group-item"><span>' + this.ItemValue
                    + '</span><span class="glyphicon glyphicon-remove delete-mark"></span><span class="glyphicon glyphicon-pencil edit-mark"></span></li>');
                $('ul[item-level=0]').append(li);
                var area = this;
                li.children('.delete-mark').on('click', function () {
                    removeArea(area, li);
                });
                li.on('click', function () {
                    active(0, li);
                });
            });
        }
    });

    function removeArea(area, li) {
        base.AjaxGet('Management/DeleteArea', { 'ItemKey': area.ItemKey }, function () {
            AreaInfo.splice(AreaInfo.indexOf(area), 1);
            $(li).addClass('removed');
            setTimeout(function () { $(li).remove() }, 500);
        });
    }

    function active(itemLevel, item) {
        $('ul[item-level=' + itemLevel + ']').children('li').removeClass('active');
        $(item).addClass('active');
        $('div[item-level=' + (itemLevel + 1) + ']').addClass('float_card_hide');
        setTimeout(function () { $('div[item-level=' + (itemLevel + 1) + ']').removeClass('float_card_hide'); }, 300);
    }

    $('.area-insert-btn').on('blur', function () {
        $('.area-input').parent().removeClass('has-error');
        $('.area-input').popover('hide');
    });

    $('.area-insert-btn').on('click', function () {
        debugger;
        var relatedInput = $('input[type=text][item-level=' + $(this).attr('item-level') + ']');
        var itemLevel = parseInt($(this).attr('item-level'));
        var areaName = relatedInput.val();
        if (IsNullOrEmpty(areaName) || !AreaInfo.every(obj => obj.ItemValue !== areaName && obj.ItemLevel === itemLevel)) {
            relatedInput.parent().addClass('has-error');
            relatedInput.popover('show');
            return;
        }
        base.AjaxGet('/Management/AddAreaInfo', { 'areaName': areaName, 'itemLevel': itemLevel, 'parentNode': relatedInput.node }, function (obj) {
            var area = {
                ItemKey: obj.ItemKey,
                ItemValue: obj.ItemValue,
                ItemLevel: obj.ItemLevel,
                ParentNode: obj.ParentNode
            };
            AreaInfo.push(area);
            var li = $('<li class="list-group-item onCreate"><span>' + obj.ItemValue + '</span><span class="glyphicon glyphicon-remove delete-mark"></span><span class="glyphicon glyphicon-pencil edit-mark"></span></li>');
            $('ul[item-level=' + itemLevel + ']').append(li);
            li.one('mouseenter', function () {
                $(this).removeClass('onCreate');
            });
            li.on('click', function () {
                active(itemLevel, li);
            });
            li.on('click', function (event) {
                event.preventDefault();
                removeArea(area, li);
            });
        });
    });
});