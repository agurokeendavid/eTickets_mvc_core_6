$(function () {
    let output = document.getElementById('ImageUrlPreview');
    output.src = $('#ImageURL').val();
    $('#ImageURL').on('change', function () {
        let output = document.getElementById('ImageUrlPreview');
        output.src = $(this).val();
    });
});