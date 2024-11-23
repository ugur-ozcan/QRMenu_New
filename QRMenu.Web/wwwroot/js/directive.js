function exportLogs(format) {
    var logLevel = $('#logLevel').val();
    var startDate = $('#startDate').val();
    var endDate = $('#endDate').val();

    var url = `/Admin/ExportLogs?format=${format}`;
    if (logLevel) url += `&logLevel=${logLevel}`;
    if (startDate) url += `&startDate=${startDate}`;
    if (endDate) url += `&endDate=${endDate}`;

    window.location.href = url;
}

function clearFilters() {
    $('#logLevel').val('');
    $('#startDate').val('');
    $('#endDate').val('');
    filterLogs();
}

function showLogDetails(logId) {
    $.get(`/Admin/GetLogDetails/${logId}`, function (result) {
        if (result.isSuccess) {
            $('#logDetailsModal .modal-body').html(result.data);
            $('#logDetailsModal').modal('show');
        } else {
            showError(result.message);
        }
    });
}