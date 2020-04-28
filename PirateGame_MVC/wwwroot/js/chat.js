"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/Lobby/Index").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

var testDiv = document.getElementById("testt");
testDiv.textContent = playerNickname.toString();

connection.start().then(function () {
	document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
	return console.error(err.toString());
});

connection.on("ReceiveMessage", function (user, message) {
	var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
	var encodedMsg = user + ": " + msg;
	var newDiv = document.createElement("div");
	var divNoGutters = document.createElement("div");

	if (playerNickname == user) {
		newDiv.setAttribute("class", "chat-bubble-right");
		divNoGutters.setAttribute("class", "row no-gutters d-flex flex-row-reverse");
		divNoGutters.appendChild(newDiv);
		console.log("true");
	}
	else {
		newDiv.setAttribute("class", "chat-bubble-left");
		divNoGutters.setAttribute("class", "row no-gutters");
		divNoGutters.appendChild(newDiv);
		console.log("false");
	}

	newDiv.textContent = encodedMsg;
	(document.querySelector(".chat-panel").appendChild(divNoGutters));
});

document.getElementById("sendButton").addEventListener("click", function (event) {
	var message = document.getElementById("messageInput").value;
	connection.invoke("SendMessage", playerNickname, message).catch(function (err) {
		return console.error(err.toString());
	});
	document.getElementById("messageInput").value = "";
	event.preventDefault();
});

//popup modal
const createRoom = document.querySelector('[data-create-button]');
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
	var modal = document.getElementById("create-room");
	closeCreateRoomModal(modal);
});

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

$("[type='number']").keypress(function (evt) {
	evt.preventDefault();
});

//creating room
createRoom.addEventListener("click", function (event) {
	var roomName = document.getElementById('roomName');
	var maxPlayers = document.getElementById('maxPlayers');

	connection.invoke("CreateRoom", roomName.value, maxPlayers.value, playerNickname).catch(function (err) {
		return console.error(err.toString());
	});
	var modal = document.getElementById("create-room");
	closeCreateRoomModal(modal);
	roomName.value = "";
	maxPlayers.value = "";

	event.preventDefault();
});

connection.on("GetRoomId", function (roomId) {
	var form = document.querySelector('form');
	var roomIdInput = document.getElementById('roomIdInput');
	//var newInput = document.createElement("input");
	//newInput.value = roomId;
	//newInput.setAttribute("name", "RoomId");
	//form.appendChild(newInput);
	roomIdInput.value = roomId;
	console.log(roomId);
	form.submit();
});

connection.on("AddRoomToList", function (roomId, message) {
	var roomSelect = document.getElementById('roomSelect');
	var option = document.createElement('option');
	option.value = roomId;
	option.textContent = message;

	roomSelect.appendChild(option);
});

function showAvailableRooms() {
	connection.invoke("GetAvailableRooms").catch(function (err) {
		return console.error(err.toString());
	});

	setTimeout("showAvailableRooms()", 5000);
};

connection.on("ReceiveRoomList", function (roomList) {
	var rooms = JSON.parse(roomList);
	var roomSelect = document.getElementById('roomSelect');
	clearRoomSelect(roomSelect);

	rooms.forEach(room => {
		var option = document.createElement('option');
		option.value = room.RoomId;
		option.textContent = room.RoomName + " <---> players: " + room.Players.length + "/" + room.MaxPlayers;

		roomSelect.appendChild(option);
	});
});

function clearRoomSelect(selectElement) {
	var i, L = selectElement.options.length - 1;
	for (i = L; i >= 0; i--) {
		selectElement.remove(i);
	}
};
showAvailableRooms();