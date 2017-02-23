$(document).ready(function () {
    window.onbeforeunload = function (e) {
        if (e) {
            e.returnValue = "Any string";
        }
        return "Any string";
    }
})