Module Compatibility

    Public Function ReplaceGenericTokens(ByVal thismodule As DNNStuff.Welcome.Welcome, ByVal text As String) As String
        Dim ret As String

        Dim objTokenReplace As New DotNetNuke.Services.Tokens.TokenReplace()
        objTokenReplace.ModuleId = thismodule.ModuleId
        ret = objTokenReplace.ReplaceEnvironmentTokens(text)

        objTokenReplace.User = thismodule.UserInfo
        If thismodule.UserInfo.Profile.PreferredLocale IsNot Nothing Then ' will be nothing for anonymous users
            objTokenReplace.Language = thismodule.UserInfo.Profile.PreferredLocale
        End If
        ret = objTokenReplace.ReplaceEnvironmentTokens(ret)

        Return ret
    End Function

End Module
