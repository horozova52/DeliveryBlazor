function toggleEdit() {
    const editSection = document.querySelector('.profile-edit');
    editSection.classList.toggle('hidden');
}

function saveChanges() {
    const email = document.getElementById('edit-email').value;
    const phone = document.getElementById('edit-phone').value;
    const address = document.getElementById('edit-address').value;

    document.getElementById('user-email').textContent = email || document.getElementById('user-email').textContent;
    document.getElementById('user-phone').textContent = phone || document.getElementById('user-phone').textContent;
    document.getElementById('user-address').textContent = address || document.getElementById('user-address').textContent;

    alert('Profile updated successfully!');
    toggleEdit();
}
