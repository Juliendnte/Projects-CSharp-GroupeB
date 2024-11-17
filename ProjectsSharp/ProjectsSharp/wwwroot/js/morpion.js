const gameBoard = document.getElementById("game-board");
    const selectedRowInput = document.getElementById("selected-row");
    const selectedColInput = document.getElementById("selected-col");
    
    const attachCellHandlers = () => {
        document.querySelectorAll(".cell").forEach(cell => {
            cell.addEventListener("click", event => {
                
                const row = event.target.getAttribute("data-row");
                const col = event.target.getAttribute("data-col");

               
                selectedRowInput.value = row;
                selectedColInput.value = col;
                
                document.querySelectorAll(".cell").forEach(c => c.classList.remove("selected"));
                event.target.classList.add("selected");
            });
        });
    };


    attachCellHandlers();

function validateMove() {

    const row = selectedRowInput.value;
    const col = selectedColInput.value;
    console.log(row);
    console.log(col);


    fetch(`/api/TicTacToe/move?row=${row}&col=${col}`, { method: "GET" })
        .then(response => response.text())
        .then(html => {
            gameBoard.innerHTML = html;

            attachCellHandlers();

            selectedRowInput.value = "";
            selectedColInput.value = "";
            console.log("Plateau mis à jour");
        })
        .catch(error => {
            console.error("Erreur lors de la mise à jour du plateau :", error);
        });
};
function restart(){
    attachCellHandlers();
    fetch(`/api/TicTacToe/restart`, {
        method: 'GET'
    })
        .then(response => response.text())
        .then(html =>{
            gameBoard.innerHTML = html;
            console.log(html);
        })
    attachCellHandlers();
    location.reload()
    
}
