(function (window, document) {
    customEditors = function (options) {
        var that = this;

        // DEFAULT OPTIONS
        that.options = {

        };

        // OVERWRITE DEFAULT OPTIONS
        for (i in options) that.options[i] = options[i];
        // INIT THE WHOLE DAMN THING!!!
        that.init();
    };
    function blankStringIfNull(str) { if (str == null) str = ""; return str; };
    customEditors.prototype = {
        ajaxUrl: null,
        table: null,
        fields: [],
        editRecordObject: null,
        popupTitle: 'Edit',
        action: 'create',
        ExtraData: [],
        modalClass: "",
        init: function () {
            this.ajaxUrl = this.options.ajaxUrl ? this.options.ajaxUrl : null;
            this.table = this.options.table ? this.options.table : null;
            this.fields = this.options.fields ? this.options.fields : [];
            this.editRecordObject = this.options.editRecordObject ? this.options.editRecordObject : null;
            this.popupTitle = this.options.popupTitle ? this.options.popupTitle : "Edit";
            var table = this.table;
            table.on('select', function (e, dt, type, indexes) { var buttons = table.buttons(['.Edit', '.Delete', '.ItemImages']); if (table.rows({ selected: true }).indexes().length === 0) { buttons.disable(); } else { buttons.enable(); } })
                 .on('deselect', function (e, dt, type, indexes) { var buttons = table.buttons(['.Edit', '.Delete', '.ItemImages']); if (table.rows({ selected: true }).indexes().length === 0) { buttons.disable(); } else { buttons.enable(); } });
        },
        openAddPopup: function () { this.action = "create"; this.popupTitle = "Add new record"; this.openPopup(); },
        openEditPopup: function () { this.action = "edit"; this.popupTitle = "Edit record"; this.openPopup(); },
        openRemovePopup: function () { this.action = "remove"; this.popupTitle = "Delete record"; this.openConfirmPopup(); },
        openPopup: function () {
            $("#datatableEditor #datatableEditorLabel").html(this.popupTitle);
            var form = $("#dtForm");
            var that = this;
            if (that.editRecordObject != null && 'DT_RowId' in that.editRecordObject) form.find("#dt_Row_Id").val(that.editRecordObject.DT_RowId);
            $.each(this.fields, function (i, val) {
                var $rowContainer = $('<div class="DTE_Field DTE_Field_Type_text"></div>');
                $rowContainer.appendTo(form);
                $rowContainer.append('<label data-dte-e="label" class="col-lg-4 control-label" for="">' + val.label + ':</label>');
                var $rowInputContainer = $('<div data-dte-e="input-control" class="col-lg-8 controls"></div>');
                $rowInputContainer.appendTo($rowContainer);
                var inputElem = null;
                if (val.type == "select") {
                    inputElem = $('<select id="DTE_Field_' + val.name + '" class="form-control" name="' + val.name + '" ' + (val.validationIsRequired ? "required='required'" : "") + '></select>');
                    var ddlOptions = that.table.ajax.json().options[val.name];
                    if (!val.isDependentSelect) {
                        for (var loopi = 0; loopi < ddlOptions.length; loopi++) {
                            inputElem.append('<option value="' + ddlOptions[loopi][val.optionsPair.value] + '">' + ddlOptions[loopi][val.optionsPair.label] + '</option>');
                        }
                    }
                    else {
                        $("#datatableEditor #DTE_Field_" + val.DependentSelectField).change(function () {
                            inputElem.empty();
                            $.ajax({
                                url: val.DependentSelectOptionURL,
                                type: 'POST',
                                async: false,
                                data: { DepID: $("#datatableEditor #DTE_Field_" + val.DependentSelectField).val() },
                                success: function (returnData) {
                                    if (returnData.Result == "OK") {
                                        for (var loopi = 0; loopi < returnData.Options.length; loopi++) {
                                            inputElem.append('<option value="' + returnData.Options[loopi][val.optionsPair.value] + '">' + returnData.Options[loopi][val.optionsPair.label] + '</option>');
                                        }
                                    } else alert("Ops! Something went wrong. Please try again later.");
                                },
                                error: function () { alert("Ops! Something went wrong. Please try again later."); }
                            });
                        });
                    }
                }
                else if (val.type == "checkbox") {
                    inputElem = $('<input id="DTE_Field_' + val.name + '" type="checkbox" class="checkbox" name="' + val.name + '" />');
                    inputElem.change(function () { if ($(this).is(":checked")) { $(this).val(true); $(this).next().val(true); } else { $(this).val(false); $(this).next().val(false); } });
                }
                else if (val.type == "customTagsInput") {
                    inputElem = $('<input id="DTE_Field_' + val.name + '" type="textbox" class="customTagsInput form-control" name="' + val.name + '" />');
                }
                else if (val.type == "cropImageUpload") {
                    inputElem = $('<input id="DTE_Field_' + val.name + '" type="textbox" class="cropImageUploadInput form-control" name="' + val.name + '" />');
                }
                else if (val.type == "textarea") {
                    inputElem = $('<textarea id="DTE_Field_' + val.name + '" class="form-control" name="' + val.name + '" rows="3"></textarea>');
                }
                else {
                    var defaultInputType = "text";
                    if (val.type != null && val.type != '') defaultInputType = val.type;
                    inputElem = $('<input id="DTE_Field_' + val.name + '" type="' + defaultInputType + '" class="form-control" name="' + val.name + '" ' + (val.validationIsRequired ? "required='required'" : "") + ' ' + (val.validationFieldType ? "data-parsley-type='" + val.validationFieldType + "'" : "") + '/>');
                }
                $rowInputContainer.append(inputElem);
                $rowInputContainer.append('<div data-dte-e="msg-info" class="help-block"></div>');

                if (val.type == "checkbox") { var hdnChkElem = $('<input id="DTE_Field_' + val.name + '_chkHdn" type="hidden" name="' + val.name + '" />'); inputElem.after(hdnChkElem); hdnChkElem.val(false); }
                if (val.type == "cropImageUpload") {
                    var wrapper = $('<div></div>');
                    inputElem.wrap(wrapper); inputElem.hide();
                    inputElem.after($('<div id="cropme_' + val.name + '" class="cropme" style="width:' + val.width + 'px;height:' + val.height + 'px;" data-useinonlyuploadmode="' + val.useInOnlyUploadMode + '"></div>'));
                }
                if (val.defaultValue != undefined && val.defaultValue != null) inputElem.val(val.defaultValue).change();
                if (that.editRecordObject != null && val.name in that.editRecordObject) {
                    if (val.isDependentSelect) {
                        $("#datatableEditor #DTE_Field_" + val.DependentSelectField).change();
                    }
                    inputElem.val(that.editRecordObject[val.name]);
                    if (val.type == "checkbox") { if (that.editRecordObject[val.name] == true) { inputElem.prop("checked", true); } else { inputElem.prop("checked", false); } inputElem.next().val(that.editRecordObject[val.name]); }
                    if (val.type == "customTagsInput") { inputElem.val(blankStringIfNull(that.editRecordObject[val.name]).replace(/\|/g, ",")); }
                    if (val.type == "cropImageUpload") { inputElem.next().append("<img class='croppedImg' src='" + that.editRecordObject[val.name] + "'>"); }
                }
            });
            form.submit(function (e) {
                e.preventDefault();
                if (!$(this).parsley().isValid()) { $(this).parsley().validate(); return false; }
                $("#processigWrapper").addClass("processing");
                $(that.ExtraData).each(function (loopi, loopval) { form.append('<input type="hidden" name="' + loopval.Name + '" value="' + loopval.Value + '" />'); });
                $(this).find(".customTagsInput").each(function () { $(this).val(blankStringIfNull($(this).val()).replace(/\,/g, "|")); });
                $(this).find(".cropImageUploadInput").each(function () {
                    var src = $(this).next().find('.croppedImg').attr("src");
                    if (src != undefined && src != null && src != "") {
                        var srcFullArr = src.split("/");
                        $(this).val(srcFullArr[(srcFullArr.length - 1)]);
                    }
                });
                var data = [];
                var formData = form.serializeArray();
                var formetedFormData = {};
                for (var i = 0; i < formData.length; i++) {
                    formetedFormData[formData[i].name] = formData[i].value;
                }
                data.push(formetedFormData);
                if (that.ajaxUrl == null) { alert('URL for ajax is not defined.'); return false; }
                $("#dtSaveButton").attr('disabled', 'disabled');
                $.ajax({
                    url: that.ajaxUrl,
                    type: 'POST',
                    data: { action: that.action, data: data },
                    //contentType: 'application/json',
                    success: function (returnData) {
                        if ('fieldErrors' in returnData) {
                            var strError = "";
                            for (var e = 0; e < returnData.fieldErrors.length; e++) {
                                strError += '\n' + returnData.fieldErrors[e].status
                            }
                            alert(strError);
                        }
                        else if ('error' in returnData) {
                            alert(returnData.error);
                        }
                        else {
                            if ('data' in returnData && returnData.data.length > 0) {

                            }
                            else {
                                alert("Ops! Something went wrong. Please try again later.");
                            }
                            that.table.ajax.reload();
                            $('#datatableEditor').modal("hide"); that.reset();
                        }
                        $("#processigWrapper").removeClass("processing");
                        $("#dtSaveButton").removeAttr('disabled');
                    },
                    error: function () { alert("Ops! Something went wrong. Please try again later."); $("#processigWrapper").removeClass("processing"); $("#dtSaveButton").removeAttr('disabled'); $('#datatableEditor').modal("hide"); that.reset(); }
                });
            });
            $("#dtSaveButton").click(function () { form.submit(); });
            var modal = $('#datatableEditor').modal();
            modal.on("hide.bs.modal", function () { that.reset(); });
            if (that.modalClass != "") { modal.find(".modal-dialog").addClass(that.modalClass); }
            modal.on("shown.bs.modal", function () {
                form.find('.customTagsInput').each(function () {
                    $(this).tagsinput({ trimValue: true, freeInput: true });
                });
                form.find('.cropme').each(function () {
                    var croppicHeaderOptions = {
                        uploadUrl: '/Admin/UploadAndCropImage',
                        useOnlyUpload: true,
                        //cropUrl: '/Admin/UploadCroppedImage',
                        //modal: true,
                        loaderHtml: '<div class="loader bubblingG"><span id="bubblingG_1"></span><span id="bubblingG_2"></span><span id="bubblingG_3"></span></div> ',
                    };
                    if ($(this).data("useinonlyuploadmode") == false) {
                        croppicHeaderOptions.uploadUrl = "/Admin/UploadTempImage";
                        croppicHeaderOptions.useOnlyUpload = false;
                        croppicHeaderOptions.cropUrl = '/Admin/UploadCroppedImage';
                        croppicHeaderOptions.modal = true;
                    }
                    var croppicInstane = new Croppic($(this).attr("id"), croppicHeaderOptions);
                });
                if (that.onOpenPopup) that.onOpenPopup();
            });
        },
        openConfirmPopup: function () {
            var form = $("#dtRemoveForm");
            var that = this;
            if (that.editRecordObject != null && 'DT_RowId' in that.editRecordObject) form.find("#dt_RemoveRow_Id").val(that.editRecordObject.DT_RowId);
            form.submit(function (e) {
                e.preventDefault();
                $("#processigWrapperDeleteRec").addClass("processing");
                $(that.ExtraData).each(function (loopi, loopval) { form.append('<input type="hidden" name="' + loopval.Name + '" value="' + loopval.Value + '" />'); });
                var data = [];
                var formData = form.serializeArray();
                var formetedFormData = {};
                for (var i = 0; i < formData.length; i++) {
                    formetedFormData[formData[i].name] = formData[i].value;
                }
                data.push(formetedFormData);
                if (that.ajaxUrl == null) { alert('URL for ajax is not defined.'); return false; }
                $("#dtRemoveButton").attr('disabled', 'disabled');
                $.ajax({
                    url: that.ajaxUrl,
                    type: 'POST',
                    data: { action: that.action, data: data },
                    //contentType: 'application/json',
                    success: function (returnData) {
                        if ('fieldErrors' in returnData) {
                            var strError = "";
                            for (var e = 0; e < returnData.fieldErrors.length; e++) {
                                strError += '\n' + returnData.fieldErrors[e].status
                            }
                            alert(strError);
                        }
                        else if ('error' in returnData) {
                            alert(returnData.error);
                        }
                        else {
                            that.table.ajax.reload();
                            $('#datatableEditorDeleteRecord').modal("hide"); that.resetRemovePopup();
                        }
                        $("#processigWrapperDeleteRec").removeClass("processing");
                        $("#dtRemoveButton").removeAttr('disabled');
                    },
                    error: function () { alert("Ops! Something went wrong. Please try again later."); $("#processigWrapperDeleteRec").removeClass("processing"); $("#dtRemoveButton").removeAttr('disabled'); $('#datatableEditorDeleteRecord').modal("hide"); that.resetRemovePopup(); }
                });
            });
            $("#dtRemoveButton").click(function () { form.submit(); });
            var modal = $('#datatableEditorDeleteRecord').modal();
            modal.on("hide.bs.modal", function () { that.resetRemovePopup(); })
        },
        reset: function () {
            var form = $("#dtForm");
            form.find(".customTagsInput").each(function () { $(this).tagsinput('destroy'); });
            //form.find(".cropme").each(function () { $(this).tagsinput('destroy'); });
            form.empty(); form.append('<input type="hidden" id="dt_Row_Id" name="ID" />');
            $("#dtSaveButton").off("click"); form.off("submit");
            this.editRecordObject = null;
            this.action = "create";
            this.ExtraData = [];
            var modal = $('#datatableEditor');
            modal.off("shown.bs.modal"); modal.off("show.bs.modal");
        },
        resetRemovePopup: function () {
            var form = $("#dtRemoveForm");
            form.empty(); form.append('<input type="hidden" id="dt_RemoveRow_Id" name="ID" />');
            $("#dtRemoveButton").off("click"); form.off("submit");
            this.editRecordObject = null;
            this.action = "create";
            this.ExtraData = [];
        },
        destroy: function () {

        },
        inline: function (td_target) {
            var cell = this.table.cell(td_target);
            var column = this.table.column(cell.index().column);
            var editField = column.settings()[0].aoColumns[column.index()].editField;
            if (editField == undefined) editField = column.dataSrc();
            var row = this.table.row(cell.index().row);
            var rowData = row.data();
            var editingValue = rowData[editField];
            var node = $(cell.node());
            node.empty();
            var that = this;
            var content = $(''
                            + '<div class="DTE DTE_Inline">'
                            + '    <div class="DTE_Inline_Field">'
                            + '        <div class="DTE_Processing_Indicator"><span></span></div>'
                            + '        <div class="DTE_Field DTE_Field_Type_text DTE_Field_Name">'
                            + '            <div data-dte-e="input" class="DTE_Field_Input">'
                            + '                <div data-dte-e="input-control" class="DTE_Field_InputControl" style="display: block;">'
                            + '                    <input id="DTE_Field_' + editField + '" type="text" class="inputInline" style="color: #000;"/>'
                            + '                </div>'
                            + '            </div>'
                            + '        </div><div data-dte-e="form_error" class="DTE_Form_Error"></div>'
                            + '    </div><div class="DTE_Inline_Buttons"></div>'
                            + '</div>'
                            + '');
            node.append(content);
            var input = content.find('.inputInline');
            input.val(editingValue); input.focus();
            input.blur(function () {
                if (rowData[editField] == $(this).val()) { row.invalidate(); return false; }
                var data = [];
                var formetedFormData = {};
                for (var i = 0; i < that.fields.length; i++) {
                    formetedFormData[that.fields[i].name] = rowData[that.fields[i].name];
                }
                $(that.ExtraData).each(function (loopi, loopval) { formetedFormData[loopval.Name] = loopval.Value; });
                formetedFormData[editField] = $(this).val();
                formetedFormData.ID = rowData.DT_RowId;
                data.push(formetedFormData);
                if (that.ajaxUrl == null) { alert('URL for ajax is not defined.'); return false; }
                $.ajax({
                    url: that.ajaxUrl,
                    type: 'POST',
                    data: { action: "edit", data: data },
                    //contentType: 'application/json',
                    success: function (returnData) {
                        if ('fieldErrors' in returnData) {
                            var strError = "";
                            for (var e = 0; e < returnData.fieldErrors.length; e++) {
                                strError += '\n' + returnData.fieldErrors[e].status
                            }
                            alert(strError);
                        }
                        else if ('error' in returnData) {
                            alert(returnData.error);
                        }
                        else {
                            if ('data' in returnData && returnData.data.length > 0) {
                                row.data(returnData.data[0]);
                            }
                            else {
                                alert("Ops! Something went wrong. Please try again later.");
                            }
                            //rowData[editField] = formetedFormData[editField];
                            //rowData[column.dataSrc()] = formetedFormData[editField];
                        }
                        row.invalidate();
                    },
                    error: function () {
                        alert("Ops! Something went wrong. Please try again later."); row.invalidate();
                    }
                });
            });
            //input.blur(function () { input.change(); });
        }
    };
})(window, document);