var listOfPlayers = document.getElementById('listOfPlayers');
var readyButton = document.getElementById('ready-btn');

readyButton.addEventListener("click", function (event) {
	const playerDiv = document.getElementById(playerNickname);
	playerDiv.classList.add('player-ready-changed');
	playerDiv.nextElementSibling.classList.add('ready-box-changed');
});

connection.on("GetPlayers", function (serializedPlayers) {
	var players = JSON.parse(serializedPlayers);

	players.forEach(player => {
		var div = document.createElement("div");
		div.setAttribute("class", "col-1");
		div.textContent = player.Nickname;

		listOfPlayers.appendChild(div);
	})
})