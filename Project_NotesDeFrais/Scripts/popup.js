/*popup to create expanseReports with day and year*/
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

/*popup to create expanse*/
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

/*change project by customer selected*/
function selectProjects() {
    var selectedItem = $('#selectedCustomer').find(":selected").val();
    $.ajax({
        url: "/Expanses/ListProject",
        data: { 'customerId': selectedItem },
        type: "post",
        cache: false,
        success: function (result) {
            $('#projectUpdated').html(result)
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("error");
        }
    });
}

/*change montant HT expanseType*/
function selectCellingExpance() {
    var selectedItem = $('#expanseTypeSelect').find(":selected").val();
    $.ajax({
        url: "/ExpanseTypes/cellingById",
        data: { 'expanseTypeID': selectedItem },
        type: "post",
        cache: false,
        success: function (result) {
            if (result != 0) {
                $('#updatHt').val(result);
                $('#updatHt').prop('readonly', "readonly");
            } else {
                $('#updatHt').prop('readonly', false);
                $('#updatHt').val(result);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("error");
        }
    });
}

$(function () {
    $.ajaxSetup({ cache: false });
    $(".deleteConf").on("click", function (e) {
        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                keyboard: true
            }, 'show');
        });
        return false;
    });
});

