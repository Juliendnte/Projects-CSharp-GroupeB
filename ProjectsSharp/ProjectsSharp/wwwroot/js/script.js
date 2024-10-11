let operation = '';
let firstNumber = '';
let secondNumber = '';
let history = [];

function appendNumber(number) {
    if (operation === '') {
        firstNumber += number;
        document.getElementById('result').value = firstNumber;
    } else {
        secondNumber += number;
        document.getElementById('result').value = secondNumber;
    }
}

function setOperation(op) {
    if (firstNumber !== '') {
        operation = op;
        document.getElementById('result').value = '';
    }
}

function clearResult() {
    firstNumber = '';
    secondNumber = '';
    operation = '';
    document.getElementById('result').value = '';
}

function calculate() {
    if (firstNumber !== '' && secondNumber !== '') {
        fetch(`/api/calculatrice/${operation}?a=${firstNumber}&b=${secondNumber}`)
            .then(response => response.json())
            .then(data => {
                document.getElementById('result').value = data;

                // Sauvegarder le calcul dans l'historique
                const calculation = `${firstNumber} ${getOperationSymbol(operation)} ${secondNumber} = ${data}`;
                addToHistory(calculation);

                // Réinitialiser les variables
                firstNumber = data;
                secondNumber = '';
                operation = '';
            })
            .catch(error => console.error('Erreur:', error));
    }
}

function getOperationSymbol(op) {
    switch (op) {
        case 'addition': return '+';
        case 'soustraction': return '-';
        case 'multiplication': return '*';
        case 'division': return '/';
        default: return '';
    }
}

function addToHistory(calculation) {
    // Ajouter le calcul à l'historique
    history.push(calculation);
    
    // Mettre à jour l'affichage de l'historique
    const historyList = document.getElementById('calculationHistory');
    const newHistoryItem = document.createElement('li');
    newHistoryItem.textContent = calculation;
    historyList.appendChild(newHistoryItem);
}
