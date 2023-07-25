//  preventing the user from navigating back to the previous page using the browser's back button
function preventBack() {
    window.history.forward();
}

setTimeout(function () {
    preventBack()
}, 0);
window.onunload = function () { null };