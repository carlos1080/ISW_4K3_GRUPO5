using ISW_TP_6.Vistas;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ISW_TP_6
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
        }
        #endregion

        #region Capa Presentacion
        private void BotonAceptarCotizacionClick(object sender, RoutedEventArgs e) => HabilitarPantallaCotizacion();

        private void BotonMenuPrincipalClick(object sender, RoutedEventArgs e) => VolverMenuPrincipal();

        private void BotonPublicarEnvioClick(object sender, RoutedEventArgs e) => UserNoImplementada();
        #endregion

        #region Capa Logica
        private void HabilitarPantallaCotizacion()
        {
            Pantalla = new PantallaAceptarCotizacion();
            FormGrid.Content = null;
            FormGrid.Content = Pantalla;
        }
        private void VolverMenuPrincipal()
        {
            FormGrid.Content = null;
            Pantalla = null;
        }

        private void UserNoImplementada()=>
            MessageBox.Show("ATENCION: Esta funcionalidad no esta implementada", "Atencion", MessageBoxButton.OK, MessageBoxImage.Information);
        #endregion
    }
}