Imports COM_SIC_Activaciones
Public Class clsTipificationConfiguration
    Private _keyTipoIFI As String

    Private _keyClaseRenovacionIFIRegular As String
    Private _keySubClaseRenovacionIFIRegular As String

    Private _keyClaseRenovacionIFIAnticipada As String
    Private _keySubClaseRenovacionIFIAnticipada As String

    Private _keyClaseAfiliacionReciboIFI As String
    Private _keySubClaseAfiliacionReciboIFI As String

    Private _keyClaseAjusteOCCIFI As String
    Private _keySubClaseAjusteOCCIFI As String

    Private _keyCodTipoIFI As String

    Private _keyCodClaseRenovacionIFIAnticipada As String
    Private _keyCodSubClaseRenovacionIFIAnticipada As String

    Private _keyCodClaseAfiliacionReciboIFI As String
    Private _keyCodSubClaseAfiliacionReciboIFI As String

    Private _keyUserSU As String
    Private _keyConstanteZero As String
    Private _keyConstanteEntrante As String
    Private _keyTrxReciboEmailIFI As String
    Private _keyFmtAfiliacionReciboIFI As String

    Public Property KeyTipoIFI() As String
        Get
            Return _keyTipoIFI
        End Get
        Set(ByVal Value As String)
            _keyTipoIFI = Value
        End Set
    End Property

    Public Property KeyClaseRenovacionIFIRegular() As String
        Get
            Return _keyClaseRenovacionIFIRegular
        End Get
        Set(ByVal Value As String)
            _keyClaseRenovacionIFIRegular = Value
        End Set
    End Property

    Public Property KeySubClaseRenovacionIFIRegular() As String
        Get
            Return _keySubClaseRenovacionIFIRegular
        End Get
        Set(ByVal Value As String)
            _keySubClaseRenovacionIFIRegular = Value
        End Set
    End Property

    Public Property KeyClaseRenovacionIFIAnticipada() As String
        Get
            Return _keyClaseRenovacionIFIAnticipada
        End Get
        Set(ByVal Value As String)
            _keyClaseRenovacionIFIAnticipada = Value
        End Set
    End Property

    Public Property KeySubClaseRenovacionIFIAnticipada() As String
        Get
            Return _keySubClaseRenovacionIFIAnticipada
        End Get
        Set(ByVal Value As String)
            _keySubClaseRenovacionIFIAnticipada = Value
        End Set
    End Property

    Public Property KeyClaseAfiliacionReciboIFI() As String
        Get
            Return _keyClaseAfiliacionReciboIFI
        End Get
        Set(ByVal Value As String)
            _keyClaseAfiliacionReciboIFI = Value
        End Set
    End Property

    Public Property KeySubClaseAfiliacionReciboIFI() As String
        Get
            Return _keySubClaseAfiliacionReciboIFI
        End Get
        Set(ByVal Value As String)
            _keySubClaseAfiliacionReciboIFI = Value
        End Set
    End Property

    Public Property KeyClaseAjusteOCCIFI() As String
        Get
            Return _keyClaseAjusteOCCIFI
        End Get
        Set(ByVal Value As String)
            _keyClaseAjusteOCCIFI = Value
        End Set
    End Property

    Public Property KeySubClaseAjusteOCCIFI() As String
        Get
            Return _keySubClaseAjusteOCCIFI
        End Get
        Set(ByVal Value As String)
            _keySubClaseAjusteOCCIFI = Value
        End Set
    End Property

    Public Property KeyCodTipoIFI() As String
        Get
            Return _keyCodTipoIFI
        End Get
        Set(ByVal Value As String)
            _keyCodTipoIFI = Value
        End Set
    End Property

    Public Property KeyCodClaseRenovacionIFIAnticipada() As String
        Get
            Return _keyCodClaseRenovacionIFIAnticipada
        End Get
        Set(ByVal Value As String)
            _keyCodClaseRenovacionIFIAnticipada = Value
        End Set
    End Property

    Public Property KeyCodSubClaseRenovacionIFIAnticipada() As String
        Get
            Return _keyCodSubClaseRenovacionIFIAnticipada
        End Get
        Set(ByVal Value As String)
            _keyCodSubClaseRenovacionIFIAnticipada = Value
        End Set
    End Property

    Public Property KeyCodClaseAfiliacionReciboIFI() As String
        Get
            Return _keyCodClaseAfiliacionReciboIFI
        End Get
        Set(ByVal Value As String)
            _keyCodClaseAfiliacionReciboIFI = Value
        End Set
    End Property

    Public Property KeyCodSubClaseAfiliacionReciboIFI() As String
        Get
            Return _keyCodSubClaseAfiliacionReciboIFI
        End Get
        Set(ByVal Value As String)
            _keyCodSubClaseAfiliacionReciboIFI = Value
        End Set
    End Property

    Public Property KeyUserSU() As String
        Get
            Return _keyUserSU
        End Get
        Set(ByVal Value As String)
            _keyUserSU = Value
        End Set
    End Property

    Public Property KeyConstanteZero() As String
        Get
            Return _keyConstanteZero
        End Get
        Set(ByVal Value As String)
            _keyConstanteZero = Value
        End Set
    End Property

    Public Property KeyConstanteEntrante() As String
        Get
            Return _keyConstanteEntrante
        End Get
        Set(ByVal Value As String)
            _keyConstanteEntrante = Value
        End Set
    End Property

    Public Property KeyTrxReciboEmailIFI() As String
        Get
            Return _keyTrxReciboEmailIFI
        End Get
        Set(ByVal Value As String)
            _keyTrxReciboEmailIFI = Value
        End Set
    End Property

    Public Property KeyFmtAfiliacionReciboIFI() As String
        Get
            Return _keyFmtAfiliacionReciboIFI
        End Get
        Set(ByVal Value As String)
            _keyFmtAfiliacionReciboIFI = Value
        End Set
    End Property
End Class
