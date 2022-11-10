$(document).ready(function () {
    var candidatoEscolhido = null;
    var votoFinalizado = false;

    //#region Botão Corrige
    $('#BotaoCorrige').click(function () {
        $('#AreaRenderizarNomeCandidato').html('');
        $('#AreaRenderizarNomeVice').html('');
        $('#PrimeiroDigitoLegenda').val('');
        $('#SegundoDigitoLegenda').val('');
        votoFinalizado = false;
        candidatoEscolhido = null;
    });
    //#endregion

    //#region Atribuição dos eventos de clique dos botões da urna
    $('button[name="BotaoUrna"]').click(function () {
        if (votoFinalizado == false) {
            var textoBotao = $(this).html();
            if (textoBotao == 'BRANCO') {
                $('#AreaRenderizarNomeCandidato').html('Voto em branco');
                $('#AreaRenderizarNomeVice').html('');
                candidatoEscolhido = null;
                votoFinalizado = true;
            }
            else {
                var primeiroDigito = $('#PrimeiroDigitoLegenda').val();
                var segundoDigito = $('#SegundoDigitoLegenda').val();

                if (!primeiroDigito) {
                    $('#PrimeiroDigitoLegenda').val(textoBotao);
                }
                else if (!segundoDigito) {
                    votoFinalizado = true;
                    $('#SegundoDigitoLegenda').val(textoBotao);
                    primeiroDigito = primeiroDigito.trim();
                    segundoDigito = segundoDigito.trim();

                    var tecla = primeiroDigito.concat(textoBotao);

                    $.get('/Candidate/recuperarPorLegenda', { tecla }).done(function (data) {
                        if (data == 'Voto nulo') {
                            $('#AreaRenderizarNomeCandidato').html(data);
                            $('#AreaRenderizarNomeVice').html('');
                            candidatoEscolhido = null;
                        }
                        else {
                            var candidate = JSON.parse(data);
                            $('#AreaRenderizarNomeCandidato').html(candidate.NomeCompleto);
                            $('#AreaRenderizarNomeVice').html(candidate.NomeVice);
                            candidatoEscolhido = candidate;
                        }
                    });
                }
            }
        }
    })
    //#endregion

    //#region Clique do botão Confirmar
    $('#BotaoConfirma').click(function () {
        if (votoFinalizado) {
            if (candidatoEscolhido != null) {
                var candidatoJson = JSON.stringify(candidatoEscolhido);
            }
            else {
                var candidatoJson = '';
            }

            $.post('/Voting/vote', { candidatoJson }).done(function (data) {
                $('#BotaoCorrige').trigger('click');
            });
        }
    });
    //#endregion
});