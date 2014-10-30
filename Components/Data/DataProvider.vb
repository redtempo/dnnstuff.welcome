'* DataProvider.vb
'*
'* Copyright (c) 2007 by DNNStuff.
'* All rights reserved.
'*
'* Date:        March 19,2007
'* Author:      Richard Edwards
'* Description: Data provider
'*************/

Imports System
Imports DotNetNuke

Namespace DNNStuff.Welcome

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' An abstract class for the data access layer
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public MustInherit Class DataProvider

#Region "Shared/Static Methods"

        ' singleton reference to the instantiated object 
        Private Shared objProvider As DataProvider = Nothing

        ' constructor
        Shared Sub New()
            CreateProvider()
        End Sub

        ' dynamically create provider
        Private Shared Sub CreateProvider()
            objProvider = CType(Framework.Reflection.CreateObject("data", "DNNStuff.Welcome", ""), DataProvider)
        End Sub

        ' return the provider
        Public Shared Shadows Function Instance() As DataProvider
            Return objProvider
        End Function

#End Region

#Region "Welcome Abstract Methods"

        Public MustOverride Function GetWelcome(ByVal ModuleId As Integer) As IDataReader
        Public MustOverride Function UpdateWelcome(ByVal WelcomeId As Integer, ByVal ModuleId As Integer, ByVal FreeFormText As String, ByVal WhenHiddenText As String) As IDataReader
        Public MustOverride Sub DeleteWelcome(ByVal WelcomeId As Integer)

#End Region

    End Class

End Namespace