Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class BEEnviarMensajeResponse
    Public _idTransaccion As String
    Public _mensajeRespuesta As String
    Public _codigoRespuesta As String
    Public BEEnviarMensajeResponse() 'PROY-140126

Public Sub New()
End Sub

    Public Property idTransaccion() As String
        Set(ByVal value As String)
            _idTransaccion = value
        End Set
        Get
            Return _idTransaccion
        End Get
    End Property

    Public Property mensajeRespuesta() As String
        Set(ByVal value As String)
            _mensajeRespuesta = value
        End Set
        Get
            Return _mensajeRespuesta
        End Get
    End Property

    Public Property codigoRespuesta() As Integer
        Set(ByVal value As Integer)
            _codigoRespuesta = value
        End Set
        Get
            Return _codigoRespuesta
        End Get
    End Property
End Class