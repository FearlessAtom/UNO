﻿#pragma checksum "..\..\..\draworplay.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "684D66A4AEE1D780BA5A421BBED1E86A4B2E8668"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
using UNOui;


namespace UNOui {
    
    
    /// <summary>
    /// draworplay
    /// </summary>
    public partial class draworplay : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 39 "..\..\..\draworplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition row;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\draworplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button playbutton;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\draworplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button drawbutton;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\draworplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image cardimage;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UNOui;component/draworplay.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\draworplay.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\draworplay.xaml"
            ((UNOui.draworplay)(target)).Loaded += new System.Windows.RoutedEventHandler(this.loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.row = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 3:
            this.playbutton = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\draworplay.xaml"
            this.playbutton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.mouseleave);
            
            #line default
            #line hidden
            
            #line 48 "..\..\..\draworplay.xaml"
            this.playbutton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.mouseenter);
            
            #line default
            #line hidden
            
            #line 48 "..\..\..\draworplay.xaml"
            this.playbutton.Click += new System.Windows.RoutedEventHandler(this.play);
            
            #line default
            #line hidden
            return;
            case 4:
            this.drawbutton = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\draworplay.xaml"
            this.drawbutton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.mouseleave);
            
            #line default
            #line hidden
            
            #line 49 "..\..\..\draworplay.xaml"
            this.drawbutton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.mouseenter);
            
            #line default
            #line hidden
            
            #line 49 "..\..\..\draworplay.xaml"
            this.drawbutton.Click += new System.Windows.RoutedEventHandler(this.draw);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cardimage = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

