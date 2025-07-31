Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class SGAResponseVenta
    Public SGAResponseVenta() 'PROY-140126

    Public Sub New()
        codRepuesta = -99
    End Sub
    Public idnumsec As Integer

    '<remarks/>
    Public codcli As String

    '<remarks/>
    Public codsucins As String

    '<remarks/>
    Public codsucfac As String

    '<remarks/>
    Public codect As String

    '<remarks/>
    Public numslc As String

    '<remarks/>
    Public codsolot As Integer

    '<remarks/>
    Public tiptra As Integer

    '<remarks/>
    Public estsol As Integer

    '<remarks/>
    Public fecagenda As String

    '<remarks/>
    Public hora As String

    '<remarks/>
    Public codcon As Integer

    '<remarks/>
    Public codcuadri As String
    Public codRepuesta As String
    Public msgRepuesta As String
End Class
