function clickSubmit() {
    const errorDiv = document.getElementById("errordiv");
    const email = document.getElementById("email-field").value;
    const password = document.getElementById("psw-field").value;

    if (email.trim() === '') {
        errorDiv.innerText = 'Email is required';
        clearErrorAfter(errorDiv, 5000);
        return false;
    }

    if (password.trim() === '') {
        errorDiv.innerText = 'Password is required';
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