"use strict";
var listOfPlayers = document.getElementById('listOfPlayers');
var readyButton = document.getElementById('ready-btn');
var leaveButton = document.getElementById('leave-btn');
var startGameForm = document.getElementById('startGame');
var playerIsReady = false;

readyButton.addEventListener("click", function (event) {
	playerIsReady = !playerIsReady;

	changeButton();

	connection.invoke("UpdatePlayersState", playerNickname, roomId, playerIsReady).catch(function (err) {
		return console.error(err.toString());
	});

	event.preventDefault();
});

function changeButton() {
	playerIsReady ? readyButton.classList.add('ready-box-changed') : readyButton.classList.remove('ready-box-changed');
}

leaveButton.addEventListener("click", function (event) {
	connection.invoke("LeaveRoom", playerNickname, roomId).catch(function (err) {
		return console.error(err.toString());
	})
	event.preventDefault();
});

connection.on("GoBack", function () {
	window.history.back();
});

connection.on("ReceivePlayers", function (serializedPlayers) {
	var allPlayersAreReady = true;
	var players = JSON.parse(serializedPlayers);
	var playersListTitle = document.getElementById("players-list-title");
	playersListTitle.innerHTML = "Players " + players.length + "/" + maxPlayers;

	clearListOfPlayers();

	players.forEach(player => {
		if (!player.GameFieldIsSet) {
			allPlayersAreReady = false;
		}
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

		updatePlayerState(player.Nickname, player.GameFieldIsSet);
	})

	if (players.length == maxPlayers && allPlayersAreReady) {
		startGameForm.submit();
	}
});

function clearListOfPlayers() {
	listOfPlayers.innerHTML = "";
}

function updatePlayerState(nickname, playerIsReady) {
	var readyBox = document.querySelector('#' + nickname + '.col .ready-box');

	if (playerIsReady) {
		readyBox.classList.add('ready-box-changed');
		readyBox.innerHTML = '&radic;';
	}
	else {
		readyBox.classList.remove('ready-box-changed');
		readyBox.innerHTML = '';
	}
}