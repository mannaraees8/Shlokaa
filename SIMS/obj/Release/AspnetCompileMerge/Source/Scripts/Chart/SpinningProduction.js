var spinningProductionChart;
var spinningRejectionChart;
let spinningProductionGraph = () => {
    let date = new Date()
    let month = date.getMonth()
    let year = date.getFullYear()
    let oneYearGraphFetched = false;
    let dataNotFoundCount = 0;

    return {
        isLoading: true,
        isLoadingSR: true,
        startDate: document.querySelector('.spinningProduction #startDate').value,
        endDate: document.querySelector('.spinningProduction #endDate').value,
        userId: document.getElementById("spinningProductionUserSelect").value,
        userName: document.getElementById('spinningProductionUserSelect').options[document.getElementById('spinningProductionUserSelect').selectedIndex].text,
        dataNotFound: true,
        selectDateRequired: false,
        selectDateRequiredSR: false,
        achieved: 0,
        pending: 0,
        BrokenPercentage: 0,
        NetBrokenPercentage: 0,

        init() {
            this.getGraphDataForAllUsers()
            this.generateGraph(this.achieved, this.pending)
            this.getGraphDataForAllUsersSR()
            this.generateGraphSR(this.BrokenPercentage, this.NetBrokenPercentage)
        },

        getGraphDataForAllUsers() {
            this.getGraphDataForAllUsersSR()
            dataNotFoundCount++;
            if (dataNotFoundCount == 2) {
                this.dataNotFound = false;
            }
            this.startDate = document.querySelector('.spinningProduction #startDate').value,
                this.endDate = document.querySelector('.spinningProduction #endDate').value,
                this.userName = document.getElementById('spinningProductionUserSelect').options[document.getElementById('spinningProductionUserSelect').selectedIndex].text,
                this.userId = document.getElementById("spinningProductionUserSelect").value;

            generateGraph = (achieved, pending) => {
                this.updateGraph(achieved, pending)
            }
            //updateRec = (data) => {
            //    this.achieved = data[0].Achieved,
            //    this.pending = data[0].pending

            //}
            setDataNotFound = () => {
                this.dataNotFound = true;
            }
            setRecToZero = () => { this.achieved = 0; this.pending = 0 }
            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {
                this.selectDateRequired = false
                let startDateDiv = $('.spinningProduction #startDate')
                let endDateDiv = $('.spinningProduction #endDate')
                $.ajax({
                    type: "GET",
                    url: '/Chart/RetrieveSpinningProductionChartDataById',
                    data: { 'id': this.userId, 'startDate': this.startDate, 'endDate': this.endDate },
                    success: function (data) {
                        setRecToZero()
                        if (data.length <= 0) {
                            //if (!oneYearGraphFetched) {
                            //    oneYearGraphFetched = true
                            //    setDataNotFound()
                            //    startDateDiv.val(new Date(new Date().getFullYear(), 0, 2).toISOString().slice(0, 10));
                            //    endDateDiv.val(new Date(year, month + 1).toISOString().slice(0, 10))
                            //    startDateDiv.change()
                            //    endDateDiv.change()
                            //}

                            generateGraph(0, 0)
                        }
                        else {

                            this.achieved = data[0].Achieved;
                            this.pending = data[0].Pending;
                            generateGraph(this.achieved, this.pending)
                        }
                    }
                })
            }

        },

        updateGraph(achieved, pending) {
            this.isLoading = false
            achieved == 0 && pending == 0 ? this.selectDateRequired = true : this.selectDateRequired = false;

            if (this.selectDateRequired == true) {
                $('#spinningProductionGraph').fadeOut();
                $('#selectDateRequired').fadeIn();
            } else {
                $('#spinningProductionGraph').fadeIn();
                $('#selectDateRequired').fadeOut();
            }
            spinningProductionChart.updateOptions({
                series: [Number(achieved), Number(pending)],
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

        },

        generateGraph(achieved, pending) {
            this.isLoading = false
            achieved == 0 && pending == 0 ? this.selectDateRequired = true : this.selectDateRequired = false;
            var options = {

                series: [Number(achieved), Number(pending)],
                colors: ['#00e676', '#e91e63'],
                stroke: {
                    width: 0.5,
                },
                chart: {
                    width: 380,
                    type: 'donut',
                },
                labels: ['Achieved', 'Pending'],
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

            spinningProductionChart = new ApexCharts(document.querySelector('#spinningProductionGraph'), options)
            spinningProductionChart.render()
        },


        getGraphDataForAllUsersSR() {

            this.startDate = document.querySelector('.spinningProduction #startDate').value,
                this.endDate = document.querySelector('.spinningProduction #endDate').value,
                this.userName = document.getElementById('spinningProductionUserSelect').options[document.getElementById('spinningProductionUserSelect').selectedIndex].text,
                this.userId = document.getElementById("spinningProductionUserSelect").value;

            generateGraphSR = (brokenPercentage, netBrokenPercentage) => {
                this.updateGraphSR(brokenPercentage, netBrokenPercentage)
            }

            setRecToZeroSR = () => { this.BrokenPercentage = 0; this.NetBrokenPercentage = 0 }
            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {
                this.selectDateRequiredSR = false

                $.ajax({
                    type: "GET",
                    url: '/Chart/RetrieveSpinningRejection',
                    data: { 'id': this.userId, 'startDate': this.startDate, 'endDate': this.endDate },
                    success: function (data) {
                        setRecToZeroSR()
                        if (data.length <= 0) {

                            generateGraphSR(0, 0)
                        }
                        else {
                            this.BrokenPercentage = data[0].BrokenPercentage;
                            this.NetBrokenPercentage = data[0].NetBrokenPercentage;
                            generateGraphSR(this.BrokenPercentage, this.NetBrokenPercentage)
                        }
                    }
                })
            }

        },

        updateGraphSR(brokenPercentage, netBrokenPercentage) {
            this.isLoadingSR = false
            brokenPercentage == 0 && netBrokenPercentage == 0 ? this.selectDateRequiredSR = true : this.selectDateRequiredSR = false;


            if (this.selectDateRequiredSR == true) {
                $('#selectDateRequiredSR').fadeIn();
                $('#spinningRejectionGraph').fadeOut();

            } else {
                $('#selectDateRequiredSR').fadeOut();
                $('#spinningRejectionGraph').fadeIn();
            }

            var broken = Number(brokenPercentage).toFixed(2);
            var netBroken = Number(netBrokenPercentage).toFixed(2);
            spinningRejectionChart.updateOptions({
                series: [Number(broken), Number(netBroken)],
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

        },

        generateGraphSR(brokenPercentage, netBrokenPercentage) {
            this.isLoadingSR = false
            brokenPercentage == 0 && netBrokenPercentage == 0 ? this.selectDateRequiredSR = true : this.selectDateRequiredSR = false;
            var options = {

                series: [brokenPercentage, netBrokenPercentage],
                colors: ['#00e676', '#e91e63'],
                stroke: {
                    width: 0.5,
                },
                chart: {
                    width: 380,
                    type: 'donut',
                },
                labels: ['BrokenPercentage', 'NetBrokenPercentage'],
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

            spinningRejectionChart = new ApexCharts(document.querySelector('#spinningRejectionGraph'), options)
            spinningRejectionChart.render()
        },

















    }
}