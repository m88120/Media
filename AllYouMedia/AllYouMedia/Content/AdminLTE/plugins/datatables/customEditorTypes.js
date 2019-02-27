// customTagsInput field type plug-in code
(function ($, DataTable) {

    if (!DataTable.ext.editorFields) {
        DataTable.ext.editorFields = {};
    }

    var Editor = DataTable.Editor;
    var _fieldTypes = DataTable.ext.editorFields;

    _fieldTypes.customTagsInput = {
        create: function (conf) {
            var that = this;

            conf._enabled = true;

            // Create the elements to use for the input
            conf._input = $('<input class="customTagsInput" id="' + Editor.safeId(conf.id) + '" />');

            // Use the fact that we are called in the Editor instance's scope to call
            // the API method for setting the value when needed
            //$('input.customTagsInput', conf._input).change(function () {
            //    if (conf._enabled) {
            //        that.set(conf.name, $(this).attr('value'));
            //    }

            //    return false;
            //});

            return conf._input;
        },

        get: function (conf) {
            var tempVal = conf._input.val().replace(/\,/g, "|");
            return tempVal;
        },

        set: function (conf, val) {
            var tempVal = val.replace(/\|/g, ",");
            conf._input.val(tempVal);
            return conf._input;
        },
        bindTagsInput: function (conf) { conf._input.tagsinput({ trimValue: true, freeInput: true }); },
        destroyTagsInput: function (conf) { conf._input.tagsinput('destroy'); },
        enable: function (conf) {
            conf._enabled = true;
            $(conf._input).removeClass('disabled');
        },

        disable: function (conf) {
            conf._enabled = false;
            $(conf._input).addClass('disabled');
        },
        destroy: function (conf) { conf._input.tagsinput('destroy'); },

    };

    _fieldTypes.cropImageUpload = {
        croppicInstane: null,
        create: function (conf) {
            var that = this;

            conf._enabled = true;

            // Create the elements to use for the input
            conf._input = $('<div><input class="cropImageUploadInput" type="text" style="display:none;" /><div  id="' + Editor.safeId(conf.id) + '" class="cropme" style="width:600px;height:600px;"></div></div>');

            // Use the fact that we are called in the Editor instance's scope to call
            // the API method for setting the value when needed
            //$('input.customTagsInput', conf._input).change(function () {
            //    if (conf._enabled) {
            //        that.set(conf.name, $(this).attr('value'));
            //    }

            //    return false;
            //});

            return conf._input;
        },

        get: function (conf) {
            var tempInput = conf._input.find('.cropme img');
            var arrSRC = tempInput.attr("src").split("/");
            return arrSRC[(arrSRC.length - 1)];
        },

        set: function (conf, val) {
            conf._input.find('.cropme').append("<img class='croppedImg' src='" + val + "'>");
            return conf._input;
        },
        bindSimpleCropper: function (conf, val) {
            var croppicHeaderOptions = {
                uploadUrl: '/Admin/UploadTempImage',
                cropUrl: '/Admin/UploadCroppedImage',
                modal: true,
                loaderHtml: '<div class="loader bubblingG"><span id="bubblingG_1"></span><span id="bubblingG_2"></span><span id="bubblingG_3"></span></div> ',
            };
            var elem = conf._input.find('.cropme');
            this.croppicInstane = new Croppic($(elem).attr("id"), croppicHeaderOptions);
        },
        destroySimpleCropper: function (conf) { this.croppicInstane.destroy(); },
        enable: function (conf) {
            conf._enabled = true;
            $(conf._input).removeClass('disabled');
        },
        disable: function (conf) {
            conf._enabled = false;
            $(conf._input).addClass('disabled');
        },
        destroy: function (conf) { conf._input.remove(); },
    };
})(jQuery, jQuery.fn.dataTable);