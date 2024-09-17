using ISW_TP_6.Adapters;
using ISW_TP_6.Entities;
using System;
using System.Collections.Generic;
using System.Data;
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
        private PedidoEnvio PedidoSeleccionado = new();
        private List<Tarjeta> TarjetasValidas = new();
        private StarDrawer _starDrawer;

        public PantallaAceptarCotizacion()
        {
            InitializeComponent();
            _starDrawer = new StarDrawer { Rating = 3 };
            this.DataContext = _starDrawer;
        }

        private void AceptarUserControlLoaded(object sender, RoutedEventArgs e)
        {
            HabilitarMetodos(false);
            VerificarMetodo();
            MetodosPagoDesplegable.ItemsSource = null;
            TiposDocumentoDesplegable.ItemsSource = new List<string>() {"DNI","Pasaporte","Libreta de Familia","Libreta Enrolamiento" };
            PasarelaDesplegable.ItemsSource = new List<string>() { "Paypal", "Banelco", "Pago Facil", "Mercado Pago" };
            TiposDocumentoDesplegable.SelectedIndex = -1;
            UsuarioCargado = BuscarCotizaciones();
            CargarDataGrid();
            CargarTarjetas();
        }
        private void TarjetaNumeroTextBoxTextChanged(object sender, TextChangedEventArgs e) => Generics.MaskedNumber((TextBox)sender, 13);
        private void TarjetaPinTextBoxTextChanged(object sender, TextChangedEventArgs e) => Generics.MaskedNumber((TextBox)sender, 4);
        private void GrillaCotizacionesMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) => SeleccionarCotizacion();
        private void MetodosPagoDesplegableSelectionChanged(object sender, SelectionChangedEventArgs e) => VerificarMetodo();
        private void BotonAceptarCotizacionClick(object sender, RoutedEventArgs e) => AceptarCotizacion();
        private void BotonVerTarjetasClick(object sender, RoutedEventArgs e)=>VerTarjetas();
        private void ButtonClick(object sender, RoutedEventArgs e) => LimpiarTodo();
        private void VerTarjetas()
        {
            TarjetasWindows tarjetasDisponibles = new(TarjetasValidas);
            tarjetasDisponibles.ShowDialog();
            if (tarjetasDisponibles.TarjetaSeleccionada.Numero != 0) AutocompletarCampos(tarjetasDisponibles.TarjetaSeleccionada);
        }
        private void AutocompletarCampos(Tarjeta tar)
        {
            TarjetaNumeroTextBox.Text = tar.Numero.ToString();
            TarjetaPinTextBox.Text = tar.Pin.ToString();
            TarjetaNombreTextBox.Text = tar.NombreCompleto;
            TiposDocumentoDesplegable.Text= tar.TipoDocumento;
            NumeroDocumentoTextBox.Text=tar.NumeroDocumento.ToString();
        }
        private void HabilitarMetodos(bool hab)
        {
            if (hab)
            {
                PayMethodsGrid.IsEnabled = true;
                PayMethodsGrid.Visibility = Visibility.Visible;
                AceptarUserControl.Height = 1143 - 468;
            }
            else
            {
                PayMethodsGrid.IsEnabled = false;
                PayMethodsGrid.Visibility = Visibility.Collapsed;
                AceptarUserControl.Height = (1143 - 668);
            }

        }
        private void CargarDataGrid()
        {
            if (UsuarioCargado != null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Nombre Transportista");
                dt.Columns.Add("Importe");

                foreach (PedidoEnvio pedido in UsuarioCargado.PedidosPresentados)
                {
                    foreach (Cotizacion cotiza in pedido.Cotizaciones)
                    {
                        ListadoCotizaciones.Add(cotiza);

                        string importe = Generics.MaskedFinancial(cotiza.ImporteEnvio);

                        dt.Rows.Add(cotiza.Id, cotiza.Transp.NombreCompleto, importe);

                    }

                }

                GrillaCotizaciones.ItemsSource = dt.DefaultView;
                MetodosPagoDesplegable.ItemsSource = null;
            }
            else MessageBox.Show("ATENCION: Este usuario no \n tiene pedidos asociados", "ATENCION",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void SeleccionarCotizacion()
        {
            if(GrillaCotizaciones.SelectedIndex != -1)
            {
                HabilitarMetodos(true);
                int idSeleecionado = int.Parse(((DataView)GrillaCotizaciones.ItemsSource).Table?.Rows[GrillaCotizaciones.SelectedIndex][0].ToString()??"0");
                CotizacionSeleccionada = ListadoCotizaciones.Find(x=>x.Id== idSeleecionado)??new();
                PedidoSeleccionado = EncontrarPedido(CotizacionSeleccionada);
                CargaLabel.Content = PedidoSeleccionado.Carga;

                TransTextbox.Text = CotizacionSeleccionada.Transp.NombreCompleto;
                NroCotizacionLabel.Content = CotizacionSeleccionada.Id.ToString();
                NroPedidoLabel.Content = PedidoSeleccionado.Id;
                TextBoxRetiro.Text = CotizacionSeleccionada.FechaRetiro.ToShortDateString();
                TextBoxEntrega.Text= CotizacionSeleccionada.FechaEntrega.ToShortDateString();
                _starDrawer.Rating = CotizacionSeleccionada.Transp.Calificacion;       
                this.DataContext = null;  
                this.DataContext = _starDrawer;  

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
                    AceptarUserControl.Height = 1143;
                    MainGrid.Height = 1143;
                }
                else
                {
                    TarjetaGrid.IsEnabled = false;
                    TarjetaGrid.Visibility = Visibility.Collapsed;
                    AceptarUserControl.Height = (1143 - 468);
                }
            }
            else
            {
                TarjetaGrid.IsEnabled = false;
                TarjetaGrid.Visibility = Visibility.Collapsed;
            }
        }
        private void AceptarCotizacion()
        {
            // No payment method selected, show an error message
            if (MetodosPagoDesplegable.SelectedIndex == -1) {
                MessageBox.Show("ERROR: Debe seleccionar una Forma de Pago.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Stop the process
            }

            //Verifica que el pedido este en estado pendiente
            if (PedidoSeleccionado.Estado == "Pendiente")
            {
                if (VerificarPago())
                {
                    PedidoSeleccionado.Estado = "Confirmado";
                    Random random = new Random();
                    string numeroDePago = random.Next(2000, 5000).ToString() + random.Next(10, 99).ToString();

                    MessageBox.Show($"EXITO: El pedido de envio \n paso a Confirmado.\n Su nro de Pago es: {numeroDePago}", "ATENCION", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarTodo();

                    // TODO: Send an email to the user or the transportist ??
                    EmailSender emailSender = new EmailSender("smtp.gmail.com", 587, true, "tomastrangist@gmail.com", "ykck lpst zrwo tmtm");
                    emailSender.SendEmail("83296@sistemas.frc.utn.edu.ar", "Confirmacion de Pago", $"Su pedido de envio ha sido confirmado.\n Su nro de Pago es: {numeroDePago}");
                }
            }
            else MessageBox.Show("ERROR: El Pedido esta en \n estado Confirmado", "ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
        }

        private void LimpiarTodo()
        {
            Generics.CleanFields(PayMethodsGrid);
            Generics.CleanFields(TarjetaGrid);
            HabilitarMetodos(false);
        }
        private bool VerificarPago()
        {
            int index = MetodosPagoDesplegable.SelectedIndex;
            string metodo = CotizacionSeleccionada.FormasHabilitadas[index].Nombre;
            string[] metodos = metodo.Split("Tarjeta");

            //Pago con tarjeta
            if (metodos.Length > 1)
            {
                //Este metodo valida que los campos no sean nulos y haya algo seleccionado en le combo box
                if (Generics.ValidateAllGrid(TarjetaGrid))
                {
                    string nTarjeta = TarjetaNumeroTextBox.Text;
                    string pin = TarjetaPinTextBox.Text;
                    string nombre = TarjetaNombreTextBox.Text;
                    string tipoDoc = TiposDocumentoDesplegable.Text;
                    string doc = NumeroDocumentoTextBox.Text;
                    Tarjeta tar = new();
                    bool esValido = false;
                    foreach (Tarjeta card in TarjetasValidas)
                    {
                        if(card.Numero.ToString() == nTarjeta && 
                            card.Pin.ToString() == pin && 
                            nombre == card.NombreCompleto && 
                            tipoDoc ==card.TipoDocumento &&
                            doc == card.NumeroDocumento.ToString())
                        {
                            esValido = true;
                            tar = card;
                            break;
                        }
                    }
                    // La tarjeta la tomamos como valida
                    if(esValido)
                    {
                        //El saldo alcanza
                        if (tar.Saldo >= CotizacionSeleccionada.ImporteEnvio) return true;
                        else
                        {
                            MessageBox.Show("ERROR: Saldo Insuficiente \n  para el metodo de pago seleccionado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("ERROR: Los datos utilizados \n  no son validos", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("ERROR: Hay datos sin completar \n  para el metodo de pago seleccionado", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }//Paga en efectivo
            else return true;
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
            Transportista trans4 = new(3, "Tomas Agustin Lindo", "Libreta de Enrolamiento", 22364, 3, "Audi A4", "90080@sistemas.frc.utn.edu.ar");
            Transportista trans5 = new(4, "Ignacio Martin", "DNI", 1522364, 3, "Toyota Corolla", "83464@sistemas.frc.utn.edu.ar");

            Cotizacion coti1 = new Cotizacion(1, trans1,
            DateTime.Now,
            DateTime.Now.AddDays(14),85000.00,
            new List<FormasDePago>() { f, f2 });

            Cotizacion coti2 = new Cotizacion(2, trans2,
            DateTime.Now,
            DateTime.Now.AddDays(4), 115000.00,
            new List<FormasDePago>() { f, f2,f3,f4 });

            Cotizacion coti3 = new Cotizacion(3, trans3,
            DateTime.Now,
            DateTime.Now.AddDays(2), 225000.00,
            new List<FormasDePago>() { f, f3, f4 });

            Cotizacion coti4 = new Cotizacion(4, trans4,
            DateTime.Now,
            DateTime.Now.AddDays(20), 65000.00,
            new List<FormasDePago>() { f2, f4 });

            Cotizacion coti6 = new Cotizacion(5, trans5,
            DateTime.Now,
            DateTime.Now.AddDays(6), 105000.00,
            new List<FormasDePago>() { f, f2,f3,f4 });

            PedidoEnvio ped1 = new(0,
                "-31.443207181487363, -64.18105660204746", "-31.404065344149718, -64.20461381267789", 
                DateTime.Now.AddDays(1), DateTime.Now.AddDays(15), "Pendiente", new() {coti1},null,"Ropa Usada");
            PedidoEnvio ped2 = new(1, "-31.651884876109598, -64.42038211678525", "-31.370289013341633, -64.22189966941868",
                DateTime.Now, DateTime.Now.AddDays(6), "Pendiente", new() { coti3, coti2 }, null,"Muebles Pino");
            PedidoEnvio ped3 = new(2, "-31.443207181487363, -64.18105660204746", "-31.651884876109598, -64.42038211678525", 
                DateTime.Now.AddDays(5), DateTime.Now.AddDays(30), "Pendiente", new() { coti4, coti6 }, null,"Mercancia");

            DadorDeCarga dado = new("Ignacio Nayi",new(),"Libreta Familiar", 351694384, "bokitalover34@gmail.com", new() { ped1 ,ped2,ped3});
            return dado;
        }
        private void CargarTarjetas()
        {
            Tarjeta t1 = new() { 
                NombreCompleto = "Genaro Rafael Bergesio",
                Numero = 55554444,
                Pin = 4455,
                NumeroDocumento=5555555,
                Saldo= 122555.5,
                TipoDocumento= "DNI",
                Tipo = "Debito"
            };
            Tarjeta t2 = new()
            {
                NombreCompleto = "Ignacion Nayi",
                Numero = 11112222,
                Pin = 3333,
                NumeroDocumento = 2222222,
                Saldo = 235.5,
                TipoDocumento = "DNI",
                Tipo = "Credito"
            };
            Tarjeta t3 = new()
            {
                NombreCompleto = "Pablito Lescano",
                Numero = 9666668,
                Pin = 1111,
                NumeroDocumento = 788896,
                Saldo = 4225555.5,
                TipoDocumento = "DNI",
                Tipo = "Credito"
            };
            Tarjeta t4 = new()
            {
                NombreCompleto = "Matias Moreno",
                Numero = 3333441,
                Pin = 6666,
                NumeroDocumento = 123123,
                Saldo = 5568.5,
                TipoDocumento = "Libreta de Familia",
                Tipo = "Debito"
            };
            TarjetasValidas = new() {
                t1,t2,t3,t4 
            };
        }

        private PedidoEnvio EncontrarPedido(Cotizacion cotiza)
        {
            foreach (PedidoEnvio pedido in UsuarioCargado.PedidosPresentados)
            {
                foreach (Cotizacion cot in pedido.Cotizaciones)
                {
                    if (cot == cotiza) return pedido;
                }
            }
            return new();
        }
        #endregion

    }
}
