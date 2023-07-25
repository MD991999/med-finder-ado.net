// to check the value is selected from the dropdown by using onfocusout method
function validateDropdownState() {
    const dropdown = document.getElementById("state");
   const errorSpan = document.querySelector(".validateDropdownErrorState");

    if (dropdown.value === "") {
       // setError(dropdown,"Please select a state.")
       errorSpan.textContent = "Please select a state.";
         parentdiv.style.height="50px"

    } else {
       // setSuccess(dropdown)

    errorSpan.textContent = "";
    }
}

// to check the value is selected from the dropdown by using onfocusout method
function validateDropdownCity() {
    const dropdown = document.getElementById("city");
const errorSpan = document.querySelector(".validateDropdownErrorCity");
    if (dropdown.value === "") {
       errorSpan.textContent = "Please select a city.";
       parentdiv.style.height="50px"
    } else {
       errorSpan.textContent = "";
    }
}

//to populate districts according to the state
function populateDistricts() {
    const stateSelector = document.getElementById("state");
    const districtSelector = document.getElementById("city");
    const selectedState = stateSelector.value;
    districtSelector.innerHTML = '<option value="" selected disabled>Select your city</option>';
    if (selectedState === 'Kerala') {
        populateKeralaDistricts();
    }
    else if (selectedState === 'Karnataka') {
        populateKarnatakaDistricts()
    }
    else if (selectedState === 'TamilNadu') {
        populateTamilDistricts()
    }
}

//to populate the district of kerala if the chosen state is kerala
function populateKeralaDistricts() {
    const districtSelector = document.getElementById("city");
    const keralaDistricts = ["Thrissur", "Wayanad", "Thiruvananthapuram", "Kochi", "Kasargod", "Kannur"];
    // populate options
    keralaDistricts.forEach((district) => {
        const option = document.createElement("option");
        option.value = district;
        option.text = district;
        districtSelector.appendChild(option);
    }
    )
}

//to populate the district of karnataka if the chosen state is kerala
function populateKarnatakaDistricts() {
    const districtSelector = document.getElementById("city");
    const karnatakaDistricts = ["Mysuru", "Kodagu", "Mandya", "Hassan", "Benagaluru", "Uduppi"];
    karnatakaDistricts.forEach((district) => {
        const option = document.createElement("option");
        option.value = district;
        option.text = district;
        districtSelector.appendChild(option);
    }
    )
}

//to populate the district of tamilnadu if the chosen state is kerala
function populateTamilDistricts() {
    const districtSelector = document.getElementById("city");
    const TamilDistricts = ["Chennai", "Coimbatore", "Cuddalore", "Erode", "Kanchipuram", "Madurai"];
    TamilDistricts.forEach((district) => {
        const option = document.createElement("option");
        option.value = district;
        option.text = district;
        districtSelector.appendChild(option);
    }
    )
}

/*document.addEventListener('DOMContentLoaded', function () {
    //By wrapping the JavaScript code inside the DOMContentLoaded event listener, we ensure that the code executes only after the HTML document has finished loading. This is important because we are accessing and manipulating the DOM elements in the code.
    var dateOfBirthInput = document.getElementById('dateOfBirth');
    var input = dateOfBirthInput.parentElement
    small = input.querySelector('small')
    dateOfBirthInput.addEventListener('change', function () {
        var selectedDate = this.value;
        console.log(selectedDate)
        var currentDate = new Date().getDate();
        var currentMonth = new Date().getMonth()+1;
        var currentYear = new Date().getFullYear();
        var presentDate = currentYear + '-' + currentMonth.toString().padStart(2, '0') + '-' + currentDate.toString().padStart(2, '0');
        //variables with the arguments (2, '0'). This ensures that the resulting string will have a minimum length of 2 characters, and if the length is less than 2, it will be padded with leading zeros.
        console.log(presentDate)
       // currentDate.setHours(0, 0, 0, 0); // Set current date to midnight

        if (selectedDate > presentDate) {
            this.value = '';
            small.innerHTML = "Please select a valid date of birth."
            small.style.color = 'red';
            small.style.fontWeight = "bold";
            small.style.fontSize="18px"
        }
        else {
            this.value = selectedDate;
            small.innerHTML = "";

        }
    });
});*/

/*document.addEventListener("DOMContentLoaded", function () {
    var dateInput = document.getElementById('dateInput');
    // Set the minimum date to today
    var today = new Date();
    var day = String(today.getDate()).padStart(2, '0')
    var month = String(today.getMonth() + 1).padStart(2, '0');
    var year = today.getFullYear();
    var minDate = day + '-' + month + '-' + year;
    dateInput.addEventListener('change', function () {
        // Ensure the selected date is not before the minimum date
        if (dateInput.value < minDate) {
            dateInput.value = minDate;
        }
    });
    dateInput.setAttribute('min', minDate);
});*/

// date time picker -date of birth validation
document.addEventListener('DOMContentLoaded', function () {
    //By wrapping the JavaScript code inside the DOMContentLoaded event listener, we ensure that the code executes only after the HTML document has finished loading. This is important because we are accessing and manipulating the DOM elements in the code.
    var dateOfBirthInput = document.getElementById('dateOfBirth');
    var input = dateOfBirthInput.parentElement
    small = input.querySelector('small')
    dateOfBirthInput.addEventListener('change', function () {
        var selectedDate = this.value;
        console.log(selectedDate)
        var currentDate = new Date().getDate();
        var currentMonth = new Date().getMonth() + 1;
        var currentYear = new Date().getFullYear();
        var presentDate = currentYear + '-' + currentMonth.toString().padStart(2, '0') + '-' + currentDate.toString().padStart(2, '0');
        //variables with the arguments (2, '0'). This ensures that the resulting string will have a minimum length of 2 characters, and if the length is less than 2, it will be padded with leading zeros.
        console.log(presentDate)
        // currentDate.setHours(0, 0, 0, 0); // Set current date to midnight

        if (selectedDate > presentDate) {
            this.value = '';
            small.innerHTML = "Please select a valid date of birth."
            small.style.color = 'red';
            small.style.fontWeight = "bold";
            small.style.fontSize = "18px"
        }
        else {
            this.value = selectedDate;
            small.innerHTML = "";

        }
    });
});



