"use strict";
var listOfPlayers = document.getElementById('listOfPlayers');
var readyButton = document.getElementById('ready-btn');
var playerIsReady = false;

readyButton.addEventListener("click", function (event) {
	var readyBox = document.querySelector('#' + playerNickname + '.col .ready-box');

	playerIsReady = !playerIsReady;

	if (playerIsReady) {
		readyBox.classList.add('ready-box-changed');
		readyBox.innerHTML = '&radic;';

		readyButton.classList.add('ready-box-changed');

		connection.invoke("UpdatePlayerState")
	}
	else {
		readyBox.classList.remove('ready-box-changed');
		readyBox.innerHTML = '';
		readyButton.classList.remove('ready-box-changed');
	}
	event.preventDefault();
});

connection.on("ReceivePlayers", function (serializedPlayers) {
	var players = JSON.parse(serializedPlayers);
	console.log(serializedPlayers);

	clearListOfPlayers();

	players.forEach(player => {
		var divCol = document.createElement("div");
		divCol.setAttribute("class", "col");
		divCol.setAttribute("id", player.Nickname);

		var readyBox = document.createElement("div");
		readyBox.setAttribute("class", "ready-box");

		var textDiv = document.createElement("div");
		textDiv.setAttribute("class", "text-justify");
		textDiv.textContent = player.Nickname;

		var divW100 = document.createElement("div");
		divW100.setAttribute("class", "w-100");

		divCol.appendChild(readyBox);
		divCol.appendChild(textDiv);

		listOfPlayers.appendChild(divCol);
		listOfPlayers.appendChild(divW100);
	})
});

function clearListOfPlayers() {
	listOfPlayers.innerHTML = "";
}