var listOfPlayers = document.getElementById('listOfPlayers');

connection.on("GetPlayers", function (serializedPlayers) {
	var players = JSON.parse(serializedPlayers);

	players.forEach(player => {
		var div = document.createElement("div");
		div.setAttribute("class", "col-1");
		div.textContent = player.Nickname;

		listOfPlayers.appendChild(div);
	})
})