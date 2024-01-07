const url = "/api/MatchesAPI";

const connection = new signalR.HubConnectionBuilder().withUrl("/MatchHub").build();

connection.start()
    .catch(function (err) { return console.error(err.tostring() )})

document.getElementById("savebt").addEventListener("click", function (event) {
    var id = document.getElementById("id").value;
    var equipe1 = document.getElementById("equipe1").value;
    var equipe2 = document.getElementById("equipe2").value;
    var score = document.getElementById("score").value;
    var temps = document.getElementById("temps").value;

    const match = {
        id: id, equipe1: equipe1, equipe2: equipe2, score: score, temps: temps
    };

    fetch(url+ "/" +id, {
        method:"PUT",
        headers:{
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(match)
    })
    .then(response => response.json())
    .then(() => {
        connection.invoke("SendMessage").catch(function (err) {
            return console.error(err.tostring());
        });
    })
    .catch(error => alert("Erreur API"));
    event.preventDefault();
});