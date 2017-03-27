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
