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
            "backColor"
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

