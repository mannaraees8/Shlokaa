$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: '/Chart/TallyPendingCount',
        success: function (data) {
            $("#lblTallyEntryPending").text(data);
        }
    });
    $.ajax({
        type: "GET",
        url: '/Chart/RetrievePaymentAmountPending',
        success: function (data) {
            $("#lblPaymentPending").text(data);
        }
    });
    $.ajax({
        type: "GET",
        url: '/Chart/RetrieveChequePending',
        success: function (data) {
            $("#lblChequePending").text(data[0].PendingCount);
            $("#lblChequePaymentAmountPending").text(data[0].PendingAmount);
        }
    });
})