

var xValues = [];
var yValues = [];
var barColors = [
    "#b91d47",
    "#00aba9",
    "#2b5797",
    "#e8c3b9",
    "#1e7145"
];


$.ajax({
    type: "POST",
    url: "/Chart/GetUserCharts",
    success: function (response) {

        $('#total-users').text(response.totalUsers);
        $('#active-users').text(response.activeUsers);
        $('#inactive-users').text(response.inactiveUsers);

        if (response && response.location && response.location.length > 0) {

            xValues = response.location.map(item => item.location);
            yValues = response.location.map(item => item.count);


            new Chart("pie-chart", {
                type: "pie",
                data: {
                    labels: xValues,
                    datasets: [{
                        backgroundColor: barColors,
                        data: yValues
                    }]
                },
                options: {
                    title: {
                        display: true,
                        text: "NUMBER OF USERS FROM LOCATIONS"
                    }
                }
            });
        } else {

        }
    },
    error: function (error) {

    }
});




