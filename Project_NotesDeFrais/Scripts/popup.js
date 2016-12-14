$(function () {
    $.ajaxSetup({ cache: false });
    $("#dayMonth").on("click", function (e) {
        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                keyboard: true
            }, 'show');
        });
        return false;
    });
});

$(function () {
    $.ajaxSetup({ cache: false });
    $("#ajoutType").on("click", function (e) {
        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                keyboard: true
            }, 'show');
        });
        return false;
    });
});

$(function () {
    $.ajaxSetup({ cache: false });
    $("#modif").on("click", function (e) {
        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                keyboard: true
            }, 'show');
        });
        return false;
    });
});