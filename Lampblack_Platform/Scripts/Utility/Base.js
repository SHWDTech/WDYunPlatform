var base = {};

$(function () {
    base.AjaxGet = function (ajaxUrl, params, callback) {
        $.get(ajaxUrl, params, function (ret) {
            if (ret.Message !== null) {
                var message = ret.Message;
                if (ret.Exception !== null) {
                    message += ('\r\nExceptionInfo:\r\n' + ret.Exception);
                }
                Msg(message, { title: '提示！' });
            }

            if (!ret.Success) {
                return;
            }

            if (callback !== null) {
                callback(ret.Result);
            }
        });
    }
});

var trimStr = function (str) {
    var re = /^\s+|\s+$/;
    return !str ? "" : str.replace(re, "");
}

var IsNullOrEmpty = function (obj) {
    try {
        if (typeof obj == "function") {
            return false;
        }
        if (obj.length === 0) {
            return true;
        }

        return false;
    }
    catch (e) {
        if (!obj) {
            return true;
        }
        if (typeof obj == "string" && trimStr(obj) === "") {
            return true;
        }

        if (typeof obj == "number" && isNaN(obj))
            return true;



        return false;
    }
}

var hasOwnProperty = Object.prototype.hasOwnProperty;

function isEmpty(obj) {

    // null and undefined are "empty"
    if (obj == null) return true;

    // Assume if it has a length property with a non-zero value
    // that that property is correct.
    if (obj.length > 0) return false;
    if (obj.length === 0) return true;

    // Otherwise, does it have any properties of its own?
    // Note that this doesn't handle
    // toString and valueOf enumeration bugs in IE < 9
    for (var key in obj) {
        if (hasOwnProperty.call(obj, key)) return false;
    }

    return true;
}

var Msg = function (msg, option) {
    var baseModel = $('#baseModal');
    if (option.title !== null) {
        baseModel.find('.modal-title').empty();
        baseModel.find('.modal-title').append('<h3>' + option.title + '</h3>');
    }

    baseModel.find('.modal-body').empty();
    baseModel.find('.modal-body').append(msg);

    baseModel.find('.btn-default').empty();
    if (!IsNullOrEmpty(option.cancel)) {
        baseModel.find('.btn-default').append(option.cancel);
    }
    baseModel.find('.btn-default').append('关闭');

    baseModel.find('.btn-main').hide();
    if (!IsNullOrEmpty(option.confirm)) {
        baseModel.find('.btn-main').show().innerHTML = option.confirm;
    }

    baseModel.modal({ show: true, keyboard: true });

    if (!IsNullOrEmpty(option.callback)) {
        $('#modal-confirm').off();
        $('#modal-confirm').on('click', function() {
             option.callback(option.param);
        });
    }
};

function resetValidation() {
    //Removes validation from input-fields
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').each(function() {this.innerHTML = ""});
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    //Removes validation summary 
    $('.validation-summary-errors').addClass('validation-summary-valid');
    $('.validation-summary-errors').removeClass('validation-summary-errors');

}