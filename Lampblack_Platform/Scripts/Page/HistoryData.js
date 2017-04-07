$(function () {
    window.statusFormatter = function (value) {
        if (value) {
            return '<img src="/Resources/Images/Site/CleanRate/RUN.png" />';
        } else {
            return '<img src="/Resources/Images/Site/CleanRate/STOP.png" />';
        }
    }

    $('#StartDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'YYYY-MM-DD'
    });

    $('#EndDateTime').datetimepicker({
        locale: 'zh-cn',
        format: 'YYYY-MM-DD'
    });

    var queryParams = function (params) {
        params.Hotel = $('#DistrictHotels').val();
        params.StartDate = $('#StartDateTime').val();
        params.EndDate = $('#EndDateTime').val();
        return params;
    }

    var table;

    $('#query').on('click', function () {
        table = $('#history_data').bootstrapTable('refresh',
            {
                url: '/Query/HistoryDataTable'
            });
    });

    $('#history_data').bootstrapTable({
        url: '/Query/HistoryDataTable',
        queryParams: queryParams,
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


});