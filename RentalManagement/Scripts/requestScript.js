$('#savebtn').submit(function () {
    $('#savedReq').show();
    setTimeout(function () {
        $('#savedReq').fadeOut('slow');
    }, 5000);
});

$('#closebtn').submit(function () {
    $('#closeReq').show();
    setTimeout(function () {
        $('#closeReq').fadeOut('slow');
    }, 5000);
});

function redirect() {
    location.href = '~/MaintenanceRequest/Create.cshtml';
}