﻿

#pragma checksum "C:\Users\Martin\Documents\GitHub\LANParty\LANParty\LANParty.Windows\Pages\PartiesSearch.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D6D5AC66639B5449607B0ED6B40564DB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LANParty.Pages
{
    partial class PartiesSearch : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 45 "..\..\Pages\PartiesSearch.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.ListView_ItemClick;
                 #line default
                 #line hidden
                #line 47 "..\..\Pages\PartiesSearch.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Holding += this.ListView_Holding;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


