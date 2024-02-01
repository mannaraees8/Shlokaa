
$('#SubContents').css('display', 'none');
$('#SubContents1').css('display', 'none');
$('#Point1').css('display', 'none');
$('#Point2').css('display', 'none');
$('#Point3').css('display', 'none');
$('#Point4').css('display', 'none');
$('#Point5').css('display', 'none');
$('#Point6').css('display', 'none');
$('#Point7').css('display', 'none');
$('#Point8').css('display', 'none');
$('#Point9').css('display', 'none');
$('#Point10').css('display', 'none');
$('#LPoint1').css('display', 'none');
$('#LPoint2').css('display', 'none');
$('#LPoint3').css('display', 'none');
$('#LPoint4').css('display', 'none');
$('#LPoint5').css('display', 'none');
$('#LPoint6').css('display', 'none');
$('#LPoint7').css('display', 'none');
$('#LPoint8').css('display', 'none');
$('#LPoint9').css('display', 'none');
$('#LPoint10').css('display', 'none');
$('#Type').ready(function contentTypeLoad() {
    var a = document.getElementById('Type');
    var type = a.options[a.selectedIndex].text;

    //  paragraph = document.getElementById("SubContents");
    //   point = document.getElementById("Point");
    if (type == "Select") {
        alert('Please select Correct Type');
    }
    else if (type == "Paragraph") {
        $('#SubContents').css('display', 'block');
        $('#SubContents1').css('display', 'block');
        $('#Point1').css('display', 'none');
        $('#Point2').css('display', 'none');
        $('#Point3').css('display', 'none');
        $('#Point4').css('display', 'none');
        $('#Point5').css('display', 'none');
        $('#Point6').css('display', 'none');
        $('#Point7').css('display', 'none');
        $('#Point8').css('display', 'none');
        $('#Point9').css('display', 'none');
        $('#Point10').css('display', 'none');

        $('#LPoint1').css('display', 'none');
        $('#LPoint2').css('display', 'none');
        $('#LPoint3').css('display', 'none');
        $('#LPoint4').css('display', 'none');
        $('#LPoint5').css('display', 'none');
        $('#LPoint6').css('display', 'none');
        $('#LPoint7').css('display', 'none');
        $('#LPoint8').css('display', 'none');
        $('#LPoint9').css('display', 'none');
        $('#LPoint10').css('display', 'none');
        //paragraph.style.display="block;"
    }
    else if (type == "Point") {
        // alert();
        $('#SubContents').css('display', 'none');
        $('#SubContents1').css('display', 'none');
        $('#Point1').css('display', 'block');
        $('#Point2').css('display', 'block');
        $('#Point3').css('display', 'block');
        $('#Point4').css('display', 'block');
        $('#Point5').css('display', 'block');
        $('#Point6').css('display', 'block');
        $('#Point7').css('display', 'block');
        $('#Point8').css('display', 'block');
        $('#Point9').css('display', 'block');
        $('#Point10').css('display', 'block');

        $('#LPoint1').css('display', 'block');
        $('#LPoint2').css('display', 'block');
        $('#LPoint3').css('display', 'block');
        $('#LPoint4').css('display', 'block');
        $('#LPoint5').css('display', 'block');
        $('#LPoint6').css('display', 'block');
        $('#LPoint7').css('display', 'block');
        $('#LPoint8').css('display', 'block');
        $('#LPoint9').css('display', 'block');
        $('#LPoint10').css('display', 'block');
    }
    else if (type == "Paragraph and Point") {
        $('#SubContents').css('display', 'block');
        $('#SubContents1').css('display', 'block');
        $('#Point1').css('display', 'block');
        $('#Point2').css('display', 'block');
        $('#Point3').css('display', 'block');
        $('#Point4').css('display', 'block');
        $('#Point5').css('display', 'block');
        $('#Point6').css('display', 'block');
        $('#Point7').css('display', 'block');
        $('#Point8').css('display', 'block');
        $('#Point9').css('display', 'block');
        $('#Point10').css('display', 'block');

        $('#LPoint1').css('display', 'block');
        $('#LPoint2').css('display', 'block');
        $('#LPoint3').css('display', 'block');
        $('#LPoint4').css('display', 'block');
        $('#LPoint5').css('display', 'block');
        $('#LPoint6').css('display', 'block');
        $('#LPoint7').css('display', 'block');
        $('#LPoint8').css('display', 'block');
        $('#LPoint9').css('display', 'block');
        $('#LPoint10').css('display', 'block');
    }
});

$('#Type').change(function contentType() {

    var a = document.getElementById('Type');
    var type = a.options[a.selectedIndex].text;

    //  paragraph = document.getElementById("SubContents");
    //   point = document.getElementById("Point");
    if (type == "Select") {
        alert('Please select Correct Type');
    }
    else if (type == "Paragraph") {
        $('#SubContents').css('display', 'block');
        $('#SubContents1').css('display', 'block');
        $('#Point1').css('display', 'none');
        $('#Point2').css('display', 'none');
        $('#Point3').css('display', 'none');
        $('#Point4').css('display', 'none');
        $('#Point5').css('display', 'none');
        $('#Point6').css('display', 'none');
        $('#Point7').css('display', 'none');
        $('#Point8').css('display', 'none');
        $('#Point9').css('display', 'none');
        $('#Point10').css('display', 'none');

        $('#LPoint1').css('display', 'none');
        $('#LPoint2').css('display', 'none');
        $('#LPoint3').css('display', 'none');
        $('#LPoint4').css('display', 'none');
        $('#LPoint5').css('display', 'none');
        $('#LPoint6').css('display', 'none');
        $('#LPoint7').css('display', 'none');
        $('#LPoint8').css('display', 'none');
        $('#LPoint9').css('display', 'none');
        $('#LPoint10').css('display', 'none');
        //paragraph.style.display="block;"
    }
    else if (type == "Point") {
        // alert();
        $('#SubContents').css('display', 'none');
        $('#SubContents1').css('display', 'none');
        $('#Point1').css('display', 'block');
        $('#Point2').css('display', 'block');
        $('#Point3').css('display', 'block');
        $('#Point4').css('display', 'block');
        $('#Point5').css('display', 'block');
        $('#Point6').css('display', 'block');
        $('#Point7').css('display', 'block');
        $('#Point8').css('display', 'block');
        $('#Point9').css('display', 'block');
        $('#Point10').css('display', 'block');

        $('#LPoint1').css('display', 'block');
        $('#LPoint2').css('display', 'block');
        $('#LPoint3').css('display', 'block');
        $('#LPoint4').css('display', 'block');
        $('#LPoint5').css('display', 'block');
        $('#LPoint6').css('display', 'block');
        $('#LPoint7').css('display', 'block');
        $('#LPoint8').css('display', 'block');
        $('#LPoint9').css('display', 'block');
        $('#LPoint10').css('display', 'block');
    }
    else if (type == "Paragraph and Point") {
        $('#SubContents').css('display', 'block');
        $('#SubContents1').css('display', 'block');
        $('#Point1').css('display', 'block');
        $('#Point2').css('display', 'block');
        $('#Point3').css('display', 'block');
        $('#Point4').css('display', 'block');
        $('#Point5').css('display', 'block');
        $('#Point6').css('display', 'block');
        $('#Point7').css('display', 'block');
        $('#Point8').css('display', 'block');
        $('#Point9').css('display', 'block');
        $('#Point10').css('display', 'block');

        $('#LPoint1').css('display', 'block');
        $('#LPoint2').css('display', 'block');
        $('#LPoint3').css('display', 'block');
        $('#LPoint4').css('display', 'block');
        $('#LPoint5').css('display', 'block');
        $('#LPoint6').css('display', 'block');
        $('#LPoint7').css('display', 'block');
        $('#LPoint8').css('display', 'block');
        $('#LPoint9').css('display', 'block');
        $('#LPoint10').css('display', 'block');
    }

});

