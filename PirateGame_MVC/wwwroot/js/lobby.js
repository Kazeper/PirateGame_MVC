const createRoom = document.querySelector('[data-create-button]');

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