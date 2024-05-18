// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let rootURL = window.location.origin;

document.addEventListener('DOMContentLoaded', () => { pageLoadActions(); });

function pageLoadActions() {
    setNavItemActive();
    $('#homepage-formError').html();
    $('#home_page_signup_form').show();
    $('#form-completed').hide();
}

function setNavItemActive() {
    let pageURL = window.location.href.replace(rootURL, '');
    $(`#topNavBar li > a[href="${pageURL}"]`).parent().addClass('active')
}


//form submission handling

function home_page_signup_form_submit() {

    let $form = $(`#home_page_signup_form`);

    $.ajax({
        type: "POST",
        dataType: 'json',
        url: $form.attr('action'),
        data: $form.serialize(),
        error: function (xhr, status, error) {
            $('#homepage-form-to-complete #homepage-formError').html("Something went wrong.  Please try again later");
        },
        success: function (response) {

            if (response.result) {
                $('#signedbyName').html(response.signedByName);
                $('#home_page_signup_form').hide();
                $('#form-completed').show();
            }
            else {
                $('#homepage-formError').html(response.error);
            }
        }
    });

    return false;

}

