function validateForm() {
    // Form alanlarini degiskenlere ata
    const userName = document.getElementById("UserName").value.trim();
    const email = document.getElementById("Email").value.trim();
    const password = document.getElementById("Password").value.trim();

    // eger tum alanlar doluysa form gonderilebilir
    return true;
}
