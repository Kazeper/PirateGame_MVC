const fields = document.querySelectorAll(".col-1.border.border-warning");
const fieldCells = document.querySelectorAll(".col-1.border.border-warning.text-center.d-flex");
let draggedItem = null;

for(let i = 0; i < fields.length;i++){
    const item = fields[i];
    console.log('dragstart');

    item.addEventListener('dragstart', function(){
        draggedItem = item;
        setTimeout(function(){
            item.style.display = 'none'; 
        }, 0)
    });

    item.addEventListener('dragend', function(){
        setTimeout(function(){
            draggedItem.style.display = 'block';
            draggedItem = null;
        }, 0);
    });
}

for(let j = 0; j < fieldCells.length; j++){
    const cell = fieldCells[j];

    cell.addEventListener('dragover', function(e){
        e.preventDefault();
    });
    cell.addEventListener('dragenter', function(e){
        e.preventDefault();
        this.style.backgroundColor = 'rgba(0, 0, 0, 0.5)';
    });
    cell.addEventListener('dragleave', function(e){
        this.style.backgroundColor = 'rgba(0, 0, 0, 1)';
    });
    cell.addEventListener('drop', function(e){
        this.append(draggedItem);
    });
}