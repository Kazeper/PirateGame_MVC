var listOfPlayers = document.getElementById('listOfPlayers');
var readyButton = document.getElementById('ready-btn');
var playerIsReady = false;

readyButton.addEventListener("click", function (event) {
	const playerDiv = document.getElementById(playerNickname);

	playerIsReady = !playerIsReady;

	if (playerIsReady) {
		playerDiv.classList.add('player-ready-changed');
		playerDiv.nextElementSibling.classList.add('ready-box-changed');
		playerDiv.nextElementSibling.innerHTML = '&radic;';

		readyButton.classList.add('ready-box-changed');
	}
	else {
		playerDiv.classList.remove('player-ready-changed');
		playerDiv.nextElementSibling.classList.remove('ready-box-changed');
		playerDiv.nextElementSibling.innerHTML = '';
		readyButton.classList.remove('ready-box-changed');
	}
});

connection.server;

connection.on("GetPlayers", function (serializedPlayers) {
	var players = JSON.parse(serializedPlayers);

	players.forEach(player => {
		var div = document.createElement("div");
		div.setAttribute("class", "col text-justify");
		div.setAttribute("id", player.Nickname);
		div.textContent = player.Nickname;

		var readyBox = document.createElement("div");
		readyBox.setAttribute("class", "ready-box");

		listOfPlayers.appendChild(readyBox);
		listOfPlayers.appendChild(div);
	})
})