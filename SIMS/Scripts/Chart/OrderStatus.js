

var orderStatusChart;
let orderStatusGraph = () => {
    let date = new Date()
    let month = date.getMonth()
    let year = date.getFullYear()
    let oneYearGraphFetched = false;
    let dataNotFoundCount = 0;

    return {
        isLoading: true,
        startDate: document.querySelector('.orderStatus #startDate').value,
        endDate: document.querySelector('.orderStatus #endDate').value,
        userId: document.getElementById("orderStatusUserSelect").value,
        userName: document.getElementById('orderStatusUserSelect').options[document.getElementById('orderStatusUserSelect').selectedIndex].text,
        selectDateRequired: false,
        rec: 0,
        notrec: 0,

        init() {
            this.getGraphDataForAllUsers()
            this.generateGraph()
        },

        getGraphDataForAllUsers() {
            dataNotFoundCount++;
            if (dataNotFoundCount == 2) {
                this.dataNotFound = false;
            }
            this.startDate = document.querySelector('.orderStatus #startDate').value,
                this.endDate = document.querySelector('.orderStatus #endDate').value,
                this.userName = document.getElementById('orderStatusUserSelect').options[document.getElementById('orderStatusUserSelect').selectedIndex].text,
                this.userId = document.getElementById("orderStatusUserSelect").value;

            generateGraph = () => {
                this.updateGraph()
            }
            updateRec = (data) => {
                if (data.Orderstatus == 'Not Received') {

                    this.notrec = data.OrderStatusCount;
                }
                if (data.Orderstatus == 'Received') {
                    this.rec = data.OrderStatusCount;
                }

                if (data.Orderstatus == 'Not Received') {
                    this.notrec = data.OrderStatusCount;
                }
                if (data.Orderstatus == 'Received') {
                    this.rec = data.OrderStatusCount;
                }

            }

            setRecToZero = () => { this.rec = 0; this.notrec = 0 }
            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {
                this.selectDateRequired = false
                $.ajax({
                    type: "GET",
                    url: '/Chart/RetrieveOrderStatusChartDataById',
                    data: { 'id': this.userId, 'startDate': this.startDate, 'endDate': this.endDate },
                    success: function (data) {
                        if (!data || data.length <= 0) {
                            setRecToZero()
                        }
                        if (data[0] != undefined) {
                            updateRec(data[0])
                        }
                        if (data[1] != undefined) {
                            updateRec(data[1])
                        }
                        generateGraph()
                    }
                })
            }

        },

        updateGraph() {
            this.isLoading = false
            this.rec == 0 && this.notrec == 0 ? this.selectDateRequired = true : this.selectDateRequired = false;
    
            orderStatusChart.updateOptions({
                series: [this.rec, this.notrec],
                title: {
                    text: this.userName,
                    align: 'center',
                },
            });

        },

        generateGraph() {
            this.isLoading = false
            this.rec == 0 && this.notrec == 0 ? this.selectDateRequired = true : this.selectDateRequired = false;
            var options = {

                series: [this.rec, this.notrec],
                colors: ['#00e676', '#e91e63'],
                stroke: {
                    width: 0.5,
                },
                chart: {
                    width: 380,
                    type: 'donut',
                },
                labels: ['Received', 'Not Received'],
                plotOptions: {
                    pie: {
                        startAngle: -90,
                        endAngle: 270
                    }
                },
                dataLabels: {
                    enabled: true,
                    style: {
                        colors: ['#000']
                    },
                    dropShadow: {
                        enabled: false,
                    }
                },
                fill: {
                    type: 'gradient',
                },
                legend: {
                    formatter: function (val, opts) {
                        return val + " - " + opts.w.globals.series[opts.seriesIndex]
                    },

                    position: 'bottom',
                },
                title: {
                    text: this.userName,
                    align: 'center',

                },
            };

            orderStatusChart = new ApexCharts(document.querySelector('#orderStatusGraph'), options)
            orderStatusChart.render()
        },
    }
}

