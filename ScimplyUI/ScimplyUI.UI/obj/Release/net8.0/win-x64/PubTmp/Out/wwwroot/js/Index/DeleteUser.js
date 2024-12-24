
// DELETE USER
$(document).on('click', '.btn-delete', function () {

    var row = $(this).closest('tr');

    var userId = row.find('td').eq(1).text().trim();

    if (!confirm("are you sure you want to delete this user ? ")) {
        return;
    }

    $.ajax({
        url: '/Admin/DeleteUser',
        type: 'POST',
        data: { id: userId },
        success: function (response) {
            displayMessage(response.message, response.status === false ? 'error' : 'success');
            loadUserList();
        },
        error: function (xhr, status, error) {
            displayMessage("An error occurred. Please try again", 'error');
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