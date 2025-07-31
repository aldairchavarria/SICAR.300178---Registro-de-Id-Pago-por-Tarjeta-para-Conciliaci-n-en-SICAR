Namespace ActualizarDatosFacturacion

    'INICIATIVA-219
    Public Class ListaOpcional

        Private _clave As String
        Private _valor As String

        Public Property clave() As String
            Get
                Return _clave
            End Get
            Set(ByVal Value As String)
                _clave = Value
            End Set
        End Property

        Public Property valor() As String
            Get
                Return _valor
            End Get
            Set(ByVal Value As String)
                _valor = Value
            End Set
        End Property


    End Class


End Namespace
