$(function () {
    var output = document.getElementById('CinemaLogoPreview');
    output.src = $('#Logo').val();
    $('#Logo').on('change', function () {
        var output = document.getElementById('CinemaLogoPreview');
        output.src = $(this).val();
    });
});