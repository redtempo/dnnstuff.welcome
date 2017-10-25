'***************************************************************************/
'* Copyright (c) 2007 by DNNStuff.
'* All rights reserved.
'*
'* Date:        March 19,2007
'* Author:      Richard Edwards
'* Description: DotNetNuke Module for displaying a Welcome message
'*************/

Imports DotNetNuke
Imports DotNetNuke.Services.Localization
Imports System.Text.RegularExpressions
Imports DotNetNuke.Security.Permissions
Imports DotNetNuke.Security

Namespace DNNStuff.Welcome

    Partial Class Welcome
        Inherits DotNetNuke.Entities.Modules.PortalModuleBase
        Implements DotNetNuke.Entities.Modules.IActionable

        ' private
        Private _ms As ModuleSettings
        Private _unhiddenThisRequest As Boolean = False

        ' constants
        '' tokens
        Const TOKEN_HIDE As String = "[HIDE]"
        Const TOKEN_KEEPHIDDEN As String = "[KEEPHIDDEN]"
        Const TOKEN_KEEPHIDDENLINK As String = "[KEEPHIDDENLINK]"
        Const TOKEN_UNHIDE As String = "[UNHIDE]"

        '' choices
        Const CHOICE_NONE As String = "NONE"

#Region " Properties"
        Private ReadOnly Property CookieKey() As String
            Get
                Return "WelcomeViews" & ModuleId.ToString & "-" & UserId.ToString
            End Get
        End Property

        Private ReadOnly Property HideSessionKey() As String
            Get
                Return "WelcomeHide" & ModuleId.ToString & "-" & UserId.ToString & "-" & _ms.Version
            End Get
        End Property
        Private ReadOnly Property ViewsSessionKey() As String
            Get
                Return "WelcomeViews" & ModuleId.ToString & "-" & UserId.ToString & "-" & _ms.Version
            End Get
        End Property
#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()

            InitializeModule()

        End Sub


        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            LoadModule()

        End Sub
#End Region

#Region " Optional Interfaces"

        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
                Actions.Add(GetNextActionID, Localization.GetString(Entities.Modules.Actions.ModuleActionType.ContentOptions, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.ContentOptions, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
                Return Actions
            End Get
        End Property

#End Region

        Private Sub InitializeModule()
            Try
                ' get settings
                _ms = New ModuleSettings(ModuleId)

            Catch ex As Exception
                DotNetNuke.Services.Exceptions.ProcessModuleLoadException(Me, ex, True)
            End Try
        End Sub

        Private Sub LoadModule()

            Page.ClientScript.RegisterClientScriptInclude("dnnstuff", ResolveUrl("resources/support/dnnstuff-min.js"))

            If ShouldRenderModule() Then
                RenderModule()
            End If

        End Sub

        Private Function ShouldRenderModule() As Boolean
            If ModulePermissionController.CanEditModuleContent(ModuleConfiguration) And IsEditable Then Return True
            If Not String.IsNullOrEmpty(_ms.IpAddressRegEx) Then
                Return Regex.IsMatch(GetIPAddress(), _ms.IpAddressRegEx)
            End If
            Return True
        End Function

        Private Sub InjectText(ByVal s As String)
            Dim c As Control = ParseControl(s)
            WireUpClickEvents(c)
            pnlContent.Controls.Clear()
            pnlContent.Controls.Add(c)
        End Sub

        Private Sub WireUpClickEvents(ByVal parentControl As Control)
            ' wire click event handlers to click event
            For Each c As Control In parentControl.Controls
                If TypeOf c Is LinkButton Then
                    Dim lb As LinkButton = DirectCast(c, LinkButton)
                    AddHandler lb.Click, AddressOf Link_Click
                End If
            Next
        End Sub


        Private Sub RenderModule()
            Try

                ' deal with view trigger
                Dim count As Integer = GetCurrentCount()
                If ShouldShowModule(count) Then

                    Dim text As String = Server.HtmlDecode(_ms.Text)

                    If RequiresHide(text) Then text = AppendHide(text)

                    If RequiresKeepHidden(text) Then text = AppendKeepHidden(text)

                    ' do replacements
                    text = MakeWelcomeReplacements(text, count)

                    InjectText(text)

                Else
                    HideModule()
                End If

                SaveCurrentCount(count)
            Catch ex As Exception
                DotNetNuke.Services.Exceptions.ProcessModuleLoadException(Me, ex)
            End Try

        End Sub

        Private Function AppendHide(ByVal s As String) As String
            s = s & String.Format("<br />{0}", TOKEN_HIDE)
            Return s
        End Function

        Private Function RequiresHide(ByVal s As String) As Boolean
            Return _ms.DisplayHide And Not s.ToUpper.Contains(TOKEN_HIDE)
        End Function

        Private Function AppendKeepHidden(ByVal s As String) As String
            ' add KeepHiden button text if not present, also check KeepHiddenLink
            Select Case _ms.DisplayKeepHidden.ToUpper
                Case "CHECKBOX"
                    s = s & String.Format("<br />{0}", TOKEN_KEEPHIDDEN)
                Case "LINK"
                    s = s & String.Format("<br />{0}", TOKEN_KEEPHIDDENLINK)
            End Select
            Return s
        End Function

        Private Function RequiresKeepHidden(ByVal s As String) As Boolean
            Return _ms.DisplayKeepHidden.ToUpper <> CHOICE_NONE And Not (s.ToUpper.Contains(TOKEN_KEEPHIDDEN) Or s.ToUpper.Contains(TOKEN_KEEPHIDDENLINK))
        End Function

        Private Function MakeWelcomeReplacements(ByVal s As String, ByVal count As Integer) As String
            ' create custom replacements
            Dim custom As Hashtable = New Hashtable
            custom.Add("VIEWS", count.ToString)
            custom.Add("VIEWSREMAINING", Math.Max(_ms.Views - count, 0).ToString)
            custom.Add("HIDE", String.Format("<asp:LinkButton class=""WelcomeHide"" id=""btnHide"" runat=""server"" CommandName=""Hide"" text=""{0}"" />", _ms.HideText))
            custom.Add("KEEPHIDDENLINK", String.Format("<asp:LinkButton class=""WelcomeKeepHidden"" id=""btnKeepHidden"" runat=""server"" CommandName=""KeepHidden"" text=""{0}"" />", _ms.KeepHiddenText))
            custom.Add("KEEPHIDDEN", String.Format("<asp:Checkbox class=""WelcomeKeepHidden"" id=""chkKeepHidden"" runat=""server"" text=""{0}"" />", _ms.KeepHiddenText))
            custom.Add("UNHIDE", String.Format("<asp:LinkButton class=""WelcomeHide"" id=""btnUnhide"" runat=""server"" CommandName=""Unhide"" text=""{0}"" />", _ms.UnhideText))

            ' do replacements
            s = MakeReplacements(s, custom)

            Return s
        End Function

        Private Function MakeReplacements(ByVal s As String, ByVal custom As Hashtable) As String
            ' do generic replacements
            s = Compatibility.ReplaceGenericTokens(Me, s)

            Dim replacer As New DNNStuff.Utilities.RegularExpression.TokenReplacement(custom)
            replacer.ReplaceIfNotFound = False
            Return replacer.Replace(s)

        End Function

        Public Sub Link_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim cmd As LinkButton = DirectCast(sender, LinkButton)

            If KeepHiddenChecked() Or cmd.CommandName.ToUpper = "KEEPHIDDEN" Then
                ' keep hidden was checked or keep hidden link clicked
                SaveKeepHidden()
            End If

            ' don't hide if user has edit permissions
            If ModulePermissionController.CanEditModuleContent(ModuleConfiguration) Then Exit Sub

            Select Case cmd.CommandName.ToUpper
                Case "HIDE", "KEEPHIDDEN"
                    HideModule()
                Case "UNHIDE"
                    UnhideModule()
            End Select
        End Sub

        Private Function KeepHiddenChecked() As Boolean
            ' determines if the keep hidden checkbox has been checked

            If _ms.DisplayHide Then
                Dim cb As New CheckBox
                cb = DirectCast(FindControl("chkKeepHidden"), CheckBox)
                If Not cb Is Nothing Then
                    Return cb.Checked
                End If
            Else
                Return False
            End If
        End Function

        Private Function ShouldShowModule(ByVal count As Integer) As Boolean
            ' determines whether the module should be hidden or not
            If ModulePermissionController.CanEditModuleContent(ModuleConfiguration) And IsEditable Then Return True

            If CBool(Session(HideSessionKey)) Or GetKeepHidden() Then Return False

            If (_ms.Views > 0 And count > _ms.Views) Then Return False

            Return True

        End Function

        Public Shared Function GetIPAddress() As String
            Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
            Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If String.IsNullOrEmpty(sIPAddress) Then
                Return context.Request.ServerVariables("REMOTE_ADDR")
            Else
                Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
                Return ipArray(0)
            End If
        End Function

        Private Sub HideModule()
            If _ms.WhenHiddenText.Length > 0 Then
                Dim text As String = Server.HtmlDecode(_ms.WhenHiddenText)
                text = MakeWelcomeReplacements(text, 0)
                InjectText(text)
            Else
                Me.Visible = False
                Me.ContainerControl.Visible = False
            End If
            Session(HideSessionKey) = True
        End Sub

        Private Sub UnhideModule()
            _unhiddenThisRequest = True
            Me.Visible = True
            Me.ContainerControl.Visible = True
            Session(HideSessionKey) = False
            ResetViewCookie()
            LoadModule()
        End Sub

#Region " Cookie Handling"
        Private Function GetCurrentCount() As Integer
            Dim count As Integer = 0

            If _ms.ViewsSessionBased Then
                count = Convert.ToInt32(Session(ViewsSessionKey))
            Else
                ' grab cookie
                Dim cookie As HttpCookie = GetViewCookie()
                count = CInt(cookie.Values("Views"))
            End If

            count += 1
            Return count
        End Function

        Private Function GetViewCookie() As HttpCookie
            Dim cookie As HttpCookie
            If Request.Cookies(CookieKey) IsNot Nothing Then
                cookie = Request.Cookies(CookieKey)
                ' reset cookie if we have a different version now
                If CInt(cookie.Values("Version")) <> _ms.Version Then cookie = NewViewCookie()
            Else
                cookie = NewViewCookie()
            End If
            Return cookie
        End Function

        Private Function NewViewCookie() As HttpCookie
            Dim cookie As HttpCookie
            cookie = New HttpCookie(CookieKey)
            cookie.Values("Views") = "0"
            cookie.Values("Version") = _ms.Version.ToString
            cookie.Values("KeepHidden") = "False"
            Return cookie
        End Function

        Private Sub ResetViewCookie()
            Dim cookie As HttpCookie = NewViewCookie()
            Response.Cookies.Add(cookie)

            Session.Remove(ViewsSessionKey)
        End Sub

        Private Sub SaveCurrentCount(ByVal count As Integer)
            If _ms.ViewsSessionBased Then
                Session(ViewsSessionKey) = count
            Else
                ' save cookie
                Dim cookie As HttpCookie = GetViewCookie()
                With cookie
                    .Values("Views") = count.ToString
                    .Values("Version") = _ms.Version.ToString
                    .Expires = Date.Now.AddYears(1)
                End With
                Response.Cookies.Add(cookie)
            End If
        End Sub

        Private Function GetKeepHidden() As Boolean
            If _unhiddenThisRequest Then Return False
            Dim cookie As HttpCookie = GetViewCookie()
            Dim keepHidden As Boolean = CBool(cookie.Values("KeepHidden"))
            Return keepHidden
        End Function

        Private Sub SaveKeepHidden()
            ' save cookie
            Dim cookie As HttpCookie = GetViewCookie()
            With cookie
                .Values("KeepHidden") = "True"
                .Expires = Date.Now.AddYears(1)
            End With
            Response.Cookies.Add(cookie)
        End Sub
#End Region

    End Class

End Namespace
