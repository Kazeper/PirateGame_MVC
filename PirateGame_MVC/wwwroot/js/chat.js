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
	event.preventDefault();
});

const createRoom = document.querySelector('[data-create-button]');

createRoom.addEventListener("click", function (event) {
	var roomName = document.getElementById('roomName').value;
	var maxPlayers = document.getElementById('maxPlayers').value;
	if (true) {
	}
	connection.invoke("CreateRoom", roomName, maxPlayers, playerNickname).catch(function (err) {
		return console.error(err.toString());
	});
	event.preventDefault();
	var modal = document.querySelector('.create-room.acitve');
	closeCreateRoomModal(modal);
});

connection.on("AddRoomToList", function (roomId, message) {
	var roomSelect = document.getElementById('roomSelect');
	var option = document.createElement('option');
	option.value = roomId;
	option.textContent = message;

	roomSelect.appendChild(option);
});