﻿$(document).ready(function () {

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
        $('.awardStudents').prop('checked', !$('.awardStudents').prop('checked'));
    })


    $(".studentModal").click(function () {

        var _href = $(".removestudent").attr("href");
        $(".removestudent").attr('href', _href + '&studentID=' + $(this).attr('id'));

        $(".keepstudent").click(function(){
            $(".removestudent").attr('href', _href);
        })
    })

    $(".guardianModal").click(function () {

        var _href = $(".removeguardian").attr("href");
        $(".removeguardian").attr('href', _href + '&guardianID=' + $(this).attr('id'));

        $(".keepguardian").click(function () {
            $(".removeguardian").attr('href', _href);
        })
    })

    //when editing select students already in class
    var selectedStaff = $("#staffList").text();
    var staffList = String($("#staffList").attr("value", selectedStaff));
    if (selectedStaff != null) {
        var last = selectedStaff.substr(selectedStaff.length - 1);
        if (last == ",") {
            selectedStaff = selectedStaff.slice(0, -1);
        }
        var array = selectedStaff.split(',');
        for (var i = 0; i < array.length; i++) {
            var value = parseInt(array[i]);
            $("#" + value).prop('checked', true);
        }
    }
    //display in boxes
    $(".checkedStaff").each(function () {
        if ($(this).prop('checked') == true) {
            var textValue = $("#selectedStaff").attr('value');
            var userName = $(this).attr('alt');
            var count = parseInt($("#staffCount").attr('value'));
            if (textValue == null || textValue == "") {
                textValue = userName;
            }
            else {
                textValue = textValue + ", " + userName;
            }
            count = count + 1;
            $("#selectedStaff").attr('value', textValue);
            $("#staffCount").attr('value', count);
        }
    })

    $(".checkedStaff").click(function () {
        var textValue = $("#selectedStaff").attr('value');
        var userName = $(this).attr('alt');
        var count = parseInt($("#staffCount").attr('value'));
        var idList = $("#staffList").attr('value');
        var id = $(this).attr('id');

        if ($(this).prop('checked') == true) {
            if (textValue == null || textValue == "") {
                textValue = userName;
                idList = id;
            }
            else {
                textValue = textValue + ", " + userName;
                idList = idList + "," + id;
            }
            count = count + 1;
        }
        else {
            var replace = ", " + userName;
            var index = textValue.indexOf(replace)
            if (index >= 0) {
                textValue = textValue.replace(", " + userName, "");
            }
            else {
                textValue = textValue.replace(userName, "");
            }
            count = count - 1;
        }

        $("#selectedStaff").attr('value', textValue);
        $("#staffCount").attr('value', count);
        $("#staffList").attr('value', idList);
    })

    //when editing select students already in class   
    var selectedStudents = $("#studentList").text();
    var studentList = String($("#studentList").attr("value", selectedStudents));
    if (selectedStudents != null) {
        var last = selectedStudents.substr(selectedStudents.length - 1);
        if (last == ",") {
            selectedStudents = selectedStudents.slice(0, -1);
        }
        var array = selectedStudents.split(',');
        for (var i = 0; i < array.length; i++) {
            var value = parseInt(array[i]);
            $("#" + value).prop('checked', true);
        }
    }
    


    //display in boxes
    $(".checkedStudents").each(function () {
        if ($(this).prop('checked') == true) {
            var textValue = $("#selectedStudents").attr('value');
            var userName = $(this).attr('alt');
            var count = parseInt($("#studentcount").attr('value'));
            if (textValue == null || textValue == "") {
                textValue = userName;
            }
            else {
                textValue = textValue + ", " + userName;
            }
            count = count + 1;
            $("#selectedStudents").attr('value', textValue);
            $("#studentcount").attr('value', count);
        }
    })
    


    $(".checkedStudents").click(function () {
        var textValue = $("#selectedStudents").attr('value');
        var userName = $(this).attr('alt');
        var count = parseInt($("#studentcount").attr('value'));
        var idList = $("#studentList").attr('value');
        var id = $(this).attr('id');

        if ($(this).prop('checked') == true) {
            if (textValue == null || textValue == "") {
                textValue = userName;
                idList = id;
            }
            else {
                textValue = textValue + ", " + userName;
                idList = idList + "," + id;
            }
            count = count + 1;
        }
        else {
            var replace = ", " + userName;
            var index = textValue.indexOf(replace)
            if (index >= 0) {
                textValue = textValue.replace(", " + userName, "");
            }
            else {
                textValue = textValue.replace(userName, "");
            }
            count = count - 1;
        }

        $("#selectedStudents").attr('value', textValue);
        $("#studentcount").attr('value', count);
        $("#studentList").attr('value', idList);
    })

    $("#userSearch").click(function () {
        var staffSearch = $("#staffSearch").val();
        var studentSearch = $("#studentSearch").val();
        var className = $("#className").val();
        var url = $(this).attr('href') + '?staffSearch=' + staffSearch +'&studentSearch=' + studentSearch + '&className=' + className;
        location.href = url;
        return false;
    })

    $("#search").click(function () {
        var staffSearch = $("#staffSearch").val();
        var studentSearch = $("#studentSearch").val();
        var className = $("#className").val();
        var url = $(this).attr('href') + '?staffSearch=' + staffSearch + '&studentSearch=' + studentSearch + '&className=' + className;
        location.href = url;
        return false;
    })

    $("#studentBasket").mouseover(function () {
        $("#basketDropdown").css("display", "block");
        $("#studentBasket").mouseout(function () {
            $("#basketDropdown").css("display", "none");
        })
    })

    $("#Student_User_ID").click(function () {
        $.ajax({
            type: "POST",
            url: "/Pledges/AjaxMethod",
            data: '{studentID: "' + $(this).val() + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#student").text(response.Name);
                $("#points").text(response.Points);
            },
            failure: function (response) {
                $("#pointsTest").text("Failure");
            },
            error: function (response) {
                $("#pointsTest").text("Error");
            }
        });
    });

    $("#classid").attr('value', $("#classID").val());
    $("#classID").click(function () {
        $("#classid").attr('value', $("#classID").val());
    })

    //when editing select students already in class   
    var selectedStudents = $("#studentList").text();
    var studentList = String($("#studentList").attr("value", selectedStudents));
    if (selectedStudents != null) {
        var last = selectedStudents.substr(selectedStudents.length - 1);
        if (last == ",") {
            selectedStudents = selectedStudents.slice(0, -1);
        }
        var array = selectedStudents.split(',');
        for (var i = 0; i < array.length; i++) {
            var value = parseInt(array[i]);
            $("#" + value).prop('checked', true);
        }
    }

    $(".awardStudents").click(function () {
        var idList = $("#studentList").attr('value');
        var id = $(this).attr('id');

        if ($(this).prop('checked') == true) {
            if (idList == null || idList == "") {
                idList = id;
            }
            else {
                idList = idList + "," + id;
            }
        }
        else {
            var replace = ", " + id;
            var index = idList.indexOf(replace)
            if (index >= 0) {
                idList = idList.replace(", " + id, "");
            }
            else {
                idList = idList.replace(id, "");
            }
        }

        $("#studentList").attr('value', idList);
    })

})