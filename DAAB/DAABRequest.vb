' Static Model

Imports System
Imports System.Data
Imports System.Collections
Imports System.ComponentModel


'<EnterpriseServices.Transaction(EnterpriseServices.TransactionOption.Required), EnterpriseServices.Synchronization(EnterpriseServices.SynchronizationOption.Required), EnterpriseServices.JustInTimeActivation(True)> _
Public Class DAABRequest
    '   Inherits EnterpriseServices.ServicedComponent

#Region "Clase Parámetro"
    Public Class Parameter
        Implements IDbDataParameter

        Private m_TipoBD As DbType
        Private m_Direccion As ParameterDirection
        Private m_bEsNulo As Boolean
        Private m_sParamName As String
        Private m_oParamValue As Object
        Private m_sColumnaOrigen As String
        Private m_VersionDato As DataRowVersion
        Private m_iPrecision As Byte
        Private m_iScale As Byte
        Private m_iSize As Integer

#Region "Constructores Parametros"


        Public Sub New()
            m_TipoBD = DbType.AnsiString
            m_Direccion = ParameterDirection.Input
            m_sParamName = ""
            m_oParamValue = DBNull.Value
            m_sColumnaOrigen = ""
            m_VersionDato = DataRowVersion.Current
            m_iPrecision = 0
            m_iScale = 0
            m_iSize = 0
        End Sub

        Public Sub New(ByVal sParameterName As String, _
                        ByVal oValue As Object)

            m_sParamName = sParameterName
            m_oParamValue = oValue

            m_TipoBD = DbType.AnsiString
            m_Direccion = ParameterDirection.Input
            m_sColumnaOrigen = ""
            m_VersionDato = DataRowVersion.Current
            m_iPrecision = 0
            m_iScale = 0
            m_iSize = 0
        End Sub

        Public Sub New(ByVal sParameterName As String, _
                        ByVal oDbType As DbType)

            m_sParamName = sParameterName
            m_TipoBD = oDbType
            m_oParamValue = DBNull.Value
            m_Direccion = ParameterDirection.Input
            m_sColumnaOrigen = ""
            m_VersionDato = DataRowVersion.Current
            m_iPrecision = 0
            m_iScale = 0
            m_iSize = 0

        End Sub

        Public Sub New(ByVal sParameterName As String, _
                        ByVal oDbType As DbType, _
                        ByVal oValue As Object)

            m_sParamName = sParameterName
            m_TipoBD = oDbType
            m_oParamValue = oValue
            m_Direccion = ParameterDirection.Input
            m_sColumnaOrigen = ""
            m_VersionDato = DataRowVersion.Current
            m_iPrecision = 0
            m_iScale = 0
            m_iSize = 0

        End Sub

        Public Sub New(ByVal sParameterName As String, _
                        ByVal oDbType As DbType, _
                        ByVal oSize As Integer)

            m_sParamName = sParameterName
            m_TipoBD = oDbType
            m_iSize = oSize
            m_oParamValue = DBNull.Value

            m_Direccion = ParameterDirection.Input
            m_sColumnaOrigen = ""
            m_VersionDato = DataRowVersion.Current
            m_iPrecision = 0
            m_iScale = 0

        End Sub


        Public Sub New(ByVal sParameterName As String, _
                        ByVal oDbType As DbType, _
                        ByVal oSize As Integer, _
                        ByVal oValue As Object)


            m_sParamName = sParameterName
            m_TipoBD = oDbType
            m_iSize = oSize
            m_oParamValue = oValue

            m_Direccion = ParameterDirection.Input
            m_sColumnaOrigen = ""
            m_VersionDato = DataRowVersion.Current
            m_iPrecision = 0
            m_iScale = 0

        End Sub

        Public Sub New(ByVal sParameterName As String, _
                                ByVal oDbType As DbType, _
                                ByVal oValue As Object, _
                                ByVal oDirection As ParameterDirection)


            m_sParamName = sParameterName
            m_TipoBD = oDbType
            m_iSize = 0
            m_oParamValue = oValue

            m_Direccion = oDirection
            m_sColumnaOrigen = ""
            m_VersionDato = DataRowVersion.Current
            m_iPrecision = 0
            m_iScale = 0

        End Sub

        Public Sub New(ByVal sParameterName As String, _
                                ByVal oDbType As DbType, _
                                ByVal oValue As Integer, _
                                ByVal oDirection As ParameterDirection)


            m_sParamName = sParameterName
            m_TipoBD = oDbType
            m_iSize = 0
            m_oParamValue = oValue

            m_Direccion = oDirection
            m_sColumnaOrigen = ""
            m_VersionDato = DataRowVersion.Current
            m_iPrecision = 0
            m_iScale = 0

        End Sub

        Public Sub New(ByVal sParameterName As String, _
                                        ByVal oDbType As DbType, _
                                        ByVal oDirection As ParameterDirection)


            m_sParamName = sParameterName
            m_TipoBD = oDbType
            m_iSize = 0
            m_oParamValue = DBNull.Value

            m_Direccion = oDirection
            m_sColumnaOrigen = ""
            m_VersionDato = DataRowVersion.Current
            m_iPrecision = 0
            m_iScale = 0

        End Sub

        Public Sub New(ByVal sParameterName As String, _
                        ByVal oDbType As DbType, _
                        ByVal oSize As Integer, _
                        ByVal oValue As Object, _
                        ByVal oDirection As ParameterDirection)


            m_sParamName = sParameterName
            m_TipoBD = oDbType
            m_iSize = oSize
            m_oParamValue = oValue

            m_Direccion = oDirection
            m_sColumnaOrigen = ""
            m_VersionDato = DataRowVersion.Current
            m_iPrecision = 0
            m_iScale = 0

        End Sub

#End Region

        Public Property DbType() As DbType Implements IDataParameter.DbType
            Get
                Return m_TipoBD
            End Get
            Set(ByVal Value As System.Data.DbType)
                m_TipoBD = Value
            End Set
        End Property

        Public Property Direction() As ParameterDirection Implements IDataParameter.Direction
            Get
                Return m_Direccion
            End Get
            Set(ByVal Value As System.Data.ParameterDirection)
                m_Direccion = Value
            End Set
        End Property

        Public ReadOnly Property IsNullable() As Boolean Implements IDataParameter.IsNullable
            Get
                Return m_bEsNulo
            End Get
        End Property

        Public Property ParameterName() As String Implements IDataParameter.ParameterName
            Get
                Return m_sParamName
            End Get
            Set(ByVal Value As String)
                m_sParamName = Value
            End Set
        End Property

        Public Property SourceColumn() As String Implements IDataParameter.SourceColumn
            Get
                Return m_sColumnaOrigen
            End Get
            Set(ByVal Value As String)
                m_sColumnaOrigen = Value
            End Set
        End Property

        Public Property SourceVersion() As DataRowVersion Implements IDataParameter.SourceVersion
            Get
                Return m_VersionDato
            End Get
            Set(ByVal Value As DataRowVersion)
                m_VersionDato = Value
            End Set
        End Property

        Public Property Value() As Object Implements IDataParameter.Value
            Get
                Return m_oParamValue
            End Get
            Set(ByVal Value As Object)
                m_oParamValue = Value
            End Set
        End Property

        Public Property Size() As Integer Implements IDbDataParameter.Size
            Get
                Return m_iSize
            End Get
            Set(ByVal Value As Integer)
                m_iSize = Value
            End Set
        End Property

        Public Property Scale() As Byte Implements IDbDataParameter.Scale
            Get
                Return m_iScale
            End Get
            Set(ByVal Value As Byte)
                m_iScale = Value
            End Set
        End Property

        Public Property Precision() As Byte Implements IDbDataParameter.Precision
            Get
                Return m_iPrecision
            End Get
            Set(ByVal Value As Byte)
                m_iPrecision = Value
            End Set
        End Property


    End Class
#End Region

#Region "Variables de Módulo"

    Public Enum TipoOrigenDatos
        SQL = 1
        ORACLE = 2
    End Enum

    Private m_lCommandType As CommandType
    Private m_sCommand As String
    Private m_bTransactional As Boolean
    Private m_colParameters As New ArrayList
    Private m_oException As Exception
    Private m_DataSet As DataSet
    Private m_CadCon As String
    Private m_Factory As DAABAbstracFactory
    Private m_aTablesName As String()

#End Region

#Region "Constructor"

    Public Sub New()

    End Sub

    Public Sub New(ByVal oTypoOrigen As TipoOrigenDatos, ByVal p_c_CadConexion As String)
        Select Case oTypoOrigen
            Case TipoOrigenDatos.SQL
                m_Factory = New DAABSqlFactory
            Case TipoOrigenDatos.ORACLE
                m_Factory = New DAABOracleFactory
                'Case TipoOrigenDatos.OLEDB
                '    m_Factory = New DAABOleDbFactory()
                'Case TipoOrigenDatos.ODBC
                '    m_Factory = New DAABOdbcFactory()
        End Select
        m_CadCon = p_c_CadConexion
    End Sub

#End Region

    Public Function GeneraFactory(ByVal oTypoOrigen As TipoOrigenDatos, _
                                    ByVal p_c_CadConexion As String) As DAABAbstracFactory
        Select Case oTypoOrigen
            Case TipoOrigenDatos.SQL
                m_Factory = New DAABSqlFactory
            Case TipoOrigenDatos.ORACLE
                m_Factory = New DAABOracleFactory
        End Select
        m_CadCon = p_c_CadConexion
        Return m_Factory

    End Function

#Region "Propiedades"

    <Description("Contiene el Objeto Factory del origen de datos seleccionado")> _
    Public ReadOnly Property Factory() As DAABAbstracFactory
        Get
            If m_Factory Is Nothing Then
                Throw New ApplicationException("La clase Factory no se ha instanciado...!!")
            End If
            Return m_Factory
        End Get
    End Property

    <Description("Se establece el tipo de comando que se va a ejecutar")> _
    Public Property CommandType() As CommandType
        Get
            Return m_lCommandType
        End Get
        Set(ByVal Value As CommandType)
            m_lCommandType = Value
        End Set
    End Property

    <Description("Contiene la instrucción SQL o nombre del store procedure")> _
    Public Property Command() As String
        Get
            Return m_sCommand
        End Get
        Set(ByVal Value As String)
            m_sCommand = Value
        End Set
    End Property

    <Description("Array de Objetos Parameter")> _
    Public Property Parameters() As ArrayList
        Get
            Return m_colParameters
        End Get
        Set(ByVal Value As ArrayList)
            m_colParameters = Value
        End Set
    End Property

    <Description("Si la llamada al acceso a datos va ha ser transaccional")> _
    Public Property Transactional() As Boolean
        Get
            Return m_bTransactional
        End Get
        Set(ByVal Value As Boolean)
            m_bTransactional = Value
        End Set
    End Property

    <Description("Contiene la ultima excepción del acceso a datos")> _
    Public Property Exception() As Exception
        Get
            Return m_oException
        End Get
        Set(ByVal Value As Exception)
            m_oException = Value
        End Set
    End Property

    <Description("Cuando necesita llenar un DataSet existente")> _
    Public Property RequestDataSet() As DataSet
        Get
            Return m_DataSet
        End Get
        Set(ByVal Value As DataSet)
            m_DataSet = Value
        End Set
    End Property

    <Description("Contiene la Cadena de Conexion")> _
    Public Property ConnectionString() As String
        Get
            Return m_CadCon
        End Get
        Set(ByVal Value As String)
            m_CadCon = Value
        End Set
    End Property

    <Description("Devuelve el valor de retorno de un Procedimiento Almacenado")> _
    Public ReadOnly Property ReturnValue() As Integer
        Get
            If m_colParameters.Count > 0 Then
                Dim obPara As IDbDataParameter = CType(m_colParameters(m_colParameters.Count - 1), IDbDataParameter)
                If obPara.Direction = ParameterDirection.ReturnValue Then
                    Return CType(obPara.Value, Integer)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        End Get
    End Property

    <Description("Array que contiene los nombres de las tablas con las que se van a crear un dataset")> _
    Public Property TableNames() As String()
        Get
            Return m_aTablesName
        End Get
        Set(ByVal Value As String())
            m_aTablesName = Value
        End Set
    End Property

#End Region

End Class ' END CLASS DEFINITION DAABRequest



