function updateStatus(selectElement, statusId) {
    const selectedStatus = selectElement.value;
    const statusElement = document.getElementById(statusId);

    // Actualizarea statutului vizual în funcție de opțiunea selectată
    if (selectedStatus === "Delivered") {
        statusElement.textContent = "✅ Delivered";
        statusElement.className = "status delivered";
    } else if (selectedStatus === "Failed") {
        statusElement.textContent = "❌ Failed";
        statusElement.className = "status failed";
    } else {
        statusElement.textContent = "⏳ Pending";
        statusElement.className = "status pending";
    }
}
