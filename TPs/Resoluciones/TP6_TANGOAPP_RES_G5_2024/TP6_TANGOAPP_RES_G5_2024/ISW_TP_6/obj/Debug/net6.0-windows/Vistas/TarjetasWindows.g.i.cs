﻿#pragma checksum "..\..\..\..\Vistas\TarjetasWindows.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F49A545DFCFBCBD2DAA944FFF456BC21AE0608D9"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TP6_TANGOAPP_RES_G5_2024.Vistas;


namespace TP6_TANGOAPP_RES_G5_2024.Vistas {
    
    
    /// <summary>
    /// TarjetasWindows
    /// </summary>
    public partial class TarjetasWindows : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\..\Vistas\TarjetasWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TP6_TANGOAPP_RES_G5_2024.Vistas.TarjetasWindows VentanaTarjeas;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\..\Vistas\TarjetasWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Viewbox MainView;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\..\Vistas\TarjetasWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\Vistas\TarjetasWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BotonCerrar;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Vistas\TarjetasWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid TarjetasDataGrid;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Vistas\TarjetasWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelTitulo;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TP6_TANGOAPP_RES_G5_2024;component/vistas/tarjetaswindows.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Vistas\TarjetasWindows.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.VentanaTarjeas = ((TP6_TANGOAPP_RES_G5_2024.Vistas.TarjetasWindows)(target));
            
            #line 8 "..\..\..\..\Vistas\TarjetasWindows.xaml"
            this.VentanaTarjeas.Loaded += new System.Windows.RoutedEventHandler(this.VentanaTarjeasLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainView = ((System.Windows.Controls.Viewbox)(target));
            return;
            case 3:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.BotonCerrar = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\..\Vistas\TarjetasWindows.xaml"
            this.BotonCerrar.Click += new System.Windows.RoutedEventHandler(this.BotonCerrarClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TarjetasDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 19 "..\..\..\..\Vistas\TarjetasWindows.xaml"
            this.TarjetasDataGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.TarjetasDataGridMouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.LabelTitulo = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

