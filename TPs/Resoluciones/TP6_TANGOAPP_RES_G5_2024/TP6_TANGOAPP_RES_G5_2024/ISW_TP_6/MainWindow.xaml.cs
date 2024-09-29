using TP6_TANGOAPP_RES_G5_2024.Vistas;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TP6_TANGOAPP_RES_G5_2024
{
    /// <summary>
    /// Corresponde a la parte logica de la pantalla .xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Atributos
        private UserControl? Pantalla;
        #endregion

        #region Iniciador
        public MainWindow()
        {
            InitializeComponent();
            CerrarNotificacion();
        }
        #endregion

        #region Capa Presentacion
        private void BotonAceptarCotizacionClick(object sender, RoutedEventArgs e) => HabilitarPantallaCotizacion();

        private void BotonMenuPrincipalClick(object sender, RoutedEventArgs e) => VolverMenuPrincipal();

        private void BotonPublicarEnvioClick(object sender, RoutedEventArgs e) => UserNoImplementada();
        private void TitleGridMouseEnter(object sender, System.Windows.Input.MouseEventArgs e) => CerrarNotificacion();
        #endregion

        #region Capa Logica
        private void HabilitarPantallaCotizacion()
        {
            Pantalla = new PantallaAceptarCotizacion(this);
            FormGrid.Content = null;
            FormGrid.Content = Pantalla;
        }
        private void VolverMenuPrincipal()
        {
            FormGrid.Content = null;
            Pantalla = null;
        }
        public void MostrarNotificacion(string mensaje)
        {
            BotonNotificacion.Visibility = Visibility.Visible;
            LabelNotificacion.Content = mensaje;
        }
        private void CerrarNotificacion()
        {
            BotonNotificacion.Visibility= Visibility.Collapsed;
        }
        private void UserNoImplementada()=>
            MessageBox.Show("ATENCION: Esta funcionalidad no esta implementada", "Atencion", MessageBoxButton.OK, MessageBoxImage.Information);
        #endregion
    }
}