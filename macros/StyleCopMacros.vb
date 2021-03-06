Imports System
Imports EnvDTE
Imports EnvDTE80
Imports EnvDTE90
Imports System.Diagnostics
Imports System.Text
Imports System.IO

Public Module StyleCopMacros

    Public Sub DeleteDoubleBlankLine()

        Dim x As Integer = 1
        Dim selection As EnvDTE.TextSelection
        Dim startPoint As EnvDTE.EditPoint
        Dim endPoint As TextPoint
        Dim commentStart As String

        selection = DTE.ActiveDocument.Selection()
        startPoint = selection.TopPoint.CreateEditPoint()
        endPoint = selection.BottomPoint
        Dim line As String
        Dim originalLine As String
        Dim lastLineIsEmpty As Boolean = False
        Dim lineNumber As Integer
        Dim strippedText As StringBuilder = New StringBuilder()

        Using sr As New StringReader(selection.Text)
            Do
                originalLine = sr.ReadLine()
                If originalLine Is Nothing Then
                    Exit Do
                End If

                lineNumber = startPoint.Line
                startPoint.LineDown()
                line = originalLine.Replace(" ", "").Replace(vbTab, "")
                If line = "" Then
                    If Not lastLineIsEmpty Then
                        strippedText.AppendLine(originalLine)
                    End If
                    lastLineIsEmpty = True
                Else
                    strippedText.AppendLine(originalLine)
                    lastLineIsEmpty = False
                End If
            Loop
        End Using

        selection.Delete()
        selection.Insert(strippedText.ToString())
    End Sub

    Sub StyleCopIt()
        DTE.ExecuteCommand("Edit.SortUsings")
        DTE.ExecuteCommand("Rauchy.Regionerate.Presentation.Addins.VisualStudio2005.Connect.RegionerateThis")
        DTE.ActiveDocument.Selection.SelectAll()
        DeleteDoubleBlankLine()
        DTE.ActiveDocument.Selection.SelectAll()
        DTE.ActiveDocument.Selection.Untabify()
    End Sub

End Module
