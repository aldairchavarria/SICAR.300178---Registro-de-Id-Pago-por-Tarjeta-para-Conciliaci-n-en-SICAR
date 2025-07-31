Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class clsUsuario

    Public _UsuarioId As Long
    Public _Login As String
    Public _Nombre As String
    Public _Apellido As String
    Public _ApellidoMaterno As String
    Public _NombreCompleto As String
    Public _CodigoVendedor As String
    Public _AreaId As String
    Public _AreaDescripcion As String
    Public clsUsuario() 'PROY-140126

    Public Sub New()
    End Sub

    Public Property UsuarioId() As String
        Set(ByVal value As String)
            _UsuarioId = value
        End Set
        Get
            Return UsuarioId
        End Get
    End Property
    Public Property Login() As String
        Set(ByVal value As String)
            _Login = value
        End Set
        Get
            Return _Login
        End Get
    End Property
    Public Property Nombre() As String
        Set(ByVal value As String)
            _Nombre = value
        End Set
        Get
            Return _Nombre
        End Get

    End Property
    Public Property Apellido() As String
        Set(ByVal value As String)
            _Apellido = value
        End Set
        Get
            Return _Apellido
        End Get
    End Property
    Public Property ApellidoMaterno() As String
        Set(ByVal value As String)
            _ApellidoMaterno = value
        End Set
        Get
            Return _ApellidoMaterno
        End Get
    End Property
    Public Property NombreCompleto() As String
        Set(ByVal value As String)
            _NombreCompleto = value
        End Set
        Get
            Return _NombreCompleto
        End Get
    End Property
    Public Property CodigoVendedor() As String
        Set(ByVal value As String)
            _CodigoVendedor = value
        End Set
        Get
            Return _CodigoVendedor
        End Get
    End Property
    Public Property AreaId() As String
        Set(ByVal value As String)
            _AreaId = value
        End Set
        Get
            Return _AreaId
        End Get
    End Property
    Public Property AreaDescripcion() As String
        Set(ByVal value As String)
            _AreaDescripcion = value
        End Set
        Get
            Return _AreaDescripcion
        End Get
    End Property
End Class
