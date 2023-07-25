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
// to check whether the new psssword entered are  in the correct format

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
// to check whether the key entered are in the correct format

function VerificationkeychangePassword() {
    const Identityverification = document.getElementById("Identityverification");

    const verificationerror = document.getElementById("verification-error");
    if (Identityverification.value === "") {
        verificationerror.innerHTML = "Fields are required";
    }
  
    else {
        verificationerror.innerHTML = "";
    }
}
