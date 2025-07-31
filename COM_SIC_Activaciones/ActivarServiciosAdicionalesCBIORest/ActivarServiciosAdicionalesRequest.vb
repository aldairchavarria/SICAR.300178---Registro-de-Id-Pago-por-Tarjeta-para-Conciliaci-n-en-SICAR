Namespace ActivarServiciosAdicionalesCBIORest

    'INICIATIVA-219
    Public Class ActivarServiciosAdicionalesRequest
        Private _linea As String
        Private _billCycleId As String
        Private _coIdPub As String
        Private _csIdPub As String
        Private _csId As String
        Private _codTransaction As String
        Private _featureType As String
        Private _featureId As String
        Private _creditLimit As String
        Private _creditLimitType As String
        Private _poType As String
        Private _subType As String
        Private _subType2 As String
        Private _productOfferingId As String
        Private _action As String
        Private _paymentType As String
        Private _nroAcuerdo As String
        Private _customerFirstName As String
        Private _customerLastName As String
        Private _customerEmail As String

        Public Property linea() As String
            Get
                Return _linea
            End Get
            Set(ByVal Value As String)
                _linea = Value
            End Set
        End Property

        Public Property billCycleId() As String
            Get
                Return _billCycleId
            End Get
            Set(ByVal Value As String)
                _billCycleId = Value
            End Set
        End Property

        Public Property coIdPub() As String
            Get
                Return _coIdPub
            End Get
            Set(ByVal Value As String)
                _coIdPub = Value
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

        Public Property csId() As String
            Get
                Return _csId
            End Get
            Set(ByVal Value As String)
                _csId = Value
            End Set
        End Property

        Public Property codTransaction() As String
            Get
                Return _codTransaction
            End Get
            Set(ByVal Value As String)
                _codTransaction = Value
            End Set
        End Property

        Public Property featureType() As String
            Get
                Return _featureType
            End Get
            Set(ByVal Value As String)
                _featureType = Value
            End Set
        End Property

        Public Property featureId() As String
            Get
                Return _featureId
            End Get
            Set(ByVal Value As String)
                _featureId = Value
            End Set
        End Property

        Public Property creditLimit() As String
            Get
                Return _creditLimit
            End Get
            Set(ByVal Value As String)
                _creditLimit = Value
            End Set
        End Property

        Public Property creditLimitType() As String
            Get
                Return _creditLimitType
            End Get
            Set(ByVal Value As String)
                _creditLimitType = Value
            End Set
        End Property

        Public Property poType() As String
            Get
                Return _poType
            End Get
            Set(ByVal Value As String)
                _poType = Value
            End Set
        End Property

        Public Property subType() As String
            Get
                Return _subType
            End Get
            Set(ByVal Value As String)
                _subType = Value
            End Set
        End Property

        Public Property subType2() As String
            Get
                Return _subType2
            End Get
            Set(ByVal Value As String)
                _subType2 = Value
            End Set
        End Property

        Public Property productOfferingId() As String
            Get
                Return _productOfferingId
            End Get
            Set(ByVal Value As String)
                _productOfferingId = Value
            End Set
        End Property

        Public Property action() As String
            Get
                Return _action
            End Get
            Set(ByVal Value As String)
                _action = Value
            End Set
        End Property

        Public Property paymentType() As String
            Get
                Return _paymentType
            End Get
            Set(ByVal Value As String)
                _paymentType = Value
            End Set
        End Property

        Public Property nroAcuerdo() As String
            Get
                Return _nroAcuerdo
            End Get
            Set(ByVal Value As String)
                _nroAcuerdo = Value
            End Set
        End Property

        Public Property customerFirstName() As String
            Get
                Return _customerFirstName
            End Get
            Set(ByVal Value As String)
                _customerFirstName = Value
            End Set
        End Property

        Public Property customerLastName() As String
            Get
                Return _customerLastName
            End Get
            Set(ByVal Value As String)
                _customerLastName = Value
            End Set
        End Property

        Public Property customerEmail() As String
            Get
                Return _customerEmail
            End Get
            Set(ByVal Value As String)
                _customerEmail = Value
            End Set
        End Property

    End Class

End Namespace