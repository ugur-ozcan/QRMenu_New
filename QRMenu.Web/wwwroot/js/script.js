document.getElementById('loginForm').addEventListener('submit', function (e) {
    e.preventDefault();

    const username = document.getElementById('username').value.trim();
    const password = document.getElementById('password').value.trim();

    // Kullanıcı türüne göre yönlendirme
    if (username === "admin" && password === "admin") {
        window.location.href = "admin-dashboard.html";
    } else if (username === "bayi" && password === "bayi") {
        window.location.href = "bayi-dashboard.html";
    } else if (username === "firma" && password === "firma") {
        window.location.href = "firma-dashboard.html";
    } else {
        alert("Kullanıcı adı veya şifre hatalı!");
    }
});
