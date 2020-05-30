function allowDrop(e) {
	e.preventDefault();
}
function drag(e) {
	e.dataTransfer.setData("handler", e.target.id);
	e.dataTransfer.setData("handlerParent", e.target.parentElement.id);

	var draggedInputValue = document.querySelector("#" + e.dataTransfer.getData("handlerParent") + ".img-fluid input").value;
	e.dataTransfer.setData("draggedInputValue", draggedInputValue);

	console.log(draggedInputValue);
}

function drop(e) {
	e.preventDefault();
	var handler = e.dataTransfer.getData("handler");
	var handlerParentId = e.dataTransfer.getData("handlerParent");

	const parent = document.getElementById(handlerParentId);
	var targetParentID = e.target.parentElement.id;
	const targetParent = document.getElementById(targetParentID);

	var draggedInputValue = document.querySelector("#" + handlerParentId + ".img-fluid input").value;
	var targetValue = document.querySelector('#' + e.target.parentElement.id + ".img-fluid input").value;
	var tempValue = targetValue;

	//swap values
	document.querySelector('#' + e.target.parentElement.id + ".img-fluid input").value = draggedInputValue;
	document.querySelector("#" + handlerParentId + ".img-fluid input").value = tempValue;

	//swap images
	parent.appendChild(e.target);
	targetParent.appendChild(document.getElementById(handler));
}