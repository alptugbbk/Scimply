// Toggle password visibility
$(document).ready(function () {
    $('#toggle-password-login').on('click', function () {
        var passwordField = $('#password-login');
        var type = passwordField.attr('type') === 'password' ? 'text' : 'password';
        passwordField.attr('type', type);

        var icon = $(this);
        icon.toggleClass('bi-eye bi-eye-slash');
    });
});


$("#login-form").on('submit', function (event) {
    event.preventDefault();

    const formData = {
        username: $("#UserName").val(),
        password: $("#password-login").val(),
    };

    $.ajax({
        url: '/auth/login',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (response) {

            $('#error-message').addClass('d-none');
            $('#success-message').addClass('d-none');

            if (response.status === false) {

                $('#error-message').removeClass('d-none').text(response.message);
                setTimeout(function () {
                    $('#error-message').addClass('d-none');
                }, 3000);
            } else {
                $('#success-message').removeClass('d-none').text(response.message);
                setTimeout(function () {
                    window.location.href = "/Admin/Index";
                }, 1000);
            }

        },
        error: function (e) {

            $('#success-message').addClass('d-none');

            $('#error-message').removeClass('d-none').text("An error occurred. Please try again");

            setTimeout(function () {
                $('#error-message').addClass('d-none');
            }, 3000);

        }
    });


});

