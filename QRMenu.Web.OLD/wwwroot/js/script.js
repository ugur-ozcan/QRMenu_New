// Örnek müşteri verileri
const customers = [
    { id: 1, name: "John Doe", email: "john@example.com", phone: "+90 555 123 4567", status: "Aktif", totalOrders: 15, totalSpent: 1200 },
    { id: 2, name: "Jane Smith", email: "jane@example.com", phone: "+90 555 987 6543", status: "Pasif", totalOrders: 8, totalSpent: 750 },
    // ... (diğer müşteriler)
];

let currentPage = 1;
let recordsPerPage = 10;

function displayCustomers(page) {
    const start = (page - 1) * recordsPerPage;
    const end = start + recordsPerPage;
    const paginatedCustomers = customers.slice(start, end);

    const tableBody = document.getElementById('customerTableBody');
    tableBody.innerHTML = '';

    paginatedCustomers.forEach(customer => {
        const row = `
            <tr>
                <td>
                    <div style="display: flex; align-items: center; gap: 10px;">
                        <img src="https://via.placeholder.com/32" alt="${customer.name}" style="width: 32px; height: 32px; border-radius: 50%;">
                        <span>${customer.name}</span>
                    </div>
                </td>
                <td><span class="status-badge ${customer.status === 'Aktif' ? 'status-active' : 'status-pending'}">${customer.status}</span></td>
                <td>$${customer.totalSpent}</td>
                <td>
                    <button onclick="openModal('viewModal'); fillViewModal(${customer.id})" style="padding: 5px; border: none; background: none; cursor: pointer; color: #3b82f6;"><i class="fas fa-eye"></i></button>
                    <button onclick="openModal('editModal'); fillEditModal(${customer.id})" style="padding: 5px; border: none; background: none; cursor: pointer; color: #f59e0b;"><i class="fas fa-edit"></i></button>
                    <button onclick="openModal('deleteModal'); setDeleteCustomerId(${customer.id})" style="padding: 5px; border: none; background: none; cursor: pointer; color: #ef4444;"><i class="fas fa-trash"></i></button>
                </td>
            </tr>
        `;
        tableBody.innerHTML += row;
    });

    updatePagination();
}

function updatePagination() {
    const totalPages = Math.ceil(customers.length / recordsPerPage);
    const paginationButtons = document.getElementById('paginationButtons');
    paginationButtons.innerHTML = '';

    // Önceki sayfa butonu
    if (currentPage > 1) {
        const prevButton = document.createElement('button');
        prevButton.innerText = 'Önceki';
        prevButton.onclick = () => {
            currentPage--;
            displayCustomers(currentPage);
        };
        paginationButtons.appendChild(prevButton);
    }

    // Sayfa numaraları
    for (let i = 1; i <= totalPages; i++) {
        if (
            i === 1 ||
            i === totalPages ||
            (i >= currentPage - 1 && i <= currentPage + 1)
        ) {
            const button = document.createElement('button');
            button.innerText = i;
            button.classList.toggle('active', i === currentPage);
            button.onclick = () => {
                currentPage = i;
                displayCustomers(currentPage);
            };
            paginationButtons.appendChild(button);
        } else if (
            i === currentPage - 2 ||
            i === currentPage + 2
        ) {
            const ellipsis = document.createElement('span');
            ellipsis.innerText = '...';
            paginationButtons.appendChild(ellipsis);
        }
    }

    // Sonraki sayfa butonu
    if (currentPage < totalPages) {
        const nextButton = document.createElement('button');
        nextButton.innerText = 'Sonraki';
        nextButton.onclick = () => {
            currentPage++;
            displayCustomers(currentPage);
        };
        paginationButtons.appendChild(nextButton);
    }
}

function changeRecordCount() {
    recordsPerPage = parseInt(document.getElementById('recordCount').value);
    currentPage = 1;
    displayCustomers(currentPage);
}

function openModal(modalId) {
    document.getElementById(modalId).style.display = 'block';
}

function closeModal(modalId) {
    document.getElementById(modalId).style.display = 'none';
}

function toggleNotifications() {
    document.getElementById("notificationDropdown").classList.toggle("show");
}

function toggleProfileMenu() {
    document.getElementById("profileMenu").classList.toggle("show");
}

function setTheme(theme) {
    document.body.setAttribute('data-theme', theme);
    const sunIcon = document.querySelector('.fa-sun');
    const moonIcon = document.querySelector('.fa-moon');
    if (theme === 'dark') {
        sunIcon.classList.remove('active');
        moonIcon.classList.add('active');
    } else {
        moonIcon.classList.remove('active');
        sunIcon.classList.add('active');
    }
}

function fillViewModal(customerId) {
    const customer = customers.find(c => c.id === customerId);
    if (customer) {
        document.getElementById('viewName').textContent = customer.name;
        document.getElementById('viewEmail').textContent = customer.email;
        document.getElementById('viewPhone').textContent = customer.phone;
        document.getElementById('viewStatus').innerHTML = `<span class="status-badge ${customer.status === 'Aktif' ? 'status-active' : 'status-pending'}">${customer.status}</span>`;
        document.getElementById('viewTotalOrders').textContent = customer.totalOrders;
        document.getElementById('viewTotalSpent').textContent = `$${customer.totalSpent}`;
    }
}

function fillEditModal(customerId) {
    const customer = customers.find(c => c.id === customerId);
    if (customer) {
        document.getElementById('editName').value = customer.name;
        document.getElementById('editEmail').value = customer.email;
        document.getElementById('editPhone').value = customer.phone;
        document.getElementById('editStatus').value = customer.status;
    }
}

let deleteCustomerId = null;

function setDeleteCustomerId(id) {
    deleteCustomerId = id;
}

function deleteCustomer() {
    if (deleteCustomerId) {
        const index = customers.findIndex(c => c.id === deleteCustomerId);
        if (index !== -1) {
            customers.splice(index, 1);
            displayCustomers(currentPage);
            closeModal('deleteModal');
        }
    }
}

function toggleMobileMenu() {
    document.querySelector('.sidebar').classList.toggle('active');
}

// Sayfa yüklendiğinde müşteri listesini göster
document.addEventListener('DOMContentLoaded', () => {
    displayCustomers(currentPage);
});

// Modal dışına tıklandığında kapatma
window.onclick = function(event) {
    if (event.target.className === 'modal') {
        event.target.style.display = 'none';
    }
    if (!event.target.matches('.navbar-icon') && !event.target.matches('.dropdown img')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
        document.getElementById("profileMenu").classList.remove("show");
    }
}