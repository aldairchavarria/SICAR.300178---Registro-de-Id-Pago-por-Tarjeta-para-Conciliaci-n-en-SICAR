' The SqlHelper class ha sido modificada por Roberth Ramos para 
' implementar el patron Abstract Factory
' ===============================================================================
' Release history
' VERSION	DESCRIPTION
'   2.0	Added support for FillDataset, UpdateDataset and "Param" helper methods
'
' ===============================================================================


Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml

Public Class DAABSqlFactory
    Inherits DAABAbstracFactory
    'Implements IDisposable

    Private m_conSql As String

    Private m_conecSQL As SqlConnection
    Private m_TranSQL As SqlTransaction



#Region "Métodos privados"

    ' This method is used to attach array of SqlParameters to a SqlCommand.
    ' This method will assign a value of DbNull to any parameter with a direction of
    ' InputOutput and a value of null.  
    ' This behavior will prevent default values from being used, but
    ' this will be the less common case than an intended pure output parameter (derived as InputOutput)
    ' where the user provided no input value.
    ' Parameters:
    ' -command - The command to which the parameters will be added
    ' -commandParameters - an array of SqlParameters to be added to command
    Private Sub AttachParameters(ByVal command As SqlCommand, ByVal commandParameters() As SqlParameter)
        If (command Is Nothing) Then Throw New ArgumentNullException("command")
        If (Not commandParameters Is Nothing) Then
            Dim p As SqlParameter
            For Each p In commandParameters
                If (Not p Is Nothing) Then
                    ' Check for derived output value with no value assigned
                    If (p.Direction = ParameterDirection.InputOutput OrElse p.Direction = ParameterDirection.Input) AndAlso p.Value Is Nothing Then
                        p.Value = DBNull.Value
                    End If
                    command.Parameters.Add(p)
                End If
            Next p
        End If
    End Sub ' AttachParameters

    ' This method assigns dataRow column values to an array of SqlParameters.
    ' Parameters:
    ' -commandParameters: Array of SqlParameters to be assigned values
    ' -dataRow: the dataRow used to hold the stored procedure' s parameter values
    Private Overloads Sub AssignParameterValues(ByVal commandParameters() As SqlParameter, ByVal dataRow As DataRow)

        If commandParameters Is Nothing OrElse dataRow Is Nothing Then
            ' Do nothing if we get no data    
            Exit Sub
        End If

        ' Set the parameters values
        Dim commandParameter As SqlParameter
        Dim i As Integer
        For Each commandParameter In commandParameters
            ' Check the parameter name
            If (commandParameter.ParameterName Is Nothing OrElse commandParameter.ParameterName.Length <= 1) Then
                Throw New Exception(String.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: ' {1}' .", i, commandParameter.ParameterName))
            End If
            If dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) <> -1 Then
                commandParameter.Value = dataRow(commandParameter.ParameterName.Substring(1))
            End If
            i = i + 1
        Next
    End Sub

    ' This method assigns an array of values to an array of SqlParameters.
    ' Parameters:
    ' -commandParameters - array of SqlParameters to be assigned values
    ' -array of objects holding the values to be assigned
    Private Overloads Sub AssignParameterValues(ByVal commandParameters() As SqlParameter, ByVal parameterValues() As Object)

        Dim i As Integer
        Dim j As Integer

        If (commandParameters Is Nothing) AndAlso (parameterValues Is Nothing) Then
            ' Do nothing if we get no data
            Return
        End If

        ' We must have the same number of values as we pave parameters to put them in
        If commandParameters.Length <> parameterValues.Length Then
            Throw New ArgumentException("Parameter count does not match Parameter Value count.")
        End If

        ' Value array
        j = commandParameters.Length - 1
        For i = 0 To j
            ' If the current array value derives from IDbDataParameter, then assign its Value property
            If TypeOf parameterValues(i) Is IDbDataParameter Then
                Dim paramInstance As IDbDataParameter = CType(parameterValues(i), IDbDataParameter)
                If (paramInstance.Value Is Nothing) Then
                    commandParameters(i).Value = DBNull.Value
                Else
                    commandParameters(i).Value = paramInstance.Value
                End If
            ElseIf (parameterValues(i) Is Nothing) Then
                commandParameters(i).Value = DBNull.Value
            Else
                commandParameters(i).Value = parameterValues(i)
            End If
        Next
    End Sub ' AssignParameterValues

    ' This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
    ' to the provided command.
    ' Parameters:
    ' -command - the SqlCommand to be prepared
    ' -connection - a valid SqlConnection, on which to execute this command
    ' -transaction - a valid SqlTransaction, or ' null' 
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-SQL command
    ' -commandParameters - an array of SqlParameters to be associated with the command or ' null' if no parameters are required
    Private Sub PrepareCommand(ByVal command As SqlCommand, _
                                      ByVal connection As SqlConnection, _
                                      ByVal transaction As SqlTransaction, _
                                      ByVal commandType As CommandType, _
                                      ByVal commandText As String, _
                                      ByVal commandParameters() As SqlParameter, ByRef mustCloseConnection As Boolean)

        If (command Is Nothing) Then Throw New ArgumentNullException("command")
        If (commandText Is Nothing OrElse commandText.Length = 0) Then Throw New ArgumentNullException("commandText")

        ' If the provided connection is not open, we will open it
        If connection.State <> ConnectionState.Open Then
            connection.Open()
            mustCloseConnection = True
        Else
            mustCloseConnection = False
        End If

        ' Associate the connection with the command
        command.Connection = connection

        ' Set the command text (stored procedure name or SQL statement)
        command.CommandText = commandText
        command.CommandTimeout = 180
        ' If we were provided a transaction, assign it.
        If Not (transaction Is Nothing) Then
            If transaction.Connection Is Nothing Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")
            command.Transaction = transaction
        End If

        ' Set the command type
        command.CommandType = commandType

        ' Attach the command parameters if they are provided
        If Not (commandParameters Is Nothing) Then
            AttachParameters(command, commandParameters)
        End If
        Return
    End Sub ' PrepareCommand

    ' This method return a SqlPameter array that it be necesaty for execute the command ' 
    ' Parameters:
    ' -Parametros (ArrayList) este parametro viene desde la clase fachada

    Private Function DevuelveParametros(ByVal commandType As CommandType, ByRef parametros As ArrayList) As SqlParameter()


        If Not parametros Is Nothing AndAlso parametros.Count > 0 Then
            Dim aParam(parametros.Count) As SqlParameter
            Dim i As Short

            For i = 0 To parametros.Count - 1
                If TypeOf parametros(i) Is SqlParameter Then
                    aParam(i) = parametros(i)
                Else
                    aParam(i) = CreaParametro(CType(parametros(i), IDbDataParameter))
                End If
                If parametros(i).Direction <> ParameterDirection.Input Then
                    parametros(i) = aParam(i)
                End If
            Next

            If commandType = commandType.StoredProcedure Then
                Dim parameter As New SqlParameter()

                With parameter
                    .ParameterName = "@ReturnValue"
                    .DbType = SqlDbType.Int
                    .Size = 4
                    .Direction = ParameterDirection.ReturnValue
                    .IsNullable = False
                    .Precision = 0
                    .Scale = 0
                    .SourceColumn = String.Empty
                    .SourceVersion = DataRowVersion.Default
                    .Value = Nothing
                End With
                aParam(parametros.Count) = parameter
                parametros.Add(parameter)
            End If
            Return aParam
        Else
            Return CType(Nothing, SqlParameter())
        End If

    End Function

    'Este método crea un Sqlparameter a partir de un IDBDataParameter
    Private Function CreaParametro(ByVal parametro As IDbDataParameter) As SqlParameter
        Dim oParam As New SqlParameter()


        With oParam
            .Direction = parametro.Direction
            .ParameterName = parametro.ParameterName
            If parametro.Value Is Nothing Then
                .Value = DBNull.Value
            Else
                .Value = parametro.Value
            End If
            .SourceColumn = parametro.SourceColumn
            .SourceVersion = parametro.SourceVersion
            Select Case parametro.DbType
                Case DbType.Currency
                    .SqlDbType = SqlDbType.Money
                Case DbType.Double, DbType.Single
                    .SqlDbType = SqlDbType.Float
                Case DbType.Decimal, DbType.VarNumeric
                    .SqlDbType = SqlDbType.Decimal
                    If parametro.Size > 0 Then .Size = parametro.Size
                    .Scale = parametro.Scale
                    .Precision = parametro.Precision
                Case DbType.Byte
                    .SqlDbType = SqlDbType.TinyInt
                Case DbType.AnsiString
                    .SqlDbType = SqlDbType.VarChar
                    If parametro.Size > 0 Then .Size = parametro.Size
                Case DbType.AnsiStringFixedLength
                    .SqlDbType = SqlDbType.Char
                    If parametro.Size > 0 Then .Size = parametro.Size
                Case DbType.Binary
                    .SqlDbType = SqlDbType.Binary
                Case DbType.Boolean
                    .SqlDbType = SqlDbType.Bit
                Case DbType.Date
                    .SqlDbType = SqlDbType.SmallDateTime
                Case DbType.DateTime
                    .SqlDbType = SqlDbType.DateTime
                Case DbType.Guid
                    .SqlDbType = SqlDbType.UniqueIdentifier
                Case DbType.SByte, DbType.Int16, DbType.UInt16
                    .SqlDbType = SqlDbType.SmallInt
                Case DbType.Int32, DbType.UInt32
                    .SqlDbType = SqlDbType.Int
                Case DbType.Int64, DbType.UInt64
                    .SqlDbType = SqlDbType.BigInt
                Case DbType.Object
                    .SqlDbType = SqlDbType.Variant
                Case DbType.String
                    .SqlDbType = SqlDbType.NVarChar
                    If parametro.Size > 0 Then .Size = parametro.Size
                Case DbType.StringFixedLength
                    .SqlDbType = SqlDbType.NChar
                    If parametro.Size > 0 Then .Size = parametro.Size
                Case DbType.Time
                    .SqlDbType = SqlDbType.SmallDateTime
            End Select

        End With
        Return oParam
    End Function

    Private Function estableceConexion(ByVal pb_transaccional As Boolean, _
                                        ByVal pc_cadConexion As String) As Boolean

        If m_conecSQL Is Nothing OrElse m_conecSQL.State <> ConnectionState.Open Then
            m_conecSQL = New SqlConnection(pc_cadConexion)
            m_conecSQL.Open()
        End If
        If pb_transaccional AndAlso m_TranSQL Is Nothing Then
            m_TranSQL = m_conecSQL.BeginTransaction
        End If
        
    End Function

#End Region

#Region "ExecuteDataset"

    Public Overrides Function ExecuteDataset(ByRef Request As DAABRequest) As DataSet

        Dim connectionString As String = Request.ConnectionString

        'Dim tranSql As SqlTransaction
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (Request.Command Is Nothing OrElse Request.Command.Length = 0) Then Throw New ArgumentNullException("No ha ingresado el commando a ejecutar.")

        'Dim connection As SqlConnection
        Try
            estableceConexion(Request.Transactional, connectionString)
            'connection = New SqlConnection(connectionString)
            'connection.Open()
            Dim aparam = DevuelveParametros(Request.CommandType, Request.Parameters)
            If Request.Transactional Then
                'tranSql = connection.BeginTransaction
                Return ExecuteDatasets(m_TranSQL, Request.CommandType, Request.Command, aparam, Request.TableNames)
            Else
                Return ExecuteDatasets(m_conecSQL, Request.CommandType, Request.Command, aparam, Request.TableNames)
            End If

        Finally
            'If Request.Transactional And connection.State = ConnectionState.Open Then
            '    tranSql.Commit()
            'End If
            If Not Request.Transactional And Not m_conecSQL Is Nothing Then m_conecSQL.Dispose()
        End Try

        'PrepareCommand(cmd, transaction.Connection, transaction, Request.CommandType, Request.Command, Request.Parameters, mustCloseConnection)


    End Function

    ' Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
    ' using the provided parameters.
    ' e.g.:  
    ' Dim ds As Dataset = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
    ' Parameters:
    ' -connection - a valid SqlConnection
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-SQL command
    ' -commandParameters - an array of SqlParamters used to execute the command
    ' Returns: A dataset containing the resultset generated by the command
    Private Overloads Function ExecuteDatasets(ByVal connection As SqlConnection, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String, _
                                                    ByVal commandParameters() As SqlParameter, _
                                                    ByVal tableNames As String()) As DataSet
        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
        ' Create a command and prepare it for execution
        Dim cmd As New SqlCommand()
        Dim ds As New DataSet()
        Dim dataAdatpter As SqlDataAdapter
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters, mustCloseConnection)

        Try
            ' Create the DataAdapter & DataSet
            dataAdatpter = New SqlDataAdapter(cmd)

            If Not tableNames Is Nothing AndAlso tableNames.Length > 0 Then

                Dim tableName As String = "Table"
                Dim index As Integer

                For index = 0 To tableNames.Length - 1
                    If (tableNames(index) Is Nothing OrElse tableNames(index).Length = 0) Then Throw New ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames")
                    dataAdatpter.TableMappings.Add(tableName, tableNames(index))
                    tableName = tableName & (index + 1).ToString()
                Next
            End If
            ' Fill the DataSet using default values for DataTable names, etc
            dataAdatpter.Fill(ds)

            ' Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear()
        Finally

            If (Not dataAdatpter Is Nothing) Then dataAdatpter.Dispose()
        End Try
        If (mustCloseConnection) Then connection.Close()

        ' Return the dataset
        Return ds

    End Function ' ExecuteDataset

    ' Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
    ' using the provided parameters.
    ' e.g.:  
    ' Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
    ' Parameters
    ' -transaction - a valid SqlTransaction 
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-SQL command
    ' -commandParameters - an array of SqlParamters used to execute the command
    ' Returns: A dataset containing the resultset generated by the command
    Private Overloads Function ExecuteDatasets(ByVal transaction As SqlTransaction, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String, _
                                                    ByVal commandParameters() As SqlParameter, _
                                                    ByVal tableNames As String()) As DataSet
        If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
        If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")

        ' Create a command and prepare it for execution
        Dim cmd As New SqlCommand()
        Dim ds As New DataSet()
        Dim dataAdatpter As SqlDataAdapter
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

        Try
            ' Create the DataAdapter & DataSet
            dataAdatpter = New SqlDataAdapter(cmd)

            If Not tableNames Is Nothing AndAlso tableNames.Length > 0 Then

                Dim tableName As String = "Table"
                Dim index As Integer

                For index = 0 To tableNames.Length - 1
                    If (tableNames(index) Is Nothing OrElse tableNames(index).Length = 0) Then Throw New ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames")
                    dataAdatpter.TableMappings.Add(tableName, tableNames(index))
                    tableName = tableName & (index + 1).ToString()
                Next
            End If
            ' Fill the DataSet using default values for DataTable names, etc
            dataAdatpter.Fill(ds)

            ' Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear()
        Finally
            If (Not dataAdatpter Is Nothing) Then dataAdatpter.Dispose()
        End Try

        ' Return the dataset
        Return ds

    End Function ' ExecuteDataset

#End Region

#Region "ExecuteNonQuery"

    Public Overrides Function ExecuteNonQuery(ByRef Request As DAABRequest) As Integer

        Dim connectionString As String = Request.ConnectionString

        'Dim tranSql As SqlTransaction
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (Request.Command Is Nothing OrElse Request.Command.Length = 0) Then Throw New ArgumentNullException("No ha ingresado el commando a ejecutar.")

        'Dim connection As SqlConnection
        Try

            Dim aparam = DevuelveParametros(Request.CommandType, Request.Parameters)

            estableceConexion(Request.Transactional, connectionString)

            'connection = New SqlConnection(connectionString)
            'connection.Open()

            Dim iretval As Integer
            If Request.Transactional Then
                'tranSql = connection.BeginTransaction
                iretval = ExecuteNonQuerys(m_TranSQL, Request.CommandType, Request.Command, aparam)
            Else
                iretval = ExecuteNonQuerys(m_conecSQL, Request.CommandType, Request.Command, aparam)
            End If
            Return iretval
        Finally
            'If Request.Transactional And connection.State = ConnectionState.Open Then
            '    tranSql.Commit()
            'End If
            'If Not connection Is Nothing Then connection.Dispose()
            If Not Request.Transactional And Not m_conecSQL Is Nothing Then m_conecSQL.Dispose()
        End Try


    End Function

    ' Execute a SqlCommand (that returns no resultset) against the specified SqlConnection 
    ' using the provided parameters.
    ' e.g.:  
    '  Dim result As Integer = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24))
    ' Parameters:
    ' -connection - a valid SqlConnection 
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-SQL command 
    ' -commandParameters - an array of SqlParamters used to execute the command 
    ' Returns: An int representing the number of rows affected by the command 
    Private Overloads Function ExecuteNonQuerys(ByVal connection As SqlConnection, _
                                                     ByVal commandType As CommandType, _
                                                     ByVal commandText As String, _
                                                     ByVal commandParameters() As SqlParameter) As Integer

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")

        ' Create a command and prepare it for execution
        Dim cmd As New SqlCommand()
        Dim retval As Integer
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters, mustCloseConnection)

        ' Finally, execute the command
        cmd.ExecuteNonQuery()

        If cmd.Parameters.IndexOf("@ReturnValue") >= 0 Then
            retval = cmd.Parameters("@ReturnValue").Value()
        Else
            retval = cmd.Parameters("@RETURN_VALUE").Value()
        End If
        ' Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear()

        If (mustCloseConnection) Then connection.Close()

        Return retval
    End Function ' ExecuteNonQuery


    ' Execute a SqlCommand (that returns no resultset) against the specified SqlTransaction
    ' using the provided parameters.
    ' e.g.:  
    ' Dim result As Integer = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
    ' Parameters:
    ' -transaction - a valid SqlTransaction 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-SQL command 
    ' -commandParameters - an array of SqlParamters used to execute the command 
    ' Returns: An int representing the number of rows affected by the command 
    Private Overloads Function ExecuteNonQuerys(ByVal transaction As SqlTransaction, _
                                                     ByVal commandType As CommandType, _
                                                     ByVal commandText As String, _
                                                     ByVal commandParameters() As SqlParameter) As Integer

        If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
        If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")

        ' Create a command and prepare it for execution
        Dim cmd As New SqlCommand()
        Dim retval As Integer
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

        ' Finally, execute the command
        cmd.ExecuteNonQuery()
        If cmd.Parameters.IndexOf("@ReturnValue") >= 0 Then
            retval = cmd.Parameters("@ReturnValue").Value()
        Else
            retval = cmd.Parameters("@RETURN_VALUE").Value()
        End If

        ' Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear()

        Return retval
    End Function ' ExecuteNonQuery


#End Region


#Region "ExecuteScalar"

    Public Overrides Function ExecuteScalar(ByRef Request As DAABRequest) As Object

        Dim connectionString As String = Request.ConnectionString

        'Dim tranSql As SqlTransaction
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (Request.Command Is Nothing OrElse Request.Command.Length = 0) Then Throw New ArgumentNullException("No ha ingresado el commando a ejecutar.")


        'Dim connection As SqlConnection
        Try
            estableceConexion(Request.Transactional, connectionString)

            'connection = New SqlConnection(connectionString)
            'connection.Open()
            Dim aparam = DevuelveParametros(Request.CommandType, Request.Parameters)
            If Request.Transactional Then
                'tranSql = connection.BeginTransaction
                Return ExecuteScalares(m_TranSQL, Request.CommandType, Request.Command, aparam)
            Else
                Return ExecuteScalares(m_conecSQL, Request.CommandType, Request.Command, aparam)
            End If

        Finally
            'If Request.Transactional And connection.State = ConnectionState.Open Then
            '    tranSql.Commit()
            'End If
            'If Not connection Is Nothing Then connection.Dispose()
            If Not Request.Transactional And Not m_conecSQL Is Nothing Then m_conecSQL.Dispose()
        End Try

    End Function

    ' Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
    ' using the provided parameters.
    ' e.g.:  
    ' Dim orderCount As Integer = CInt(ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24)))
    ' Parameters:
    ' -connection - a valid SqlConnection 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-SQL command 
    ' -commandParameters - an array of SqlParamters used to execute the command 
    ' Returns: An object containing the value in the 1x1 resultset generated by the command 
    Private Overloads Function ExecuteScalares(ByVal connection As SqlConnection, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal commandParameters() As SqlParameter) As Object

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")

        ' Create a command and prepare it for execution
        Dim cmd As New SqlCommand()
        Dim retval As Object
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters, mustCloseConnection)

        ' Execute the command & return the results
        retval = cmd.ExecuteScalar()

        ' Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear()

        If (mustCloseConnection) Then connection.Close()

        Return retval

    End Function ' ExecuteScalar

    ' Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
    ' using the provided parameters.
    ' e.g.:  
    ' Dim orderCount As Integer = CInt(ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24)))
    ' Parameters:
    ' -transaction - a valid SqlTransaction  
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-SQL command 
    ' -commandParameters - an array of SqlParamters used to execute the command 
    ' Returns: An object containing the value in the 1x1 resultset generated by the command 
    Private Overloads Function ExecuteScalares(ByVal transaction As SqlTransaction, _
                                                   ByVal commandType As CommandType, _
                                                   ByVal commandText As String, _
                                                   ByVal commandParameters() As SqlParameter) As Object

        If (transaction Is Nothing) Then Throw New ArgumentNullException("transaction")
        If Not (transaction Is Nothing) AndAlso (transaction.Connection Is Nothing) Then Throw New ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction")

        ' Create a command and prepare it for execution
        Dim cmd As New SqlCommand()
        Dim retval As Object
        Dim mustCloseConnection As Boolean = False

        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

        ' Execute the command & return the results
        retval = cmd.ExecuteScalar()

        ' Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear()

        Return retval
    End Function ' ExecuteScalar

#End Region

#Region "ExecuteReader"
    Private Enum SqlConnectionOwnership
        ' Connection is owned and managed by SqlHelper
        Internal
        ' Connection is owned and managed by the caller
        [External]
    End Enum ' SqlConnectionOwnership


    Public Overrides Function ExecuteReader(ByRef Request As DAABRequest) As DAABDataReader

        Dim oDataReaderSQL As DAAB.DAABSqlDataReader

        Dim connectionString As String = Request.ConnectionString
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")

        ' Create & open a SqlConnection
        'Dim connection As SqlConnection
        Try
            Dim aparam = DevuelveParametros(Request.CommandType, Request.Parameters)

            estableceConexion(False, connectionString)

            'connection = New SqlConnection(connectionString)
            'connection.Open()
            ' Call the private overload that takes an internally owned connection in place of the connection string
            Dim drSQL As SqlDataReader

            drSQL = ExecuteReaders(m_conecSQL, CType(Nothing, SqlTransaction), Request.CommandType, Request.Command, aparam, SqlConnectionOwnership.Internal)

            oDataReaderSQL = New DAAB.DAABSqlDataReader()
            oDataReaderSQL.ReturnDataReader = drSQL

            Return oDataReaderSQL

        Catch ex1 As SqlException
            If Not m_conecSQL Is Nothing Then m_conecSQL.Dispose()
            Request.Exception = ex1
            Throw ex1
            Return CType(Nothing, DAABDataReader)
        Catch ex1 As Exception
            ' If we fail to return the SqlDatReader, we need to close the connection ourselves
            Request.Exception = ex1
            If Not m_conecSQL Is Nothing Then m_conecSQL.Dispose()
            Throw
            Return CType(Nothing, DAABDataReader)
        End Try

    End Function

    ' Create and prepare a SqlCommand, and call ExecuteReader with the appropriate CommandBehavior.
    ' If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
    ' If the caller provided the connection, we want to leave it to them to manage.
    ' Parameters:
    ' -connection - a valid SqlConnection, on which to execute this command 
    ' -transaction - a valid SqlTransaction, or ' null' 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-SQL command 
    ' -commandParameters - an array of SqlParameters to be associated with the command or ' null' if no parameters are required 
    ' -connectionOwnership - indicates whether the connection parameter was provided by the caller, or created by SqlHelper 
    ' Returns: SqlDataReader containing the results of the command 
    Private Overloads Function ExecuteReaders(ByVal connection As SqlConnection, _
                                                    ByVal transaction As SqlTransaction, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String, _
                                                    ByVal commandParameters() As SqlParameter, _
                                                    ByVal connectionOwnership As SqlConnectionOwnership) As SqlDataReader

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")

        Dim mustCloseConnection As Boolean = False
        ' Create a command and prepare it for execution
        Dim cmd As New SqlCommand()
        Try
            ' Create a reader
            Dim dataReader As SqlDataReader

            PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

            ' Call ExecuteReader with the appropriate CommandBehavior
            If connectionOwnership = SqlConnectionOwnership.External Then
                dataReader = cmd.ExecuteReader()
            Else
                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            End If

            ' Detach the SqlParameters from the command object, so they can be used again
            Dim canClear As Boolean = True
            Dim commandParameter As SqlParameter
            For Each commandParameter In cmd.Parameters
                If commandParameter.Direction <> ParameterDirection.Input Then
                    canClear = False
                End If
            Next

            If (canClear) Then cmd.Parameters.Clear()

            Return dataReader
        Catch
            If (mustCloseConnection) Then connection.Close()
            Throw
        End Try
    End Function ' ExecuteReader



#End Region

#Region "FillDataset"


    Public Overrides Sub FillDataset(ByRef Request As DAABRequest)

        Dim connectionString As String = Request.ConnectionString

        'Dim tranSql As SqlTransaction
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (Request.Command Is Nothing OrElse Request.Command.Length = 0) Then Throw New ArgumentNullException("No ha ingresado el commando a ejecutar.")
        If (Request.RequestDataSet Is Nothing) Then Throw New ArgumentNullException("RequestDataSet")

        'Dim connection As SqlConnection
        Try
            'connection = New SqlConnection(connectionString)
            'connection.Open()
            estableceConexion(Request.Transactional, connectionString)

            Dim aparam = DevuelveParametros(Request.CommandType, Request.Parameters)
            If Request.Transactional Then
                'tranSql = connection.BeginTransaction
                FillDatasets(m_conecSQL, m_TranSQL, Request.CommandType, Request.Command, Request.RequestDataSet, Request.TableNames, aparam)
            Else
                FillDatasets(m_conecSQL, CType(Nothing, SqlTransaction), Request.CommandType, Request.Command, Request.RequestDataSet, Request.TableNames, aparam)
            End If
            'If Request.Transactional And m_conecSQL.State = ConnectionState.Open Then
            'm_TranSQL.Commit()
            'End If
        Catch ex1 As Exception
            'If Request.Transactional And m_conecSQL.State = ConnectionState.Open Then
            '    m_TranSQL.Rollback()
            'End If
            Request.Exception = ex1
            Throw ex1
        Finally
            'If Not m_conecSQL Is Nothing Then m_conecSQL.Dispose()
            If Not Request.Transactional And Not m_conecSQL Is Nothing Then m_conecSQL.Dispose()
        End Try


    End Sub


    ' Private helper method that execute a SqlCommand (that returns a resultset) against the specified SqlTransaction and SqlConnection
    ' using the provided parameters.
    ' e.g.:  
    '   FillDataset(conn, trans, CommandType.StoredProcedure, "GetOrders", ds, new String() {"orders"}, new SqlParameter("@prodid", 24))
    ' Parameters:
    ' -connection: A valid SqlConnection
    ' -transaction: A valid SqlTransaction
    ' -commandType: the CommandType (stored procedure, text, etc.)
    ' -commandText: the stored procedure name or T-SQL command
    ' -dataSet: A dataset wich will contain the resultset generated by the command
    ' -tableNames: this array will be used to create table mappings allowing the DataTables to be referenced
    '             by a user defined name (probably the actual table name)
    ' -commandParameters: An array of SqlParamters used to execute the command
    Private Overloads Sub FillDatasets(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandType As CommandType, _
        ByVal commandText As String, ByVal dataSet As DataSet, ByVal tableNames() As String, _
        ByVal commandParameters() As SqlParameter)

        If (connection Is Nothing) Then Throw New ArgumentNullException("connection")
        If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")

        ' Create a command and prepare it for execution
        Dim command As New SqlCommand()

        Dim mustCloseConnection As Boolean = False
        PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, mustCloseConnection)

        ' Create the DataAdapter & DataSet
        Dim dataAdapter As SqlDataAdapter = New SqlDataAdapter(command)

        Try
            ' Add the table mappings specified by the user
            If Not tableNames Is Nothing AndAlso tableNames.Length > 0 Then

                Dim tableName As String = "Table"
                Dim index As Integer

                For index = 0 To tableNames.Length - 1
                    If (tableNames(index) Is Nothing OrElse tableNames(index).Length = 0) Then Throw New ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames")
                    dataAdapter.TableMappings.Add(tableName, tableNames(index))
                    tableName = tableName & (index + 1).ToString()
                Next
            End If

            ' Fill the DataSet using default values for DataTable names, etc
            dataAdapter.Fill(dataSet)

            ' Detach the SqlParameters from the command object, so they can be used again
            command.Parameters.Clear()
        Finally
            If (Not dataAdapter Is Nothing) Then dataAdapter.Dispose()
        End Try

        If (mustCloseConnection) Then connection.Close()

    End Sub

#End Region

#Region "UpdateDataset"
    Public Overrides Sub UpdateDataSet(ByRef RequestInsert As DAABRequest, _
                                        ByRef RequestUpdate As DAABRequest, _
                                        ByRef RequestDelete As DAABRequest)

        Dim connectionString As String = RequestInsert.ConnectionString
        Dim cmdCommandInsert As SqlCommand
        Dim cmdCommandUpdate As SqlCommand
        Dim cmdCommandDelete As SqlCommand

        'Dim tranSql As SqlTransaction
        If (connectionString Is Nothing OrElse connectionString.Length = 0) Then Throw New ArgumentNullException("connectionString")
        If (RequestInsert.Command Is Nothing OrElse RequestInsert.Command.Length = 0) Then Throw New ArgumentNullException("No ha ingresado el commando a ejecutar.RequestInsert")
        If (RequestUpdate.Command Is Nothing OrElse RequestUpdate.Command.Length = 0) Then Throw New ArgumentNullException("No ha ingresado el commando a ejecutar.RequestUpdate")
        If (RequestDelete.Command Is Nothing OrElse RequestDelete.Command.Length = 0) Then Throw New ArgumentNullException("No ha ingresado el commando a ejecutar.RequestDelete")
        If (RequestInsert.RequestDataSet Is Nothing) Then Throw New ArgumentNullException("RequestDataSet:RequestInsert")
        If RequestInsert.TableNames Is Nothing Then Throw New ArgumentNullException("Falta especificar el nombre de la tabla a actualizar")

        'Dim connection As SqlConnection
        Try
            'connection = New SqlConnection(connectionString)
            'connection.Open()
            'Insert
            estableceConexion(RequestInsert.Transactional, connectionString)
            'm_conecSQL
            'm_TranSQL
            'If RequestInsert.Transactional Then
            '    tranSql = connection.BeginTransaction
            'End If
            'Insert
            cmdCommandInsert = New SqlCommand()
            Dim aparamInsert As SqlParameter() = DevuelveParametros(RequestInsert.CommandType, RequestInsert.Parameters)
            PrepareCommand(cmdCommandInsert, m_conecSQL, m_TranSQL, RequestInsert.CommandType, RequestInsert.Command, aparamInsert, False)
            'Update
            cmdCommandUpdate = New SqlCommand()
            Dim aparamUpdate As SqlParameter() = DevuelveParametros(RequestUpdate.CommandType, RequestUpdate.Parameters)
            PrepareCommand(cmdCommandUpdate, m_conecSQL, m_TranSQL, RequestUpdate.CommandType, RequestUpdate.Command, aparamUpdate, False)
            'delete
            cmdCommandDelete = New SqlCommand()
            Dim aparamDelete As SqlParameter() = DevuelveParametros(RequestDelete.CommandType, RequestDelete.Parameters)
            PrepareCommand(cmdCommandDelete, m_conecSQL, m_TranSQL, RequestDelete.CommandType, RequestDelete.Command, aparamDelete, False)

            UpdateDatasets(cmdCommandInsert, cmdCommandDelete, cmdCommandUpdate, RequestInsert.RequestDataSet, RequestInsert.TableNames(0))

            'If RequestInsert.Transactional And m_conecSQL.State = ConnectionState.Open Then
            '    m_TranSQL.Commit()
            'End If

        Catch ex1 As Exception
            'If RequestInsert.Transactional And m_conecSQL.State = ConnectionState.Open Then
            '    m_TranSQL.Rollback()
            'End If
            RequestInsert.Exception = ex1
            RequestDelete.Exception = ex1
            RequestUpdate.Exception = ex1
            Throw ex1
        Finally
            'If Not m_conecSQL Is Nothing Then m_conecSQL.Dispose()
            If Not RequestInsert.Transactional And Not m_conecSQL Is Nothing Then m_conecSQL.Dispose()
        End Try

    End Sub


    Public Sub UpdateDatasets(ByVal insertCommand As SqlCommand, _
                                ByVal deleteCommand As SqlCommand, _
                                ByVal updateCommand As SqlCommand, _
                                ByVal dataSet As DataSet, _
                                ByVal tableName As String)

        If (insertCommand Is Nothing) Then Throw New ArgumentNullException("insertCommand")
        If (deleteCommand Is Nothing) Then Throw New ArgumentNullException("deleteCommand")
        If (updateCommand Is Nothing) Then Throw New ArgumentNullException("updateCommand")
        If (dataSet Is Nothing) Then Throw New ArgumentNullException("dataSet")
        If (tableName Is Nothing OrElse tableName.Length = 0) Then Throw New ArgumentNullException("tableName")

        ' Create a SqlDataAdapter, and dispose of it after we are done
        Dim dataAdapter As New SqlDataAdapter()
        Try
            ' Set the data adapter commands
            dataAdapter.UpdateCommand = updateCommand
            dataAdapter.InsertCommand = insertCommand
            dataAdapter.DeleteCommand = deleteCommand

            ' Update the dataset changes in the data source
            dataAdapter.Update(dataSet, tableName)

            ' Commit all the changes made to the DataSet
            dataSet.AcceptChanges()
        Finally
            If (Not dataAdapter Is Nothing) Then dataAdapter.Dispose()
        End Try
    End Sub
#End Region

#Region "Destructor"

    Protected Overrides Sub Finalize()
        If Not m_conecSQL Is Nothing Then
            If Not (m_conecSQL.State = ConnectionState.Closed) Or _
                      Not (m_conecSQL.State = ConnectionState.Broken) Then
                m_conecSQL.Dispose()
            End If
        End If
        MyBase.Finalize()
    End Sub

#End Region


    Public Overrides Sub CommitTransaction()
        If Not m_conecSQL Is Nothing AndAlso m_conecSQL.State = ConnectionState.Open AndAlso Not m_TranSQL Is Nothing Then
            m_TranSQL.Commit()
            m_TranSQL = Nothing
            Dispose()
        End If
    End Sub

    Public Overrides Sub RollBackTransaction()
        If Not m_conecSQL Is Nothing AndAlso m_conecSQL.State = ConnectionState.Open AndAlso Not m_TranSQL Is Nothing Then
            m_TranSQL.Rollback()
            m_TranSQL = Nothing
            Dispose()
        End If
    End Sub

    Public Overrides Sub Dispose() 'Implements System.IDisposable.Dispose
        If Not m_conecSQL Is Nothing AndAlso _
                        (Not (m_conecSQL.State = ConnectionState.Closed) Or _
                          Not (m_conecSQL.State = ConnectionState.Broken)) Then
            If m_conecSQL.State = ConnectionState.Open AndAlso Not m_TranSQL Is Nothing Then
                m_TranSQL.Commit()
                m_TranSQL = Nothing
            End If
            m_conecSQL.Dispose()
        End If
        'MyBase.Finalize()
    End Sub

End Class ' END CLASS DEFINITION DAABSqlFactory



