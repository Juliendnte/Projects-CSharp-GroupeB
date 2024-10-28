let buttons = document.querySelectorAll(`button`);
buttons.forEach(button => {
    button.disabled = false;
});

function submitOption(questionId, selectedOption) {
    buttons = document.querySelectorAll(`button[data-id^="${questionId}-"]`);
    buttons.forEach(button => {
        button.disabled = true;
    });
    selectedOption = selectedOption.replace(/&apos;/g,"'");

    fetch('/Questions/SubmitOption', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        },
        body: JSON.stringify({ questionId: questionId, selectedOption: selectedOption })
    })
        .then(response => response.json())
        .then(data => {
            if(!data.correct){
                document.querySelector(`button[data-id="${questionId}-${data.correctAnswer.replace(/'/g,"&apos;")}"]`).classList.add('true');
            }
            document.querySelector(`button[data-id="${questionId}-${selectedOption.replace(/'/g, "&apos;")}"]`).classList.add(data.correct);
            updateScore();
            document.querySelector('#next').innerHTML = '<button onclick="window.location.reload();">Suivant</button>'
        })
        .catch(error => {
            console.error('Erreur:', error);
        });
    }

function resetQuestions() {
    buttons = document.querySelectorAll(`button`);
    fetch('/Questions/ResetQuestions', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        }
    })
        .then(response => {
            if (response.ok) {
                window.location.reload();
            }
        })
        .catch(error => {
            console.error('Erreur:', error);
        })
        .finally(() => {
            buttons.forEach(button => {
                button.disabled = false;
            });
        });
}

function updateScore(){
    fetch('/Questions/GetScore', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    })
        .then(response => response.json())
        .then(data => {
            document.querySelector('#score').innerHTML = data.score;
        })
        .catch(error => {
            console.error('Erreur:', error);
        });
}


updateScore();