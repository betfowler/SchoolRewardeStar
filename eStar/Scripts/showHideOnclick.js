function guardianstudentlist(id) {

    $(".studentDetailsTitle").css("display", "none");
    $(".studentDetails").css("display", "none");
    

    if ($("#" + id).text() == "View Students ")
    {
        var value = "student" + id;
        $(".studentdropdown").html("View Students <i class='fa fa-angle-double-down' aria-hidden='true'></i>");
        
        $("." + value).css("display", "table-row");

        $("#" + id).html("Hide Students <i class='fa fa-angle-double-up' aria-hidden='true'></i>");
    }
    else {
        $("#" + id).html("View Students <i class='fa fa-angle-double-down' aria-hidden='true'></i>");
    }
}

function studentguardianlist(id) {

    $(".guardianDetailsTitle").css("display", "none");
    $(".guardianDetails").css("display", "none");


    if ($("#" + id).text() == "View Guardians ") {
        var value = "guardian" + id;
        $(".guardiandropdown").html("View Guardians <i class='fa fa-angle-double-down' aria-hidden='true'></i>");

        $("." + value).css("display", "table-row");

        $("#" + id).html("Hide Guardians <i class='fa fa-angle-double-up' aria-hidden='true'></i>");
    }
    else {
        $("#" + id).html("View Guardians <i class='fa fa-angle-double-down' aria-hidden='true'></i>");
    }
}

function setGuardianId(id) {
    $("#Guardian_User_ID").attr("Value", id);
    $("#createForm").submit();
}

function setStudentId(id) {
    $("#Student_User_ID").attr("Value", id);
    $("#createForm").submit();
}

function selectstaff() {

    if ($("#showstafflink").text() == "Select Staff ") {
        $("#showstafflink").html("Hide Staff <i class='fa fa-angle-double-up' aria-hidden='true'></i>");
        $(".selectStaff").css("display", "block");
    }
    else {
        $("#showstafflink").html("Select Staff <i class='fa fa-angle-double-down' aria-hidden='true'></i>");
        $(".selectStaff").css("display", "none");
    }
}

function selectstudent() {
    if ($("#showstudentslink").text() == "Select Students ") {
        $("#showstudentslink").html("Hide Students <i class='fa fa-angle-double-up' aria-hidden='true'></i>");
        $(".selectStudents").css("display", "block");
    }
    else {
        $("#showstudentslink").html("Select Students <i class='fa fa-angle-double-down' aria-hidden='true'></i>");
        $(".selectStudents").css("display", "none");
    }
}

function deadline() {
    var checkboxValue = $("#deadlineCheck").attr('value');
    if (checkboxValue == "false") {
        $("#deadlineCheck").attr('value', 'true');
        $("#deadlineField").css('display', 'block');
    }
    else {
        $("#deadlineCheck").attr('value', 'false');
        $("#deadlineField").css('display', 'none');
        $("#Deadline").val("");
    }
}

function classSearchUsers() {
    var staffList = String($("#staffList").attr("value"));
    var studentList = String($("#studentList").attr("value"));
    var searchStaff = $("#staffSearch").val();
    var searchStudent = $("#studentSearch").val();
    var name = $("#className").val();
    window.location.href = "../Classes/Create?staffSearch=" + searchStaff + "&staff=" + staffList + "&className=" + name + "&student=" + studentList + "&studentSearch=" + searchStudent
}

function classSearchEdit(id) {
    var staffList = String($("#staffList").attr("value"));
    var studentList = String($("#studentList").attr("value"));
    var searchStaff = $("#staffSearch").val();
    var searchStudent = $("#studentSearch").val();
    var name = $("#className").val();
    window.location.href = "../../Classes/Edit/"+id+"?staffSearch=" + searchStaff + "&staff=" + staffList + "&className=" + name + "&student=" + studentList + "&studentSearch=" + searchStudent
}