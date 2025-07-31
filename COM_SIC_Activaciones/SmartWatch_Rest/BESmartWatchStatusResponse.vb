'INICIO PROY-140379
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BESmartWatchStatusResponse

    Private _status As Integer
    Private _codeResponse As String
    Private _descriptionResponse As String
    Private _errorLocation As String
    Private _date As String
    Private _origin As String
    Private _errorDetails As ArrayList 'BESmartWatchErrorDetails


    Public Property status() As Integer
        Set(ByVal value As Integer)
            _status = value
        End Set
        Get
            Return _status
        End Get
    End Property
    Public Property codeResponse() As String
        Set(ByVal value As String)
            _codeResponse = value
        End Set
        Get
            Return _codeResponse
        End Get
    End Property
    Public Property descriptionResponse() As String
        Set(ByVal value As String)
            _descriptionResponse = value
        End Set
        Get
            Return _descriptionResponse
        End Get
    End Property
    Public Property errorLocation() As String
        Set(ByVal value As String)
            _errorLocation = value
        End Set
        Get
            Return _errorLocation
        End Get
    End Property
    Public Property [date]() As String
        Set(ByVal value As String)
            _date = value
        End Set
        Get
            Return _date
        End Get
    End Property
    Public Property origin() As String
        Set(ByVal value As String)
            _origin = value
        End Set
        Get
            Return _origin
        End Get
    End Property
    Public Property errorDetails() As ArrayList
        Set(ByVal value As ArrayList)
            _errorDetails = value
        End Set
        Get
            Return _errorDetails
        End Get
    End Property
End Class
'FIN PROY-140379