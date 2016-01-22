'***************************************************************************/
'* Copyright (c) 2007 by DNNStuff.
'* All rights reserved.
'*
'* Date:        March 19,2007
'* Author:      Richard Edwards
'* Description: DotNetNuke Module for displaying a Welcome message
'*************/

Imports System
Imports System.Configuration
Imports System.Data
Imports System.XML
Imports System.Web
Imports System.Collections.Generic

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities.XmlUtils
Imports DotNetNuke.Common.Utilities


Namespace DNNStuff.Welcome

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' The Controller class for Welcome
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class WelcomeController
        Implements Entities.Modules.IPortable


#Region "Injection Public Methods"

        Public Function GetWelcome(ByVal WelcomeId As Integer) As WelcomeInfo
            Return CType(CBO.FillObject(DataProvider.Instance().GetWelcome(WelcomeId), GetType(WelcomeInfo)), WelcomeInfo)
        End Function

        Public Sub UpdateWelcome(ByVal objInfo As WelcomeInfo)
            DataProvider.Instance().UpdateWelcome(objInfo.WelcomeId, objInfo.ModuleId, objInfo.FreeFormText, objInfo.WhenHiddenText)
        End Sub

        Public Sub DeleteWelcome(ByVal WelcomeId As Integer)
            DataProvider.Instance().DeleteWelcome(WelcomeId)
        End Sub

#End Region

#Region "Optional Interfaces"
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' ExportModule implements the IPortable ExportModule Interface
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="ModuleID">The Id of the module to be exported</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Function ExportModule(ByVal ModuleID As Integer) As String Implements DotNetNuke.Entities.Modules.IPortable.ExportModule
            Dim strXML As New Text.StringBuilder()
            Dim settings As New XmlWriterSettings()
            settings.Indent = True
            settings.OmitXmlDeclaration = True

            Dim Writer As XmlWriter = XmlWriter.Create(strXML, settings)
            Writer.WriteStartElement("Welcome")

            Dim ms As New ModuleSettings(ModuleID)
            Writer.WriteElementString("text", ms.Text)
            Writer.WriteElementString("views", ms.Views.ToString)
            Writer.WriteElementString("viewssessionbased", ms.ViewsSessionBased.ToString)
            Writer.WriteElementString("displayhide", ms.DisplayHide.ToString)
            Writer.WriteElementString("hidetext", ms.HideText)
            Writer.WriteElementString("unhidetext", ms.UnhideText)
            Writer.WriteElementString("displaykeephidden", ms.DisplayKeepHidden.ToString)
            Writer.WriteElementString("keephiddentext", ms.KeepHiddenText)
            Writer.WriteElementString("version", ms.Version.ToString)
            Writer.WriteElementString("whenhiddentext", ms.WhenHiddenText)

            Writer.WriteEndElement()

            Writer.Close()

            Return strXML.ToString()
        End Function
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' ImportModule implements the IPortable ImportModule Interface
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <param name="ModuleID">The Id of the module to be imported</param>
        ''' <param name="Content">The content to be imported</param>
        ''' <param name="Version">The version of the module to be imported</param>
        ''' <param name="UserId">The Id of the user performing the import</param>
        ''' <history>
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub ImportModule(ByVal ModuleID As Integer, ByVal Content As String, ByVal Version As String, ByVal UserId As Integer) Implements DotNetNuke.Entities.Modules.IPortable.ImportModule

            Dim xmlWelcome As XmlNode = GetContent(Content, "Welcome")

            Dim ms As New ModuleSettings(ModuleID)
            With ms
                .Text = xmlWelcome.SelectSingleNode("text").InnerText
                .Views = Convert.ToInt32(xmlWelcome.SelectSingleNode("views").InnerText)
                .ViewsSessionBased = Convert.ToBoolean(xmlWelcome.SelectSingleNode("viewssessionbased").InnerText)
                .DisplayHide = Convert.ToBoolean(xmlWelcome.SelectSingleNode("displayhide").InnerText)
                .HideText = xmlWelcome.SelectSingleNode("hidetext").InnerText
                .UnhideText = xmlWelcome.SelectSingleNode("unhidetext").InnerText
                .DisplayKeepHidden = xmlWelcome.SelectSingleNode("displaykeephidden").InnerText
                .KeepHiddenText = xmlWelcome.SelectSingleNode("keephiddentext").InnerText
                .Version = Convert.ToInt32(xmlWelcome.SelectSingleNode("version").InnerText)
                .WhenHiddenText = xmlWelcome.SelectSingleNode("whenhiddentext").InnerText
            End With
            ms.UpdateSettings()

        End Sub

#End Region

    End Class
End Namespace
