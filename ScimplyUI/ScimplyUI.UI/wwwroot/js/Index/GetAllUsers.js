// GET USER
function loadUserList() {
	$.ajax({
		url: '/Home/GetAllUsers',
		type: 'POST',
		dataType: 'json',
		success: function (data) {
			var tableBody = $('#tableBody');
			tableBody.empty();

			if (data && Array.isArray(data)) {
				data.forEach(function (user) {
					var id = user.id || 'NULL';
					var resourceType = user.meta ? user.meta.resourceType : 'NULL';
					var userName = user.userName || 'NULL';
					var password = user.password || 'NULL';
					var created = user.meta ? user.meta.created : 'NULL';
					var lastModified = user.meta ? user.meta.lastModified : 'NULL';
					var version = user.meta ? user.meta.version : 'NULL';
					var location = user.meta ? user.meta.location : 'NULL';
					var status = user.status;

					var statusIndicator = status
						? '<span class="status-indicator status-true"></span>'
						: '<span class="status-indicator status-false"></span>';

					var row = `
													<tr>
													    <td>${statusIndicator}</td>
														<td>${id}</td>
														<td>${resourceType}</td>
														<td class="username-column" data-fulltext="${userName}">${userName}</td>
														<td>${created}</td>
														<td>${lastModified}</td>
														<td data-fulltext="${version}">${version}</td>
														<td data-fulltext="${location}">${location}</td>
														<td><button class="btn-edit">Edit</button></td>
														<td><button class="btn-delete">Delete</button></td>
													</tr>`;
					tableBody.append(row);
				});
			} else {
				tableBody.append('<tr><td colspan="10">User not found.</td></tr>');
			}
		},
		error: function (xhr, status, error) {
			console.error('Veri çekme hatası:', error);
			var tableBody = $('#tableBody');
			tableBody.empty();
			tableBody.append('<tr><td colspan="10">Failed to load data</td></tr>');
		}
	});

	// SEARCH
	$('.top-search-input').on('input', function () {
		const searchValue = $(this).val().toLowerCase();

		$('#tableBody tr').each(function () {
			const username = $(this).find('.username-column').text().toLowerCase();
			$(this).toggle(username.includes(searchValue));
		});
	});
}

loadUserList();