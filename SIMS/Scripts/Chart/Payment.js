

var paymentChart;
let paymentGraphData = () => {
    let date = new Date()
    let month = date.getMonth()
    let year = date.getFullYear()
    let oneYearGraphFetched = false;

    return {
        isLoading: true,
        startDate: document.querySelector('.payment #startDate').value,
        endDate: document.querySelector('.payment #endDate').value,
        userId: document.getElementById("paymentUserSelect").value,
        userName: document.getElementById('paymentUserSelect').options[document.getElementById('paymentUserSelect').selectedIndex].text,
        dataNotFound: false,
        selectDateRequired: true,
        rec: 0,
        notrec: 0,

        init() {
            this.getGraphDataForAllUsers()
            this.generateGraph(data = [])
        },

        getGraphDataForAllUsers() {
            this.dataNotFound = false
            this.startDate = document.querySelector('.payment #startDate').value,
                this.endDate = document.querySelector('.payment #endDate').value,
                this.userName = document.getElementById('paymentUserSelect').options[document.getElementById('paymentUserSelect').selectedIndex].text,
                this.userId = document.getElementById("paymentUserSelect").value;


            generateGraph = (data) => {
                this.isLoading = false
                if (data)
                    this.updateGraph(data)
            }

            setDataNotFound = () => {
                this.dataNotFound = true;
            }
            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {
                let startDateDiv = $('.payment #startDate')
                let endDateDiv = $('.payment #endDate')
                let flag = false;
                $.ajax({
                    type: "GET",
                    url: '/Chart/RetrievePaymentChartDataById',
                    data: { 'id': this.userId, 'startDate': this.startDate, 'endDate': this.endDate },
                    success: function (data) {

                        data = data?.slice(0, 4);
                        data?.forEach(ele => {
                            if (ele == 0) {
                                flag = true;
                            }
                            else {
                                flag = false;
                            }

                        })
                        if (flag) {
                            if (!oneYearGraphFetched) {
                                oneYearGraphFetched = true
                                setDataNotFound()
                                startDateDiv.val(new Date(new Date().getFullYear(), 0, 2).toISOString().slice(0, 10));
                                endDateDiv.val(new Date(year, month + 1).toISOString().slice(0, 10))
                                startDateDiv.change()
                                endDateDiv.change()
                                generateGraph(data)
                            }
                            generateGraph(data)
                        }
                        else {
                            generateGraph(data)
                        }

                    }

                })
            }

        },

        updateGraph(data) {
            if (data) {
                data?.forEach(ele => {
                    ele == 0 ? this.selectDateRequired = true : this.selectDateRequired = false
                })
                paymentChart.updateOptions({
                    series: data,
                    title: {
                        text: this.userName,
                        align: 'center',
                    },
                });
            }
        },

        generateGraph(data) {
            this.isLoading = false
            data?.forEach(ele => {
                ele == 0 ? this.selectDateRequired = true : this.selectDateRequired = false
            })
            var options = {

                series: [0, 0, 0, 0],
                colors: ['#00e676', '#ffc107', '#ff5722', '#e91e63', '#ffd600'],
                stroke: {
                    width: 0.5,
                },
                chart: {
                    width: 380,
                    type: 'donut',
                },
                labels: ['Cash', 'Cheque', 'NEFT', 'No Payment'],
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

            paymentChart = new ApexCharts(document.querySelector('#paymentGraph'), options)
            paymentChart.render()
        },
    }
}



