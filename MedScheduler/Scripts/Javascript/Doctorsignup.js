//validation for dropdown
function validateDropdownState() {
    const dropdown = document.getElementById("specialisation");
    const errorSpan = document.querySelector(".validateDropdownError");
    if (dropdown.value === "") {
        errorSpan.textContent = "Choose from the dropdown";
    } else {
        errorSpan.textContent = "";
    }
}
