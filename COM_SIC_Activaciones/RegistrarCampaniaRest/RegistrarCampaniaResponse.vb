'INC000002161718 inicio
Imports System

Public Class RegistrarCampaniaResponse

    Private _auditResponse As BEAuditResponse
    Private _listaResponseOpcional As listaResponseOpcional


    Public Property auditResponse() As BEAuditResponse
        Get
            Return _auditResponse
        End Get
        Set(ByVal Value As BEAuditResponse)
            _auditResponse = Value
        End Set
    End Property
    Public Property listaResponseOpcional() As listaResponseOpcional
        Get
            Return _listaResponseOpcional
        End Get
        Set(ByVal Value As listaResponseOpcional)
            _listaResponseOpcional = Value
        End Set
    End Property


    Public Sub New()
        _auditResponse = New BEAuditResponse
        _listaResponseOpcional = New ListaResponseOpcional
    End Sub
End Class
