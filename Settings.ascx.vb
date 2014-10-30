'***************************************************************************/
'* Copyright (c) 2007 by DNNStuff.
'* All rights reserved.
'*
'* Date:        March 19,2007
'* Author:      Richard Edwards
'* Description: DotNetNuke Module for displaying a Welcome message
'*************/

Imports DotNetNuke
Imports DotNetNuke.Common

Imports System.Configuration

Namespace DNNStuff.Welcome

    Partial Class Settings
        Inherits DotNetNuke.Entities.Modules.PortalModuleBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Base Method Implementations"
        Private Sub UpdateSettings()
            Dim ms As New ModuleSettings(ModuleId)
            With ms
                .Text = teText.Text
                .WhenHiddenText = teWhenHiddenText.Text
                .Views = Convert.ToInt32(txtViews.Text)
                .ViewsSessionBased = chkViewsSessionBased.Checked
                .DisplayHide = chkDisplayHide.Checked
                .HideText = txtHideText.Text
                .DisplayKeepHidden = ddlDisplayKeepHidden.SelectedValue
                .KeepHiddenText = txtKeepHiddenText.Text
                .Version = Convert.ToInt32(txtVersion.Text)
                .UnhideText = txtUnhideText.Text
            End With
            ms.UpdateSettings()
        End Sub

        Private Sub LoadSettings()
            Dim ms As New ModuleSettings(ModuleId)

            teText.Text = ms.Text
            teWhenHiddenText.Text = ms.WhenHiddenText
            txtViews.Text = ms.Views.ToString
            chkViewsSessionBased.Checked = ms.ViewsSessionBased
            chkDisplayHide.Checked = ms.DisplayHide
            txtHideText.Text = ms.HideText
            Dim li As ListItem = ddlDisplayKeepHidden.Items.FindByValue(ms.DisplayKeepHidden)
            If li IsNot Nothing Then li.Selected = True
            txtKeepHiddenText.Text = ms.KeepHiddenText
            txtVersion.Text = ms.Version.ToString
            txtUnhideText.Text = ms.UnhideText

        End Sub
#End Region

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
            UpdateSettings()
            ' Redirect back to the portal home page
            Response.Redirect(NavigateURL(), True)
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            ' Redirect back to the portal home page
            Response.Redirect(NavigateURL(), True)
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If DNNUtilities.SafeDNNVersion().Major = 5 Then
                DNNUtilities.InjectCSS(Me.Page, ResolveUrl("Resources/Support/edit_5.css"))
            Else
                DNNUtilities.InjectCSS(Me.Page, ResolveUrl("Resources/Support/edit.css"))
            End If
            Page.ClientScript.RegisterClientScriptInclude(Me.GetType, "yeti", ResolveUrl("resources/support/yetii-min.js"))

            If Not Page.IsPostBack Then
                LoadSettings()

            End If
        End Sub
    End Class

End Namespace
