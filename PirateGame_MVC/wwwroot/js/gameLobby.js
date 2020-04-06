var connection;
var uri = "ws://" + window.location.host + "/server";
window.onload = function () {
	connection = new WebSocketManager.Connection(uri);//check 5000

	this.connection.connectionMethods.onConnected = () => {
		console.log("ff");
	}

	this.connection.connectionMethods.onDisconnected = () => {
	}

	this.connection.start();
}