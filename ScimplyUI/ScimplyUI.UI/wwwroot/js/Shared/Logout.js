$('#logout').on('click', function () {
    $.ajax({
        url: '/auth/logout',
        method: 'POST',
        success: function (response) {
            window.location.href = response.redirectUrl;
        },
        error: function (e) {

        }
    });
});
