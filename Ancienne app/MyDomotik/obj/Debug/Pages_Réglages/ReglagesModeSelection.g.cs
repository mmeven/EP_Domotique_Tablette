﻿

#pragma checksum "C:\Users\Diane\Desktop\Etude pratique\EP_Domotique_Tablette\Ancienne app\MyDomotik\Pages_Réglages\ReglagesModeSelection.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1DD0FB61247E5C7FA1A76BB87FEA582D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyDomotik
{
    partial class ReglagesModeSelection : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 62 "..\..\Pages_Réglages\ReglagesModeSelection.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SelectionClic_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 65 "..\..\Pages_Réglages\ReglagesModeSelection.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SelectionParDefilement;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 34 "..\..\Pages_Réglages\ReglagesModeSelection.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.retourAdminPage;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 47 "..\..\Pages_Réglages\ReglagesModeSelection.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.exitAdmin;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


