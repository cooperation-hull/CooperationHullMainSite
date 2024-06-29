// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let rootURL = window.location.origin;

//form submission handling

function home_page_signup_form_submit() {

    let $form = $(`#home_page_signup_form`);

    $.ajax({
        type: "POST",
        dataType: 'json',
        url: $form.attr('action'),
        data: $form.serialize(),
        error: function (xhr, status, error) {
            $('#form-error').show();
        },
        success: function (response) {
            if (response.result) {
                $('#signed-by-name').html(response.signedByName);
                $('#home_page_signup_form').hide();
                $('#form-completed').show();
            }
            else {
                $('#form-error').show();
            }
        }
    });

    return false;

}

