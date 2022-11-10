using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UrnaMvc.Interfaces;
using UrnaMvc.Models;
using UrnaMvc.ViewModels;

namespace UrnaMvc.Controllers
{
    public class VotingController : Controller
    {
        private IVotingRepositorio _IVotingRepositorio;
        private ICandidateRepositorio _ICandidateRepositorio;

        public VotingController(IVotingRepositorio IVotingRepositorio, ICandidateRepositorio ICandidateRepositorio)
        {
            _IVotingRepositorio = IVotingRepositorio;
            _ICandidateRepositorio = ICandidateRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Apuracao()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> vote(string candidatoJson)
        {
            Voting voting = new Voting();
            voting.DataVoto = DateTime.Now;

            if (!string.IsNullOrEmpty(candidatoJson))
            {
                Candidate candidate = JsonConvert.DeserializeObject<Candidate>(candidatoJson);
                voting.CandidateId = candidate.Id;
            }

            await _IVotingRepositorio.Inserir(voting);
            return Json("SUCESSO");
        }

        [HttpGet]
        public async Task<JsonResult> votes()
        {
            int totalVotosApurados = 0;
            List<ApuracaoViewModel> listaVotosApurados = new List<ApuracaoViewModel>();
            List<Candidate> listaCandidatos = await _ICandidateRepositorio.ListarTodos();

            if (listaCandidatos != null && listaCandidatos.Count > 0)
            {
                ApuracaoViewModel apuracaoViewModel = new ApuracaoViewModel();
                apuracaoViewModel.NomeCompleto = "Votos brancos e nulos";
                apuracaoViewModel.QtdVotos = _IVotingRepositorio.RecuperarQtdVotos(null);
                listaVotosApurados.Add(apuracaoViewModel);

                totalVotosApurados += apuracaoViewModel.QtdVotos;

                foreach (Candidate candidate in listaCandidatos)
                {
                    apuracaoViewModel = new ApuracaoViewModel();
                    apuracaoViewModel.NomeCompleto = candidate.NomeCompleto;
                    apuracaoViewModel.NomeVice = candidate.NomeVice;
                    apuracaoViewModel.QtdVotos = candidate.ListaVotos != null ? candidate.ListaVotos.Count : 0;
                    listaVotosApurados.Add(apuracaoViewModel);
                    totalVotosApurados += apuracaoViewModel.QtdVotos;
                }

                foreach (ApuracaoViewModel votosApurados in listaVotosApurados)
                {
                    if (votosApurados.QtdVotos > 0)
                    {
                        double percentual = Convert.ToDouble(votosApurados.QtdVotos) * 100.0 / Convert.ToDouble(totalVotosApurados);
                        votosApurados.PercentualVotos = Math.Round(percentual, 2);
                    }
                }

                listaVotosApurados = listaVotosApurados.OrderByDescending(x => x.QtdVotos).ToList();

            }

            return Json(JsonConvert.SerializeObject(listaVotosApurados));
        }
    }
}
