



var Id;
var date;
var Department;
var intime;
var outtime;
var oldintime
var oldouttime
$('.date').text(new Date().toISOString().slice(0, 10));
var status = "Present";
var reason = "";


$(".GetId").click(function () {

    $('#status').val('Select');
    $('#reason').val('Select')

    $('#divreason').css('display', 'none');

    var $row = $(this).closest("tr");    // Find the row
    Id = $row.find(".ID").text(); // Find the text
    var $Name = $row.find(".Name").text();
    date = $row.find(".Date").text();
    Department = $row.find(".Department").text().trim();
    intime = $row.find(".InTime").text().trim();
    outtime = $row.find(".OutTime").text().trim();
    oldintime = $row.find(".Enteringtime").text().trim();
    oldouttime = $row.find(".Leavingtime").text().trim();
    $('#staffName').text($Name);
    $('#department').text(Department);




    var dt = "2020-12-20 ";

    var defaultintime = dt.concat(intime);
    var defaultouttime = dt.concat(outtime);

    var oldintime = dt.concat(oldintime);
    var oldouttime = dt.concat(oldouttime);


    var DefaultInTime = new Date(defaultintime);
    var DefaultOutTime = new Date(defaultouttime);

    var oldintime = new Date(oldintime);
    var oldouttime = new Date(oldouttime);

    oldintime = (oldintime).toString();
    oldintime = oldintime.slice(16, 24);
    oldouttime = (oldouttime).toString();
    oldouttime = oldouttime.slice(16, 24);

    defaultintime = formatAMPM(DefaultInTime);
    defaultouttime = formatAMPM(DefaultOutTime);

    $('#intime').text(defaultintime);
    $('#outtime').text(defaultouttime);
    $('#enteringTime').val(oldintime);
    $('#leavingTime').val(oldouttime);


    DefaultInTime.setHours(DefaultInTime.getHours() + 1);
    DefaultOutTime.setHours(DefaultOutTime.getHours() - 1);
    DefaultOutTime.setMinutes(DefaultOutTime.getMinutes() - 1);
    DefaultInTime = (DefaultInTime).toString();
    DefaultInTime = DefaultInTime.slice(16, 24);
    DefaultOutTime = (DefaultOutTime).toString();
    DefaultOutTime = DefaultOutTime.slice(16, 24);

    // Let's test it out
    $('#staticBackdrop').modal('show')

    $('#enteringTime').change(function () {

        if ($('#enteringTime').val() > DefaultInTime || $('#leavingTime').val() < DefaultOutTime) {

            $('#divreason').css('display', 'block');
            status = "Half Day";
            reason = $('#reason').val();
        }
        else {
            $('#divreason').css('display', 'none');
            status = $('#status').val();
        }
    });

    $('#leavingTime').change(function () {
        if ($('#leavingTime').val() < DefaultOutTime || $('#enteringTime').val() > DefaultInTime) {
            $('#divreason').css('display', 'block');
            status = "Half Day";
            reason = $('#reason').val();
        }
        else {
            $('#divreason').css('display', 'none');
            status = $('#status').val();
        }
    });

    $('#status').change(function () {
        if ($('#status').val() == 'Absent') {
            $('#divreason').css('display', 'block');
            $('.timestamp').css('display', 'none');
        }
        else if ($('#status').val() == 'Present' && $('#leavingTime').val() < DefaultOutTime) {
            $('#divreason').css('display', 'block');
            $('.timestamp').css('display', 'block');
            status = "Half Day";
        }
        else if ($('#status').val() == 'Present' && $('#enteringTime').val() > DefaultInTime) {
            $('#divreason').css('display', 'block');
            $('.timestamp').css('display', 'block');
            status = "Half Day";
        }
        else {
            $('#divreason').css('display', 'none');
            $('.timestamp').css('display', 'block');
            status = $('#status').val();
        }
    });

});



$("#submit").click(function () {

    var isValid = true;
    if ($('#status').val() == 'Select') {
        $('#errormsg').text('Please select status');
        isValid = false;
    }
    else if ($('#divreason').css('display') == 'block') {

        if ($('#reason').val() == 'Select') {
            $('#errormsg').text('Please select reason');
            isValid = false;
        }
    }
    else if ($('#divreason').css('display') == 'none') {
        $('#reason').val('');
    }
    if ($('#status').val() == 'Absent') {

        if ($('#reason').val() == 'Select') {
            $('#errormsg').text('Please select reason');
            isValid = false;
        }
        else {

            reason = $('#reason').val();

        }
    }
    if (isValid == true) {

        if ($('#status').val() == 'Absent') {


            var Attendance = {
                Id: Id,
                Status: $('#status').val(),
                Date: date,
                EnteringTime: $('#intime').text().trim(),
                LeavingTime: $('#outtime').text().trim(),
                Department: $('#department').text().trim(),
                Reason: $('#reason').val(),
                InTime: $('#intime').text().trim(),
                OutTime: $('#outtime').text().trim(),

            }
        }
        else {

            var Attendance = {
                Id: Id,
                status: status,
                Date: date,
                EnteringTime: $('#enteringTime').val().trim(),
                LeavingTime: $('#leavingTime').val().trim(),
                Department: $('#department').text().trim(),
                Reason: $('#reason').val(),
                InTime: $('#intime').text().trim(),
                OutTime: $('#outtime').text().trim(),
            }
        }




        $.ajax({

            type: 'POST',
            url: '/Attendance/update',

            data: JSON.stringify(Attendance),
            contentType: 'application/json',
            success: function (data) {
                if (data.status) {

                    $('#department').val('');
                    $('#reason').val('');
                    $('#successmsg').val('New attendance created.');
                    $('#staticBackdrop').modal('hide');
                    location.reload();
                }
                else {
                    $('#errormsg').text('Please select proper details.');
                }

            },
            error: function (error) {
                console.log(error);
                $('#submit').text('Save');
            }
        });

    }
    else {
        $('#errormsg').text('Please select proper details.');
    }


});

function formatAMPM(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}

