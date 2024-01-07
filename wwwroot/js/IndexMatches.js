const url = "/api/MatchesAPI";
let $matches = $("#matches");
getMatches();
function getMatches() {
    fetch(url)
        .then(response => response.json())
        .then(data => data.forEach(match => {
            let template = `<tr>
                                <td>${match.equipe1}</td>
                                <td>${match.equipe2}</td>
                                <td>${match.score}</td>
                                <td>${match.temps}'</td>
                                <td>
                                    <a href="/Matches/Edit/${match.matchId}">Éditer</a> |
                                    <a href="/Matches/Details/${match.matchId}">Détails</a> |
                                    <a href="/Matches/Delete/${match.matchId}">Supprimer</a>
                                </td>
                            </tr>`;
            $matches.append($(template));
        }))
        .catch(error => alert("Erreur API"));
}

const connection = new signalR.HubConnectionBuilder().withUrl("/MatchHub").build();
connection.start()
    .catch(function (err) { return console.error(err.tostring()) })

connection.on("NewMatch", function () {
    $matches.empty();
    getMatches();
});