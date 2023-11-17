$(function () {
    var output = document.getElementById('ProfilePicturePreview');
    output.src = $('#ProfilePictureURL').val();
    $('#ProfilePictureURL').on('change', function () {
        var output = document.getElementById('ProfilePicturePreview');
        output.src = $(this).val();
    });
});