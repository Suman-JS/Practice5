$('#UserImage').click(function () {
    UploadClickEvent();
});

$('.upload-photo').click(function () {
    UploadClickEvent();
});

function UploadClickEvent() {
    $('#BrowseImage').trigger('click');
}

$('#BrowseImage').change(function () {

    var file = this.files; 

    if (file && file[0]) {
        var fileReader = new FileReader();
        fileReader.readAsDataURL(file[0]);

        fileReader.onload = function (x) {

            var image = new Image;
            image.src = x.target.result;

            image.onload = function () {
                var width = this.width;
                var height = this.height;
                var type = file[0].type;

                if ((width == "8000" && height == "8000") && (type == "image/jpg" || type == "image/jpeg" || type == "image/png" ))
                {

                    $('#UserImage').attr('src', x.target.result);
                    $(".image-desc").css("color", "black");
                }
                else {
                    $(".image-desc").css("color", "red");
                }
            }

        }
    }
});

$('.remove-photo').click(function () {

    $('#UserImage').attr('src', "/Images/no_image.png");
    $('#ImagePath').val("/Images/no_image.png");
});