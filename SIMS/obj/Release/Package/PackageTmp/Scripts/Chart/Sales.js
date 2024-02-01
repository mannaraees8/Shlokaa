

var salesChart;
let salesGraphData = () => {

    return {
        isLoading: true,
        startDate: document.querySelector('.sales #startDate').value,
        endDate: document.querySelector('.sales #endDate').value,
        userId: document.getElementById("salesUserSelect").value,
        userName: document.getElementById('salesUserSelect').options[document.getElementById('salesUserSelect').selectedIndex].text,
        selectDateRequired: true,
        rec: 0,
        notrec: 0,

        init() {
            this.getGraphDataForAllUsers()
            this.generateGraph(data = [])
        },

        getGraphDataForAllUsers() {
            this.dataNotFound = false
            this.startDate = document.querySelector('.sales #startDate').value,
                this.endDate = document.querySelector('.sales #endDate').value,
                this.userName = document.getElementById('salesUserSelect').options[document.getElementById('salesUserSelect').selectedIndex].text,
                this.userId = document.getElementById("salesUserSelect").value;


            generateGraph = (data) => {
                this.isLoading = false
                this.updateGraph(data)
            }


            if (new Date(this.startDate).getFullYear() <= new Date(this.endDate).getFullYear()) {
                $.ajax({
                    type: "GET",
                    url: '/Chart/RetrieveSalesData',
                    data: { 'id': this.userId, 'startDate': this.startDate, 'endDate': this.endDate },
                    success: function (data) {
                        generateGraph(data)
                    }

                })
            }

        },

        updateGraph(data) {
            if (data.length > 0) {
                this.selectDateRequired = false;
                var options = {
                    width: 400, height: 220,
                    redFrom: 90, redTo: 100,
                    yellowFrom: 75, yellowTo: 90,
                    minorTicks: 5
                };
                var data = google.visualization.arrayToDataTable([
                    ['Label', 'Value'],
                    ['Sales', data[0].SalesPercentage],
                ]);

                salesChart.draw(data, options);
            }
            else {
                this.selectDateRequired = true;
            }
        },

        generateGraph(data) {
            this.isLoading = false
            data.length <= 0 ? this.selectDateRequired = true : this.selectDateRequired=false

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

                salesChart = new google.visualization.Gauge(document.getElementById('salesGraph'));

                salesChart.draw(data, options);

            }
        },
    }
}



