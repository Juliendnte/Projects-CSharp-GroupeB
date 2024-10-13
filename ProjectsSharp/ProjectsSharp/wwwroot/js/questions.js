function submitOption(questionId, selectedOption) {
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
            document.querySelector(`button[data-id="${questionId}-${selectedOption}"]`).classList.add(data.correct);
        })
        .catch(error => {
            console.error('Erreur:', error);
        });
}

function resetQuestions() {
    fetch('/Questions/ResetQuestions', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        }
    })
        .then(response => {
            if (response.ok) {
                window.location.reload();
            }
        })
        .catch(error => {
            console.error('Erreur:', error);
        });
}