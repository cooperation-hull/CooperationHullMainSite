// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let rootURL = window.location.origin;

document.addEventListener('DOMContentLoaded', () => { pageLoadActions(); });

function pageLoadActions() {
    setNavItemActive();
}

function setNavItemActive() {
    let pageURL = window.location.href.replace(rootURL, '');
    $(`#topNavBar li > a[href="${pageURL}"]`).parent().addClass('active')
}


//form submission handling


function form_submit(formName) {

    let $form = $(`#${formName}`);

    $.ajax({
        type: "POST",
        dataType: 'json',
        url: $form.attr('action'),
        data: $form.serialize(),
        error: function (xhr, status, error) {
            debugger;
            $(`#${formName}_confirmation #form_result_text`).html('<p>Something has gone wrong<p>.  <p>Try again later<p>');
            $(`#${formName}_confirmation`).show();

        },
        success: function (response) {
            debugger;
            $(`#${formName}_confirmation #form_result_text`).html(response.messageText);
            $(`#${formName}_confirmation`).modal('show');
            $(`#${formName} input`).val('');
        }
    });

    return false;

}

