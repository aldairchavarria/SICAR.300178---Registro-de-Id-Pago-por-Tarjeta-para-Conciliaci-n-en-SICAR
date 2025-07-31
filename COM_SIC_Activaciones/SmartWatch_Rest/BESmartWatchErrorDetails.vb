
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BESmartWatchErrorDetails
    Private _errorCode As String
    Private _errorDescription As String

    Public Property errorCode() As String
        Get
            Return _errorCode
        End Get
        Set(ByVal Value As String)
            _errorCode = Value
        End Set
    End Property


    Public Property errorDescription() As String
        Get
            Return _errorDescription
        End Get
        Set(ByVal Value As String)
            _errorDescription = Value
        End Set
    End Property
End Class
