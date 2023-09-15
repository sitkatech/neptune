function tinyMCEEditorToggle(isEditing, tinyMCEEditorID) {
    var tinyMCEEditorDivID = "#tinyMCEForNeptunePage" + tinyMCEEditorID;
    jQuery(".neptunePageContent", tinyMCEEditorDivID).css("display", isEditing ? "none" : "block");
    jQuery(".neptunePageTinyMCEEditor", tinyMCEEditorDivID).css("display", isEditing ? "block" : "none");
}

function tinyMCEEditorPost(tinyMCEEditorID, postURL) {
    var neptunePageContent = {
        NeptunePageContent: tinymce.get('TinyMCEEditorForNeptunePageContentID' + tinyMCEEditorID).getContent()
    };
    var config = { type: "POST", url: postURL, data: JSON.stringify(neptunePageContent), contentType: "application/json" };
    SitkaAjax.ajax(config, function (result) {
        window.location.reload();
    });
}        
