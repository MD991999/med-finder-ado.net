// JavaScript
// email-validation
function validateEmail() {
    const inputEmail = document.getElementById("inputEmail");
    const errorSpan = document.querySelector(".validateEmailError");
    const emailpattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (inputEmail.value.trim() === "") {
        errorSpan.textContent = "Email address is required.";
    } else if (!emailpattern.test(inputEmail.value.trim()))
    //   The test method is used to test if a string matches a regular expression. It returns true if the pattern is found in the string, and false otherwise.
    {
        errorSpan.textContent = "Invalid email address.";
    } else {
        errorSpan.textContent = "";
    }
}

// mobile validation
function validateMobile() {
    const inputMob = document.getElementById("inputmob")
    const validateMobileError = document.getElementsByClassName("validateMobileError")[0]
    if (inputMob.value.trim() === "") {
        validateMobileError.textContent = "Mobile Number is required"
    }
    else if (!inputMob.checkValidity())
    // It also uses the checkValidity() method to verify if the entered value matches the specified pattern ([0-9]{10}) for a 10-digit mobile number.
    {
        validateMobileError.textContent = "Invalid mobile number.";
    } else {
        validateMobileError.textContent = "";
    }
}
// message validation
function validateMessage() {
    const inputMessage = document.getElementById('inputMessage')
    const validateMessageError = document.getElementsByClassName('validateMessageError')[0]
    if (inputMessage.value.trim() === "") {
        validateMessageError.textContent = "Message is required."
    }
    else {
        validateMessageError.textContent = ""

    }
}
// name validation
function validateName() {

    const inputName = document.getElementById('inputName')
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

function backButton() {
    window.location = "index.html"
}




