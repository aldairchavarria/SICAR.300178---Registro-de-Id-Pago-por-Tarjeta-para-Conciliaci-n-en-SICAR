'PROY-140623 - IDEA 142200 - Nuevo formato contratos Osiptel
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class RepositorioDocumentos

    Private _nombreCorto As String
    Private _rutaPdf As String
    Private _nombrePdf As String
    Private _codigoAcuerdo As String

    Public Property nombreCorto() As String
        Get
            Return _nombreCorto
        End Get
        Set(ByVal Value As String)
            _nombreCorto = Value
        End Set
    End Property

    Public Property rutaPdf() As String
        Get
            Return _rutaPdf
        End Get
        Set(ByVal Value As String)
            _rutaPdf = Value
        End Set
    End Property

    Private Property nombrePdf() As String
        Get
            Return _nombrePdf
        End Get
        Set(ByVal Value As String)
            _nombrePdf = Value
        End Set
    End Property

    Private Property codigoAcuerdo() As String
        Get
            Return _codigoAcuerdo
        End Get
        Set(ByVal Value As String)
            _codigoAcuerdo = Value
        End Set
    End Property

End Class
