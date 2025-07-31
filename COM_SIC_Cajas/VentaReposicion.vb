Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class VentaReposicion


    Dim _FECHA_REGISTRO As DateTime
    Dim _TIPO_OFICINA As String
    Dim _OFICINA As String
    Dim _OFICINA_DESC As String
    Dim _VENDEDOR As String
    Dim _USUARIO As String
    Dim _FLAG_BLOQUEO As String
    Dim _COD_BLOQUEO As String
    Dim _TELEFONO As String
    Dim _TELEFONO_REFERENCIA As String
    Dim _ICCID_SERIE_NUEVO As String
    Dim _ICCID_SERIE_ACTUAL As String
    Dim _INTER_REPO_EQUI As String
    Dim _INTER_REPO_SIM As String
    Dim _INTER_DESBLOQ As String
    Dim _PRECIO_NETO As Double
    Dim _PRECIO_BRUTO As Double
    Dim _PRECIO_CHIP_NETO As Double
    Dim _PRECIO_CHIP_BRUTO As Double
    Dim _ESTADO_REGISTRO As String
    Dim _TIPO_DOCUMENTO As String
    Dim _DOC_CLIE_NUMERO As String
    Dim _TITULAR_NOMBRE As String
    Dim _TITULAR_APELLIDO As String
    Dim _REPRESENTANTE As String
    Dim _MOTI_REPOSICION_COD As String
    Dim _MOTI_REPOSICION_DES As String
    Dim _TICKET_PRE_VENTA As String
    Dim _EQUIPO_SERIE As String
    Dim _Equipo_Material_Cod As String
    Dim _Iccid_Material_Cod As String
    Dim _Equipo_Material_Des As String
    Dim _Iccid_Material_Des As String
    Dim _TOTAL_PUNTOS As Integer
    Dim _IMPORTE_DESCUENTO_PUNTOS As Integer
    Public VentaReposicion() 'PROY-140126

    Public Property FECHA_REGISTRO() As DateTime

        Set(ByVal value As DateTime)
            _FECHA_REGISTRO = value
        End Set
        Get
            Return _FECHA_REGISTRO
        End Get

    End Property

    Public Property TIPO_OFICINA() As String

        Set(ByVal value As String)
            _TIPO_OFICINA = value
        End Set
        Get
            Return _TIPO_OFICINA
        End Get

    End Property
    Public Property OFICINA() As String

        Set(ByVal value As String)
            _OFICINA = value
        End Set
        Get
            Return _OFICINA
        End Get

    End Property

    Public Property OFICINA_DESC() As String

        Set(ByVal value As String)
            _OFICINA_DESC = value
        End Set
        Get
            Return _OFICINA_DESC
        End Get

    End Property

    Public Property VENDEDOR() As String

        Set(ByVal value As String)
            _VENDEDOR = value
        End Set
        Get
            Return _VENDEDOR
        End Get

    End Property

    Public Property USUARIO() As String

        Set(ByVal value As String)
            _USUARIO = value
        End Set
        Get
            Return _USUARIO
        End Get

    End Property
    Public Property FLAG_BLOQUEO() As String

        Set(ByVal value As String)
            _FLAG_BLOQUEO = value
        End Set
        Get
            Return _FLAG_BLOQUEO
        End Get

    End Property
    Public Property COD_BLOQUEO() As String

        Set(ByVal value As String)
            _COD_BLOQUEO = value
        End Set
        Get
            Return _COD_BLOQUEO
        End Get

    End Property

    Public Property TELEFONO() As String

        Set(ByVal value As String)
            _TELEFONO = value
        End Set
        Get
            Return _TELEFONO
        End Get

    End Property

    Public Property TELEFONO_REFERENCIA() As String

        Set(ByVal value As String)
            _TELEFONO_REFERENCIA = value
        End Set
        Get
            Return _TELEFONO_REFERENCIA
        End Get

    End Property

    Public Property ICCID_SERIE_NUEVO() As String

        Set(ByVal value As String)
            _ICCID_SERIE_NUEVO = value
        End Set
        Get
            Return _ICCID_SERIE_NUEVO
        End Get

    End Property

    Public Property ICCID_SERIE_ACTUAL() As String

        Set(ByVal value As String)
            _ICCID_SERIE_ACTUAL = value
        End Set
        Get
            Return _ICCID_SERIE_ACTUAL
        End Get

    End Property

    Public Property INTER_REPO_EQUI() As String

        Set(ByVal value As String)
            _INTER_REPO_EQUI = value
        End Set
        Get
            Return _INTER_REPO_EQUI
        End Get

    End Property

    Public Property INTER_REPO_SIM() As String

        Set(ByVal value As String)
            _INTER_REPO_SIM = value
        End Set
        Get
            Return _INTER_REPO_SIM
        End Get

    End Property

    Public Property INTER_DESBLOQ() As String

        Set(ByVal value As String)
            _INTER_DESBLOQ = value
        End Set
        Get
            Return _INTER_DESBLOQ
        End Get

    End Property

    Public Property PRECIO_NETO() As String

        Set(ByVal value As String)
            _PRECIO_NETO = value
        End Set
        Get
            Return _PRECIO_NETO
        End Get

    End Property

    Public Property PRECIO_BRUTO() As String

        Set(ByVal value As String)
            _PRECIO_BRUTO = value
        End Set
        Get
            Return _PRECIO_BRUTO
        End Get

    End Property

    Public Property PRECIO_CHIP_NETO() As String

        Set(ByVal value As String)
            _PRECIO_CHIP_NETO = value
        End Set
        Get
            Return _PRECIO_CHIP_NETO
        End Get

    End Property

    Public Property PRECIO_CHIP_BRUTO() As String

        Set(ByVal value As String)
            _PRECIO_CHIP_BRUTO = value
        End Set
        Get
            Return _PRECIO_CHIP_BRUTO
        End Get

    End Property

    Public Property ESTADO_REGISTRO() As String

        Set(ByVal value As String)
            _ESTADO_REGISTRO = value
        End Set
        Get
            Return _ESTADO_REGISTRO
        End Get

    End Property

    Public Property TIPO_DOCUMENTO() As String

        Set(ByVal value As String)
            _TIPO_DOCUMENTO = value
        End Set
        Get
            Return _TIPO_DOCUMENTO
        End Get

    End Property

    Public Property DOC_CLIE_NUMERO() As String

        Set(ByVal value As String)
            _DOC_CLIE_NUMERO = value
        End Set
        Get
            Return _DOC_CLIE_NUMERO
        End Get

    End Property

    Public Property TITULAR_NOMBRE() As String

        Set(ByVal value As String)
            _TITULAR_NOMBRE = value
        End Set
        Get
            Return _TITULAR_NOMBRE
        End Get

    End Property

    Public Property TITULAR_APELLIDO() As String

        Set(ByVal value As String)
            _TITULAR_APELLIDO = value
        End Set
        Get
            Return _TITULAR_APELLIDO
        End Get

    End Property

    Public Property REPRESENTANTE() As String

        Set(ByVal value As String)
            _REPRESENTANTE = value
        End Set
        Get
            Return _REPRESENTANTE
        End Get

    End Property

    Public Property MOTI_REPOSICION_COD() As String

        Set(ByVal value As String)
            _MOTI_REPOSICION_COD = value
        End Set
        Get
            Return _MOTI_REPOSICION_COD
        End Get

    End Property

    Public Property MOTI_REPOSICION_DES() As String

        Set(ByVal value As String)
            _MOTI_REPOSICION_DES = value
        End Set
        Get
            Return _MOTI_REPOSICION_DES
        End Get

    End Property

    Public Property TICKET_PRE_VENTA() As String

        Set(ByVal value As String)
            _TICKET_PRE_VENTA = value
        End Set
        Get
            Return _TICKET_PRE_VENTA
        End Get

    End Property


    Public Property EQUIPO_SERIE() As String

        Set(ByVal value As String)
            _EQUIPO_SERIE = value
        End Set
        Get
            Return _EQUIPO_SERIE
        End Get

    End Property


    Public Property Equipo_Material_Cod() As String

        Set(ByVal value As String)
            _Equipo_Material_Cod = value
        End Set
        Get
            Return _Equipo_Material_Cod
        End Get

    End Property

    Public Property Iccid_Material_Cod() As String

        Set(ByVal value As String)
            _Iccid_Material_Cod = value
        End Set
        Get
            Return _Iccid_Material_Cod
        End Get

    End Property

    Public Property Equipo_Material_Des() As String

        Set(ByVal value As String)
            _Equipo_Material_Des = value
        End Set
        Get
            Return _Equipo_Material_Des
        End Get

    End Property

    Public Property Iccid_Material_Des() As String

        Set(ByVal value As String)
            _Iccid_Material_Des = value
        End Set
        Get
            Return _Iccid_Material_Des
        End Get

    End Property

    Public Property TOTAL_PUNTOS() As String

        Set(ByVal value As String)
            _TOTAL_PUNTOS = value
        End Set
        Get
            Return _TOTAL_PUNTOS
        End Get

    End Property

    Public Property IMPORTE_DESCUENTO_PUNTOS() As String

        Set(ByVal value As String)
            _IMPORTE_DESCUENTO_PUNTOS = value
        End Set
        Get
            Return _IMPORTE_DESCUENTO_PUNTOS
        End Get

    End Property


End Class
