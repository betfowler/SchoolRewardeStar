$(document).ready(function () {

    $("#studentsIcon img").mouseover(function () {
        $(this).attr("src", "../Content/Images/Students2.png")
    })

    $("#studentsIcon img").mouseout(function () {
        $(this).attr("src", "../Content/Images/Students.png")
    })

    $("#classesIcon img").mouseover(function () {
        $(this).attr("src", "../Content/Images/Classes2.png")
    })

    $("#classesIcon img").mouseout(function () {
        $(this).attr("src", "../Content/Images/Classes.png")
    })

    $("#userIcons img").mouseover(function () {
        $(this).attr("src", "../Content/Images/Users2.png")
        $(".manageUsers").css("display", "block")
    })


})