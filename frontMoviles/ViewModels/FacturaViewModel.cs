using frontMoviles.Models;
using frontMoviles.Validations.Base;
using frontMoviles.Validations.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace frontMoviles.ViewModels
{
    public class FacturaViewModel : ViewModelBase
    {
        #region Properties

        private ObservableCollection<PlatoModel> lista_plates;
        public List<PlatoModel> PlatosListado { get; set; }
        public ValidatableObject<string> NombreCliente { get; set; }  //Campo de Busqueda
        public ValidatableObject<string> Cedula { get; set; }
        public ValidatableObject<float> Descuento { get; set; }
        public ValidatableObject<int> Valor { get; set; }

        private ObservableCollection<FacturaModel> factura;

        private List<FacturaModel> listaBill;

        private FacturaModel bill;

        public ICommand FacturarCommand { get; set; }

        public ICommand AtrasCommand { get; set; }
        #endregion Properties
        public ObservableCollection<FacturaModel> Factura
        {
            get { return factura; }
            set
            {
                factura = value;
                OnPropertyChanged();
            }
        }
        public List<FacturaModel> ListaBill
        {
            get { return listaBill; }
            set
            {
                listaBill = value;
                OnPropertyChanged();
            }
        }


        public FacturaModel Bill
        {
            get { return bill; }
            set
            {
                bill = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<PlatoModel> Lista_plates
        {
            get { return lista_plates; }
            set
            {
                lista_plates = value;
                OnPropertyChanged();
            }
        }

        #region initialize
        public FacturaViewModel(ObservableCollection<PlatoModel> listPlates, List<PlatoModel> enlistamiento)
        {
            ListaBill = new List<FacturaModel>();
            Factura = new ObservableCollection<FacturaModel>();
            Bill = new FacturaModel();
            PlatosListado = new List<PlatoModel>();
            PlatosListado = enlistamiento;
            InitializeFields(listPlates);
        }

        public void InitializeFields(ObservableCollection<PlatoModel> listPlate)
        {
            NombreCliente = new ValidatableObject<string>();
            Cedula = new ValidatableObject<string>();
            Descuento = new ValidatableObject<float>();
            Valor = new ValidatableObject<int>();

            NombreCliente.Validations.Add(new RequiredRule<string> { ValidationMessage = "El nombre es Obligatorio" });            
            Cedula.Validations.Add(new RequiredRule<string> { ValidationMessage = "La cedula es Obligatoria" });
            Descuento.Validations.Add(new RequiredRule<float> { ValidationMessage = "El descuento es Obligatorio" });
            Valor.Validations.Add(new RequiredRule<int> { ValidationMessage = "El valor es Obligatoria" });
            FacturarCommand = new Command(async () => await generarFactura(listPlate), () => true);
            AtrasCommand = new Command(async () => await Volver(), () => true);
        }
        #endregion initialize
        public async Task generarFactura(ObservableCollection<PlatoModel> listPlate)
        {
            Lista_plates = listPlate;
            Bill.Cliente = NombreCliente.Value;
            Bill.Cedula = Cedula.Value;
            Bill.Descuento = Descuento.Value;
            Bill.FechaFactura = DateTime.Now;
            float desc = Bill.Descuento / 100;
            float total = 0;
            foreach (PlatoModel plato in PlatosListado)
            {
                total = total + plato.Valor;
            }
            float cuenta = total - (total * desc);
            Bill.ValorTotal = cuenta;
            ListaBill.Add(Bill);
            
            Factura = new ObservableCollection<FacturaModel>(ListaBill);
        }
        public async Task Volver()
        {
            await NavigationService.PopPage();
        }
    }
}
