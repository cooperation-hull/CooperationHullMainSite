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



