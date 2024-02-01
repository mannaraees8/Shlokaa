var productCategoryTrendChart;
var productCategoryShareChart;
let productCategoryGraph = () => {
    let date = new Date()
    let month = date.getMonth()
    let year = date.getFullYear()
    let oneYearGraphFetched = false;
    let dataNotFoundCount = 0;
    let sTM = {
        jan: 0, feb: 0, mar: 0, apr: 0, may: 0, jun: 0, jul: 0, aug: 0, spt: 0, oct: 0, nov: 0, dec: 0
    }
    return {
        isLoadingProductTrend: true,
        isLoadingProductShare: true,
        startDate: document.querySelector('.productCategory #startDate').value,
        endDate: document.querySelector('.productCategory #endDate').value,
        productCategoryVal: document.getElementById("productCategorySelect").value,
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
            this.startDate = document.querySelector('.productCategory #startDate').value,
                this.endDate = document.querySelector('.productCategory #endDate').value,
                this.productCategoryVal = document.getElementById("productCategorySelect").value;

            generateSRGraph = (data) => {

                this.updateGraph(data)

            }


            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {
                $.ajax({
                    type: "GET",
                    url: '/Chart/RetrieveProductCategoryTrendData',
                    data: { 'productCategory': this.productCategoryVal, 'startDate': this.startDate, 'endDate': this.endDate },
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

            this.isLoadingProductTrend = false
            data.length <= 0 ? this.selectDateRequiredTrend = true : this.selectDateRequiredTrend = false
            if (data) {
                sTM.jan = 0, sTM.feb = 0, sTM.mar = 0, apr = 0, sTM.may = 0, sTM.jun = 0, sTM.jul = 0, sTM.aug = 0, sTM.spt = 0,
                    sTM.oct = 0, sTM.nov = 0, sTM.dec = 0;

                data.forEach(ele => {
                    if (parseInt(ele.Month) == 1) {
                        sTM.jan = ele.SalesAmount;

                    }
                    if (parseInt(ele.Month) == 2) {
                        sTM.feb = ele.SalesAmount;

                    }
                    if (parseInt(ele.Month) == 3) {
                        sTM.mar = ele.SalesAmount;


                    }
                    if (parseInt(ele.Month) == 4) {
                        sTM.apr = ele.SalesAmount;

                    }
                    if (parseInt(ele.Month) == 5) {
                        sTM.may = ele.SalesAmount;

                    }
                    if (parseInt(ele.Month) == 6) {
                        sTM.jun = ele.SalesAmount;

                    }
                    if (parseInt(ele.Month) == 7) {
                        sTM.jul = ele.SalesAmount;

                    }
                    if (parseInt(ele.Month) == 8) {
                        sTM.aug = ele.SalesAmount;

                    }
                    if (parseInt(ele.Month) == 9) {
                        sTM.spt = ele.SalesAmount;

                    }
                    if (parseInt(ele.Month) == 10) {
                        sTM.oct = ele.SalesAmount;

                    }
                    if (parseInt(ele.Month) == 11) {
                        sTM.nov = ele.SalesAmount;

                    }
                    if (parseInt(ele.Month) == 12) {
                        sTM.dec = ele.SalesAmount;

                    }

                })

                productCategoryTrendChart.updateOptions({
                    series: [{
                        name: 'Month',
                        data: [sTM.jan, sTM.feb, sTM.mar, sTM.apr, sTM.may, sTM.jun, sTM.jul, sTM.aug, sTM.spt, sTM.oct, sTM.nov, sTM.dec],
                    }],
                    title: {
                        text: this.productCategoryVal,
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
                    $('#productCategoryTrendGraph').css('min-height', '370px')
                })
            }
        },

        generateGraph(data) {
            this.isLoadingProductTrend = false
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
                    text: 'Product Category Sales Trend',
                    floating: true,
                    offsetY: 330,
                    align: 'center',
                    style: {
                        color: '#444'
                    }
                }
            };

            productCategoryTrendChart = new ApexCharts(document.querySelector('#productCategoryTrendGraph'), options)
            productCategoryTrendChart.render()

        },


        getGraphDataForAllUsersSR() {

            this.startDate = document.querySelector('.productCategory #startDate').value,
                this.endDate = document.querySelector('.productCategory #endDate').value,
                this.productCategoryVal = document.getElementById("productCategorySelect").value,

                generateGraphPR = (data) => {

                    this.updateGraphSR(data)
                }

            setRecToZeroSR = () => { this.BrokenPercentage = 0; this.NetBrokenPercentage = 0 }
            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {

                $.ajax({
                    type: "GET",
                    url: '/Chart/RetrieveProductCategoryShareData',
                    data: { 'productCategory': this.productCategoryVal, 'startDate': this.startDate, 'endDate': this.endDate },
                    success: function (data) {
                        setRecToZeroSR()
                        generateGraphPR(data)

                    }
                })
            }

        },

        updateGraphSR(data) {
            this.isLoadingProductShare = false
            data.length <= 0 ? this.selectDateRequiredSR = true : this.selectDateRequiredSR = false

            var productCategorySalesPercentageStr = [];
            var productCategorySalesPercentageLabels = [];
            for (let i = 0; i < data.length; i++) {
                productCategorySalesPercentageStr.push(data[i].SalesPercentage)
                productCategorySalesPercentageLabels.push(data[i].UserName)
            }

            productCategoryShareChart.updateOptions({
                series: productCategorySalesPercentageStr,
                title: {
                    text: '',
                    align: 'center',
                },
                labels: productCategorySalesPercentageLabels,
                legend: {
                    formatter: function (val, opts) {
                        return val + " - " + opts.w.globals.series[opts.seriesIndex]
                    },

                    position: 'bottom',
                },
            });

        },

        generateGraphSR(data) {
            this.isLoadingProductShare = false
            data.length <= 0 ? this.selectDateRequiredSR = true : this.selectDateRequiredSR = false

            var productCategorySalesPercentageStr = [];
            var productCategorySalesPercentageLabels = [];
            for (let i = 0; i < data.length; i++) {
                productCategorySalesPercentageStr.push(data[i].SalesPercentage)
                productCategorySalesPercentageLabels.push(data[i].UserName)

            } //brokenPercentage == 0 && netBrokenPercentage == 0 ? this.selectDateRequiredSR = true : this.selectDateRequiredSR = false;
            var options = {

                series: productCategorySalesPercentageStr,
                colors: ['#00e676', '#e91e63', '#711A75', '#E45826', '#0E3EDA', '#630606', '#0E185F', '#361500', '#8A39E1', '#D9D7F1'],
                stroke: {
                    width: 0.5,
                },
                chart: {
                    width: 380,
                    type: 'donut',
                },
                labels: productCategorySalesPercentageLabels,
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
                    text: this.productCategoryVal,
                    align: 'center',

                },
            };

            productCategoryShareChart = new ApexCharts(document.querySelector('#productCategoryShareGraph'), options)
            productCategoryShareChart.render()

        },

    }
}