var salesTrendChart;
var salesReturnTrendChart;
let salesTrendGraph = () => {
    let sTM = {
        jan: 0, feb: 0, mar: 0, apr: 0, may: 0, jun: 0, jul: 0, aug: 0, spt: 0, oct: 0, nov: 0, dec: 0
    }
    let sTRM = {
        jan: 0, feb: 0, mar: 0, apr: 0, may: 0, jun: 0, jul: 0, aug: 0, spt: 0, oct: 0, nov: 0, dec: 0
    }

    return {
        isLoading: true,
        isLoadingSR: true,
        startDate: document.querySelector('.salesTrend #startDate').value,
        endDate: document.querySelector('.salesTrend #endDate').value,
        userId: document.getElementById("salesTrendUserSelect").value,
        userName: document.getElementById('salesTrendUserSelect').options[document.getElementById('salesTrendUserSelect').selectedIndex].text,
        selectDateRequiredSalesTrend: false,
        selectDateRequiredSalesTrendReturn: false,

        init() {
            this.generateGraph(data = [])
            this.getGraphDataForAllUsers()
            this.generateGraphSR(data = [])
            this.getGraphDataForAllUsersSR()
            
        },

        getGraphDataForAllUsers() {
         
            this.startDate = document.querySelector('.salesTrend #startDate').value,
                this.endDate = document.querySelector('.salesTrend #endDate').value,
                this.userName = document.getElementById('salesTrendUserSelect').options[document.getElementById('salesTrendUserSelect').selectedIndex].text,
                this.userId = document.getElementById("salesTrendUserSelect").value;

            generateSTGraph = (data) => {
            
                this.updateGraphST(data)

            }

            setDataNotFound = () => {
                this.dataNotFound = true;
            }
            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {
                $.ajax({
                    type: "GET",
                    url: '/Chart/RetrieveSalesTrendData',
                    data: { 'id': this.userId, 'startDate': this.startDate, 'endDate': this.endDate },
                    success: function (data) {
                      
                        if (data.length <= 0) {
                            generateSTGraph(newdata = [])
                        }
                        else {
                            generateSTGraph(data)
                        }
                    }
                })
            }

        },

        updateGraphST(salesTrend) {
           
            this.isLoading = false
            salesTrend.length <= 0 ? this.selectDateRequiredSalesTrend = true : this.selectDateRequiredSalesTrend = false
           
            if (salesTrend) {
                sTM.jan = 0, sTM.feb = 0, sTM.mar = 0, apr = 0, sTM.may = 0, sTM.jun = 0, sTM.jul = 0, sTM.aug = 0, sTM.spt = 0,
                    sTM.oct = 0, sTM.nov = 0, sTM.dec = 0;

                salesTrend.forEach(ele => {
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

                salesTrendChart.updateOptions({
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
            }
        },

        generateGraph(data) {
            this.isLoading = false
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
                    text: 'Sales Trend',
                    floating: true,
                    offsetY: 330,
                    align: 'center',
                    style: {
                        color: '#444'
                    }
                }
            };

            salesTrendChart = new ApexCharts(document.querySelector('#salesTrendGraph'), options)
            salesTrendChart.render()
        },


        getGraphDataForAllUsersSR() {

            this.startDate = document.querySelector('.salesTrend #startDate').value,
                this.endDate = document.querySelector('.salesTrend #endDate').value,
                this.userName = document.getElementById('salesTrendUserSelect').options[document.getElementById('salesTrendUserSelect').selectedIndex].text,
                this.userId = document.getElementById("salesTrendUserSelect").value;

            generateGraphSalesReturn = (data) => {
                this.updateGraphSalesReturn(data)
            }

            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {

                $.ajax({
                    type: "GET",
                    url: '/Chart/RetrieveSalesReturnTrendData',
                    data: { 'id': this.userId, 'startDate': this.startDate, 'endDate': this.endDate },
                    success: function (data) {
                        if (data.length <= 0) {
                            generateGraphSalesReturn(data = [])
                        }
                        else {
                            generateGraphSalesReturn(data)
                        }
                    }
                })
            }

        },

        updateGraphSalesReturn(data) {
            this.isLoadingSR = false
            data.length <= 0 ? this.selectDateRequiredSalesTrendReturn = true : this.selectDateRequiredSalesTrendReturn = false

            if (data) {
                sTRM.jan = 0, sTRM.feb = 0, sTRM.mar = 0, sTRM.apr = 0, sTRM.may = 0, sTRM.jun = 0, sTRM.jul = 0, sTRM.aug = 0, sTRM.spt = 0,
                    sTRM.oct = 0, sTRM.nov = 0, sTRM.dec = 0;

                data.forEach(ele => {
                    if (parseInt(ele.Month) == 1) {
                        sTRM.jan = ele.SalesAmount;
                    }
                    if (parseInt(ele.Month) == 2) {
                        sTRM.feb = ele.SalesAmount;
                    }
                    if (parseInt(ele.Month) == 3) {
                        sTRM.mar = ele.SalesAmount;
                    }
                    if (parseInt(ele.Month) == 4) {
                        sTRM.apr = ele.SalesAmount;
                    }
                    if (parseInt(ele.Month) == 5) {
                        sTRM.may = ele.SalesAmount;
                    }
                    if (parseInt(ele.Month) == 6) {
                        sTRM.jun = ele.SalesAmount;
                    }
                    if (parseInt(ele.Month) == 7) {
                        sTRM.jul = ele.SalesAmount;
                    }
                    if (parseInt(ele.Month) == 8) {
                        sTRM.aug = ele.SalesAmount;
                    }
                    if (parseInt(ele.Month) == 9) {
                        sTRM.spt = ele.SalesAmount;
                    }
                    if (parseInt(ele.Month) == 10) {
                        sTRM.oct = ele.SalesAmount;
                    }
                    if (parseInt(ele.Month) == 11) {
                        sTRM.nov = ele.SalesAmount;
                    }
                    if (parseInt(ele.Month) == 12) {
                        sTRM.dec = ele.SalesAmount;
                    }

                })
          

                salesReturnTrendChart.updateOptions({
                    series: [{
                        name: 'Month',
                        data: [sTRM.jan, sTRM.feb, sTRM.mar, sTRM.apr, sTRM.may, sTRM.jun, sTRM.jul, sTRM.aug, sTRM.spt, sTRM.oct, sTRM.nov, sTRM.dec],
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
            }

        },

        generateGraphSR(data) {
            this.isLoadingSR = false
            var options = {
                series: [{
                    name: 'Month',
                    data: [sTRM.jan, sTRM.feb, sTRM.mar, sTRM.apr, sTRM.may, sTRM.jun, sTRM.jul, sTRM.aug, sTRM.spt, sTRM.oct, sTRM.nov, sTRM.dec]
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
                    text: 'Sales Trend',
                    floating: true,
                    offsetY: 330,
                    align: 'center',
                    style: {
                        color: '#444'
                    }
                }
            };
            salesReturnTrendChart = new ApexCharts(document.querySelector('#salesReturnTrendGraph'), options)
            salesReturnTrendChart.render()
        },

    }
}