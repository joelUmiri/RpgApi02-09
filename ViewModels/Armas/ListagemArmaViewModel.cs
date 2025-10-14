//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using AppRpgEtec.Models;
//using AppRpgEtec.Services.Armas;

//namespace AppRpgEtec.ViewModels.Armas
//{
//    class ListagemArmaViewModel : BaseViewModel
//    {
//        private ArmaService aService;
//        public ObservableCollection<Arma> Personagens { get; set; }
//        public ListagemArmaViewModel()
//        {
//            string token = Preferences.Get("UsuarioToken", string.Empty);
//            aService = new ArmaService(token);
//            Arma = new ObservableCollection<Arma>();

//            _ = ObterArmas();

//            NovaArmaCommand = new Command(async () => { await ExibirCadastroArma(); });
//            RemoverArmaCommand =
//                new Command<Arma>(async (Arma a) => { await RemoverArma(a); });
//        }
//        public ICommand NovaArmaCommand { get; }
//        public ICommand RemoverArmaCommand { get; set; }


//        public async Task ObterArmas()
//        {
//            try
//            {
//                Personagens = await aService.GetArmasAsync();
//                OnPropertyChanged(nameof(Personagens));
//            }
//            catch (Exception ex)
//            {
//                await Application.Current.MainPage
//                    .DisplayAlert("Ops", ex.Message + " Detalhes " + ex.InnerException, "Ok");
//            }
//        }

//        public async Task ExibirCadastroArma()
//        {
//            try
//            {
//                await Shell.Current.GoToAsync("cadPersonagemView");
//            }
//            catch (Exception ex)
//            {
//                await Application.Current.MainPage
//                    .DisplayAlert("ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
//            }
//        }

//        private Arma ArmaSelecionada;

//        public Arma ArmaSelecionada
//        {
//            get { return ArmaSelecionada; }
//            set
//            {
//                if (value != null)
//                {
//                    ArmaSelecionada = value;

//                    Shell.Current
//                        .GoToAsync($"cadArmaView?pId={ArmaSelecionada.Id}");
//                }
//            }
//        }

//        public async Task RemoverArma(Arma a)
//        {
//            try
//            {
//                if (await Application.Current.MainPage
//                    .DisplayAlert("Confirmação", $"Confirma a remoção de {a.Nome}?", "Sim", "Não"))
//                {
//                    await aService.RemoverArmaAsync(a.Id);

//                    await Application.Current.MainPage.DisplayAlert("Mensagem",
//                        "Personagem removido com sucesso!", "Ok");

//                    _ = ObterArmas();
//                }
//            }
//            catch (Exception ex)
//            {
//                await Application.Current.MainPage
//                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
//            }
//        }
//    }
//}
