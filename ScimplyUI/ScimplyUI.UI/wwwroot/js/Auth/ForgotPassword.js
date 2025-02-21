$(document).ready(function () {
    

    $('#submitBtn').click(function (e) {
        e.preventDefault();

        const email = $('#email').val();

        if (email.trim() === "") {
            displayMessage("Please enter your email address", 'error');
        } else {
            $.ajax({
                url: '/auth/forgotpassword',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ email: email }),
                success: function (response) {
                    displayMessage(response.message, response.status === 400 ? 'error' : 'success');
                },
                error: function (error) {
                    displayMessage("An error occurred. Please try again", 'error');
                }
            });
        }

    });

    function displayMessage(message, type) {
        var messageElement = type === 'error' ? $('#error-message') : $('#success-message');
        messageElement.removeClass('d-none').text(message);
        setTimeout(function () {
            messageElement.addClass('d-none');
        }, 3000);
    }

});

