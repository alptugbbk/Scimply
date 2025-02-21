$(document).ready(function () {
    $("#reset-password-form").on('submit', function (event) {
        event.preventDefault();

        const userid = $("#userId").val();
        const newpassword = $("#newPassword").val();
        const confirmpassword = $("#confirmPassword").val();

        if (newpassword !== confirmpassword) {
            displayMessage("Please check your password again", 'error');
            return;
        }

        var forgotPasswordModel = {
            UserId: userid,
            NewPassword: newpassword
        };


        $.ajax({
            url: '/auth/resetpassword',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(forgotPasswordModel),
            success: function (response) {
                displayMessage(response.message, response.status === 400 ? 'error' : 'success');
                if (response.status === 200) {
                    setTimeout(function () {
                        window.location.href = '/auth/login';
                    }, 1000);
                }
            },
            error: function (xhr) {
                displayMessage("An error occurred. Please try again", 'error');
            }
        });
    });

    function displayMessage(message, type) {
        var messageElement = type === 'error' ? $('#error-message') : $('#success-message');
        messageElement.removeClass('d-none').text(message);
        setTimeout(function () {
            messageElement.addClass('d-none');
        }, 3000);
    }

    // Toggle password
    $('#toggle-password-reset').on('click', function () {
        var passwordField = $('#newPassword');

        var type = passwordField.attr('type') === 'password' ? 'text' : 'password';
        passwordField.attr('type', type);

        var icon = $(this);
        icon.toggleClass('bi-eye bi-eye-slash');
    });


});


