$(document).on('click', '.btn-edit', function () {

    const row = $(this).closest('tr');

    const statusText = row.find('td').eq(0).text().trim();
    const status = statusText === "True";
    const id = row.find('td').eq(1).text().trim();
    const resourceType = row.find('td').eq(2).text().trim();
    const userName = row.find('.username-column').data('fulltext');
    const version = row.find('td').eq(7).text().trim();
    const location = row.find('td').eq(8).data('fulltext');

    $('#inputStatusDropdown').val(status ? "true" : "false");
    $('#updateId').val(id);
    $('#updateUsername').val(userName);
    $('#updateResourceType').val(resourceType);
    $('#updateVersion').val(version);
    $('#updateLocation').val(location);

    $('#updateModal').fadeIn();
});


        $('#updateSubmit').on('click', function () {
            const updatedUser = {
                id: $('#updateId').val(),
                userName: $('#updateUsername').val(),
                resourceType: $('#updateResourceType').val(),
                version: $('#updateVersion').val(),
                location: $('#updateLocation').val(),
                status: $('#inputStatusDropdown').val() === "true"
            };
            if (confirm("Are you sure you want to update the user ? ")) {
                $.ajax({
                    url: '/Admin/UpdateUser',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(updatedUser),
                    success: function (response) {
                        displayMessage(response.message, response.status === 400 ? 'error' : 'success');
                        $('#updateModal').fadeOut();
                        loadUserList();
                    },
                    error: function (xhr, status, error) {
                        displayMessage("An error occurred. Please, try again", 'error');
                    }
                });
            }

            function displayMessage(message, type) {
                var messageElement = type === 'error' ? $('#error-message') : $('#success-message');
                messageElement.removeClass('d-none').text(message);
                setTimeout(function () {
                    messageElement.addClass('d-none');
                }, 3000);
            }
        });
    
        

$(window).on('click', function (e) {
    if ($(e.target).hasClass('modal')) {
        $('#updateModal').fadeOut();
    }
});
