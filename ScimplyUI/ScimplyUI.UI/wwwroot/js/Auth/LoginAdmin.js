$(document).ready(function () {
    $("#codeModal").css('display', 'none');

    // toggle-password
    $('#toggle-password-login').on('click', function () {
        var passwordField = $('#password-login');
        var type = passwordField.attr('type') === 'password' ? 'text' : 'password';
        passwordField.attr('type', type);

        var icon = $(this);
        icon.toggleClass('bi-eye bi-eye-slash');
    });

    // login form
    $("#login-form").on('submit', function (event) {
        event.preventDefault();

        const formData = {
            username: $("#UserName").val(),
            password: $("#password-login").val(),
            rememberMe: $("#rememberme").is(":checked")
        };

        $.ajax({
            url: '/auth/login',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                displayMessage(response.message, response.status === 400 ? 'error' : 'success');
                if (response.isTwoFactor) {
                    $("#codeModal").fadeIn();
                    startCountdown();

                    // 2fa form
                    $("#otpForm").on('submit', function (event) {
                        event.preventDefault();

                        const otpData = {
                            code: [
                                $(".otp-input:eq(0)").val(),
                                $(".otp-input:eq(1)").val(),
                                $(".otp-input:eq(2)").val(),
                                $(".otp-input:eq(3)").val()
                            ].join(''),
                            id: response.id
                        };

                        if ($(".otp-input").toArray().some(input => $(input).val() === '')) {
                            displayMessage("Please fill in all fields!", 'error');
                            return;
                        }

                        $.ajax({
                            url: '/auth/twofactor',
                            method: 'POST',
                            contentType: 'application/json',
                            data: JSON.stringify(otpData),
                            success: function (response) {
                                if (response.status === 200) {
                                    window.location.href = '/home/index';
                                } else {
                                    displayMessage(response.message, 'error');
                                }
                            },
                            error: function (e) {
                                displayMessage("An error occurred during OTP verification", 'error');
                            }
                        });
                    });
                } else if (response.status === 200) {
                    setTimeout(function () {
                        window.location.href = '/home/index';
                    }, 1000);
                }
            },
            error: function (e) {
                displayMessage("An error occurred. Please try again", 'error');
            }
        });

        $(".close-button").on('click', function () {
            $("#codeModal").fadeOut();
        });

        // message
        function displayMessage(message, type) {
            var messageElement = type === 'error' ? $('#error-message') : $('#success-message');
            messageElement.removeClass('d-none').text(message);
            setTimeout(function () {
                messageElement.addClass('d-none');
            }, 3000);
        }

        // otp input
        $('.otp-input').on('input', function () {
            const $this = $(this);
            const value = $this.val();

            if (value.length > 1) {
                $this.val(value.charAt(0));
            }

            if (value && $this.next('.otp-input').length) {
                $this.next('.otp-input').focus();
            }
        }).on('keydown', function (e) {
            const $this = $(this);

            if (e.key === 'Backspace' && !$this.val() && $this.prev('.otp-input').length) {
                $this.prev('.otp-input').focus();
            }
        });

    });

    // modal count
    function startCountdown() {
        let duration = 120;
        const timerDisplay = $("#timer-display");

        let interval = setInterval(function () {
            let minutes = Math.floor(duration / 60);
            let seconds = duration % 60;

            seconds = seconds < 10 ? '0' + seconds : seconds;
            timerDisplay.text(`${minutes}:${seconds}`);

            duration--;

            if (duration < 0) {
                clearInterval(interval);
                $("#codeModal").fadeOut();
                
            }
        }, 1000);
    }
});

