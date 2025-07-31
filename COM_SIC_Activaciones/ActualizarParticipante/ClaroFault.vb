Option Explicit On 

'INICIATIVA-219
Namespace ActualizarParticipante


    Public Class ClaroFault

        Private _idAudit As String
        Private _codeError As String
        Private _descriptionError As String
        Private _locationError As String
        Private _date As String
        Private _originError As String


        Public Property idAudit() As String
            Get
                Return _idAudit
            End Get
            Set(ByVal Value As String)
                _idAudit = Value
            End Set
        End Property

        Public Property codeError() As String
            Get
                Return _codeError
            End Get
            Set(ByVal Value As String)
                _codeError = Value
            End Set
        End Property

        Public Property descriptionError() As String
            Get
                Return _descriptionError
            End Get
            Set(ByVal Value As String)
                _descriptionError = Value
            End Set
        End Property

        Public Property locationError() As String
            Get
                Return _locationError
            End Get
            Set(ByVal Value As String)
                _locationError = Value
            End Set
        End Property

        'Public Property  Date() As String 
        '    Get
        '        Return _date
        '    End Get
        '    Set(ByVal Value As String)
        '        _date = Value
        '    End Set
        'End Property

        Public Property originError() As String
            Get
                Return _originError
            End Get
            Set(ByVal Value As String)
                _originError = Value
            End Set
        End Property



    End Class

End Namespace