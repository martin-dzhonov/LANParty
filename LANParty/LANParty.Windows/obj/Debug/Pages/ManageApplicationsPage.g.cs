﻿

#pragma checksum "C:\Users\Martin\Documents\GitHub\LANParty\LANParty\LANParty.Windows\Pages\ManageApplicationsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "40D1D6CC5BA2185DDC7105823920B07A"
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
    partial class ManageApplicationsPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 55 "..\..\Pages\ManageApplicationsPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.ListView_ItemClick;
                 #line default
                 #line hidden
                #line 57 "..\..\Pages\ManageApplicationsPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).DragEnter += this.ListView_DragEnter;
                 #line default
                 #line hidden
                #line 58 "..\..\Pages\ManageApplicationsPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).DragItemsStarting += this.ListView_DragItemsStarting;
                 #line default
                 #line hidden
                #line 59 "..\..\Pages\ManageApplicationsPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).DragLeave += this.ListView_DragLeave;
                 #line default
                 #line hidden
                #line 60 "..\..\Pages\ManageApplicationsPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Drop += this.ListView_Drop;
                 #line default
                 #line hidden
                #line 61 "..\..\Pages\ManageApplicationsPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).DragOver += this.ListView_DragOver;
                 #line default
                 #line hidden
                #line 63 "..\..\Pages\ManageApplicationsPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).DoubleTapped += this.ListView_DoubleTapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


