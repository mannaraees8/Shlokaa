
let salesOrderGraph = () => {
    var objects = {};
    let date = new Date()
    let month = date.getMonth()
    let year = date.getFullYear()
    let oneYearGraphFetch = true;
    let dataNotFoundCount = 0
    return {
        userList: [],
        isLoading: false,
        startDate: document.querySelector('.salesOder #startDate').value,
        endDate: document.querySelector('.salesOder #endDate').value,
        dataNotFound: true,
        selectDateRequired: false,
        tallyEntyrPending: 0,

        init() {



        },

        getuserList() {
            setUserList = (data) => {
                this.userList = [...data]
                this.getGraphDataForAllUsers()
            }
            $.ajax({
                type: "GET",
                url: '/Chart/TallyChartUserIdList',
                success: function (data) {
                    setUserList(data)

                }
            })
        },

        getTallyEntryPendingCount() {

            setTallyEntryPendingCount = (data) => {
                this.tallyEntyrPending = data
            }
            $.ajax({
                type: "GET",
                url: '/Chart/TallyPendingCount',
                success: function (data) {
                    setTallyEntryPendingCount(data)
                }
            });
        },

        getGraphDataForAllUsers() {
            dataNotFoundCount++;
            if (dataNotFoundCount == 3) {
                this.dataNotFound = false;
            }
            let salesOrderData = []

            this.startDate = document.querySelector('.salesOder #startDate').value,
                this.endDate = document.querySelector('.salesOder #endDate').value,

                setGraph = (data) => {
                    console.log(data, 'salesOrderData')
                }

            setSalesOrderData = (data, id, name) => {

                data.forEach(ele => {
                    salesOrderData.push({
                        Id: id,
                        StaffName: name,
                        Achieved: ele.Achieved,
                        Pending: ele.Pending

                    })
                })
                this.generateGraph(salesOrderData)
            }
            callOneYearData = () => {
                this.getGraphDataForAllUsers()
            }

            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {
                this.selectDateRequired = false
                let userDataLength = this.userList.length;
                let startDateDiv = $('.salesOder #startDate')
                let endDateDiv = $('.salesOder #endDate')
                let userLoop = 0
                for (let i = 0; i < this.userList.length; i++) {
                    let id = this.userList[i].StaffId
                    let name = this.userList[i].StaffName
                    xhr = $.ajax({
                        type: "GET",
                        url: '/Chart/TallyChart',
                        data: { 'userId': id, 'startDate': this.startDate, 'endDate': this.endDate },
                        success: function (data) {
                            if (data.length <= 0) {
                                userLoop++;
                                if (userLoop == userDataLength) {
                                    if (oneYearGraphFetch) {
                                        oneYearGraphFetch = false;
                                        startDateDiv.val(new Date(new Date().getFullYear(), 0, 2).toISOString().slice(0, 10));
                                        endDateDiv.val(new Date(year, month + 1).toISOString().slice(0, 10))
                                        callOneYearData()
                                    }
                                }
                            } else {
                                setSalesOrderData(data, id, name)
                            }

                        }
                    })
                }


            }
            else {

                let jsEndDate = new Date(year, month + 1).toISOString().slice(0, 10);
                document.querySelector('.salesOder #endDate').value = jsEndDate;
                this.getGraphDataForAllUsers()
            }
        },
        generateGraph(data) {

            this.userList.forEach(ele => {

                if (objects[ele.StaffId]?.name) {
                    objects[ele.StaffId].name.destroy()
                }
            })

            if (data) {
                data.forEach(ele => {


                    var options = {

                        series: [ele.Achieved, ele.Pending],
                        colors: ['#00e676', '#e91e63'],
                        stroke: {
                            width: 0.5,
                        },
                        annotations: {
                            padding: {
                                left: 0,
                                right: 0,
                                top: 0,
                                bottom: 0,
                            }
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
                            formatter: (value, context) => {
                                let sum = parseInt(ele.Achieved) + parseInt(ele.Pending);

                                let percentage = (value * sum / sum).toFixed(2) + "%";
                                return percentage;

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
                            text: ele.StaffName,
                            align: 'center',

                        },
                    };

                    if (document.querySelector('#salesOrderGrap_' + ele.Id)) {
                        objects[ele.Id] = {
                            name: new ApexCharts(document.querySelector('#salesOrderGrap_' + ele.Id), options)
                        };

                        objects[ele.Id].name.render();
                    }

                })


            }

        }
    }
}

