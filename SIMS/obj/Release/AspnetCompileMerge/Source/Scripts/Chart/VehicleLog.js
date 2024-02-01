



var vehicleLogChart;
let vehicleLogData = () => {
    let jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, spt = 0, oct = 0, nov = 0, dec = 0;

    return {
        isLoading: true,
        startDate: document.querySelector('.vehicleLog #startDate').value,
        endDate: document.querySelector('.vehicleLog #endDate').value,
        vehicleNo: document.getElementById("vehicleLogUserSelect").value,
        userName: document.getElementById('vehicleLogUserSelect').options[document.getElementById('vehicleLogUserSelect').selectedIndex].text,
        dataNotFound: true,
        selectDateRequired: false,
        vehicleData: [],

        init() {
            this.getGraphDataForAllUsers()
            this.generateGraph(data = [])
        },

        getGraphDataForAllUsers() {

            this.startDate = document.querySelector('.vehicleLog #startDate').value,
                this.endDate = document.querySelector('.vehicleLog #endDate').value,
                this.userName = document.getElementById('vehicleLogUserSelect').options[document.getElementById('vehicleLogUserSelect').selectedIndex].text,
                this.vehicleNo = document.getElementById("vehicleLogUserSelect").value;

            generateGraph = (data) => {

                this.updateGraph(data)
            }


            callOneYearGraph = () => {
                this.getGraphDataForAllUsers()
            }

            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {
                this.selectDateRequired = false
                $.ajax({
                    type: "GET",
                    url: '/Chart/RetrieveVehicleLogChartDataById',
                    data: { 'vehicleNo': this.vehicleNo, 'startDate': this.startDate, 'endDate': this.endDate },
                    success: function (data) {
                            generateGraph(data)

                    }
                })
            }

        },

        updateGraph(data) {

            this.isLoading = false

            data && data.length <= 0 ? this.selectDateRequired = true : this.selectDateRequired = false;
            console.log(data,'graph')
            if (data) {

              
                jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, spt = 0, oct = 0, nov = 0, dec = 0;
                data.forEach(ele => {
                    console.log(ele)
                    if (parseInt(ele.Month) == 1) {
                        jan = ele.DistanceTravelled;
                    }
                    if (parseInt(ele.Month) == 2) {
                        feb = ele.DistanceTravelled;
                    }
                    if (parseInt(ele.Month) == 3) {
                        mar = ele.DistanceTravelled;
                    }
                    if (parseInt(ele.Month) == 4) {
                        apr = ele.DistanceTravelled;
                    }
                    if (parseInt(ele.Month) == 5) {
                        may = ele.DistanceTravelled;
                    }
                    if (parseInt(ele.Month) == 6) {
                        jun = ele.DistanceTravelled;
                    }
                    if (parseInt(ele.Month) == 7) {
                        jul = ele.DistanceTravelled;
                    }
                    if (parseInt(ele.Month) == 8) {
                        aug = ele.DistanceTravelled;
                    }
                    if (parseInt(ele.Month) == 9) {
                        spt = ele.DistanceTravelled;
                    }
                    if (parseInt(ele.Month) == 10) {
                        oct = ele.DistanceTravelled;
                    }
                    if (parseInt(ele.Month) == 11) {
                        nov = ele.DistanceTravelled;
                    }
                    if (parseInt(ele.Month) == 12) {
                        dec = ele.DistanceTravelled;
                    }

                })
            }

            vehicleLogChart.updateOptions({
                series: [{
                    name: 'Month',
                    data: [jan, feb, mar, apr, may, jun, jul, aug, spt, oct, nov, dec],
                }],
                title: {
                    text: this.userName,
                    align: 'center',
                },
            });

        },

        generateGraph(data) {

            this.isLoading = false

            var options = {
                series: [{
                    name: 'Month',
                    data: [2.3, 3.1, 4.0, 10.1, 4.0, 3.6, 3.2, 2.3, 1.4, 0.8, 0.5, 0.2]
                }],
                chart: {
                    height: 350,
                    type: 'bar',
                },
                plotOptions: {
                    bar: {
                        borderRadius: 10,
                        dataLabels: {
                            position: 'top', // top, center, bottom
                        },
                    }
                },
                dataLabels: {
                    enabled: true,
                    formatter: function (val) {
                        return val
                    },
                    offsetY: -20,
                    style: {
                        fontSize: '12px',
                        colors: ["#304758"]
                    }
                },

                xaxis: {
                    categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                    position: 'top',
                    axisBorder: {
                        show: false
                    },
                    axisTicks: {
                        show: false
                    },
                    crosshairs: {
                        fill: {
                            type: 'gradient',
                            gradient: {
                                colorFrom: '#D8E3F0',
                                colorTo: '#BED1E6',
                                stops: [0, 100],
                                opacityFrom: 0.4,
                                opacityTo: 0.5,
                            }
                        }
                    },
                    tooltip: {
                        enabled: true,
                    }
                },
                yaxis: {
                    axisBorder: {
                        show: false
                    },
                    axisTicks: {
                        show: false,
                    },
                    labels: {
                        show: false,
                        formatter: function (val) {
                            return val;
                        }
                    }

                },
                title: {
                    text: 'All Vehicles',
                    floating: true,
                    offsetY: 330,
                    align: 'center',
                    style: {
                        color: '#444'
                    }
                }
            };

            vehicleLogChart = new ApexCharts(document.querySelector("#vehicleLogGraph"), options);
            vehicleLogChart.render();
        },
    }
}














//var jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, spt = 0, oct = 0, nov = 0, dec = 0;
//var vehicleLogStartDate = document.getElementById('VehicleLogStartDate').value;
//var vehicleLogEndDate = document.getElementById('VehicleLogEndDate').value;
//var vehicleNo = document.getElementById("VehicleNoSelect").value;
//let currentVehicleLogDataNotFound = false;
////VehicleLog Page load

//var context = document.querySelector('#VehicleLogGraph').getContext('2d');
//var VehicleLogChart = new Chart(context, {
//    type: 'bar',
//    data: {
//        labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'July', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'],
//        datasets: [{
//            data: [],
//            label: 'Distance',
//            backgroundColor: '#00e676',
//            borderWidth: 0.1,
//            borderColor: '#ddd'
//        }]
//    },
//    options: {
//        title: {
//            display: true,
//            position: 'top',
//            text: vehicleNo,
//            fontSize: 18
//        },
//        legend: {
//            display: true,
//            position: 'top',

//        },
//        plugins: {
//            datalabels: {
//                color: '#000'

//            }
//        }
//    },

//});
//var vehicleLogStartDate = document.getElementById('VehicleLogStartDate').value;
//var vehicleLogEndDate = document.getElementById('VehicleLogEndDate').value;
//var vehicleNo = document.getElementById("VehicleNoSelect").value;
//$.ajax({
//    type: "GET",
//    url: '/Chart/RetrieveVehicleLogChartDataById',
//    data: { 'vehicleNo': vehicleNo, 'startDate': vehicleLogStartDate, 'endDate': vehicleLogEndDate },
//    success: function (data) {
//        if (data.length == 0) {
//            currentVehicleLogDataNotFound = true
//            //$('#lblVehicleLogGraphDataNotFound').css('display', 'block');
//            //$('#VehicleLogGraph').css('display', 'none');
//            $('#VehicleLogStartDate').change();
//            vehicleLogUpdate();
//        } else {
//            //$('#lblVehicleLogGraphDataNotFound').css('display', 'none');
//            //$('#VehicleLogGraph').css('display', 'block');
//            currentVehicleLogDataNotFound = false;
//        }
//        for (let i = 0; i < data.length; i++) {
//            if (parseInt(data[i].Month) == 1) {
//                jan = data[i].DistanceTravelled;
//            }
//            if (parseInt(data[i].Month) == 2) {
//                feb = data[i].DistanceTravelled;
//            }
//            if (parseInt(data[i].Month) == 3) {
//                mar = data[i].DistanceTravelled;
//            }
//            if (parseInt(data[i].Month) == 4) {
//                apr = data[i].DistanceTravelled;
//            }
//            if (parseInt(data[i].Month) == 5) {
//                may = data[i].DistanceTravelled;
//            }
//            if (parseInt(data[i].Month) == 6) {
//                jun = data[i].DistanceTravelled;
//            }
//            if (parseInt(data[i].Month) == 7) {
//                jul = data[i].DistanceTravelled;
//            }
//            if (parseInt(data[i].Month) == 8) {
//                aug = data[i].DistanceTravelled;
//            }
//            if (parseInt(data[i].Month) == 9) {
//                spt = data[i].DistanceTravelled;
//            }
//            if (parseInt(data[i].Month) == 10) {
//                oct = data[i].DistanceTravelled;
//            }
//            if (parseInt(data[i].Month) == 11) {
//                nov = data[i].DistanceTravelled;
//            }
//            if (parseInt(data[i].Month) == 12) {
//                dec = data[i].DistanceTravelled;
//            }
//        }
//        VehicleLogChart.options = {
//            title: {
//                display: true,
//                position: 'top',
//                text: vehicleNo,
//                fontSize: 18
//            },
//            legend: {
//                display: true,
//                position: 'top',

//            },
//            plugins: {
//                datalabels: {
//                    color: 'blue',

//                }
//            }
//        }
//        VehicleLogChart.data.datasets[0].data = [jan, feb, mar, apr, may, jun, jul, aug, spt, oct, nov, dec];

//        vehicleLogUpdate();
//    }
//});
////update VehicleLog chart
//vehicleLogUpdate = function VehicleLogChartUpdate() {

//    if (currentVehicleLogDataNotFound) {
//        $(".VehicleDistanceAlert").css("display", "block")
//        let date = new Date(new Date().getFullYear(), 0, 2).toISOString().slice(0, 10);
//        $('#VehicleLogStartDate').val(date);
//    } else {
//        $(".VehicleDistanceAlert").css("display", "none");
//    }
//    var vehicleLogStartDate = document.getElementById('VehicleLogStartDate').value;
//    var vehicleLogEndDate = document.getElementById('VehicleLogEndDate').value;
//    var vehicleNo = document.getElementById("VehicleNoSelect").value;
//    $.ajax({
//        type: "GET",
//        url: '/Chart/RetrieveVehicleLogChartDataById',
//        data: { 'vehicleNo': vehicleNo, 'startDate': vehicleLogStartDate, 'endDate': vehicleLogEndDate },
//        success: function (data) {

//            if (data.length == 0) {
//                //$('#lblVehicleLogGraphDataNotFound').css('display', 'block');
//                $('#VehicleLogGraph').css('display', 'none');
//            } else {
//                //$('#lblVehicleLogGraphDataNotFound').css('display', 'none');
//                $('#VehicleLogGraph').css('display', 'block');
//            }
//            for (let i = 0; i < data.length; i++) {
//                if (parseInt(data[i].Month) == 1) {
//                    jan = data[i].DistanceTravelled;
//                }
//                if (parseInt(data[i].Month) == 2) {
//                    feb = data[i].DistanceTravelled;
//                }
//                if (parseInt(data[i].Month) == 3) {
//                    mar = data[i].DistanceTravelled;
//                }
//                if (parseInt(data[i].Month) == 4) {
//                    apr = data[i].DistanceTravelled;
//                }
//                if (parseInt(data[i].Month) == 5) {
//                    may = data[i].DistanceTravelled;
//                }
//                if (parseInt(data[i].Month) == 6) {
//                    jun = data[i].DistanceTravelled;
//                }
//                if (parseInt(data[i].Month) == 7) {
//                    jul = data[i].DistanceTravelled;
//                }
//                if (parseInt(data[i].Month) == 8) {
//                    aug = data[i].DistanceTravelled;
//                }
//                if (parseInt(data[i].Month) == 9) {
//                    spt = data[i].DistanceTravelled;
//                }
//                if (parseInt(data[i].Month) == 10) {
//                    oct = data[i].DistanceTravelled;
//                }
//                if (parseInt(data[i].Month) == 11) {
//                    nov = data[i].DistanceTravelled;
//                }
//                if (parseInt(data[i].Month) == 12) {
//                    dec = data[i].DistanceTravelled;
//                }
//            }
//            VehicleLogChart.options = {
//                title: {
//                    display: true,
//                    position: 'top',
//                    text: vehicleNo,
//                    fontSize: 18
//                },
//                legend: {
//                    display: true,
//                    position: 'top',

//                },
//                plugins: {
//                    datalabels: {
//                        color: '#000',

//                    }
//                }
//            }
//            VehicleLogChart.data.datasets[0].data = [jan, feb, mar, apr, may, jun, jul, aug, spt, oct, nov, dec];

//            VehicleLogChart.update();
//        }
//    });
//    jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, spt = 0, oct = 0, nov = 0, dec = 0;
//    currentVehicleLogDataNotFound = false;
//}
