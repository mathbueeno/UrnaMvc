$(document).ready(function () {
    $.get('/Voting/votes').done(function (data) {
        if (data != '[]') {
            var listaApuracao = JSON.parse(data);
            const containerApuracao = document.getElementById('ContainerApuracao');

            for (var i = 0; i < listaApuracao.length; i++) {
                const divRow = document.createElement('div');
                divRow.className = 'row';

                const divCol3 = document.createElement('div');
                divCol3.className = 'col-3';

                const pCandidato = document.createElement('p');
                pCandidato.className = 'h6 text-right';
                pCandidato.innerHTML = listaApuracao[i].NomeCompleto != 'Votos brancos e nulos' ? 'Candidato: ' + listaApuracao[i].NomeCompleto : listaApuracao[i].NomeCompleto;

                const divQtdVotos = document.createElement('div');
                divQtdVotos.className = 'd-flex flex-row-reverse';

                const badgeQtdVotos = document.createElement('span');
                badgeQtdVotos.className = 'badge badge-primary text-right';
                badgeQtdVotos.innerHTML = listaApuracao[i].QtdVotos + ' votos';

                const divCol9 = document.createElement('div');
                divCol9.className = 'col-9';

                const divProgress = document.createElement('div');
                divProgress.className = 'progress';

                const divProgressBar = document.createElement('div');
                divProgressBar.className = 'progress-bar';
                divProgressBar.setAttribute('role', 'progressbar');
                divProgressBar.setAttribute('aria-valuenow', listaApuracao[i].PercentualVotos);
                divProgressBar.setAttribute('aria-valuemin', "0");
                divProgressBar.setAttribute('aria-valuemax', "100");
                console.log(listaApuracao[i].PercentualVotos + '%;');
                divProgressBar.style.width = listaApuracao[i].PercentualVotos + '%';
                divProgressBar.innerHTML = listaApuracao[i].PercentualVotos + '%';


                divCol3.appendChild(pCandidato);

                if (listaApuracao[i].NomeVice) {
                    const pVice = document.createElement('p');
                    pVice.className = 'text-right m-0';
                    pVice.innerHTML = 'Vice: ' + listaApuracao[i].NomeVice;
                    divCol3.appendChild(pVice);
                }

                divQtdVotos.appendChild(badgeQtdVotos);
                divCol3.appendChild(divQtdVotos);
                divRow.appendChild(divCol3);

                divProgress.appendChild(divProgressBar);
                divCol9.appendChild(divProgress);
                divRow.appendChild(divCol9);

                containerApuracao.appendChild(divRow);
                containerApuracao.appendChild(document.createElement('hr'));
            }
        }
        else {
            $('#ContainerApuracao').append('<p class="h4 text-muted text-center">Nenhum voto para ser apurado!</p>');
        }
    })
});