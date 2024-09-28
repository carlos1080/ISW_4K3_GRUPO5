using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Image = System.Windows.Controls.Image;

namespace ISW_TP_6.Adapters
{
    /// <summary>
    /// Generics es un conjunto de funciones que se puede utilizar en diversas ventanas y user control ya que las mismas utilizan el polimorfismo
    /// de los objetos y la generalizacion de las soluciones para brindar comodidad al programador.
    /// 
    /// Los nombres de las funciones o metodos estaticos deben ser escritos con Mayuscula e intentar resolver la mayor cantidad de cosas en esta
    /// clase para reducir la cantidad de codigo en el resto del programa
    /// </summary>
    public static class Generics
    {
        /*
        * Leer con Atencion:
        * Para utilizar muchas de estas funciones el atributo Tag del elemento debe estar inicializado aunque no se utilice, vease si queremos que se encuentre vacio
        * el textbox cuando limpiemos los campos a Tag lo tenemos que escribir en el XAML como Tag="" para que a la hora de usar esta generics se limpie
        * 
        * Sino lo utilizamos tag no estara inicializado y no aceptara realizar la funcion try/catch con la excepcion (NullReferenceException) porque es como si dicho atributo
        * no existiera y rompe completamente el programa
        */

        /// <summary>
        /// Pantalla principal que se almacena su referencia para poder llamarla en diferentes puntos del programa
        /// </summary>
        private static MainWindow? Principal;


        /// <summary>
        /// Estos numeros escritos a mano se utilizan para pasar de decimal a texto coloquial. Principalmente en documentacion
        /// </summary>
        public static readonly string[] numbers = { "cero", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve" };
        /// <summary>
        /// Estos numeros escritos a mano se utilizan para pasar de decimal a texto coloquial. Principalmente en documentacion
        /// </summary>
        public static readonly string[] oneDigit = { "cero", "un", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve" };
        /// <summary>
        /// Estos numeros escritos a mano se utilizan para pasar de decimal a texto coloquial. Principalmente en documentacion
        /// </summary>
        private static readonly string[] twoDigitTens = { "diez", "once", "doce", "trece", "catorce", "quince", "dieciseis", "diecisiete", "dieciocho", "diecinueve" };
        /// <summary>
        /// Estos numeros escritos a mano se utilizan para pasar de decimal a texto coloquial. Principalmente en documentacion
        /// </summary>
        private static readonly string[] twoDigitRest = { "veinte", "veinti", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa" };
        /// <summary>
        /// Estos numeros escritos a mano se utilizan para pasar de decimal a texto coloquial. Principalmente en documentacion
        /// </summary>
        private static readonly string[] threeDigits = { "ciento", "cientos", "quinientos", "setecientos", "novecientos" };
        /// <summary>
        /// Estos numeros escritos a mano se utilizan para pasar de decimal a texto coloquial. Principalmente en documentacion
        /// </summary>
        private static readonly string[] numeros = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        /// <summary>
        /// Modos de Paginacion creados para un metodo que dejamos de usar
        /// </summary>
        private enum PagingMode { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };
        /// <summary>
        /// Listado de imput.Key permitidas por el usuario
        /// </summary>
        private static readonly List<string> keysPermitted = new()
        {
            "Return",
            "Tab",
            "Back",
            "D1",
            "D2",
            "D3",
            "D4",
            "D5",
            "D6",
            "D7",
            "D8",
            "D9",
            "D0",
            "OemComma",
            "NumPad0",
            "NumPad1",
            "NumPad2",
            "NumPad3",
            "NumPad4",
            "NumPad5",
            "NumPad6",
            "NumPad7",
            "NumPad8",
            "NumPad9",
            "Return"
        };

        /// <summary>
        /// Para no escribir todas las letras que usamos como modulo cree la siguiente funcion que recupera de la A a la P
        /// </summary>
        /// <returns> El listado de modulos usados en la aplicacion en letra mayuscula</returns>
        public static List<char> GetModules()
        {
            List<char> modulos = new();
            // Sirve para cargar letras siguiendo el codigo ASCII (A= 65 || P = 80)
            for (int i = 65; i <= 80; i++) modulos.Add((char)i);
            return modulos;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sizesWidht"></param>
        /// <param name="showDataGrid"></param>
        public static void FormatDataGridColums(List<double> sizesWidht, DataGrid showDataGrid)
        {
            if (showDataGrid.Columns.Count != sizesWidht.Count)
            {
                double sizePerColumn = (showDataGrid.ActualWidth - 1) / showDataGrid.Columns.Count;
                for (int i = 0; i < showDataGrid.Columns.Count; i++) showDataGrid.Columns[i].Width = Math.Round(sizesWidht[i] * sizePerColumn, 2);
            }
            else
            {
                double totalSize = showDataGrid.ActualWidth - 1;
                for (int i = 0; i < showDataGrid.Columns.Count; i++) showDataGrid.Columns[i].Width = Math.Round(sizesWidht[i] * totalSize, 2);
            }
        }
        /// <summary>
        /// Apenas se inicializa el Main lo guardamos en Generics para agilizar el llamado desde cada manager
        /// </summary>
        /// <param name="Main"> La referencia del objeto principal </param>
        public static void SaveMainWindows(MainWindow Main) => Principal = Main;
        /// <summary>
        /// Este metodo existe porque el formato usado en el FrontEnd es XX-XXXXXXXX-X y para buscarlo en la base usamos
        /// el numero sin guiones
        /// </summary>
        /// <param name="cuil"> El CUIL para removerle los guiones</param>
        /// <returns> El CUIL original o el mismo texto si no supera los 12 de longitud </returns>
        public static string GetOriginalCUIL(string cuil)
        {
            if (cuil.Length > 12)
            {
                string Original = cuil;
                Original = Original.Remove(2, 1);
                Original = Original.Remove(Original.Length - 2, 1);
                return Original;
            }
            else return cuil;
        }
        /// <summary>
        /// Crea un texto con puntos suspensivos al final 
        /// </summary>
        /// <param name="texto"> El texto a modificar </param>
        /// <param name="largo"> El largo del texto final </param>
        /// <returns> Un string con puntos suspensivos al final </returns>
        public static string StringWithDots(string texto, int largo)
        {
            if (texto.Length > largo) return $"{texto[..(largo - 3)]}...";
            return texto;
        }
        /// <summary>
        /// Esta funcion genera un Message Box para notificar el error donde solo es necesario mandarle el string principal
        /// de esta ventana
        /// </summary>
        /// <param name="message"> Un string que queremos que aparezca </param>
        public static void WrongInput(string message) => MessageBox.Show(message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        /// <summary>
        /// Esta funcion se creo principalmente para limpiar los campos personalizados para contabilidad. Ante el problema que supone
        /// el Culture de cada equipo decidimos mantener el mismo formato en la aplicacion sin depender de la region del programa.
        /// </summary>
        /// <param name="txt"> El Textbox que queremos recuperar el valor original </param>
        /// <returns> El numero en decimal entendible para C# </returns>
        public static decimal GetUnformatPrice(TextBox txt)
        {
            string formatPrice = txt.Text;
            return GetUnformatPrice(formatPrice);
        }
        /// <summary>
        /// Esta funcion se creo principalmente para limpiar los campos personalizados para contabilidad. Ante el problema que supone
        /// el Culture de cada equipo decidimos mantener el mismo formato en la aplicacion sin depender de la region del programa.
        /// </summary>
        /// <param name="formatPrice"> El valor en string de dicho txtbox </param>
        /// <returns> El numero en decimal entendible para C# </returns>
        public static decimal GetUnformatPrice(string formatPrice)
        {
            if (formatPrice == null) return 0;
            //Esto es para prevenir la mayoria de los $0
            if (formatPrice == "$00.00" || formatPrice == "00.00" ||
                formatPrice == "$0.00" || formatPrice == "$0.00" ||
                formatPrice == "$0" ||
                formatPrice == "0" || formatPrice == "$.00" || formatPrice == "$0.0") return 0;

            string[] numbers = formatPrice.Split(',');

            numbers[0] = numbers[0].Replace('$', ' ').Trim();

            string[] integersParts = numbers[0].Split('.');
            string stringEnteros = "";
            foreach (string item in integersParts)
            {
                stringEnteros += item;
            }
            if (numbers[1].Length == 0)
            {
                numbers[1] = "00";
            }
            decimal enteros = decimal.Parse(stringEnteros);
            decimal decimales = decimal.Parse(numbers[1]);
            return enteros + decimales / 100;
        }
        /// <summary>
        /// Este metodo surgio por 2 inconvenientes. El primero es que no nos avisaron que en el esquema viejo se guardaba CTA/DIV con una
        /// cantidad aleatoria de 0s (Diferentes a la de BD). La segunda es que desde el front quieren el dato de CTA sin datos aleatorias.
        /// </summary>
        /// <param name="cta">CTA = cue_cta campo del numero de cuenta</param>
        /// <returns> El string sin 0s al principio</returns>
        public static string CleanCTA(string cta)
        {
            if (string.IsNullOrEmpty(cta)) return cta;

            if (cta[^1] == '0') cta = cta.Insert(cta.Length, "*");

            cta = cta.Replace('0', ' ');
            cta = cta.Trim();
            cta = cta.Replace(' ', '0');
            cta = cta.Replace('*', ' ');

            return cta.Trim();
        }

        /// <summary>
        /// Sirve para activar y desactivar los TextBox y de esta manera poder borrar su contenido y cambiar el color de su letra
        /// </summary>
        /// <param name="tb"> Es el TextBox que se desea activar que debe tener algun valor en el Tag </param>
        /// <param name="color"> Es el color de la letra que se desea tener </param>
        public static void ManageBox(TextBox tb, Brush color)
        {
            if (tb.Tag == null) return;
            tb.Foreground = color;
            if (tb.Text.Equals(tb.Tag.ToString())) tb.Text = "";
            else
            {
                if (tb.Text.Equals("")) tb.Text = tb.Tag.ToString();
            }
        }
        /// <summary>
        /// Esta funcion, que su traduccion es 'Estas Seguro?', crea una ventana de confirmacion donde el usuario debe elegir entre dos opciones que se convierten
        /// en valores booleanos para activar la accion. La diferencia que esta funcion utiliza una version Generica que provee el mismo visaul estudio mediante el
        /// objeto MessageBox, asi mismo el mensaje ya esta predefinido desde esta funcion y no es necesario mandarle un mensaje.
        /// </summary>
        /// <returns> Un valor (True o False) dependiendo que seleccione el usuario </returns>
        public static bool GenericAreYouSure()
        {
            MessageBoxResult a = MessageBox.Show("Esta seguro que desea confirmar esta accion? ", "CONFIRMACION", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (a == MessageBoxResult.Yes) return true;
            else return false;
        }
        /// <summary>
        /// Funcion simple que nos permite obtener el nombre escrito del mes pasandole el numero con el cual se representa en el calendario gregoriano
        /// </summary>
        /// <param name="month"> El entero con el cual se representa al mes </param>
        /// <returns> El nombre escrito en lengua Castellana </returns>
        public static string GetMonthSpanish(int month)
        {
            if (month <= 0 || month > 12) return "Error, numero fuera de rango";
            return month switch
            {
                1 => "enero",
                2 => "febrero",
                3 => "marzo",
                4 => "abril",
                5 => "mayo",
                6 => "junio",
                7 => "julio",
                8 => "agosto",
                9 => "septiembre",
                10 => "octubre",
                11 => "noviembre",
                12 => "diciembre",
                _ => "Fuera de parametros",
            };
        }
        /// <summary>
        /// Funcion creada explicitamente para generar de forma coloquial la cantidad de dinero que le pasemos
        /// Por ejemplo: 
        ///     string money = GetStringMoney(154631.40) --->  money = Ciento cincuenta y cuatro mil seiscientos treinta y uno pesos con 40 centavos
        ///    
        /// De esta manera podemos rapidamente tener el resultado para la utilizacion en contratos, enmiendas, etc.
        /// </summary>
        /// <param name="amount"> Es una cantidad pasado como string que puede tener un punto representando los centavos. 
        /// Para mayor comodidad se recomienda usar la funcion MaskedFloat con MoneyMode activado ya que previene que se utilicen mas de 2 digitos en los centavos
        /// </param>
        /// <returns> La cantidad de dinero escrito de manera coloquialmente </returns>
        public static string GetStringMoney(string amount)
        {
            string centavos = "";
            if (amount.Contains(',') || amount.Contains('.'))
            {
                centavos = amount.Substring(amount.Length - 2, 2);
                //Si recupera un numero como .8 significa que son 80 centavos nada mas que el mismo c# lo limpia del 0 final por "Innecesario"
                if (centavos.Contains(',') || centavos.Contains('.'))
                {
                    centavos = centavos.Substring(1, 1);
                    centavos += "0";
                    amount += "0";
                }
                centavos = NumberToWords(int.Parse(centavos));
                amount = amount.Remove(amount.Length - 3, 3);
            }
            if (amount.Length > 9) return "No procesable";
            else
            {
                if (centavos.Equals("")) return NumberToWords(int.Parse(amount)) + " pesos";
                else return NumberToWords(int.Parse(amount)) + " pesos con " + centavos + " centavos";
            }
        }

        /// <summary>
        /// Una funcion que utiliza la recursion para devolver el entero que se pase como un string con el numero escrito de manera coloquial
        /// </summary>
        /// <param name="numb"> Es un numero entero no mayor a 100 millones de pesos quue deseamos recuperar como un string </param>
        /// <returns> Devuelve el entero pero como una cadena escrita de manera coloquial </returns>
        public static string NumberToWords(int numb)
        {
            int hp;
            if (numb < 100)
            {
                if (numb == 0) return "";
                if (numb < 10) return oneDigit[numb];
                if (numb < 20) return twoDigitTens[numb - 10];
                if (numb < 30)
                {
                    if (numb == 20) return twoDigitRest[0];
                    if (numb > 20) return twoDigitRest[1] + NumberToWords(numb - 20);
                }
                hp = numb / 10;
                if (numb % 10 == 0) return twoDigitRest[hp - 1];
                else return twoDigitRest[hp - 1] + " y " + NumberToWords(numb - hp * 10);
            }
            if (numb < 1000)
            {
                hp = numb / 100;
                if (numb < 200) return threeDigits[0] + " " + NumberToWords(numb % 100);
                if (numb < 500) return NumberToWords(hp) + threeDigits[1] + " " + NumberToWords(numb % 100);
                if (numb < 600) return threeDigits[2] + " " + NumberToWords(numb % 100);
                if (numb < 700) return NumberToWords(6) + threeDigits[1] + " " + NumberToWords(numb % 100);
                if (numb < 800) return threeDigits[3] + " " + NumberToWords(numb % 100);
                if (numb < 900) return NumberToWords(8) + threeDigits[1] + " " + NumberToWords(numb % 100);
                return threeDigits[4] + " " + NumberToWords(numb % 100);
            }
            if (numb < 1000000)
            {
                hp = numb / 1000;
                return NumberToWords(hp) + " mil " + NumberToWords(numb - hp * 1000);
            }
            if (numb < 1000000000)
            {
                hp = numb / 1000000;
                if (hp == 1) return NumberToWords(hp) + " millon " + NumberToWords(numb - hp * 1000000);
                else return NumberToWords(hp) + " millones " + NumberToWords(numb - hp * 1000000);
            }
            return "Por mucha plata";
        }
        /// <summary>
        /// Valida utilizando un atributo local que el string pasado sea un numero y devuelve un booleano
        /// </summary>
        /// <param name="a"> El string 'a' de longitud 1 que deseamos validar </param>
        /// <returns> True si es un numero, False si no lo es </returns>
        public static bool IsNumber(string a)
        {
            // Revisa que sea un numero lo que se mando
            foreach (string x in numeros) if (x.Equals(a)) return true;
            return false;
        }
        /// <summary>
        /// Crea una mascara para los valores monetarios donde el '.' es para separar Mil Millones y etc. Mientras que
        /// el ',' sirve para la coma.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string MaskedFinancial(double? number)
        {
            if (number == null) return "$0,00";
            if (number == 0) return "$0,00";

            string xNum = Math.Round((decimal)number, 2).ToString();
            List<string> numeros = new() { xNum };
            if (xNum.Contains(',')) numeros = xNum.Split(',').ToList();
            if (xNum.Contains('.')) numeros = xNum.Split('.').ToList();

            //No tiene decimales asi que agregamos el 00 por seguridad
            if (numeros.Count == 1) numeros.Add("00");

            //El primero son los numeros enteros
            xNum = numeros[0];
            int contador = 0;
            string yNum = "";
            char caracter;
            for (int i = xNum.Length - 1; i >= 0; i--)
            {
                contador++;
                caracter = xNum[i];
                if (contador == 3 && i != 0)
                {
                    contador = 0;
                    yNum += caracter + ".";
                }
                else yNum += caracter;
            }
            char[] charNumber = yNum.ToCharArray();
            Array.Reverse(charNumber);
            string finalNumber = new(charNumber);
            //Como agregamos el 00 siempre lo podemos recuperar
            string centavos = numeros[1];
            if (finalNumber[0] == '.') finalNumber = finalNumber[1..];

            if (centavos.Length == 0) centavos = "00";
            if (finalNumber.Length == 0) finalNumber = "00";

            return "$" + finalNumber + "," + centavos;
        }
        /// <summary>
        /// Con esta mascara existe la posibilidad que acepte numeros flotantes separando los decimales por un punto.
        /// Si ademas de eso queremos activar el modo de que acepte solo dos flotantes (Para usos contables) solo
        /// tenemos que mandar un true como segundo argumento;
        /// </summary>
        /// <param name="tx"> Es el textbox que se tiene que referenciar mediante el TextBox_Changed_Event </param>
        /// <param name="moneyMode"> Es al activacion del modo contable (Solo dos decimales) </param>
        public static void MaskedFloat(TextBox tx, bool moneyMode)
        {
            //Cada vez que se cambia el texto tanto como cuando hay una entrada por teclado o se pega algo revisa que sea un numero
            if (!tx.IsInitialized) return;
            if (tx.Text.Equals("")) return;
            string x;
            bool f_exist = false;
            int c = 0;
            for (int i = 0; i < tx.Text.Length; i++)
            {

                x = tx.Text.Substring(i, 1);
                if (moneyMode)
                {
                    if (f_exist) c++;
                    if (c > 2)
                    {
                        c--;
                        EraseChar(tx, i);
                    }
                }
                if (IsNumber(x)) continue;
                else
                {
                    if (x.Equals(","))
                    {
                        if (f_exist)
                        {
                            EraseChar(tx, i);
                            f_exist = false;
                        }
                        else f_exist = true;
                    }
                    else EraseChar(tx, i);
                }
            }
        }
        /// <summary>
        /// Funcion creada para evitar que el usuario ingrese valores que no corresponden en un textbox unicamente para numeros
        /// </summary>
        /// <param name="txt"> El textbox que queremos controlar </param>
        /// <param name="e"> El caracter del teclado que se apreta</param>
        public static void KeysPermittedFloat(TextBox txt, KeyEventArgs e)
        {
            bool permitted = false;
            foreach (var item in keysPermitted)
            {
                string key = e.Key.ToString();
                if (key == item)
                {
                    if (key == "OemComma" && !txt.Text.Contains(',')) permitted = true;
                    if (key != "OemComma") permitted = true;
                    break;
                }
            }
            if (!permitted)
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Mascara visual para un textbox que previene que se escriba cualquier cosa
        /// </summary>
        /// <param name="txtbox"> El textbox a controlar </param>
        public static void MaskedFinancial(TextBox txtbox)
        {
            if (!txtbox.IsInitialized) return;

            string text = txtbox.Text;

            if (txtbox.Tag != null && text == txtbox.Tag.ToString()) return;
            if (text.Equals("")) txtbox.Text = "$";
            if (text == "0" || text == "0,0" || text == "00,0" || text == "00,00" || text == ",0" ||
                text == "$0" || text == "$0,0" || text == "$00,0" || text == "$00,00" || text == "$,0")
            {
                txtbox.Text = "$0,00";
                return;
            }
            string x;
            List<string> interger_parts = new();
            string only_numbers_int = "";
            string only_decimal = "";
            bool f_exist = false;
            bool clean_0 = false;
            int contador_decimal = 0;
            int contador_integer = 0;

            for (int i = 0; i < text.Length; i++)
            {
                //x equivale al char en esa posicion
                x = text.Substring(i, 1);
                if (x[0] != '0' && x[0] != '$' && x[0] != '.') clean_0 = true;
                if (clean_0)
                {

                    if (IsNumber(x) && !f_exist)
                    {
                        if (contador_integer == 3)
                        {
                            interger_parts.Add(only_numbers_int);
                            contador_integer = 0;
                            only_numbers_int = "";
                        }

                        contador_integer++;
                        only_numbers_int += x;
                        continue;
                    }
                    if (x == "," && !f_exist) f_exist = true;
                    if (IsNumber(x) && f_exist)
                    {
                        contador_decimal++;
                        if (contador_decimal <= 2) only_decimal += x;
                        continue;
                    }
                }

            }
            interger_parts.Add(only_numbers_int);
            if (interger_parts.Count == 0)
            {
                txtbox.Text = "$00,00";
            }
            else
            {
                int ultimo = interger_parts[^1].Length;
                string cadenaFinal = "";
                switch (ultimo)
                {
                    case 1:
                        foreach (var item in interger_parts)
                        {

                            if (item.Length == 3) cadenaFinal += item[0].ToString() + "." + item[1].ToString() + item[2].ToString();
                            else cadenaFinal += item[0].ToString();
                        }
                        break;
                    case 2:
                        foreach (var item in interger_parts)
                        {

                            if (item.Length == 3) cadenaFinal += item[0].ToString() + item[1].ToString() + "." + item[2].ToString();
                            else cadenaFinal += item[0].ToString() + item[1].ToString();
                        }
                        break;
                    //Todos las partes estan completas y queda sin mas cambios
                    case 3:
                        foreach (var item in interger_parts) cadenaFinal += "." + item;
                        cadenaFinal = cadenaFinal.Remove(0, 1);
                        break;
                }
                if (only_decimal.Length == 0) only_decimal = "00";
                if (cadenaFinal.Length == 0) cadenaFinal = "00";
                //Limpiamos los 0s a la izquierda
                txtbox.Text = "$" + cadenaFinal + "," + only_decimal;
            }

        }
        /// <summary>
        /// Funcion de apoyo utilizada en los MaskedTextBox para eliminar un solo caracter de un textbox y acomodar su puntero
        /// </summary>
        /// <param name="tx"> El TextBox que deseamos eliminar </param>
        /// <param name="i"> El index en el cual se ubica este caracter </param>
        private static void EraseChar(TextBox tx, int i)
        {
            if (i < tx.Text.Length)
            {
                tx.Text = tx.Text.Remove(i, 1);
                tx.CaretIndex = tx.Text.Length;
                return;
            }
        }

        /// <summary>
        /// Suele usarse con el textChanged para corroborar que el textbox contenga solo numeros
        /// </summary>
        /// <param name="tx"> Es el textBox que va a tener esta masscara </param>
        public static void MaskedNumber(TextBox tx)
        {
            //Cada vez que se cambia el texto tanto como cuando hay una entrada por teclado o se pega algo revisa que sea un numero
            if (!tx.IsInitialized) return;
            if (tx.Text.Equals("")) return;
            if (tx.Tag != null && tx.Text == tx.Tag.ToString()) return;
            string x;
            for (int i = 0; i < tx.Text.Length; i++)
            {
                x = tx.Text.Substring(i, 1);
                if (!IsNumber(x)) EraseChar(tx, i);
            }
            tx.CaretIndex = tx.Text.Length;
        }
        /// <summary>
        /// Suele usarse con el textChanged para corroborar que el textbox contenga solo numeros
        /// </summary>
        /// <param name="tx"> Es el textBox que va a tener esta masscara </param>
        public static void MaskedNumber(TextBox tx, int maxLenght)
        {
            //Cada vez que se cambia el texto tanto como cuando hay una entrada por teclado o se pega algo revisa que sea un numero
            if (!tx.IsInitialized) return;
            if (tx.Text.Equals("")) return;
            if (tx.Tag != null && tx.Text == tx.Tag.ToString()) return;
            if (tx.Text.Length > maxLenght)
            {
                EraseChar(tx, tx.Text.Length - 1);
                tx.CaretIndex = tx.Text.Length;
                return;
            }
            if (tx.Text.Equals(""))
            {
                return;
            }
            string x;
            for (int i = 0; i < tx.Text.Length; i++)
            {
                x = tx.Text.Substring(i, 1);
                if (!IsNumber(x)) EraseChar(tx, i);
            }

            tx.CaretIndex = tx.Text.Length;
        }
        /// <summary>
        /// Una mascara simple que es para controlar el largo del texto un texbox
        /// </summary>
        /// <param name="tx"> El textbox a controlar</param>
        /// <param name="maxLenght"> La longitud maxima que permitimos</param>
        public static void MaskedString(TextBox tx, int maxLenght)
        {
            if (!tx.IsInitialized) return;
            if (tx.Text.Equals("")) return;
            if (tx.Tag != null && tx.Text == tx.Tag.ToString()) return;
            if (tx.Text.Length > maxLenght)
            {
                EraseChar(tx, tx.Text.Length - 1);
                tx.CaretIndex = tx.Text.Length;
                return;
            }
        }

        /// <summary>
        /// Sirve para automatizar los CUIL dandole a entender a un TextBox que solo acepta numeros y automaticamente agrega un guion en los lugares que corresponda.
        /// Si manualmente se agregan guiones no afecta al proceso y reconoce la cadena como valida.
        /// </summary>
        /// <param name="tx"> Es un textbox le tenemos que agregar un Generic_Text_Changed y ejecutarlo atravez de la funcion </param>
        public static void MaskedCuil(TextBox tx)
        {
            //Cada vez que se cambia el texto tanto como cuando hay una entrada por teclado o se pega algo revisa que sea un numero.
            if (!tx.IsInitialized) return;

            if (tx.Text.Equals(tx.Tag.ToString()) || tx.Text.Equals("")) return;
            string x;
            for (int i = 0; i < tx.Text.Length; i++)
            {
                if (i > 12)
                {
                    tx.Text = tx.Text.Remove(i, 1);
                    break;
                }
                x = tx.Text.Substring(i, 1);

                if (!IsNumber(x))
                {
                    if (x.Equals("-") && (i == 2 || i == 11)) continue;//Todo legal
                    else tx.Text = tx.Text.Remove(i, 1);
                }
            }
            // Controla que sea mayor a 0 asi no tiene problema con (legth - 1) y luego se asegura que no exista un '-' en el ultimo lugar.
            // Finalmente agrega si el texto es de longitud 3 o 12 un '-' en el index anterior.
            if (tx.Text.Length > 0)
            {
                if (!tx.Text.Substring(tx.Text.Length - 1, 1).Equals("-") && tx.Text.Length == 3) tx.Text = tx.Text.Insert(2, "-");
                if (!tx.Text.Substring(tx.Text.Length - 1, 1).Equals("-") && tx.Text.Length == 12) tx.Text = tx.Text.Insert(11, "-");
            }
            tx.CaretIndex = tx.Text.Length;
        }


        /// <summary>
        /// Reinicia todos los objetos que se encuentren bajo el control de la grilla.
        /// Los TextBox deben tener iniciada el atributo 'Tag'
        /// </summary>
        /// <param name="gd"> Es una grilla que debe contener ComboBox y Textbox </param>
        public static void CleanFields(Grid gd)
        {
            foreach (UIElement Iobject in gd.Children)
            {
                if (Iobject is TextBox)
                {
                    TextBox x = Iobject as TextBox ?? new();
                    if (x.Tag == null) x.Text = "";
                    else x.Text = x.Tag.ToString();
                }
                if (Iobject is ComboBox)
                {
                    ComboBox cmb = Iobject as ComboBox ?? new();
                    cmb.SelectedIndex = -1;
                }

                if (Iobject is TextBlock)
                {
                    TextBlock txt = Iobject as TextBlock ?? new();
                    txt.Text = "";
                }

                if (Iobject is DataGrid)
                {
                    DataGrid grid = Iobject as DataGrid ?? new();
                    grid.ItemsSource = null;
                }
            }
        }
        /// <summary>
        /// Comprueba que un TextBox no se encuentre vacio ya sea con el Tag (Que es el texto inicial) o vacio
        /// </summary>
        /// <param name="txt">El textbox que se desea validar</param>
        /// <returns> Devuelve True si tiene algun contenido y False si no se ha modificado </returns>
        public static bool ValidateTextBox(TextBox txt)
        {
            if (txt.Tag != null)
            {
                if (txt.Text.Trim().Equals("") || txt.Text.Trim() == txt.Tag?.ToString()?.Trim()) return false;
                else return true;
            }
            else return !txt.Text.Trim().Equals("");
        }
        /// <summary>
        /// Comprueba que en el DataGrid se haya seleccionado algo 
        /// </summary>
        /// <param name="gd">El Data Grid que se esta validando </param>
        /// <returns></returns>
        public static bool ValidateDataGrid(DataGrid gd)
        {
            if (gd.ItemsSource == null || gd.SelectedIndex == -1) return false;
            else return true;
        }
        /// <summary>
        /// Comprueba que en Combo Box se haya seleccionado algo
        /// </summary>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static bool ValidateComboBox(ComboBox cb)
        {
            if (cb.SelectedIndex == -1) return false;
            else return true;
        }
        /// <summary>
        /// Comprueba que todos los contenidos en una grilla esten siendo utilizados
        /// Valida TextBox, ComboBox y DataGrid
        /// </summary>
        /// <param name="mainGrid">Es la grilla que se esta validando</param>
        /// <returns> Retorna True si se esta utilizando todos los componentes </returns>
        public static bool ValidateAllGrid(Grid mainGrid)
        {
            foreach (UIElement a in mainGrid.Children)
            {
                switch (a)
                {
                    case TextBox:
                        if (!ValidateTextBox((TextBox)a)) return false;
                        break;
                    case ComboBox:
                        if (!ValidateComboBox((ComboBox)a)) return false;
                        break;
                    case DataGrid:
                        if (!ValidateDataGrid((DataGrid)a)) return false;
                        break;
                }
            }
            return true;
        }
        /// <summary>
        /// Comprueba que el objeto que le mandemos (De cualquier tipo) no sea ni null ni vacio
        /// </summary>
        /// <param name="anything"> Literalmente cualquier objeto </param>
        /// <returns> False si es nulo o vacio y True si tiene algo </returns>
        public static bool ValidateEmptyness(object? anything)
        {
            if (anything == null) return false;

            if (anything.ToString()?.Trim() == "") return false;
            return true;
        }

        /// <summary>
        /// Es una funcion que sirve para convertir un string en un patron en el lenguaje SQL ubicando '%' en ambos extremos de la cadena
        /// </summary>
        /// <param name="st"> El string que se desea convertir </param>
        /// <returns> El mismo string pero en el formato '%st%'</returns>
        public static string CreatePatron(string st)
        {
            st = st.Insert(st.Length, "%");
            return st.Insert(0, "%");
        }
        /// <summary>
        /// Es una funcion que sirve para convertir un string en un CUIL con 
        /// </summary>
        /// <param name="cuil"> Crea un CUIL agregando los guiones al texto </param>
        /// <returns> El CUIL formateado</returns>
        public static string CreateCUIL(string cuil)
        {
            if (cuil != null && cuil.Trim() != "" && cuil.Length < 13)
            {
                cuil = cuil.Trim();
                cuil = cuil.Insert(2, "-");
                return cuil.Insert(11, "-");
            }
            else return "";
        }
        /// <summary>
        /// Abre consola y abre la web que se esta mandando
        /// </summary>
        /// <param name="web"> Es una direccion https:// que deseamnos abrir </param>
        public static void AccessWebSite(string web) => System.Diagnostics.Process.Start("cmd", "/C start" + " " + web);
        /// <summary>
        /// Utilizando el System.Diagnostics se ejecuta el proceso
        /// </summary>
        /// <param name="process"> Un proceso .exe que deseamos ejecutar </param>
        public static void AccessProcess(string process) => System.Diagnostics.Process.Start(process);
        /// <summary>
        /// Esta funcion carga de matera automatica el combo box que se le pase con la informacion de la tabla que se pase
        /// Para utilizar esta funcion se debe conocer la procedencia de los datos y el atributo que deseamos representar
        /// </summary>
        /// <param name="table"> Tabla en la DB que se encuentra almacenado lo que deseamos cargar </param>
        /// <param name="attribute"> El atributo que se mostrara en el combo Box </param>
        /// <param name="comboBox"> El combo Box que deseamos cargar con la informacion </param>
        public static void LoadComboBoxWithTable(DataTable comboData, string attribute, ComboBox comboBox)
        {
            comboBox.ItemsSource = comboData.DefaultView;
            comboBox.DisplayMemberPath = attribute;
            comboBox.SelectedValue = "id";
            comboBox.SelectedIndex = -1;
        }
        /// <summary>
        /// Carga un combo box con un conjunto de objetos generico
        /// </summary>
        /// <param name="listado"> Los datos que va a tener el combo box</param>
        /// <param name="display"> El nombre que queremos mostrar </param>
        /// <param name="comboBox"> El combo box a configurar</param>
        public static void LoadComboBoxObjects(IEnumerable<object> listado, string display, ComboBox comboBox)
        {
            comboBox.ItemsSource = listado;
            comboBox.DisplayMemberPath = display;
            comboBox.SelectedValue = "id";
            comboBox.SelectedIndex = -1;
        }

        ///// <summary>
        ///// Abre un explorador de archivo y devuelve el nombre del archivo
        ///// </summary>
        ///// <returns> Devuelve el nombre del archivo seleccionado </returns>
        //private static string OpenDialog()
        //{
        //    OpenFileDialog ofd = new();
        //    ofd.ShowDialog();
        //    return ofd.FileName;
        //}

        ///// <summary>
        ///// Utiliza el user control y lo convierte en un archivo para imprimir que el usuario seleccionara para hacer
        ///// </summary>
        ///// <param name="vs"> La pantalla que deseamos imprimir </param>
        //public static void PrinterLoader(Visual vs)
        //{
        //    PrintDialog pd = new();
        //    if (pd.ShowDialog() == true) pd.PrintVisual(vs, "Impresion");
        //    else WrongInput("No se puede imprimir el archivo");
        //}

        /// <summary>
        /// Imprime usando un listado de objetos
        /// </summary>
        /// <param name="visuals"> El listado de objetos a imprimir</param>
        public static void PrinterLoader(List<Visual> visuals)
        {
            PrintDialog pd = new();
            var document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(pd.PrintableAreaWidth, pd.PrintableAreaHeight);
            switch (visuals.Count)
            {
                case 1:
                    document.Pages.Add(CreatePageContent(visuals[0]));
                    break;
                case 2:
                    document.Pages.Add(CreatePageContent(visuals[0]));
                    document.Pages.Add(CreatePageContent(visuals[1]));
                    break;
                case 3:
                    document.Pages.Add(CreatePageContent(visuals[0]));
                    document.Pages.Add(CreatePageContent(visuals[1]));
                    document.Pages.Add(CreatePageContent(visuals[2]));
                    break;
                case 4:
                    document.Pages.Add(CreatePageContent(visuals[0]));
                    document.Pages.Add(CreatePageContent(visuals[1]));
                    document.Pages.Add(CreatePageContent(visuals[2]));
                    document.Pages.Add(CreatePageContent(visuals[3]));
                    break;
                default:
                    break;
            }
            /*
            for (int i = 0; i < visuals.Count; i++)
            {
                document.Pages.Add(CreatePageContent(visuals[i]));
            }
            */
            /*
            foreach (Visual visual in visuals)
            {
                FixedPage fixedPage = new FixedPage();
                fixedPage.Width = document.DocumentPaginator.PageSize.Width;
                fixedPage.Height = document.DocumentPaginator.PageSize.Height;
                UIElement content = (UIElement)visual;
                fixedPage.Children.Add(content);
                fixedPage.UpdateLayout();

                var pageContent = new PageContent();
                ((IAddChild)pageContent).AddChild(fixedPage);
                document.Pages.Add(pageContent);
            }*/
            if (pd.ShowDialog() == true) pd.PrintDocument(document.DocumentPaginator, "Impresion");
        }
        /// <summary>
        /// Crea una pagina apartir de una pantalla
        /// </summary>
        /// <param name="vs"> La pantalla </param>
        /// <returns> Una pagina configurada para mostrar </returns>
        private static PageContent CreatePageContent(Visual vs)
        {

            PageContent pageContent = new();
            FixedPage fixedPage = new();

            UIElement visual = (UIElement)vs;

            FixedPage.SetLeft(visual, 0);
            FixedPage.SetTop(visual, 0);

            double pageWidth = 96 * 8.5;
            double pageHeight = 96 * 11;

            fixedPage.Width = pageWidth;
            fixedPage.Height = pageHeight;

            fixedPage.Children.Add(visual);

            Size sz = new(8.5 * 96, 11 * 96);
            fixedPage.Measure(sz);
            fixedPage.Arrange(new Rect(new Point(), sz));
            fixedPage.UpdateLayout();

            ((IAddChild)pageContent).AddChild(fixedPage);
            return pageContent;
        }

        /// <summary>
        /// Esta funcion provee de un controlador visual para aquellos botones que tengan la estructura de un grid con una imagen y label adentro.
        /// El metodo manipulara el efecto del contenido cuando el mismo se vea seleccionado o deseleccionado,
        /// se suele utilizar con MouseEnter => ((Button)Sender, 0,1); y MouseLeave =>((Button)Sender, 1,1);
        /// Es importante destacar que el multiplicador en ambos casos debe ser el mismo asi no se desfasa el tamaño con el paso del tiempo.
        /// </summary>
        /// <param name="eButton"> Es un boton con la estructura antes planteada </param>
        /// <param name="method"> Es el efecto que se desea, 0 = Expandir, 1=Reducir </param>
        /// <param name="multip"> Es la multiplicador de cuanto se desea expandir o reducir el boton </param>
        public static void ButtonVisualManager(Button eButton, int method, int multip)
        {
            if (method == 0)
            {
                eButton.Width += 8;
                eButton.Height += 8;
            }
            else
            {
                eButton.Width -= 8;
                eButton.Height -= 8;
            }
            if (eButton.Content is Grid grid)
            {
                foreach (UIElement element in grid.Children)
                {
                    //0 == Expand  ||  1 == Close //
                    if (method == 0)
                    {
                        for (int i = 0; i < 10 * multip; i++)
                        {
                            if (element is Image image)
                            {
                                image.Width++;
                                image.Height++;
                            }
                            if (element is Label label)
                            {
                                label.FontSize += 0.1;
                            }
                        }

                    }
                    if (method == 1)
                    {
                        for (int i = 10 * multip; i > 0; i--)
                        {
                            if (element is Image image)
                            {
                                image.Width--;
                                image.Height--;
                            }
                            if (element is Label label)
                            {
                                label.FontSize -= 0.1;
                            }
                        }
                    }
                }
            }

        }
        /// <summary>
        /// Es una funcion para encontrar el padre visual de un user control con respecto a una clase creada por el usuario que le mandamos
        /// </summary>
        /// <param name="child"> Es el User control del cual surge </param>
        /// <param name="parent"> Es la clase padre que deseamos encontrar mandandole un objeto ya iniciado (Porque da error si es nulo la funcion GetType) </param>
        /// <returns>Recupera la referencia al objeto que lo contiene (Padre)</returns>
        public static FrameworkElement? GetParentFromClass(FrameworkElement child, FrameworkElement parent)
        {
            object x = child.Parent;
            for (int i = 0; i < 10; i++)
            {
                if (x.GetType() == parent.GetType())
                {
                    return (FrameworkElement)x;
                }
                //No es hijo de la clase seleccionada
                else x = ((FrameworkElement)x).Parent;
            }
            return null;
        }
        /// <summary>
        /// Revierte la tabla que se ingresa donde las columnas van donde estan las filas y viceversa
        /// </summary>
        /// <param name="dt"> Tabla original que deseamos invertir </param>
        /// <param name="headers"> Encabezados que se convertiran en la columna 0 </param>
        /// <returns> La misma tabla pero horizontal </returns>
        public static DataTable ReverseDataTable(DataTable dt, string[] headers)
        {
            DataTable dtBuffer = new();
            dtBuffer.Columns.Add("Atributo");
            dtBuffer.Columns.Add("Valor");
            for (int j = 0; j < dt.Rows.Count; j++) for (int i = 0; i < headers.Length; i++) dtBuffer.Rows.Add(headers[i], dt.Rows[j][i].ToString());
            return dtBuffer;
        }
        /// <summary>
        /// Es una funcion para encontrar el padre visual de un user control con respecto a una clase creada por el usuario que le mandamos
        /// </summary>
        /// <param name="child"> Es el User control del cual surge </param>
        /// <param name="parent"> Es la clase padre que deseamos encontrar mandandole un objeto ya iniciado (Porque da error si es nulo la funcion GetType) </param>
        /// <returns> El Wrap Panel que contiene a nuestro objeto </returns>
        public static WrapPanel? GetWrapPanel(FrameworkElement child, WrapPanel parent)
        {
            object x = child.Parent;
            for (int i = 0; i < 10; i++)
            {
                if (x.GetType() == parent.GetType())
                {
                    return (WrapPanel)x;
                }
                //No es hijo de la clase seleccionada
                else x = ((Control)x).Parent;
            }
            return null;
        }
        /// <summary>
        /// Es una funcion para encontrar el padre visual de un user control con respecto a una clase creada por el usuario que le mandamos
        /// </summary>
        /// <param name="child"> Es el User control del cual surge </param>
        /// <param name="parent"> Es la clase padre que deseamos encontrar mandandole un objeto ya iniciado (Porque da error si es nulo la funcion GetType) </param>
        /// <returns> El StackPanel que contiene a nuestro objeto </returns>
        public static StackPanel? GetStackPanel(FrameworkElement child, WrapPanel parent)
        {
            object x = child.Parent;
            for (int i = 0; i < 10; i++)
            {
                if (x.GetType() == parent.GetType())
                {
                    return (StackPanel)x;
                }
                //No es hijo de la clase seleccionada
                else x = ((Control)x).Parent;
            }
            return null;
        }
        /// <summary>
        /// Recupera la pantalla principal para poder manipularla desde otro FrameworkElement
        /// </summary>
        /// <param name="child"> El elemento que es contenido por la pantalla principal </param>
        /// <returns> La referencia a la ventana principal </returns>
        public static Window GetMainWindow(FrameworkElement child)
        {
            if (Principal == null)//Esto no deberia ocurrir comunmente
            {
                object x = child.Parent;
                for (int i = 0; i < 10; i++)
                {
                    if (x is Window)
                    {
                        Principal = (MainWindow)x;
                        return Principal;
                    }
                    //No es hijo de la clase seleccionada
                    else x = ((FrameworkElement)x).Parent;
                }
                return new();//Esta opcion seria imposible
            }
            else return Principal;
        }
        /// <summary>
        /// Metodo para eliminar los recuadros en un documento
        /// </summary>
        /// <param name="paragraph"> El elemento recuadrado </param>
        /// <param name="flowDocument"> El documento que lo contiene </param>
        public static void EliminateBoxParagraph(Paragraph paragraph, FlowDocumentScrollViewer flowDocument)
        {
            var elementWithNmae = (Paragraph)flowDocument.FindName(paragraph.Name);
            elementWithNmae.Name = null;
        }
        /// <summary>
        /// Metodo para eliminar los recuadros en un documento
        /// </summary>
        /// <param name="tabla"> El elemento recuadrado </param>
        /// <param name="flowDocument"> El documento que lo contiene </param>
        public static void EliminateBoxTable(Table tabla, FlowDocumentScrollViewer flowDocument)
        {
            var elementWithNmae = (Table)flowDocument.FindName(tabla.Name);
            elementWithNmae.Name = null;
        }
        /// <summary>
        /// Metodo para eliminar los recuadros en un documento
        /// </summary>
        /// <param name="run"> El elemento recuadrado </param>
        /// <param name="flowDocument"> El documento que lo contiene </param>
        public static void EliminateBoxParagraph(Run run, FlowDocumentScrollViewer flowDocument)
        {
            if (run.Name != null)
            {
                var elementWithNmae = (Run)flowDocument.FindName(run.Name);
                elementWithNmae.Name = null;
            }
        }
        /// <summary>
        /// Sirve para cambiar el color de la letra de una grilla.
        /// </summary>
        /// <param name="gd"> La grilla que queremos cambiar su letra </param>
        /// <param name="cl"> El color que queremos implementar </param>
        public static void ChangeGridForeground(Grid gd, Color cl)
        {
            SolidColorBrush brush = new(cl);
            foreach (var item in gd.Children)
            {
                if (item is TextBox box) box.Foreground = brush;
                if (item is ComboBox Combo) Combo.Foreground = brush;
                if (item is DataGrid grid) grid.Foreground = brush;
                if (item is Label label) label.Foreground = brush;
            }
        }
        /// <summary>
        /// Sirve para cambiar el tamaño y familia de una grilla
        /// </summary>
        /// <param name="gd"> La grilla a cambiar </param>
        /// <param name="fm"> La familia que queremos usar </param>
        /// <param name="fontsize"> El tamaño de la letra</param>
        public static void ChangeGridFont(Grid gd, FontFamily fm, int fontsize)
        {
            foreach (var item in gd.Children)
            {
                if (item is TextBox textbox)
                {
                    textbox.FontFamily = fm;
                    textbox.FontSize = fontsize;
                }
                if (item is ComboBox cmb)
                {
                    cmb.FontFamily = fm;
                    cmb.FontSize = fontsize;
                }
                if (item is DataGrid datagrid)
                {
                    datagrid.FontFamily = fm;
                    datagrid.FontSize = fontsize;
                }
                if (item is Label label)
                {
                    label.FontFamily = fm;
                    label.FontSize = fontsize;
                }
            }
        }
        /// <summary>
        /// Sirve para imprimir documentos usando un FlowDocument
        /// </summary>
        /// <param name="documento"> El documento que queremos imprimir </param>
        public static void Printing(FlowDocument documento)
        {
            documento.PageWidth = 793;
            documento.ColumnWidth = documento.PageWidth;

            PrintDialog pd = new();

            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource idp = documento;

                pd.PrintDocument(idp.DocumentPaginator, "Contrato");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="scale"></param>
        /// <param name="title"></param>
        public static void PrintDocument(FlowDocumentScrollViewer flowDocumentScrollViewer, double scale, string title)
        {
            FlowDocument document = flowDocumentScrollViewer.Document;
            document.ColumnWidth = document.MinPageWidth;
            document.PageWidth = document.MinPageWidth;

            flowDocumentScrollViewer.Print();
            //DocumentPaginator dp = ((IDocumentPaginatorSource)document).DocumentPaginator;
            //FittedDocumentPaginator fdp = new FittedDocumentPaginator(dp, scale);

            //PrintDialog pd = new();

            //pd.PrintDocument(fdp, title);
        }
        /// <summary>
        /// Sirve para imprimir documentos usando un FlowDocument
        /// </summary>
        /// <param name="documento"> El documento que queremos imprimir </param>
        public static void PrintingA4(FlowDocumentScrollViewer flowDocumentScrollViewer)
        {
            flowDocumentScrollViewer.Document.PageWidth = 793;
            flowDocumentScrollViewer.Document.ColumnWidth = flowDocumentScrollViewer.Document.PageWidth;
            flowDocumentScrollViewer.Print();

            //PrintDialog pd = new();

            //if (pd.ShowDialog() == true)
            //{
            //    flowDocumentScrollViewer.Print();
            //}

        }



        /// <summary>
        /// Una forma de imprimir documentos con tamaño de hojas personalizadas
        /// </summary>
        /// <param name="flowDocumentScrollViewer">El documento a ser impreso</param>
        /// <param name="width">Ancho de la hoja</param>
        /// <param name="height">Alto de la hoja</param>
        public static void CustomePrinting(FlowDocumentScrollViewer flowDocumentScrollViewer, double width, double height)
        {
            flowDocumentScrollViewer.Document.PageWidth = width;
            flowDocumentScrollViewer.Document.PageHeight = height;
            flowDocumentScrollViewer.Document.ColumnWidth = flowDocumentScrollViewer.Document.PageWidth;
            flowDocumentScrollViewer.Print();
        }

        /// <summary>
        /// Recupera el lunes de la semana que estamos actualmente. Especialmente util debido a los pedidos de los usarios
        /// </summary>
        /// <returns> La fecha del lunes de la semana actual</returns>
        public static DateTime GetMonday()
        {
            DateTime today = DateTime.Now;
            if (today.DayOfWeek == DayOfWeek.Monday) return today;
            else
            {
                for (int i = 1; i <= 7; i++)
                {
                    //Deberia encontrarlo si o si
                    if (today.AddDays(-i).DayOfWeek == DayOfWeek.Monday) return today.AddDays(-i);
                }
                return today.AddDays(-7);
            }
        }
        ///// <summary>
        ///// Metodo de navegabilidad en deshuso
        ///// </summary>
        ///// <param name="mode"></param>
        ///// <param name="dataView"></param>
        ///// <param name="dataGrid"></param>
        ///// <param name="BtnPrevio"></param>
        ///// <param name="BtnFirst"></param>
        ///// <param name="BtnNext"></param>
        ///// <param name="BtnLast"></param>
        ///// <param name="PageIndex"></param>
        ///// <param name="NumberRowPorPage"></param>
        ///// <param name="numeroPagina"></param>
        //public static void Navigate (int mode , DataView dataView , DataGrid dataGrid,Button BtnPrevio , Button BtnFirst , Button BtnNext , Button BtnLast, int PageIndex , int NumberRowPorPage , TextBlock numeroPagina)
        //{
        //    int count;
        //    switch (mode)
        //    {
        //        case (int)PagingMode.Next:
        //            BtnPrevio.IsEnabled = true;
        //            BtnFirst.IsEnabled = true;

        //            if (dataView?.Table?.AsEnumerable().Count() >= (PageIndex * NumberRowPorPage))
        //            {
        //                if (dataView.Table.AsEnumerable().Skip(PageIndex * NumberRowPorPage).Take(NumberRowPorPage).Count() == 0)
        //                {
        //                    dataGrid.ItemsSource = null;
        //                    dataGrid.ItemsSource = dataView.Table.AsEnumerable().Skip((PageIndex * NumberRowPorPage) - NumberRowPorPage).Take(NumberRowPorPage).CopyToDataTable().DefaultView;
        //                    count = (PageIndex * NumberRowPorPage) + (dataView.Table.AsEnumerable().Skip(PageIndex * NumberRowPorPage).Take(NumberRowPorPage)).Count();
        //                }
        //                else
        //                {
        //                    dataGrid.ItemsSource = null;
        //                    dataGrid.ItemsSource = dataView.Table.AsEnumerable().Skip(PageIndex *
        //                    NumberRowPorPage).Take(NumberRowPorPage).CopyToDataTable().DefaultView;
        //                    count = (PageIndex * NumberRowPorPage) +
        //                    (dataView.Table.AsEnumerable().Skip(PageIndex *
        //                    NumberRowPorPage).Take(NumberRowPorPage)).Count();
        //                    PageIndex++;
        //                }
        //                numeroPagina.Text = count + " de " + dataView.Table.AsEnumerable().Count();
        //            }
        //            else
        //            {
        //                BtnNext.IsEnabled = false;
        //                BtnLast.IsEnabled = false;
        //            }

        //            break;

        //        case (int)PagingMode.Previous:
        //            BtnNext.IsEnabled = true;
        //            BtnLast.IsEnabled = true;
        //            if (PageIndex > 1)
        //            {
        //                PageIndex -= 1;
        //                dataGrid.ItemsSource = null;
        //                if (PageIndex == 1)
        //                {
        //                    dataGrid.ItemsSource = dataView?.Table?.AsEnumerable().Take(NumberRowPorPage).CopyToDataTable().DefaultView;
        //                    count = dataView?.Table?.AsEnumerable().Take(NumberRowPorPage).Count() ??0;
        //                    numeroPagina.Text = count + " de " + dataView?.Table?.AsEnumerable().Count()??"";
        //                }
        //                else
        //                {
        //                    dataGrid.ItemsSource = dataView?.Table?.AsEnumerable().Skip
        //                    (PageIndex * NumberRowPorPage).Take(NumberRowPorPage).CopyToDataTable().DefaultView;
        //                    count = Math.Min(PageIndex * NumberRowPorPage, dataView?.Table?.AsEnumerable().Count() ?? 0);
        //                    numeroPagina.Text = count + " de " + dataView?.Table?.AsEnumerable().Count()??"";
        //                }
        //            }
        //            else
        //            {
        //                BtnPrevio.IsEnabled = false;
        //                BtnFirst.IsEnabled = false;
        //            }
        //            break;


        //        case (int)PagingMode.First:
        //            PageIndex = 2;
        //            Navigate((int)PagingMode.Previous, dataView, dataGrid,BtnPrevio,BtnFirst,BtnNext,BtnLast,PageIndex,NumberRowPorPage,numeroPagina);
        //            break;

        //        case (int)PagingMode.Last:
        //            PageIndex = (dataView?.Table?.AsEnumerable().Count() / NumberRowPorPage)??0;
        //            Navigate((int)PagingMode.Next, dataView??new(), dataGrid, BtnPrevio, BtnFirst, BtnNext, BtnLast, PageIndex, NumberRowPorPage, numeroPagina);
        //            break;

        //    }
        //}

    }
}
