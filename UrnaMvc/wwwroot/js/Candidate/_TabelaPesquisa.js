$(document).ready(function () {
    //#region Exclusão de um candidato
    $('#ModalExcluirBotaoExcluir').click(function () {
        var id = $('#CodigoCandidatoExcluir');

        $.ajax({
            url: '/Candidate/candidate',
            type: 'DELETE',
            data: id,
            success: function (data) {
                $('#ModalExcluir').modal('hide');

                if (data != 'SUCESSO') {
                    ('#ValidacaoGeral').html('* ' + data);
                    $('#ValidacaoGeral').css('visibility', 'visible');
                    window.scrollTo({
                        top: 0,
                        behavior: 'smooth'
                    });
                }
                else {
                    $('#ValidacaoGeral').css('visibility', 'hidden');
                }
            },
            error: function () {
                ('#ValidacaoGeral').html('* Falha ao gravar. Contate o suporte!');
                $('#ValidacaoGeral').css('visibility', 'visible');
                window.scrollTo({
                    top: 0,
                    behavior: 'smooth'
                });
            }
        });
    })
    //#endregion
});