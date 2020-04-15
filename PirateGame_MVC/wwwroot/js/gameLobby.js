//var uri = "ws://localhost:5000/ws";

var scheme = document.location.protocol === "https:" ? "wss" : "ws";
var port = document.location.port ? (":" + document.location.port) : "";
var uri = scheme + "://" + document.location.hostname + port + "/ws";
function connect() {
	socket = new WebSocket(scheme + "://" + document.location.hostname + port + "/ws");
	socket.onopen = function (e) {
		console.log("conn esatblished");
		console.log(uri);
	};

	socket.onclose = function (e) {
		console.log("conn closed");
	};

	socket.onmessage = function (e) {
		console.log("send MSG");
	};
};
connect();