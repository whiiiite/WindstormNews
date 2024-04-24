function clickSubmit() {
    const errorDiv = document.getElementById("errordiv");
    const email = document.getElementById("email-field").value;
    const password = document.getElementById("password-field").value;
    const confirmPassword = document.getElementById("confirm-password-field").value;
    const username = document.getElementById("uname-field").value;

    if (email.trim() === '') {
        errorDiv.innerText = 'Email is required';
        clearErrorAfter(errorDiv, 5000);
        return false;
    }

    if (username.trim() === '') {
        errorDiv.innerText = 'Username is required';
        clearErrorAfter(errorDiv, 5000);
        return false;
    }

    if (password.trim() === '') {
        errorDiv.innerText = 'Password is required';
        clearErrorAfter(errorDiv, 5000);
        return false;
    }

    if (confirmPassword.trim() === '') {
        errorDiv.innerText = 'Confirm password is required';
        clearErrorAfter(errorDiv, 5000);
        return false;
    }

    if (password != confirmPassword) {
        errorDiv.innerText = 'Passwords is not equals';
        clearErrorAfter(errorDiv, 5000);
        return false;
    }

    return true;
}

function clearErrorAfter(errorDiv, ms) {
    setTimeout(() => {
        errorDiv.innerText = '';
    }, ms);
}