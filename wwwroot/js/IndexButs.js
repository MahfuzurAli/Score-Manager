const url = "api/ButsAPI";
let $buts = $("#buts");

getButs();

function getButs() {
    fetch(url)
        .then(response => response.json())
        .then(data => data.forEach(but => {
            let template = `<tr>
                                <td>${but.matchId}</td>
                                <td>${but.score}</td>
                                <td>${but.temps}'</td>
                                <td>${but.joueur}</td>
                                <td>
                                <a href="/Buts/Details/${but.butId}">Détails</a> |
                                <a href="/Buts/Edit/${but.butId}">Éditer</a> |
                                <a href="/Buts/Delete/${but.butId}">Supprimer</a>
                            </td>
                            </tr>`;
            $buts.append($(template));
        }))
        .catch(error => alert("Erreur API"));
}

const connection = new signalR.HubConnectionBuilder().withUrl("/ButHub").build();
connection.start()
    .catch(function (err) { return console.error(tostring()) })

connection.on("NewBut", function () {
    $buts.empty();
    getButs();
});