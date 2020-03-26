function allowDrop(e) {
	e.preventDefault();
}
function drag(e) {
	e.dataTransfer.setData("handler", e.target.id);
	e.dataTransfer.setData("handlerParent", e.target.parentElement.id);
}

function drop(e) {
	e.preventDefault();
	var data = e.dataTransfer.getData("handler");
	var parentID = e.dataTransfer.getData("handlerParent");

	const parent = document.getElementById(parentID);

	parent.appendChild(e.target.childNodes[2]);
	e.target.appendChild(document.getElementById(data));

	//(document.getElementById(data)).appendChild(target);
	//parent.appendChild(document.getElementById('#' + e.target.id + ' .img-fluid'));
}