function allowDrop(e) {
	e.preventDefault();
}
function drag(e) {
	e.dataTransfer.setData("handler", e.target.id);
}

function drop(e) {
	e.preventDefault();
	var data = e.dataTransfer.getData("handler");
	var targetId = e.target.id;
	e.target.appendChild(document.getElementById(data));

	(document.getElementById(data)).appendChild(target);
}