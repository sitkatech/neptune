function onLoadFileReader(resizedEvent, inputChangeEvent) {
    var reader = new FileReader();
    reader.onload = function (onloadEvent) {
        // change the file input files to have the resized image
        let container = new DataTransfer();
        container.items.add(resizedEvent);
        inputChangeEvent.target.files = container.files;

        // update the preview image src with the blob url
        var fileResourcePhotoPreview = jQuery("#fileResourcePhotoPreview");
        fileResourcePhotoPreview.find("img").attr("src", onloadEvent.target.result);
        fileResourcePhotoPreview.show();

        // show the caption box
        var newPhotoCaptionFormGroup = jQuery("#newPhotoCaptionFormGroup");
        newPhotoCaptionFormGroup.show();
    };

    // Read the file as a Data URL
    reader.readAsDataURL(resizedEvent);
}

function onChangeFileInput(event) {
    var file = event.target.files ? event.target.files[0] : null;
    if (file) {
        resizeImage(file, 800, 600, onLoadFileReader, event);
    } else {
        var fileResourcePhotoPreview = jQuery("#fileResourcePhotoPreview");
        fileResourcePhotoPreview.find("img").attr("src", "#");
        fileResourcePhotoPreview.hide();

        var newPhotoCaptionFormGroup = jQuery("#newPhotoCaptionFormGroup");
        newPhotoCaptionFormGroup.find(":input").val("");
        newPhotoCaptionFormGroup.hide();
    }
}


function resizeImage(file, maxWidth, maxHeight, callback, originalEvent) {
    const reader = new FileReader();

    reader.onload = function (event) {
        const image = new Image();
        image.src = event.target.result;

        image.onload = function () {
            let width = image.width;
            let height = image.height;

            if (width > maxWidth || height > maxHeight) {
                if (width / maxWidth > height / maxHeight) {
                    width = maxWidth;
                    height = (maxWidth * image.height) / image.width;
                } else {
                    height = maxHeight;
                    width = (maxHeight * image.width) / image.height;
                }
            }

            const canvas = document.createElement('canvas');
            const ctx = canvas.getContext('2d');
            canvas.width = width;
            canvas.height = height;
            ctx.drawImage(image, 0, 0, width, height);

            canvas.toBlob(function (blob) {
                const resizedImage = new File([blob], file.name, {
                    type: file.type,
                    lastModified: file.lastModified,
                });

                callback(resizedImage, originalEvent);
            }, file.type);
        };
    };

    reader.readAsDataURL(file);
}