using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppRpgEtec.Models;
using AppRpgEtec.Services.Personagens;

namespace AppRpgEtec.ViewModels.Disputas
{
    public class DisputaViewModel : BaseViewModel
    {
        private PersonagemService pService;
        public ObservableCollection<Personagem> PersonagensEncontrados { get; set; }
        public Personagem Atacante { get; set; }
        public Personagem Oponente { get; set; }
        public DisputaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);
            Atacante = new Personagem();
            Oponente = new Personagem();
            PersonagensEncontrados = new ObservableCollection<Personagem>();
            PesquisarPersonagensCommand =
                new Command<string>(async (string pesquisa) => { await PesquisarPersonagens(pesquisa); });
        }
        public ICommand PesquisarPersonagensCommand { get; set; }
        public async Task PesquisarPersonagens(string textoPesquisapersonagem)
        {
            try
            {
                PersonagensEncontrados = await pService.GetByNomeAproximadoAsync(textoPesquisapersonagem);
                OnPropertychanged(nameof(PersonagensEncontrados));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes:" + ex.InnerException, "Ok");
            }
        }
        private void OnPropertychanged(string v)
        {
            throw new NotImplementedException();
        }

        public string DescricaoPersonagemAtacante
        {
            get => Atacante.Nome;
        }
        public string DescricaoPersonagemOponente
        {
            get => Oponente.Nome;
        }
        public async void SelecionarPersonagem(Personagem p)
        {
            try
            {
                string tipoCombatente = await Application.Current.MainPage
                    .DisplayActionSheet("Atacante ou Oponente?", "Cancelar", "", "Atacante", "Oponente");
                if (tipoCombatente == "Atacante")
                {
                    Atacante = p;
                    OnPropertychanged(nameof(DescricaoPersonagemAtacante));
                }        
                else if (tipoCombatente == "Oponente")
                {
                        Oponente = p;
                        OnPropertychanged(nameof(DescricaoPersonagemOponente));
                } 
                    
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes" + ex.InnerException, "Ok");
            }
    }

    private Personagem personagemSelecionado;
    public Personagem PersonagemSelecionado        
        {
            set
            {
                if (value != null)
                {
                    personagemSelecionado = value;
                    SelecionarPersonagem(personagemSelecionado);
                    OnPropertyChanged();
                    PersonagensEncontrados.Clear();
                }
            }
        }

       

        private string textoBuscaDigitado = string.Empty;
        public string TextoBuscaDigitado
        {
            get {
                return textoBuscaDigitado; }
            set
            {
                if ((value != null && !string.IsNullOrEmpty(value) && value.Length > 0))
                {
                    textoBuscaDigitado = value;
                    _ = PesquisarPersonagens(TextoBuscaDigitado);
                }
                else
                {
                    PersonagensEncontrados.Clear();
                }
            }



        }
    } 
}

