﻿#pragma checksum "..\..\..\LabsTablePage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "029A60F00C1E656A2CCCD643CBDE04AEBEA1ACCB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Computer_Labs_Sketch;
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


namespace Computer_Labs_Sketch {
    
    
    /// <summary>
    /// LabsTablePage
    /// </summary>
    public partial class LabsTablePage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 103 "..\..\..\LabsTablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Back;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\LabsTablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox labname;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\LabsTablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox labnumber;
        
        #line default
        #line hidden
        
        
        #line 177 "..\..\..\LabsTablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lablocation;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\..\LabsTablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox labequipment;
        
        #line default
        #line hidden
        
        
        #line 353 "..\..\..\LabsTablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Back2;
        
        #line default
        #line hidden
        
        
        #line 392 "..\..\..\LabsTablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid labs;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Computer Labs Sketch;component/labstablepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\LabsTablePage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 47 "..\..\..\LabsTablePage.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 65 "..\..\..\LabsTablePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonMinimize_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 73 "..\..\..\LabsTablePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClose_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Back = ((System.Windows.Controls.Button)(target));
            
            #line 104 "..\..\..\LabsTablePage.xaml"
            this.Back.Click += new System.Windows.RoutedEventHandler(this.Backbtn);
            
            #line default
            #line hidden
            return;
            case 5:
            this.labname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.labnumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.lablocation = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.labequipment = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            
            #line 217 "..\..\..\LabsTablePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Addbtn);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 250 "..\..\..\LabsTablePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Updatebtn);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 283 "..\..\..\LabsTablePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Deletebtn);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 316 "..\..\..\LabsTablePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Resetbtn);
            
            #line default
            #line hidden
            return;
            case 13:
            this.Back2 = ((System.Windows.Controls.Button)(target));
            
            #line 354 "..\..\..\LabsTablePage.xaml"
            this.Back2.Click += new System.Windows.RoutedEventHandler(this.Back2btn);
            
            #line default
            #line hidden
            return;
            case 14:
            this.labs = ((System.Windows.Controls.DataGrid)(target));
            
            #line 396 "..\..\..\LabsTablePage.xaml"
            this.labs.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

