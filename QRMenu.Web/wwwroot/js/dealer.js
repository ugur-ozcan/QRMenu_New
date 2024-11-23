// wwwroot/js/dealer.js

$(document).ready(function () {
    // Sayfa yüklendiğinde çalışacak kodlar
    let currentPage = 1;
    let currentPageSize = 10;
    let currentStatus = '';

    initializeDatatable();
    initializeEvents();
});

function initializeDatatable() {
    $('#dealersTable').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.10.24/i18n/Turkish.json'
        },
        pageLength: 10,
        lengthMenu: [[10, 25, 50, 100], [10, 25, 50, 100]],
        processing: true,
        serverSide: false, // Şimdilik client-side
        dom: '<"d-flex justify-content-between align-items-center mb-3"<"d-flex gap-2"l><"d-flex gap-2"f>>t<"d-flex justify-content-between align-items-center mt-3"<"d-flex gap-2"i><"d-flex gap-2"p>>',
    });
}

function initializeEvents() {
    // Modal kapandığında formu temizle
    $('#dealerModal').on('hidden.bs.modal', function () {
        $('#dealerForm')[0].reset();
        $('#dealerId').val('');
        $('#modalTitle').text('Yeni Bayi');
    });

    // Dosya seçildiğinde preview göster
    $('#logo').on('change', function (e) {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                $('#logoPreview').attr('src', e.target.result);
            };
            reader.readAsDataURL(file);
        }
    });
}

function showAddDealerModal() {
    $('#modalTitle').text('Yeni Bayi');
    $('#dealerId').val('');
    $('#dealerModal').modal('show');
}

async function editDealer(id) {
    try {
        const response = await fetch(`/Admin/GetDealer/${id}`);
        if (!response.ok) throw new Error('Bayi bilgileri alınamadı');

        const data = await response.json();
        if (data.isSuccess) {
            const dealer = data.data;

            $('#dealerId').val(dealer.id);
            $('#name').val(dealer.name);
            $('#contactPerson').val(dealer.contactPerson);
            $('#email').val(dealer.email);
            $('#phoneNumber').val(dealer.phoneNumber);
            $('#address').val(dealer.address);
            $('#instagramHandle').val(dealer.instagramHandle);
            $('#licenseExpiryDate').val(dealer.licenseExpiryDate.split('T')[0]);

            if (dealer.logo) {
                $('#logoPreview').attr('src', dealer.logo);
            }

            $('#modalTitle').text('Bayi Düzenle');
            $('#dealerModal').modal('show');
        } else {
            showError(data.message);
        }
    } catch (error) {
        showError('Bayi bilgileri alınırken bir hata oluştu');
    }
}

async function saveDealer() {
    const formData = new FormData();
    formData.append('id', $('#dealerId').val());
    formData.append('name', $('#name').val());
    formData.append('contactPerson', $('#contactPerson').val());
    formData.append('email', $('#email').val());
    formData.append('phoneNumber', $('#phoneNumber').val());
    formData.append('address', $('#address').val());
    formData.append('instagramHandle', $('#instagramHandle').val());
    formData.append('licenseExpiryDate', $('#licenseExpiryDate').val());

    const logoFile = $('#logo')[0].files[0];
    if (logoFile) {
        formData.append('logo', logoFile);
    }

    try {
        const response = await fetch('/Admin/SaveDealer', {
            method: 'POST',
            body: formData
        });

        if (!response.ok) throw new Error('İşlem başarısız');

        const data = await response.json();
        if (data.isSuccess) {
            $('#dealerModal').modal('hide');
            showSuccess('Bayi başarıyla kaydedildi');
            refreshTable();
        } else {
            showError(data.message);
        }
    } catch (error) {
        showError('İşlem sırasında bir hata oluştu');
    }
}

async function deleteDealer(id) {
    if (await showConfirm('Bayi Sil', 'Bu bayiyi silmek istediğinizden emin misiniz?')) {
        try {
            const response = await fetch(`/Admin/DeleteDealer/${id}`, {
                method: 'POST'
            });

            if (!response.ok) throw new Error('İşlem başarısız');

            const data = await response.json();
            if (data.isSuccess) {
                showSuccess('Bayi başarıyla silindi');
                refreshTable();
            } else {
                showError(data.message);
            }
        } catch (error) {
            showError('İşlem sırasında bir hata oluştu');
        }
    }
}

async function toggleDealerStatus(id) {
    try {
        const response = await fetch(`/Admin/ToggleDealerStatus/${id}`, {
            method: 'POST'
        });

        if (!response.ok) throw new Error('İşlem başarısız');

        const data = await response.json();
        if (data.isSuccess) {
            showSuccess('Bayi durumu güncellendi');
            refreshTable();
        } else {
            showError(data.message);
        }
    } catch (error) {
        showError('İşlem sırasında bir hata oluştu');
    }
}

function refreshTable() {
    $('#dealersTable').DataTable().ajax.reload();
}

// Yardımcı fonksiyonlar
function showSuccess(message) {
    Swal.fire({
        icon: 'success',
        title: 'Başarılı',
        text: message,
        timer: 2000,
        showConfirmButton: false
    });
}

function showError(message) {
    Swal.fire({
        icon: 'error',
        title: 'Hata',
        text: message
    });
}

async function showConfirm(title, message) {
    const result = await Swal.fire({
        icon: 'warning',
        title: title,
        text: message,
        showCancelButton: true,
        confirmButtonText: 'Evet',
        cancelButtonText: 'Hayır'
    });
    return result.isConfirmed;
}