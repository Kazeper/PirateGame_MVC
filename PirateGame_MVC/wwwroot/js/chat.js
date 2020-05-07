"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/Lobby/Index").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.start().then(function () {
	document.getElementById("sendButton").disabled = false;
	document.getElementById('joinRoom-btn').disabled = false;
}).catch(function (err) {
	return console.error(err.toString());
});

connection.on("ReceiveMessage", function (user, message) {
	var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
	var encodedMsg = user + ": " + msg;
	var newDiv = document.createElement("div");
	var divNoGutters = document.createElement("div");
	var chatPanel = document.querySelector(".chat-panel");

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
	chatPanel.appendChild(divNoGutters);
	chatPanel.scrollTop = chatPanel.scrollHeight;
});

document.getElementById("sendButton").addEventListener("click", function (event) {
	var message = document.getElementById("messageInput").value;
	connection.invoke("SendMessage", playerNickname, message).catch(function (err) {
		return console.error(err.toString());
	});
	document.getElementById("messageInput").value = "";
	event.preventDefault();
});