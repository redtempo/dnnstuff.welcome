'***************************************************************************/
'* Copyright (c) 2007 by DNNStuff.
'* All rights reserved.
'*
'* Date:        March 19,2007
'* Author:      Richard Edwards
'* Description: DotNetNuke Module for displaying a Welcome message
'*************/

Imports DotNetNuke

Namespace DNNStuff.Welcome

    Public Class ModuleSettings
        Private _ModuleId As Integer = 0

#Region " Properties from Database"
        Private _Text As String = ""
        Public Property Text() As String
            Get
                Return _Text
            End Get
            Set(ByVal Value As String)
                _Text = Value
            End Set
        End Property

        Private _WhenHiddenText As String = ""
        Public Property WhenHiddenText() As String
            Get
                Return _WhenHiddenText
            End Get
            Set(ByVal Value As String)
                _WhenHiddenText = Value
            End Set
        End Property

#End Region
#Region " Properties"

        Private Const SETTING_VIEWS As String = "Views"
        Private _Views As Integer = 3
        Public Property Views() As Integer
            Get
                Return _Views
            End Get
            Set(ByVal Value As Integer)
                _Views = Value
            End Set
        End Property

        Private Const SETTING_VIEWSSESSIONBASED As String = "ViewsSessionBased"
        Private _ViewsSessionBased As Boolean = False
        Public Property ViewsSessionBased() As Boolean
            Get
                Return _ViewsSessionBased
            End Get
            Set(ByVal value As Boolean)
                _ViewsSessionBased = value
            End Set
        End Property

        Private Const SETTING_VERSION As String = "Version"
        Private _Version As Integer = 1
        Public Property Version() As Integer
            Get
                Return _Version
            End Get
            Set(ByVal Value As Integer)
                _Version = Value
            End Set
        End Property

        Private Const SETTING_DISPLAYHIDE As String = "DisplayHide"
        Private _DisplayHide As Boolean = False
        Public Property DisplayHide() As Boolean
            Get
                Return _DisplayHide
            End Get
            Set(ByVal value As Boolean)
                _DisplayHide = value
            End Set
        End Property

        Private Const SETTING_HIDETEXT As String = "HideText"
        Private _HideText As String = ""
        Public Property HideText() As String
            Get
                Return _HideText
            End Get
            Set(ByVal Value As String)
                _HideText = Value
            End Set
        End Property

        Private Const SETTING_UNHIDETEXT As String = "UnhideText"
        Private _UnhideText As String = ""
        Public Property UnhideText() As String
            Get
                Return _UnhideText
            End Get
            Set(ByVal Value As String)
                _UnhideText = Value
            End Set
        End Property

        Private Const SETTING_DISPLAYKEEPHIDDEN As String = "DisplayKeepHidden"
        Private _DisplayKeepHidden As String = "None"
        Public Property DisplayKeepHidden() As String
            Get
                Return _DisplayKeepHidden
            End Get
            Set(ByVal value As String)
                ' convert legacy values, IUpgradeable doens't seem to fire
                Select Case value
                    Case "True"
                        value = "Checkbox"
                    Case "False"
                        value = "None"
                End Select
                _DisplayKeepHidden = value
            End Set
        End Property

        Private Const SETTING_KEEPHIDDENTEXT As String = "KeepHiddenText"
        Private _KeepHiddenText As String = ""
        Public Property KeepHiddenText() As String
            Get
                Return _KeepHiddenText
            End Get
            Set(ByVal Value As String)
                _KeepHiddenText = Value
            End Set
        End Property

#End Region

#Region "Methods"
        Public Sub New(ByVal moduleId As Integer)
            _ModuleId = moduleId

            LoadSettings()
        End Sub

        Private Sub LoadSettings()
            Dim ctrl As New DotNetNuke.Entities.Modules.ModuleController
            Dim settings As Hashtable = ctrl.GetModuleSettings(_ModuleId)

            Me.Views = Convert.ToInt32(DNNUtilities.GetSetting(settings, SETTING_VIEWS, "3"))
            Me.ViewsSessionBased = Convert.ToBoolean(DNNUtilities.GetSetting(settings, SETTING_VIEWSSESSIONBASED, "False"))
            Me.Version = Convert.ToInt32(DNNUtilities.GetSetting(settings, SETTING_VERSION, "1"))
            Me.DisplayHide = Convert.ToBoolean(DNNUtilities.GetSetting(settings, SETTING_DISPLAYHIDE, "False"))
            Me.HideText = DNNUtilities.GetSetting(settings, SETTING_HIDETEXT, "Hide")
            Me.DisplayKeepHidden = DNNUtilities.GetSetting(settings, SETTING_DISPLAYKEEPHIDDEN, "None")
            Me.KeepHiddenText = DNNUtilities.GetSetting(settings, SETTING_KEEPHIDDENTEXT, "Don't show again")
            Me.UnhideText = DNNUtilities.GetSetting(settings, SETTING_UNHIDETEXT, "Show")

            ' using controller for text
            Dim controller As New WelcomeController
            Dim obj As WelcomeInfo = controller.GetWelcome(_ModuleId)
            If obj IsNot Nothing Then
                Me.Text = obj.FreeFormText
                Me.WhenHiddenText = obj.WhenHiddenText
            End If
            controller = Nothing

        End Sub

        Public Sub UpdateSettings()
            Dim ctrl As New DotNetNuke.Entities.Modules.ModuleController
            With ctrl
                .UpdateModuleSetting(_ModuleId, SETTING_VIEWS, _Views.ToString)
                .UpdateModuleSetting(_ModuleId, SETTING_VIEWSSESSIONBASED, _ViewsSessionBased.ToString)
                .UpdateModuleSetting(_ModuleId, SETTING_VERSION, _Version.ToString)
                .UpdateModuleSetting(_ModuleId, SETTING_DISPLAYHIDE, _DisplayHide.ToString)
                .UpdateModuleSetting(_ModuleId, SETTING_HIDETEXT, _HideText)
                .UpdateModuleSetting(_ModuleId, SETTING_DISPLAYKEEPHIDDEN, _DisplayKeepHidden)
                .UpdateModuleSetting(_ModuleId, SETTING_KEEPHIDDENTEXT, _KeepHiddenText)
                .UpdateModuleSetting(_ModuleId, SETTING_UNHIDETEXT, _UnhideText)
            End With

            ' using controller for text
            Dim controller As New WelcomeController
            Dim obj As New WelcomeInfo
            With obj
                .FreeFormText = _Text
                .WhenHiddenText = _WhenHiddenText
                .ModuleId = _ModuleId
            End With
            controller.UpdateWelcome(obj)
            controller = Nothing

        End Sub

#End Region

    End Class
End Namespace
