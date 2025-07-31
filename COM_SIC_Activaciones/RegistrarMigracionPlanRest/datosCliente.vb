Public Class datosCliente

    Private _csId As String
    Private _csIdPub As String
    Private _customerSegment As String
    Private _customerBehaviorPaid As String
    Private _customerBillingAccountCode As String
    Private _customerBillCycleId As String

    Public Property csId() As String
        Get
            Return _csId
        End Get
        Set(ByVal Value As String)
            _csId = Value
        End Set
    End Property

    Public Property csIdPub() As String
        Get
            Return _csIdPub
        End Get
        Set(ByVal Value As String)
            _csIdPub = Value
        End Set
    End Property

    Public Property customerSegment() As String
        Get
            Return _customerSegment
        End Get
        Set(ByVal Value As String)
            _customerSegment = Value
        End Set
    End Property

    Public Property customerBehaviorPaid() As String
        Get
            Return _customerBehaviorPaid
        End Get
        Set(ByVal Value As String)
            _customerBehaviorPaid = Value
        End Set
    End Property

    Public Property customerBillingAccountCode() As String
        Get
            Return _customerBillingAccountCode
        End Get
        Set(ByVal Value As String)
            _customerBillingAccountCode = Value
        End Set
    End Property

    Public Property customerBillCycleId() As String
        Get
            Return _customerBillCycleId
        End Get
        Set(ByVal Value As String)
            _customerBillCycleId = Value
        End Set
    End Property

End Class
