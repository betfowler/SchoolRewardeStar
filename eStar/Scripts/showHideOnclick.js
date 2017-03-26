function guardianstudentlist(id) {
    $(".studentDetailsTitle").css("display", "none");
    $(".studentDetails").css("display", "none");

    var value = "student" + id;
    var titlevalue = "studentTitle" + id;

    $("#" + value).css("display", "table-row");
    $("#" + titlevalue).css("display", "table-row");
}