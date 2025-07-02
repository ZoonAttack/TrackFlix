// Handles movie card click -> loads movie partial
$(document).on('click', '.card[data-movie-id]', function () {
    var movieId = $(this).data('movie-id');

    $.ajax({
        url: '/Collection/GetMovieDetails',
        method: 'GET',
        data: { movieId: movieId },
        success: function (result) {
            $('#movie-details-container').html(result);
            $('#movie-details-modal').fadeIn();
        },
        error: function () {
            alert('Error loading movie details');
        }
    });
});

$(document).on('click', '.movie-add-list-btn', function () {
    const movieId = $(this).data('id');

    closeAllModals(); // Ensures all modals are closed first

    setTimeout(() => {
        $.ajax({
            url: '/Collection/AddMovieToList',
            method: 'GET',
            data: { movieId: movieId },
            success: function (result) {
                $('#add-to-list-container').html(result);
                $('#add-to-list-modal').fadeIn();
            },
            error: function () {
                alert('Failed to load list form.');
            }
        });
    }, 300); // Wait a bit before showing new one
});
$(document).on('submit', '#movie-add-to-list-form', function (e) {
    e.preventDefault();

    const form = $(this);
    const formData = form.serialize(); // form-url-encoded

    $.ajax({
        url: '/User/AddMovieToList',
        type: 'POST',
        data: formData,
        success: function () {
            alert('Added successfully!');
            closeAddToListModal(); // if you have this function
        },
        error: function () {
            alert('Failed to add to list.');
        }
    });
});
$(document).on('click', '.show-add-list-btn', function () {
    const showId = $(this).data('id');

    closeAllModals(); // Ensures all modals are closed first

    setTimeout(() => {
        $.ajax({
            url: '/Collection/AddShowToList',
            method: 'GET',
            data: { showId: showId },
            success: function (result) {
                $('#add-to-list-container').html(result);
                $('#add-to-list-modal').fadeIn();
            },
            error: function () {
                alert('Failed to load list form.');
            }
        });
    }, 300); // Wait a bit before showing new one
});

$(document).on('submit', '#show-add-to-list-form', function (e) {
        e.preventDefault();

    const form = $(this);
    const formData = form.serialize(); // form-url-encoded

    $.ajax({
        url: '/User/AddShowToList',
    type: 'POST',
    data: formData,
    success: function () {
        alert('Added successfully!');
    closeAddToListModal(); // if you have this function
            },
    error: function () {
        alert('Failed to add to list.');
            }
        });
});

// Handles show card click -> loads show partial
$(document).on('click', '.card[data-show-id]', function () {
    var showId = $(this).data('show-id');

    $.ajax({
        url: '/Collection/GetShowDetails',
        type: 'GET',
        data: { showId: showId },
        success: function (result) {
            $('#show-details-container').html(result);
            $('#show-details-modal').fadeIn();
        },
        error: function () {
            alert('Error loading show details');
        }
    });
});
function populateEpisodesDropdown(count) {
    const $episodeDropdown = $('#episodes-watched');
    $episodeDropdown.empty();

    for (let i = 0; i <= count; i++) {
        $episodeDropdown.append(`<option value="${i}">${i}</option>`);
    }
}
function closeMovieModal() {
    $('#movie-details-modal').fadeOut(200, function () {
        $('#movie-details-container').empty();
    });
}

function closeShowModal() {
    $('#show-details-modal').fadeOut(200, function () {
        $('#show-details-container').empty();
    });
}

function closeAddToListModal() {
    $('#add-to-list-modal').fadeOut(200, function () {
        $('#add-to-list-container').empty();
    });
}
function closeAllModals() {
    $('#movie-details-modal').fadeOut(200, function () {
        $('#movie-details-container').empty();
    });

    $('#add-to-list-modal').fadeOut(200, function () {
        $('#add-to-list-container').empty();
    });

    $('#show-details-modal').fadeOut(200, function () {
        $('#show-details-container').empty();
    });
}


$(document).on('change', '#season', function () {
    const selectedOption = $(this).find('option:selected');
    const episodeCount = parseInt(selectedOption.data('episodecount')) || 0;

    populateEpisodesDropdown(episodeCount);
});

$(document).on('click', '.close-add-to-list', function () {
    closeAllModals();
});

$(document).on('click', '#movie-details-modal .modal-close, #movie-details-modal.modal-overlay', function (e) {
    if ($(e.target).is('.modal-close') || $(e.target).hasClass('modal-overlay')) {
        closeMovieModal();
    }
});

$(document).on('click', '#add-to-list-modal .modal-close, #add-to-list-modal.modal-overlay', function (e) {
    if ($(e.target).is('.modal-close') || $(e.target).hasClass('modal-overlay')) {
        closeAddToListModal();
    }
});


// Close show modal
$(document).on('click', '#show-details-modal .modal-close, #show-details-modal.modal-overlay', function (e) {
    if ($(e.target).is('.modal-close') || $(e.target).is('.modal-overlay')) {
        closeShowModal();
    }
});