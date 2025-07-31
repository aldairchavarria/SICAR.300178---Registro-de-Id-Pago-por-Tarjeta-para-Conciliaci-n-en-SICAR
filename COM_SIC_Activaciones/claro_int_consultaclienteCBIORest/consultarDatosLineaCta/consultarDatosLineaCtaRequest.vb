Namespace claro_int_consultaclienteCBIORest.consultarDatosLineaCta

    'INICIATIVA-219
    Public Class consultarDatosLineaCtaRequest

        'Private _csCode As String ''INC000003246390
        Private _dirNum As String
        Private _listaOpcional As ArrayList
        ''INC000003246390
        'Public Property csCode() As String
        '    Get
        '        Return _csCode
        '    End Get
        '    Set(ByVal Value As String)
        '        _csCode = Value
        '    End Set
        'End Property

        Public Property dirNum() As String
            Get
                Return _dirNum
            End Get
            Set(ByVal Value As String)
                _dirNum = Value
            End Set
        End Property

        Public Property listaOpcional() As ArrayList
            Get
                Return _listaOpcional
            End Get
            Set(ByVal Value As ArrayList)
                _listaOpcional = Value
            End Set
        End Property

    End Class

End Namespace