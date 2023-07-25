
// to check whether the username entered are  in the correct format
function UsernamechangePassword() {
    const Username = document.getElementById("Username");
    const emailRegex = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;

    const usernameerror = document.getElementById("username-error");
    if (Username.value === "") {
        usernameerror.innerHTML = "Fields are required";
    }
    else if (!emailRegex.test(Username.value.trim())) {
        usernameerror.innerHTML = "Invalid email format";
    }
    else {
        usernameerror.innerHTML = "";
    }
}
// to check whether the password entered are  in the correct format
function PasswordchangePassword() {
    const Password = document.getElementById("Password");
    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=])[A-Za-z\d!@#$%^&*()_\-+=]{8,}$/;
    const passworderror = document.getElementById("password-error");
    if (Password.value === "") {
        passworderror.textContent = "Fields are required";
    } else if (!passwordRegex.test(Password.value.trim())) {
        passworderror.textContent = "Password should  contain  minimum 8 characters ,with atleast one  capital letter,small letter,a symbol(@,$,%,&,*),a number";
    }
    else {
        passworderror.innerHTML = "";
    }
}

// to check whether the new password entered are  in the correct format
function NewPasswordchangePassword() {
    const changePassword = document.getElementById("changePassword");
    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=])[A-Za-z\d!@#$%^&*()_\-+=]{8,}$/;
    const changePassworderror = document.getElementById("cpassword-error");

    if (changePassword.value === "") {
        changePassworderror.textContent = "Fields are required";
    } else if (!passwordRegex.test(changePassword.value.trim())) {
        changePassworderror.textContent = "Password should  contain  minimum 8 characters ,with atleast one  capital letter,small letter,a symbol(@,$,%,&,*),number";
    }
    else {
        changePassworderror.innerHTML = "";
    }
}


/*function filterDoctors() {
    var specialization = document.getElementById("specialization").value.toLowerCase();
    var specializationCards = document.getElementsByClassName("specialization-card");

    for (var i = 0; i < specializationCards.length; i++) {
        var card = specializationCards[i];
        var cardSpecialization = card.dataset.specialization.toLowerCase();

        if (cardSpecialization.includes(specialization)) {
            card.style.display = "block";
        } else {
            card.style.display = "none";
        }
    }
}*/

//document.addEventListener("DOMContentLoaded", function () {
  //  console.log("hello")
/*  var displayicon = document.getElementById("displayicon");
    var displayDetails = document.getElementById("displayDetails");
    displayicon.addEventListener("click", function (event) {
        event.preventDefault();
        displayDetails.classList.toggle("displaydetailsClick")
    })
})*/

/*function displayview() {
    console.log("hello")
    var displayicon = document.getElementById("displayicon");
    var displayDetails = document.getElementById("displayDetails");
    displayDetails.classList.toggle("displaydetailsClick")

}*/
/*document.addEventListener("DOMContentLoaded", function () {
    var detailsIcon = document.getElementById("detailsIcon");
    var detailsDiv = document.querySelector(".displaydetails");
    detailsIcon.addEventListener("click", function (e) {
        e.preventDefault();
        detailsDiv.classList.toggle("displaydetailsClick");
    });
});*/

/*function toggleDetails() {
    var detailsIcon = document.getElementById("detailsIcon");
    var detailsDiv = document.querySelector(".displaydetails");

    if (detailsIcon && detailsDiv) {
        detailsIcon.addEventListener("click", function (e) {
            e.preventDefault();
            detailsDiv.classList.toggle("displaydetailsClick");
        });
    }
}

document.addEventListener("DOMContentLoaded", toggleDetails);*/

       
/*window.onload = function () {
    var dateInput = document.getElementById("dateInput");
    var errordateappointment = document.getElementById("error - dateappointment");
    var currentDate = new Date().toISOString().split("T")[0];
   dateInput.setAttribute("min", currentDate);
 if (dateInput.value < min) {
        errordateappointment.innerHTML="Future dates is only possible!!"
    }
}; */




/*function futureDate(inputDate) {
    var dateerrordiv = document.getElementById("date-error-div");

    var today = new Date().getTime();
    inputDate = inputDate.split("/");
    inputDate = new Date(inputDate[2], inputDate[1] - 1, inputDate[0]).getTime();

    if (today - inputDate <= 0) {
        dateerrordiv.innerHTML = "";
    } else {
        dateerrordiv.innerHTML = "Future dates only allowed!!..";
    }

    return (today - inputDate) < 0;
}*/

/*function datecheck() {
    var dateInput = document.getElementById('dateInput');
   
        var date = new Date();
        var day = date.getDate();
        if (day < 10) {
            day = "0" + day;
        }
        var month = date.getMonth() + 1; // Add 1 to get the correct month value
        if (month < 10) {
            month = "0" + month;
        }
        var year = date.getUTCFullYear();
        // var selectedDate = day + '-' + month + '-' + year;
        var selectedDate = year + '-' + month + '-' + day;
    dateInput.setAttribute('min', selectedDate);
    };*/





/*document.addEventListener("DOMContentLoaded", function () {
    var dateInput = document.getElementById('dateInput');

    // Set the minimum date to today
    var today = new Date();
    var day = String(today.getDate()).padStart(2, '0');
    var month = String(today.getMonth() + 1).padStart(2, '0');
    var year = today.getFullYear();
    var minDate = year + '-' + month + '-' + day;

    dateInput.addEventListener('change', function () {
        // Ensure the selected date is not before the minimum date
        if (dateInput.value < minDate) {
            dateInput.value = minDate;
        }
    });

    dateInput.setAttribute('min', minDate);
});*/

/*document.addEventListener('DOMContentLoaded', function () {
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
});*/


/*function submitpassword() {
    let username = document.getElementById("Username");
    let password = document.getElementById("Password");
    let changepassword = document.getElementById("ChangePassword");
    let button = document.getElementById("button"); 

   const usernameRegex = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
    const passwordRegex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=])[A-Za-z\d!@#$%^&*()_\-+=]{8,}$"
    const newPasswordRegex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=])[A-Za-z\d!@#$%^&*()_\-+=]{8,}$"

    if (username.value == "" || password.value == "" || changepassword.value == "") {
        const small = document.createElement('div')
        small.innerHTML = "Fields are required!!!..."
        button.disabled = true;
    }
    else if (!usernameRegex.test(username.value)) {
        const small = document.createElement('div')
        small.innerHTML = "Invalid  email !!!..."
        button.disabled = true;

    }
    else if (!passwordRegex.test(password.value)) {
        const small = document.createElement('div')
        small.innerHTML = "Password should atleast contain 8 characters with atleast a capital letter,small letter ,symbol,and a number  !!!..."
        button.disabled = true;

    }
    else if (!newPasswordRegex.test(changepassword.value)) {
        const small = document.createElement('div')
        small.innerHTML = "Password should atleast contain 8 characters with atleast a capital letter,small letter ,symbol,and a number  !!!..."
        button.disabled = true;

    }
    else {
        button.disabled = false;

    }
}*/



/*function submitpassword() {
   
    const usernameerror = document.getElementById("username-error");
    const passworderror = document.getElementById("password-error");
    const changePassworderror = document.getElementById("cpassword-error");
    if (usernameerror.textContent != null || passworderror.textContent != null || changePassworderror.textContent != null) {
        alert(
            "All fields are mandatory,follow the instrutions correctly"
        )
    }
   
}*/