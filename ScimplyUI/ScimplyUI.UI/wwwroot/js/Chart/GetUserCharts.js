
// location values
var xLocationValues = [];
var yLocationValues = [];
var barLocationColors = [
    "#ff3e3e", // Canlı kırmızı
    "#00d8c2", // Parlak turkuaz
    "#4285f4", // Dikkat çekici mavi
    "#ff6ac1", // Neon pembe
    "#5cb85c", // Parlak yeşil
    "#f39c12", // Altın turuncu
    "#9b59b6", // Canlı mor
    "#e74c3c", // Parlak mercan kırmızısı
    "#34495e", // Modern koyu mavi
    "#1abc9c"  // Ferah açık yeşil
];


// date values
var xDateValues = [];
var yDateValues = [];

$.ajax({
    type: "POST",
    url: "/Chart/GetUserCharts",
    success: function (response) {

        // users count
        $('#total-users').text(response.totalUsers);
        $('#active-users').text(response.activeUsers);
        $('#inactive-users').text(response.inactiveUsers);

        // pie location
        if (response && response.location && response.location.length > 0 ) {

            xLocationValues = response.location.map(item => item.location);
            yLocationValues = response.location.map(item => item.count);


            new Chart("pie-chart", {
                type: "pie",
                data: {
                    labels: xLocationValues,
                    datasets: [{
                        backgroundColor: barLocationColors,
                        data: yLocationValues
                    }]
                },
                options: {
                    title: {
                        display: true,
                        text: "NUMBER OF USERS FROM LOCATIONS"
                    }
                }
            });

        }

        // most common date
        if (response && response.mostCommonDate && response.mostCommonDate.length > 0) {

            xDateValues = response.mostCommonDate.map(item => item.date);
            yDateValues = response.mostCommonDate.map(item => item.count);

            new Chart("line-chart", {
                type: 'line',
                data: {
                    labels: xDateValues, 
                    datasets: [{
                        label: 'USER REGİSTRATİONS BY DATE', 
                        data: yDateValues,
                        borderColor: '#36cfc9',
                        backgroundColor: 'rgba(54, 207, 201, 0.2)',
                        pointBackgroundColor: yDateValues.map((value) => {
                            if (value > 200) return '#ff3e3e';
                            else if (value > 100) return '#4285f4';
                            else if (value > 50) return '#ff6ac1';
                            return 'black';
                        }),
                        pointBorderColor: 'yellow',
                        fill: true,
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            display: true
                        }
                    }
                }
            });
        }

    },
    error: function (error) {

    }
});
