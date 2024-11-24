// QRMenu.Web/wwwroot/js/site.js

document.addEventListener('DOMContentLoaded', function () {
    // Theme Toggle Button
    const themeToggle = document.getElementById('themeToggle');
    if (themeToggle) {
        themeToggle.addEventListener('click', function () {
            document.documentElement.classList.toggle('dark');
            const isDark = document.documentElement.classList.contains('dark');
            localStorage.setItem('theme', isDark ? 'dark' : 'light');

            // Update icons
            const sunIcon = themeToggle.querySelector('.fa-sun');
            const moonIcon = themeToggle.querySelector('.fa-moon');
            if (sunIcon && moonIcon) {
                sunIcon.classList.toggle('hidden');
                moonIcon.classList.toggle('hidden');
            }
        });
    }

    // Load saved theme
    if (localStorage.theme === 'dark' || (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
        document.documentElement.classList.add('dark');
    } else {
        document.documentElement.classList.remove('dark');
    }

    // Notification Toggle
    const notificationBtn = document.getElementById('notificationsButton');
    const notificationDropdown = document.getElementById('notificationsModal');

    if (notificationBtn && notificationDropdown) {
        notificationBtn.addEventListener('click', function (e) {
            e.stopPropagation();
            notificationDropdown.classList.toggle('hidden');
            if (profileDropdown) profileDropdown.classList.add('hidden');
        });
    }

    // Profile Toggle
    const profileBtn = document.getElementById('profileButton');
    const profileDropdown = document.getElementById('profileModal');

    if (profileBtn && profileDropdown) {
        profileBtn.addEventListener('click', function (e) {
            e.stopPropagation();
            profileDropdown.classList.toggle('hidden');
            if (notificationDropdown) notificationDropdown.classList.add('hidden');
        });
    }

    // Close dropdowns when clicking outside
    document.addEventListener('click', function (e) {
        if (!notificationBtn?.contains(e.target) && !notificationDropdown?.contains(e.target)) {
            notificationDropdown?.classList.add('hidden');
        }
        if (!profileBtn?.contains(e.target) && !profileDropdown?.contains(e.target)) {
            profileDropdown?.classList.add('hidden');
        }
    });
});