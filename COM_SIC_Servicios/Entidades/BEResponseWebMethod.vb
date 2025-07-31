Imports System.Runtime.Serialization

<Serializable()> _
Public Class BEResponseWebMethod


    Public BEResponseWebMethod()
    Private _strCodigoRespuesta As String
    Private _strMensajeRespuesta As String
    Private _strMensajeErrror As String

    Public Property CodigoRespuesta() As String
        Set(ByVal value As String)
            _strCodigoRespuesta = value
        End Set
        Get
            Return _strCodigoRespuesta
        End Get
    End Property
    Public Property MensajeRespuesta() As String
        Set(ByVal value As String)
            _strMensajeRespuesta = value
        End Set
        Get
            Return _strMensajeRespuesta
        End Get
    End Property
    Public Property MensajeErrror() As String
        Set(ByVal value As String)
            _strMensajeErrror = value
        End Set
        Get
            Return _strMensajeErrror
        End Get
    End Property

End Class
