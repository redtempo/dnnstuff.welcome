'* Info.vb
'*
'* Copyright (c) 2007 by DNNStuff.
'* All rights reserved.
'*
'* Date:        March 19,2007
'* Author:      Richard Edwards
'* Description: Data classes
'*************/

Imports System
Imports System.Configuration
Imports System.Data

Namespace DNNStuff.Welcome

    Public Class WelcomeInfo

#Region "Private Members"
        Private _WelcomeId As Integer
        Private _ModuleId As Integer
        Private _FreeFormText As String
        Private _WhenHiddenText As String
#End Region

#Region "Constructors"
        Public Sub New()
        End Sub
#End Region

#Region "Public Properties"
        Public Property WelcomeId() As Integer
            Get
                Return _WelcomeId
            End Get
            Set(ByVal value As Integer)
                _WelcomeId = value
            End Set
        End Property

        Public Property ModuleId() As Integer
            Get
                Return _ModuleId
            End Get
            Set(ByVal value As Integer)
                _ModuleId = value
            End Set
        End Property

        Public Property FreeFormText() As String
            Get
                Return _FreeFormText
            End Get
            Set(ByVal Value As String)
                _FreeFormText = Value
            End Set
        End Property

        Public Property WhenHiddenText() As String
            Get
                Return _WhenHiddenText
            End Get
            Set(ByVal value As String)
                _WhenHiddenText = value
            End Set
        End Property
#End Region

    End Class

End Namespace
