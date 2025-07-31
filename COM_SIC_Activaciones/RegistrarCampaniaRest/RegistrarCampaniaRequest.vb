'INC000002161718 inicio
Imports System

Public Class RegistrarCampaniaRequest

    Private _auditRequest As BEAuditRequest
    Private _registrarCampania As RegistrarCampania

    Public Property auditRequest() As BEAuditRequest
        Get
            Return _auditRequest
        End Get
        Set(ByVal Value As BEAuditRequest)
            _auditRequest = Value
        End Set
    End Property
    Public Property registrarCampania() As RegistrarCampania
        Get
            Return _registrarCampania
        End Get
        Set(ByVal Value As registrarCampania)
            _registrarCampania = Value
        End Set
    End Property

    Public Sub New()
        _auditRequest = New BEAuditRequest
        _registrarCampania = New RegistrarCampania
    End Sub

End Class
