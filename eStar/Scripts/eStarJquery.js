$(document).ready(function () {

    function tog(v) { return v ? 'addClass' : 'removeClass'; }

    $('.clearable').on('input', function () {
        $(this)[tog(this.value)]('x');

        $('.x').on('mousemove', function (e) {
            $(this)[tog(this.offsetWidth - 18 < e.clientX - this.getBoundingClientRect().left)]('onX');

            $('.onX').on('touchstart click', function (ev) {
                ev.preventDefault();
                $(this).removeClass('x onX').val('').change();
            })
        })
    })

    $('.panel-heading a').click(function () {
        if ($(this).attr('class') == null || $(this).attr('class') == "")
            $('.panel-heading a span').attr('class', 'glyphicon glyphicon-collapse-down')
        else
            $('.panel-heading a span').attr('class', 'glyphicon glyphicon-collapse-up')
    })

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

    $("#staffIcons img").mouseover(function () {
        $(this).attr("src", "../Content/Images/UsersStaff.png")
    })
    $("#staffIcons img").mouseout(function () {
        $(this).attr("src", "../Content/Images/Staff2.png")
    })

    $("#guardianIcons img").mouseover(function () {
        $(this).attr("src", "../Content/Images/UsersGuardians.png")
    })
    $("#guardianIcons img").mouseout(function () {
        $(this).attr("src", "../Content/Images/UsersGuardians2.png")
    })

    $("#awardIcon img").mouseover(function () {
        $(this).attr("src", "../Content/Images/Awards2.png")
    })
    $("#awardIcon img").mouseout(function () {
        $(this).attr("src", "../Content/Images/Awards.png")
    })

    $('#selectAll').click(function () {
        $('.selectedStudent').prop('checked', !$('.selectedStudent').prop('checked'));
    })

})