var connection = new signalR.HubConnectionBuilder().withUrl("/Lobby/Index").build();
const openCreateRoomButton = document.querySelectorAll('[data-modal-target]');
const closeCreateRoomButton = document.querySelectorAll('[data-close-button]');
const overlay = document.getElementById('overlay');

openCreateRoomButton.forEach(button => {
	button.addEventListener('click', () => {
		const createRoomModal = document.querySelector(button.dataset.modalTarget)
		openCreateRoomModal(createRoomModal);
	})
});

overlay.addEventListener('click', () => {
	const modals = document.querySelectorAll('.create-room.acitve')
	modals.forEach(modal => {
		closeCreateRoomModal(modal);
	})
})

closeCreateRoomButton.forEach(button => {
	button.addEventListener('click', () => {
		const createRoomModal = button.closest('.create-room');
		closeCreateRoomModal(createRoomModal);
	})
});

function openCreateRoomModal(createRoomModal) {
	if (createRoomModal == null) return

	createRoomModal.classList.add('active');
	overlay.classList.add('active');
};

function closeCreateRoomModal(createRoomModal) {
	if (createRoomModal == null) return

	createRoomModal.classList.remove('active');
	overlay.classList.remove('active');
};