window.onunload = function (e) {
    if (window.opener.$("#siteTextGrid").is(':visible')) {
        if (!window.opener.$("#siteTextGrid").html().trim() == '') {
            window.opener.LoadSearchParameters();
            window.opener.LoadDatabySearchParameters();            
        }
    }
    else {
        window.opener.LoadSearchParameters();
    }
};

$(document).ready(function () {

   

    $("#submitSiteText").click(function (e) {
        if (ValidatePageTextSave()) {
            var element = $(document.body);
            kendo.ui.progress(element, true);

            return true;
        }
        return false;
    });

    $("#comboResourceKey").change(function (e) {
        LoadDefaultTextForResourceKey();
    });

    if (CheckIsHtmlTextForResourceKey()) {        
        $("#divHTMLText").show();
        $("#divPlainText").hide();
    }
    else {
        $("#divHTMLText").hide();
        $("#divPlainText").show();        
    }

    if ($("#hdnSuccessOrFailedMessage").val() == "OK") {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Success",
            resizable: false,
            modal: true,
            draggable: false,
            actions: []
        });

        kendoWindow.data("kendoWindow")
            .content($("#divSuccessOrFailedMessage").html())
            .center()
            .open();

        kendoWindow
       .find(".confirm")
       .click(function () {
           window.close();
       });
    }
    
    

    createEditor();

});

function createEditor() {
    //var editorNS = kendo.ui.editor,
    //    registerTool = editorNS.EditorUtils.registerTool,
    //    Tool = editorNS.Tool;
    //registerTool("insertLineBreak", new Tool({ key: 13, command: editorNS.NewLineCommand }));
    //registerTool("insertParagraph", new Tool({ key: 13, shift: true, command: editorNS.ParagraphCommand }));


    $("#txtResourcekeyHtmlText").kendoEditor({
        tools: [
            "bold",
            "italic",
            "underline",
            "strikethrough",
            "justifyLeft",
            "justifyCenter",
            "justifyRight",
            "justifyFull",
            "insertUnorderedList",
            "insertOrderedList",
            "indent",
            "outdent",
            "createLink",
            "unlink",
            "insertImage",
            "subscript",
            "superscript",
            "createTable",
            "addRowAbove",
            "addRowBelow",
            "addColumnLeft",
            "addColumnRight",
            "deleteRow",
            "deleteColumn",
            "formatting",
            "cleanFormatting",
            "fontName",
            "fontSize",
            "foreColor",
           "backColor",
            {
                name: "custom",
                tooltip: "Insert HTML content",
                exec: onCustomToolClick
            }
        ]
    });
}

function LoadDefaultTextForResourceKey() {
    var selectedResourceKey = $("#comboResourceKey option:selected").val();
    if (selectedResourceKey == -1) {
        $('#txtResourcekeyHtmlText').empty();
        $('#txtResourcekeyHtmlText').val('');

        var editor = $("#txtResourcekeyHtmlText").data("kendoEditor");
        editor.value("");

        $('#txtResourcekeyPlainText').empty();
        $('#txtResourcekeyPlainText').val('');

        $("#divHTMLText").show();
        $("#divPlainText").hide();
    }
    else {
        $.ajax({
            type: 'GET',
            url: $("#dvLoadDefaultTextForResourceKey").data('request-url'),
            data: { resourcekey: selectedResourceKey },
            cache: false,
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                
                if (CheckIsHtmlTextForResourceKey()) {
                    $("#divHTMLText").show();
                    $("#divPlainText").hide();

                    $('#txtResourcekeyHtmlText').empty();
                    $('#txtResourcekeyHtmlText').val(data);

                    var editor = $("#txtResourcekeyHtmlText").data("kendoEditor");
                    editor.value(data);                    
                }
                else {                    

                    $("#divHTMLText").hide();
                    $("#divPlainText").show();

                    $('#txtResourcekeyPlainText').empty();
                    $('#txtResourcekeyPlainText').val(data);
                }

            },
            error: function (xhr) {

                alert(xhr.state + xhr.statusText);
            }

        });
    }

    if (selectedResourceKey.toLowerCase().indexOf("css") >= 0) {

        $(".k-editor-toolbar-wrap").hide();
    }
    else {
        $(".k-editor-toolbar-wrap").show();
    }
}

function CheckIsHtmlTextForResourceKey() {

    var isHtmlText = true;

    var selectedResourceKey = $("#comboResourceKey option:selected").val();
    $.ajax({
        type: 'GET',
        url: $("#dvCheckIsHtmlTextForResourceKey").data('request-url'),
        data: { resourceKey: selectedResourceKey },
        cache: false,
        dataType: "json",
        async: false,
        traditional: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            isHtmlText = data;
        },
        error: function (xhr) {

            alert(xhr.state + xhr.statusText);
        }
    });


    return isHtmlText;
}

function ValidatePageTextSave() {

    var isSuccess = true;
    $("#divSiteTextValidations").empty();

    $("#divSiteTextValidations")[0].innerHTML += "<p class='message'>Please Enter/Select the below fields</p>";
    $("#divSiteTextValidations")[0].innerHTML += "<ul>"

    if ($("#comboResourceKey").val() == "-1") {
        $("#divSiteTextValidations")[0].innerHTML += "<li class='message'>Resource Key</li>";
        isSuccess = false;
    }
    
    if (CheckIsHtmlTextForResourceKey()) {

        if ($("#txtResourcekeyHtmlText").val() == "") {
            $("#divSiteTextValidations")[0].innerHTML += "<li class='message'>Text</li>";
            isSuccess = false;
        }
    }
    else {

        if ($("#txtResourcekeyPlainText").val() == "") {
            $("#divSiteTextValidations")[0].innerHTML += "<li class='message'>Text</li>";
            isSuccess = false;
        }        
    }

    $("#divSiteTextValidations")[0].innerHTML += "</ul>"

    if (!isSuccess) {
        var kendoWindow = $('<div class="rpCustomMessage" />').kendoWindow({
            title: "Alert",
            resizable: false,
            modal: true,
            draggable: false
        });

        kendoWindow.data("kendoWindow")
            .content($("#divSiteTextValidations").html())
            .center()
            .open();
    }
    return isSuccess;
}

function onCustomToolClick(e) {
    var popupHtml =
        '<div class="k-editor-dialog k-popup-edit-form k-edit-form-container" style="width:auto;">' +
          '<div style="padding: 0 1em;">' +
            '<p><textarea cols="60" rows="10" style="width:90%"></textarea></p>' +
          '</div>' +
          '<div class="k-edit-buttons k-state-default">' +
            '<button class="k-dialog-insert k-button k-primary">Insert</button>' +
            '<button class="k-dialog-close k-button">Cancel</button>' +
          '</div>' +
        '</div>';

    var editor = $(this).data("kendoEditor");

    // Store the editor range object
    // Needed for IE
    var storedRange = editor.getRange();

    // create a modal Window from a new DOM element
    var popupWindow = $(popupHtml)
    .appendTo(document.body)
    .kendoWindow({
        // modality is recommended in this scenario
        modal: true,
        width: 600,
        resizable: false,
        title: "Insert custom content",
        // ensure opening animation
        visible: false,
        // remove the Window from the DOM after closing animation is finished
        deactivate: function (e) { e.sender.destroy(); }
    }).data("kendoWindow")
    .center().open();

    var editor = $("#txtResourcekeyHtmlText").data("kendoEditor");

    popupWindow.element.find("textarea").html($("#txtResourcekeyHtmlText").val());


    // insert the new content in the Editor when the Insert button is clicked
    popupWindow.element.find(".k-dialog-insert").click(function () {
        var customHtml = popupWindow.element.find("textarea").val();
        editor.value(''); //Empty the editor so that user can insert only new html
        //editor.selectRange(storedRange);
        editor.exec("inserthtml", { value: customHtml });
    });

    // close the Window when any button is clicked
    popupWindow.element.find(".k-edit-buttons button").click(function () {
        // detach custom event handlers to prevent memory leaks
        popupWindow.element.find(".k-edit-buttons button").off();
        popupWindow.close();
    });
}
