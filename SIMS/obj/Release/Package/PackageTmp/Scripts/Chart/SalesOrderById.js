
$(document).ready(function () {
    var uId = document.getElementById("txtSalesOrderUserId").value;
    $.ajax({
        type: "GET",
        url: '/Chart/TallyPendingCountById',
        data: { "userId": uId },
        success: function (data) {
            $('#tallyCount').text(data);
        }
    });
})
let currentSalesOrderDataNotFound = false;
$(document).ready(function () {

    var context = document.querySelector('#SalesOrderGraph').getContext('2d');
    var salesOrderUserChart = new Chart(context, {
        type: 'doughnut',
        data: {
            labels: ['Actual', 'Target'],
            datasets: [{
                data: [],
                backgroundColor: ['#00e676', '#e91e63', '#ff5722', '#1e88e5', '#ffd600'],
                borderWidth: 0.5,
                borderColor: '#ddd'
            }]
        },
        options: {
            layout: {
                padding: {
                    left: 70,
                    right: 70,
                }
            },
            legend: {
                display: true,
                position: 'top',
                labels: {
                    boxWidth: 20,
                    fontColor: '#111',
                    padding: 15
                }
            },
            tooltips: {
                enabled: true
            },
            plugins: {
                datalabels: {
                    color: 'blue',

                }

            }
        }
    });
    var userId = document.getElementById("txtSalesOrderUserId").value;
    var startDate = document.getElementById('startDateSalesOrderById').value;
    var endDate = document.getElementById('endDateSalesOrderById').value;
    var context = document.querySelector('#SalesOrderGraph').getContext('2d');

    $.ajax({
        type: "GET",
        url: '/Chart/TallyChart',
        data: { 'userId': userId, 'startDate': startDate, 'endDate': endDate },
        success: function (data) {

            if (data.length == 0) {
                currentSalesOrderDataNotFound = true;
                salesOrderChartUpdate();
                //   $('#lblSalesOrderDataNotFound').css('display', 'block');
                //   $('#SalesOrderGraph').css('display', 'none');
            } else {
                currentSalesOrderDataNotFound = false;
                // $('#lblSalesOrderDataNotFound').css('display', 'none');
                // $('#SalesOrderGraph').css('display', 'block');
                salesOrderUserChart.data.datasets[0].data = [data[0].Achieved, data[0].Pending]; // Would update the first dataset's value of 'March' to be 50
                salesOrderUserChart.options = {
                    layout: {
                        padding: {
                            left: 70,
                            right: 70,
                        }
                    },
                    title: {
                        display: true,
                        position: 'top',
                        //text: userIdlist[i].StaffName,
                        fontSize: 18
                    },
                    legend: {
                        display: true,
                        position: 'bottom',
                        padding: {
                            top: 70
                        }
                    },
                    plugins: {

                        datalabels: {
                            //labels: {
                            //    title: {
                            //        color: 'blue'
                            //    }
                            //},
                            //formatter: (value, context) => {
                            //    let sum = parseInt(data[0].TotalOrderValue) + parseInt(data[0].TargetAmount);
                            //    let percentage = (value * 100 / sum).toFixed(2) + "%";

                            //    return percentage;


                            //},
                            //color: '#000',
                            labels: {
                                index: {
                                    align: 'end',
                                    anchor: 'end',
                                    color: 'black',
                                    font: { size: 14 },
                                    formatter: function (value, context) {
                                        return value;
                                    },
                                    offset: 5,
                                    //opacity: function (ctx) {
                                    //    return ctx.active ? 1 : 0.5;
                                    //}
                                },
                                value: {
                                    align: 'center',
                                    anchor: 'top',
                                    font: { size: 16 },
                                    formatter: (value, context) => {
                                        let sum = parseInt(data[0].Achieved) + parseInt(data[0].Pending);
                                        let percentage = (value * 100 / sum).toFixed(2) + "%";

                                        return percentage;


                                    },
                                    color: '#000',
                                }
                            }
                        }
                    }
                }
                salesOrderUserChart.update();
            }
        }
    });

    salesOrderChartUpdate = function SalesOrderUpdate() {

        if (currentSalesOrderDataNotFound) {
            $(".SalesOrderAlert").css("display", "block")
            let date = new Date(new Date().getFullYear(), 0, 2).toISOString().slice(0, 10);
            $('#startDateSalesOrderById').val(date);
        } else {
            $(".SalesOrderAlert").css("display", "none");
        }

        var startDate = document.getElementById('startDateSalesOrderById').value;
        var endDate = document.getElementById('endDateSalesOrderById').value;
        var userId = document.getElementById("txtSalesOrderUserId").value;

        $.ajax({
            type: "GET",
            url: '/Chart/TallyChart',
            data: { 'userId': userId, 'startDate': startDate, 'endDate': endDate },
            success: function (data) {
                if (data.length == 0) {
                    //$('#lblSalesOrderDataNotFound').css('display', 'block');
                    $('#SalesOrderGraph').css('display', 'none');
                } else {
                    //$('#lblSalesOrderDataNotFound').css('display', 'none');
                    $('#SalesOrderGraph').css('display', 'block');
                    salesOrderUserChart.data.datasets[0].data = [data[0].Achieved, data[0].Pending];
                    salesOrderUserChart.options = {
                        layout: {
                            padding: {
                                left: 70,
                                right:70,
                            }
                        },
                        title: {
                            display: true,
                            position: 'top',
                            //text: userIdlist[i].StaffName,
                            fontSize: 18
                        },
                        legend: {
                            display: true,
                            position: 'bottom',
                            padding: {
                                top: 70
                            }
                        },
                        plugins: {

                            datalabels: {
                                //labels: {
                                //    title: {
                                //        color: 'blue'
                                //    }
                                //},
                                //formatter: (value, context) => {
                                //    let sum = parseInt(data[0].TotalOrderValue) + parseInt(data[0].TargetAmount);
                                //    let percentage = (value * 100 / sum).toFixed(2) + "%";

                                //    return percentage;


                                //},
                                //color: '#000',
                                labels: {
                                    index: {
                                        align: 'end',
                                        anchor: 'end',
                                        color: 'black',
                                        font: { size: 14 },
                                        formatter: function (value, context) {
                                            return value;
                                        },
                                        offset: 5,
                                        //opacity: function (ctx) {
                                        //    return ctx.active ? 1 : 0.5;
                                        //}
                                    },
                                    value: {
                                        align: 'center',
                                        anchor: 'top',
                                        font: { size: 14 },
                                        formatter: (value, context) => {
                                            let sum = parseInt(data[0].Achieved) + parseInt(data[0].Pending);
                                            let percentage = (value * 100 / sum).toFixed(2) + "%";

                                            return percentage;


                                        },
                                        color: '#000',
                                    }
                                }
                            }
                        }
                    }
                    salesOrderUserChart.update();
                }
            }
        });
    }
});