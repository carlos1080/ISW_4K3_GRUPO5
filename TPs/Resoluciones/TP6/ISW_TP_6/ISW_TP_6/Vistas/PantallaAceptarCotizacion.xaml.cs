using ISW_TP_6.Adapters;
using ISW_TP_6.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Windows;
using System.Windows.Controls;

namespace ISW_TP_6.Vistas
{
    /// <summary>
    /// Lógica de interacción para PantallaAceptarCotizacion.xaml
    /// </summary>
    public partial class PantallaAceptarCotizacion : UserControl
    {
        private DadorDeCarga? UsuarioCargado;
        private List<Cotizacion> ListadoCotizaciones = new();
        private Cotizacion CotizacionSeleccionada = new();
        public PantallaAceptarCotizacion()
        {
            InitializeComponent();
        }

        private void AceptarUserControlLoaded(object sender, RoutedEventArgs e)
        {
            PayMethodsGrid.IsEnabled = false;
            PayMethodsGrid.Visibility = Visibility.Collapsed;
            TarjetaGrid.IsEnabled = false;
            TarjetaGrid.Visibility = Visibility.Collapsed;
            MetodosPagoDesplegable.ItemsSource = null;
            UsuarioCargado = BuscarCotizaciones();
            CargarDataGrid();
        }
        private void TarjetaNumeroTextBoxTextChanged(object sender, TextChangedEventArgs e) => Generics.MaskedNumber((TextBox)sender, 13);
        private void TarjetaPinTextBoxTextChanged(object sender, TextChangedEventArgs e) => Generics.MaskedNumber((TextBox)sender, 4);
        private void GrillaCotizacionesMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) => SeleccionarCotizacion();
        private void MetodosPagoDesplegableSelectionChanged(object sender, SelectionChangedEventArgs e) => VerificarMetodo();

        private void CargarDataGrid()
        {
            if (UsuarioCargado != null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Nombre Transportista");
                dt.Columns.Add("Calificacion");
                dt.Columns.Add("Fecha Retiro");
                dt.Columns.Add("Fecha Llegada");
                dt.Columns.Add("Importe");
                dt.Columns.Add("Formas de Pago");
                foreach (PedidoEnvio pedido in UsuarioCargado.PedidosPresentados)
                {
                    foreach (Cotizacion cotiza in pedido.Cotizaciones)
                    {
                        ListadoCotizaciones.Add(cotiza);
                        string formasPago = "";
                        foreach (FormasDePago forma in cotiza.FormasHabilitadas)
                        {
                            formasPago += forma.Nombre +" || ";
                        }
                        string importe = Generics.MaskedFinancial(cotiza.ImporteEnvio);
                        string calificacion = cotiza.Transp.Calificacion + "/5";
                        dt.Rows.Add(cotiza.Id, cotiza.Transp.NombreCompleto, 
                            calificacion, cotiza.FechaRetiro.ToShortDateString(), 
                            cotiza.FechaEntrega.ToShortDateString(), importe, formasPago);

                    }

                }

                GrillaCotizaciones.ItemsSource = dt.DefaultView;
                MetodosPagoDesplegable.ItemsSource = null;
            }
            else MessageBox.Show("ATENCION: Este usuario no tiene pedidos asociados","ATENCION",MessageBoxButton.OK,MessageBoxImage.Information);
        }
        private void SeleccionarCotizacion()
        {
            if(GrillaCotizaciones.SelectedIndex != -1)
            {
                PayMethodsGrid.IsEnabled = true;
                PayMethodsGrid.Visibility = Visibility.Visible;
                int idSeleecionado = int.Parse(((DataView)GrillaCotizaciones.ItemsSource).Table?.Rows[GrillaCotizaciones.SelectedIndex][0].ToString()??"0");
                CotizacionSeleccionada = ListadoCotizaciones.Find(x=>x.Id== idSeleecionado)??new();
                TransTextbox.Text = CotizacionSeleccionada.Transp.NombreCompleto;
                NroCotizacionLabel.Content = CotizacionSeleccionada.Id.ToString();

                if (CotizacionSeleccionada.FormasHabilitadas.Count > 0)
                {
                    DataTable pagos = new();
                    pagos.Columns.Add("Nombre");
                    foreach (var pa in CotizacionSeleccionada.FormasHabilitadas) pagos.Rows.Add(pa.Nombre);
                    MetodosPagoDesplegable.ItemsSource = CotizacionSeleccionada.FormasHabilitadas;
                    MetodosPagoDesplegable.DisplayMemberPath = "Nombre";
                    MetodosPagoDesplegable.SelectedIndex = -1;
                }

            }
        }
        private void VerificarMetodo()
        {
            if(CotizacionSeleccionada.FormasHabilitadas.Count > 0 && MetodosPagoDesplegable.SelectedIndex != -1)
            {
                int index = MetodosPagoDesplegable.SelectedIndex;
                string metodo = CotizacionSeleccionada.FormasHabilitadas[index].Nombre;
                string[] metodos = metodo.Split("Tarjeta");
                if (metodos.Length > 1)
                {
                    TarjetaGrid.IsEnabled = true;
                    TarjetaGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    TarjetaGrid.IsEnabled = false;
                    TarjetaGrid.Visibility = Visibility.Collapsed;
                }
            }
            else TarjetaGrid.Visibility = Visibility.Collapsed;
        }
        #region Persistencia
        private DadorDeCarga BuscarCotizaciones()
        {
            FormasDePago f = new("Tarjeta Debito",true);
            FormasDePago f2 = new("Tarjeta Credito", true);
            FormasDePago f3 = new("Contado al Retirar", true);
            FormasDePago f4 = new("Contado Contra Entrega", true);

            Transportista trans1 = new(0,"Marcelo Franco Cuebruno","Pasaporte",11235813,5,"Fiat Saveiro", "83296@sistemas.frc.utn.edu.ar");
            Transportista trans2 = new(1, "Genaro 'Rafita' Bergesio", "DNI", 55122224, 2, "Peugeot 205", "83464@sistemas.frc.utn.edu.ar");
            Transportista trans3 = new(2, "Octavio 'Bocta' Cavalleris", "DNI", 22555224, 4, "Fiat 600", "90052@sistemas.frc.utn.edu.ar");
            Transportista trans4 = new(2, "Tomas Agustin Lindo", "Libreta de Enrolamiento", 22364, 3, "Audi A4", "90080@sistemas.frc.utn.edu.ar");

            Cotizacion coti1 = new Cotizacion(0, trans1,
            DateTime.Now,
            DateTime.Now.AddDays(14),85000.00,
            new List<FormasDePago>() { f, f2 });

            Cotizacion coti2 = new Cotizacion(1, trans2,
            DateTime.Now,
            DateTime.Now.AddDays(4), 115000.00,
            new List<FormasDePago>() { f, f2,f3,f4 });

            Cotizacion coti3 = new Cotizacion(2, trans3,
            DateTime.Now,
            DateTime.Now.AddDays(2), 225000.00,
            new List<FormasDePago>() { f, f3, f4 });

            Cotizacion coti4 = new Cotizacion(3, trans4,
            DateTime.Now,
            DateTime.Now.AddDays(20), 65000.00,
            new List<FormasDePago>() { f2, f4 });

            PedidoEnvio ped1 = new(0,
                "-31.443207181487363, -64.18105660204746", "-31.404065344149718, -64.20461381267789", 
                DateTime.Now.AddDays(1), DateTime.Now.AddDays(15), "Pendiente", new() {coti1},null);
            PedidoEnvio ped2 = new(1, "-31.651884876109598, -64.42038211678525", "-31.370289013341633, -64.22189966941868", DateTime.Now, DateTime.Now.AddDays(6), "Pendiente", new() { coti3, coti2 }, null);
            PedidoEnvio ped3 = new(2, "-31.443207181487363, -64.18105660204746", "-31.651884876109598, -64.42038211678525", DateTime.Now.AddDays(5), DateTime.Now.AddDays(30), "Pendiente", new() { coti4 }, null);

            DadorDeCarga dado = new("Ignacio Nayi",new(),"Libreta Familiar", 351694384, "bokitalover34@gmail.com", new() { ped1 ,ped2,ped3});
            return dado;
        }

        #endregion

    }
}
