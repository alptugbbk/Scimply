

$(document).ready(function () {
	$('#btn-add-user').on('click', function () {
		$('#createModal').fadeIn();
	});


	$(window).on('click', function (e) {
		if ($(e.target).hasClass('modal')) {
			$('#createModal').fadeOut();
		}
	});

	$('#createUserBtn').on('click', function () {

		var user = {

			UserName: $('#inputUsername').val(),
			Password: $('#inputPassword').val(),
			ResourceType: $('#inputResourceType').val(),
			Version: $('#inputVersion').val(),
			Schemas: $('#inputSchemasDropdown').val()
		};


		if (validateForm(user)) {

			$.ajax({
				url: '/Admin/CreateUser',
				type: 'POST',
				contentType: 'application/json',
				data: JSON.stringify(user),
				success: function (response) {
					displayMessage(response.message, response.status === 400 ? 'error' : 'success');
					loadUserList();
					
				},
				error: function () {
					displayMessage("An error occurred. Please try again", 'error');
				}
			});
		} else {
			displayMessage("Please, fill in all fields", 'error');
		}
	});


	function displayMessage(message, type) {
		var messageElement = type === 'error' ? $('#error-message') : $('#success-message');
		messageElement.removeClass('d-none').text(message);
		setTimeout(function () {
			messageElement.addClass('d-none');
		}, 3000);
	}

	function formatDate(dateStr) {
		if (dateStr) {
			var date = new Date(dateStr);
			return date.toISOString();	
		}
		return null;
	}


	function validateForm(user) {
		return user.Password && user.UserName && user.ResourceType && user.Version;
	}

});