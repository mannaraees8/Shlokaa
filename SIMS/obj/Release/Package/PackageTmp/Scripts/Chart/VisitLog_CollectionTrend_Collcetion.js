var collectionTrendChart;
var collectionChart;
let visitLogGraph = () => {
    let date = new Date()
    let month = date.getMonth()
    let year = date.getFullYear()
    let oneYearGraphFetched = false;
    let dataNotFoundCount = 0;
    let sTM = {
        jan: 0, feb: 0, mar: 0, apr: 0, may: 0, jun: 0, jul: 0, aug: 0, spt: 0, oct: 0, nov: 0, dec: 0
    }
    return {
        isLoadingCollectionTrend: true,
        isLoadingCollection: true,
        startDate: document.querySelector('.visitLog #startDate').value,
        endDate: document.querySelector('.visitLog #endDate').value,
        userId: document.getElementById("userSelect").value,
        userName: document.getElementById('userSelect').options[document.getElementById('userSelect').selectedIndex].text,
        dataNotFound: true,
        selectDateRequiredTrend: false,
        selectDateRequiredSR: false,
        achieved: 0,
        pending: 0,
        BrokenPercentage: 0,
        NetBrokenPercentage: 0,

        init() {
            this.generateGraph(data = [])
            this.getGraphDataForAllUsers()
            this.generateGraphSR(data = [])
            this.getGraphDataForAllUsersSR()

        },

        getGraphDataForAllUsers() {
            this.getGraphDataForAllUsersSR()
            this.startDate = document.querySelector('.visitLog #startDate').value,
                this.endDate = document.querySelector('.visitLog #endDate').value,
                this.userId = document.getElementById("userSelect").value;
            this.userName = document.getElementById('userSelect').options[document.getElementById('userSelect').selectedIndex].text,

                generateSRGraph = (data) => {

                    this.updateGraph(data)

                }


            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {
                $.ajax({
                    type: "GET",
                    url: '/Chart/RetrieveCollectionTrend',
                    data: { 'id': this.userId, 'startDate': this.startDate, 'endDate': this.endDate },
                    success: function (data) {

                        if (data.length <= 0) {

                            generateSRGraph(newdata = [])
                        }
                        else {
                            generateSRGraph(data)
                        }
                    }
                })
            }

        },

        updateGraph(data) {

            this.isLoadingCollectionTrend = false
            data.length <= 0 ? this.selectDateRequiredTrend = true : this.selectDateRequiredTrend = false
            if (data) {
                sTM.jan = 0, sTM.feb = 0, sTM.mar = 0, apr = 0, sTM.may = 0, sTM.jun = 0, sTM.jul = 0, sTM.aug = 0, sTM.spt = 0,
                    sTM.oct = 0, sTM.nov = 0, sTM.dec = 0;

                data.forEach(ele => {
                    if (parseInt(ele.Month) == 1) {
                        sTM.jan = ele.Amount;

                    }
                    if (parseInt(ele.Month) == 2) {
                        sTM.feb = ele.Amount;

                    }
                    if (parseInt(ele.Month) == 3) {
                        sTM.mar = ele.Amount;


                    }
                    if (parseInt(ele.Month) == 4) {
                        sTM.apr = ele.Amount;

                    }
                    if (parseInt(ele.Month) == 5) {
                        sTM.may = ele.Amount;

                    }
                    if (parseInt(ele.Month) == 6) {
                        sTM.jun = ele.Amount;

                    }
                    if (parseInt(ele.Month) == 7) {
                        sTM.jul = ele.Amount;

                    }
                    if (parseInt(ele.Month) == 8) {
                        sTM.aug = ele.Amount;

                    }
                    if (parseInt(ele.Month) == 9) {
                        sTM.spt = ele.Amount;

                    }
                    if (parseInt(ele.Month) == 10) {
                        sTM.oct = ele.Amount;

                    }
                    if (parseInt(ele.Month) == 11) {
                        sTM.nov = ele.Amount;

                    }
                    if (parseInt(ele.Month) == 12) {
                        sTM.dec = ele.Amount;

                    }

                })

                collectionTrendChart.updateOptions({
                    series: [{
                        name: 'Month',
                        data: [sTM.jan, sTM.feb, sTM.mar, sTM.apr, sTM.may, sTM.jun, sTM.jul, sTM.aug, sTM.spt, sTM.oct, sTM.nov, sTM.dec],
                    }],
                    title: {
                        text: this.userName,
                        align: 'center',
                    },
                    legend: {
                        formatter: function (val, opts) {
                            return val + " - " + opts.w.globals.series[opts.seriesIndex]
                        },

                        position: 'bottom',
                    },
                });

                $(async function () {
                    await new Promise(resolve => setTimeout(resolve, 200));
                    $('#visitLogCollectionTrendGraph').css('min-height', '370px')
                })
            }
        },

        generateGraph(data) {
            this.isLoadingCollectionTrend = false
            data.length <= 0 ? this.selectDateRequiredTrend = true : this.selectDateRequiredTrend = false
            var options = {
                series: [{
                    name: 'Month',
                    data: [sTM.jan, sTM.feb, sTM.mar, sTM.apr, sTM.may, sTM.jun, sTM.jul, sTM.aug, sTM.spt, sTM.oct, sTM.nov, sTM.dec]
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
                    text: 'Collection Trend',
                    floating: true,
                    offsetY: 330,
                    align: 'center',
                    style: {
                        color: '#444'
                    }
                }
            };

            collectionTrendChart = new ApexCharts(document.querySelector('#visitLogCollectionTrendGraph'), options)
            collectionTrendChart.render()
        },


        getGraphDataForAllUsersSR() {
            this.startDate = document.querySelector('.visitLog #startDate').value,
            this.endDate = document.querySelector('.visitLog #endDate').value,
            this.userId = document.getElementById("userSelect").value;
            this.userName = document.getElementById('userSelect').options[document.getElementById('userSelect').selectedIndex].text,

                generateGraphPR = (data) => {

                    this.updateGraphSR(data)
                }

            setRecToZeroSR = () => { this.BrokenPercentage = 0; this.NetBrokenPercentage = 0 }
            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {
                this.selectDateRequiredSR = false

                $.ajax({
                    type: "GET",
                    url: '/Chart/RetrieveCollection',
                    data: { 'id': this.userId, 'startDate': this.startDate, 'endDate': this.endDate },
                    success: function (data) {
                        setRecToZeroSR()
                        if (data.length <= 0) {

                            generateGraphPR(data)
                        }
                        else {
                            generateGraphPR(data)
                        }
                    }
                })
            }

        },

        updateGraphSR(data) {
            this.isLoadingCollection = false
            data.length <= 0 ? this.selectDateRequiredSR = true : this.selectDateRequiredSR = false
            console.log(data)
            var collection = 0;
            if (data.length > 0) {
                collection = data[0]?.Collection;

            } else {
                collection = 0;
            }
            collection = Number(collection).toFixed(0)
            var options = {
                width: 400, height: 220,
                redFrom: 90, redTo: 100,
                yellowFrom: 75, yellowTo: 90,
                minorTicks: 5
            };
            var data1= google.visualization.arrayToDataTable([
                ['Label', 'Value'],
                ['Collection',Number(collection)],
            ]);

            collectionChart.draw(data1, options);

        },

        generateGraphSR(data) {
            this.isLoadingCollection = false
            data.length <= 0 ? this.selectDateRequiredSR = true : this.selectDateRequiredSR = false
            google.charts.load('current', { 'packages': ['gauge'] });
            google.charts.setOnLoadCallback(drawChart);
            function drawChart() {

                var data = google.visualization.arrayToDataTable([
                    ['Label', 'Value'],
                    ['Sales', 0],
                ]);

                var options = {
                    width: 400, height: 120,
                    redFrom: 90, redTo: 100,
                    yellowFrom: 75, yellowTo: 90,
                    minorTicks: 5
                };

                collectionChart = new google.visualization.Gauge(document.getElementById('visitLogCollectionGraph'));

                collectionChart.draw(data, options);

            }
        },

    }
}