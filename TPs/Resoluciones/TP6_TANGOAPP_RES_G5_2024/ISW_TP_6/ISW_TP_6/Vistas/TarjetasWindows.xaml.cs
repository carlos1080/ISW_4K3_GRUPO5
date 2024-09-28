using ISW_TP_6.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ISW_TP_6.Vistas
{
    /// <summary>
    /// Lógica de interacción para TarjetasWindows.xaml
    /// </summary>
    public partial class TarjetasWindows : Window
    {
        private List<Tarjeta> TarjetasValidas = new();
        public Tarjeta TarjetaSeleccionada = new();
        public TarjetasWindows(List<Tarjeta> tarjetas)
        {
            InitializeComponent();
            TarjetasValidas = tarjetas;
        }

        private void BotonCerrarClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void TarjetasDataGridMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(TarjetasDataGrid.SelectedIndex != -1) {
                TarjetaSeleccionada = TarjetasValidas[TarjetasDataGrid.SelectedIndex];
                this.Close();
            }
        }
        private void VentanaTarjeasLoaded(object sender, RoutedEventArgs e)
        {
            CargarGrilla();
        }
        private void CargarGrilla()
        {
            DataTable tabla = new();
            tabla.Columns.Add("Numero");
            tabla.Columns.Add("Tipo");
            tabla.Columns.Add("Nombre Completo");
            foreach (Tarjeta card in TarjetasValidas)
            {
                string numeroOculto = Ocultar(card.Numero.ToString());
                tabla.Rows.Add(numeroOculto,card.Tipo,card.NombreCompleto);
            }
            TarjetasDataGrid.ItemsSource = tabla.DefaultView;
        }
        private string Ocultar(string original)
        {
            string final = "";
            for (int i = 0; i < original.Length; i++)
            {
                if (i < original.Length / 2) final += 'X';
                else final += original[i];

            }
            return final;
        }
    }
}
