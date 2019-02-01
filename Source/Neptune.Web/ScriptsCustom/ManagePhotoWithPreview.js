function onLoadFileReader(event) {
    var fileResourcePhotoPreview = jQuery("#fileResourcePhotoPreview");
    fileResourcePhotoPreview.find("img").attr("src", event.target.result);
    fileResourcePhotoPreview.show();

    var newPhotoCaptionFormGroup = jQuery("#newPhotoCaptionFormGroup");
    newPhotoCaptionFormGroup.show();
}
function onChangeFileInput(event) {
    var file = event.target.files ? event.target.files[0] : null;
    if (file) {
        var fileReader = new FileReader();
        fileReader.onload = onLoadFileReader;
        fileReader.readAsDataURL(file);
    } else {
        var fileResourcePhotoPreview = jQuery("#fileResourcePhotoPreview");
        fileResourcePhotoPreview.find("img").attr("src", "#");
        fileResourcePhotoPreview.hide();

        var newPhotoCaptionFormGroup = jQuery("#newPhotoCaptionFormGroup");
        newPhotoCaptionFormGroup.find(":input").val("");
        newPhotoCaptionFormGroup.hide();
    }
}