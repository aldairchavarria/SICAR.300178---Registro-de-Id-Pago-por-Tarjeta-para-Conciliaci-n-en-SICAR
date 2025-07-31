Imports System.Runtime.Serialization

'INICIATIVA-219
Namespace ActualizarParticipante

    <Serializable()> _
        Public Class HeaderRequest

        Private _idTransaccion As String
        Private _msgid As String
        Private _timestamp As String
        Private _userId As String
        Private _channel As String
        Private _idApplication As String

        Public HeaderRequest()


        Public Property idTransaccion() As String
            Get
                Return _idTransaccion
            End Get
            Set(ByVal Value As String)
                _idTransaccion = Value
            End Set
        End Property

        Public Property msgid() As String
            Get
                Return _msgid
            End Get
            Set(ByVal Value As String)
                _msgid = Value
            End Set
        End Property

        Public Property timestamp() As String
            Get
                Return _timestamp
            End Get
            Set(ByVal Value As String)
                _timestamp = Value
            End Set
        End Property

        Public Property userId() As String
            Get
                Return _userId
            End Get
            Set(ByVal Value As String)
                _userId = Value
            End Set
        End Property

        Public Property channel() As String
            Get
                Return _channel
            End Get
            Set(ByVal Value As String)
                _channel = Value
            End Set
        End Property

        Public Property idApplication() As String
            Get
                Return _idApplication
            End Get
            Set(ByVal Value As String)
                _idApplication = Value
            End Set
        End Property


    End Class

End Namespace