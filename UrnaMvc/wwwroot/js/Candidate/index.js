//#region Abertura do modal de exclusão
function AbrirModalExclusao(id) {
    $('#CodigoCandidatoExcluir').val(id);
    $('#ModalExcluir').modal('show');
}
//#endregion

$(document).ready(function () {
    Pesquisar();

    //#region Realizar a pesquisa novamente sempre que o modal de exclusão for fechado
    $(document).on('hidden.bs.modal', '.modal', function (e) {
        if (e.target.id == 'ModalExcluir') {
            Pesquisar();
        }
    });
    //#endregion

    //#region Validação e gravação do registro
    $('#BotaoCadastrar').click(function () {
        //#region Captação e validação dos dados
        var NomeCompleto = $('#NomeCompleto').val();
        var NomeVice = $('#NomeVice').val();
        var Legenda = $('#Legenda').val();
        var contadorErros = 0;

        if (!NomeCompleto) {
            $('#ValidacaoNomeCompleto').html('* Preenchimento obrigatório');
            $('#ValidacaoNomeCompleto').css('visibility', 'visible');
            contadorErros++;
        }
        else {
            $('#ValidacaoNomeCompleto').css('visibility', 'hidden');
        }

        if (!NomeVice) {
            $('#ValidacaoNomeVice').html('* Preenchimento obrigatório');
            $('#ValidacaoNomeVice').css('visibility', 'visible');
            contadorErros++;
        }
        else {
            $('#ValidacaoNomeVice').css('visibility', 'hidden');
        }

        if (!Legenda) {
            $('#ValidacaoLegenda').html('* Preenchimento obrigatório');
            $('#ValidacaoLegenda').css('visibility', 'visible');
            contadorErros++;
        }
        else {
            Legenda = parseInt(Legenda);
            if (Legenda < 10) {
                $('#ValidacaoLegenda').html('* Preenchimento inválido, a legenda não pode começar com zero');
                $('#ValidacaoLegenda').css('visibility', 'visible');
                contadorErros++;
            }
            else {
                $('#ValidacaoLegenda').css('visibility', 'hidden');
            }
        }
        //#endregion

        //#region Gravação do registro
        if (contadorErros === 0) {
            var candidate = {
                NomeCompleto,
                NomeVice,
                Legenda
            };

            $.post('/Candidate/candidate', { candidateJson: JSON.stringify(candidate) }).done(function (data) {
                if (data === 'SUCESSO') {
                    $('#ValidacaoGeral').css('visibility', 'hidden');
                    $('#BotaoCancelar').trigger('click');
                    Pesquisar();
                }
                else {
                    $('#ValidacaoGeral').html('* ' + data);
                    $('#ValidacaoGeral').css('visibility', 'visible');
                    window.scrollTo({
                        top: 0,
                        behavior: 'smooth'
                    });
                }
            });
        }
        //#endregion

    });
    //#endregion

    //#region Limpar campos ao abrir ou fechar o collapse
    function LimparCampos() {
        $('#NomeCompleto').val("");
        $('#NomeVice').val("");
        $('#Legenda').val("");
    }

    $('#BotaoCancelar').click(LimparCampos);
    $('#BotaoAdicionar').click(LimparCampos);
    //#endregion

    //#region Inserção de máscara no campo de legenda
    $('#Legenda').mask('00', { reverse: true });
    //#endregion

    //#region Função visual para atribuir o foco ao campo após clicar em adicionar
    $('#BotaoAdicionar').click(function () {
        window.setTimeout(function () {
            $('#NomeCompleto').focus();
        }, 500);
    });
    //#endregion

    //#region Funções usadas para disparar a pesquisa de candidatos
    $('#TermoPesquisa').on('input', delay(function () {
        Pesquisar();
    }));

    function Pesquisar() {
        var descricao = $('#TermoPesquisa').val();
        var area = $('#Tabela');
        $.get('/Candidate/Pesquisar', { descricao }).done(function (data) {
            area.html(data);
        })
    }

    function delay(f, delay) {
        var timer = null;
        return function () {
            var context = this, args = arguments;
            clearTimeout(timer);
            timer = window.setTimeout(function () {
                f.apply(context, args);
            },
                delay || 1000);
        };
    }
    //#endregion
});