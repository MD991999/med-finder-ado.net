


// email-validation
function validateEmail() {
    const emailpattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const email = document.getElementById("email");
    if (email.value.trim() === "") {
        setError(email,"Email address is required.")
    } else if (!emailpattern.test(email.value.trim()))
    //   The test method is used to test if a string matches a regular expression. It returns true if the pattern is found in the string, and false otherwise.
    {
        setError(email, "Invalid email format.")
    } else {
        setSuccess(email)
    }

}

// mobile validation
function validateMobile() {
    const phonenumber = document.getElementById("phonenumber")
    if (phonenumber.value.trim() === "") {
        setError(phonenumber, "Mobile number is required");
    }
    else if (!phonenumber.checkValidity())
    // It also uses the checkValidity() method to verify if the entered value matches the specified pattern ([0-9]{10}) for a 10-digit mobile number.
    {
        setError(phonenumber,"Invalid mobile number ");
    } else {
        setSuccess(phonenumber)
    }

}


// message validation
function validateMessage() {
    const message = document.getElementById('message')
    if (message.value.trim() === "") {
        setError(message,"Field is required")
    }
    else {
        setSuccess(message)

    }

}



// name validation
function validateName() {
    const format = /^[a-zA-Z]+$/
    const inputName = document.getElementById('fullname')
    if (inputName.value.trim() === "") {
        setError(inputName,"Name is required")
    }
    else if (!format.test(inputName.value.trim())) {
        setError(inputName, "Name should contain only alphabets")

    }
    else if (!/^[A-Z]/.test(inputName.value.trim())) {
        setError(inputName, "First letter should be a capital letter")

    }
    else {
       setSuccess(inputName)

    }

}

// set errror message
function setError(input, message) {
    const button = document.getElementById('button');
    const formcontrol = input.parentElement;
    const small = formcontrol.querySelector('small')
    small.className = 'smallshown';
    small.innerText = message;
    small.style.color = 'red';
    small.style.fontWeight = 'bold';
    small.style.fontSize = '20px';
    button.disabled = true;
}


//setsuccess message
function setSuccess(input) {
    const button = document.getElementById('button');
    const formcontrol = input.parentElement;
    const small = formcontrol.querySelector('small')
    small.className = 'smallhidden';
    small.innerText = '';
   
    button.disabled = false;
}


function backButton() {
    window.location = "index.html"
}











// function sendEmail() {
//     const inputEmail=document.getElementById(inputEmail)
//     const inputMessage=document.getElementById(inputMessage)

//   Email.send({
//     Host: "smtp.gmail.com",
//     Username: "sender@email_address.com",
//     Password: "Enter your password",
//     To: 'devassiamichelle9@gmail.com',
//     From: inputEmail.value,
//     Subject: "To travel",
//     Body: inputMessage.value
//   })
//     .then(function (message) {
//       alert("mail sent successfully")
//     });
// }





/*function validateEmail() {
    const email = document.getElementById("email");
    const errorSpan = document.querySelector(".validateEmailError");
    const emailpattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (email.value.trim() === "") {
        errorSpan.textContent = "Email address is required.";
    } else if (!emailpattern.test(email.value.trim()))
    //   The test method is used to test if a string matches a regular expression. It returns true if the pattern is found in the string, and false otherwise.
    {
        errorSpan.textContent = "Invalid email address.";
    } else {
        errorSpan.textContent = "";
    }
}


function validateMobile() {
    const phonenumber = document.getElementById("phonenumber")
    const validateMobileError = document.getElementsByClassName("validateMobileError")[0]
    if (phonenumber.value.trim() === "") {
        validateMobileError.textContent = "Mobile Number is required"
    }
    else if (!phonenumber.checkValidity())
    // It also uses the checkValidity() method to verify if the entered value matches the specified pattern ([0-9]{10}) for a 10-digit mobile number.
    {
        validateMobileError.textContent = "Invalid mobile number.";
    } else {
        validateMobileError.textContent = "";
    }
}


function validateMessage() {
    const message = document.getElementById('message')
    const validateMessageError = document.getElementsByClassName('validateMessageError')[0]
    if (message.value.trim() === "") {
        validateMessageError.textContent = "Message is required."
    }
    else {
        validateMessageError.textContent = ""

    }
}


function validateName() {

    const inputName = document.getElementById('fullname')
    const validateNameError = document.getElementsByClassName('validateNameError')[0]
    const format = /^[a-zA-Z]+$/
    if (inputName.value.trim() === "") {
        validateNameError.textContent = "Field is required."
    }
    else if (!format.test(inputName.value.trim())) {
        validateNameError.textContent = "Enter a valid name"

    }
    else if (!/^[A-Z]/.test(inputName.value.trim())) {
        validateNameError.textContent = "First letter should be capital letter"

    }
    else {
        validateNameError.textContent = ""

    }


}
*/