using AppRpgEtec.Models;
using AppRpgEtec.ViewModels.Disputas;

namespace AppRpgEtec.Views.Disputas;

public partial class ListagemView : ContentPage
{
	DisputaViewModel ViewModel;
	public ListagemView()
	{
		ViewModel = new DisputaViewModel();
		BindingContext = ViewModel;
	}
}