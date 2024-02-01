
$('#email').click(function () {
    if ($('#email').val() == 0) {
        $('#email').css('border', '2px solid orange');
    }
    else {
        $('#email').css('border', '2px solid red');
    }
});
$('#password').click(function () {
    if ($('#password').val() == 0) {
        $('#password').css('border', '2px solid orange');
    }
    else {
        $('#password').css('border', '2px solid red');
    }
});



