Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB
Public Class clsTopeOfVenta
    Dim strCadenaConexion As String
    Dim objSeg As New Seguridad_NET.clsSeguridad
    Public Function FP_VerificaTope(ByVal p_intOpe_Codigo As Integer, _
                                ByVal p_strId_Canal As String, _
                                ByVal p_strId_OfVenta As String, _
                                ByVal p_strId_CodVendedor_sap As String) As Object()



        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("1", "PVU_TOPES")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        Dim objReturn(1) As Object

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.SQL, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("@Ope_Codigo", DbType.Int32, 4, p_intOpe_Codigo, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("@Id_Canal", DbType.String, 2, p_strId_Canal, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("@Id_OfVenta", DbType.String, 4, p_strId_OfVenta, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("@Id_CodVendedor_sap", DbType.String, 10, p_strId_CodVendedor_sap, ParameterDirection.Input), _
                          New DAAB.DAABRequest.Parameter("@Disponible", DbType.Int16, 2, ParameterDirection.Output), _
                          New DAAB.DAABRequest.Parameter("@Tipo", DbType.String, 1, ParameterDirection.Output)}


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CREDSS_VerificaTope"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        objReturn(0) = IIf(IsDBNull(CType(objRequest.Parameters(4), IDataParameter).Value), 0, CType(objRequest.Parameters(4), IDataParameter).Value)
        objReturn(1) = IIf(IsDBNull(CType(objRequest.Parameters(5), IDataParameter).Value), 0, CType(objRequest.Parameters(5), IDataParameter).Value)

        FP_VerificaTope = objReturn

    End Function

    Public Function FP_ResolucionBusqueda(ByVal p_Ope_Codigo As Integer, _
                                        ByVal p_Dat_TxnServicio As String, _
                                        ByVal p_Dat_TipoDoc As String, _
                                        ByVal p_Dat_NumDoc As String, _
                                        ByVal p_Dat_Estado As Integer) As Object


        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("1", "PVU_TOPES")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.SQL, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("@Ope_Codigo", DbType.Int32, 4, p_Ope_Codigo, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Dat_TxnServicio", DbType.String, 10, p_Dat_TxnServicio, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Dat_TipoDoc", DbType.String, 2, p_Dat_TipoDoc, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Dat_NumDoc", DbType.String, 15, p_Dat_NumDoc, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Dat_Estado", DbType.Int16, 2, p_Dat_Estado, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Resolucion", DbType.Int16, 2, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CREDSS_ResolucionBusqueda"
        objRequest.Parameters.AddRange(arrParam)

        objRequest.Factory.ExecuteNonQuery(objRequest)

        FP_ResolucionBusqueda = IIf(IsDBNull(CType(objRequest.Parameters(5), IDataParameter).Value), 0, CType(objRequest.Parameters(5), IDataParameter).Value)

    End Function

    Public Function FP_Agregar(ByVal p_intOpe_Codigo As Integer, _
                            ByVal p_strId_Canal As String, _
                            ByVal p_strId_OfVenta As String, _
                            ByVal p_strId_CodVendedor_sap As String, _
                            ByVal p_strLogin_nt As String, _
                            ByVal p_intId_Area As Integer, _
                            ByVal p_strNom_Vendedor As String, _
                            ByVal p_strTipoDoc As String, _
                            ByVal p_strNumDoc As String, _
                            ByVal p_strApePaterno As String, _
                            ByVal p_strApeMaterno As String, _
                            ByVal p_strNombre As String, _
                            ByVal p_strTxn_Txn As String, _
                            ByVal p_intResolucion As Integer, _
                            ByVal p_intCompCode As Integer, _
                            ByVal p_intErrCode As Integer, _
                            ByVal p_intEstado As Integer) As Integer


        ' 
        strCadenaConexion = objSeg.FP_GetConnectionString("1", "PVU_TOPES")  '"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.SQL, strCadenaConexion)

        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("@Ope_Codigo", DbType.Int32, 4, p_intOpe_Codigo, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Id_Canal", DbType.String, 2, p_strId_Canal, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Id_OfVenta", DbType.String, 4, p_strId_OfVenta, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Id_CodVendedor_sap", DbType.String, 10, p_strId_CodVendedor_sap, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Login_nt", DbType.String, 24, p_strLogin_nt, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Id_Area", DbType.Int16, 2, p_intId_Area, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Nom_Vendedor", DbType.String, 50, p_strNom_Vendedor, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@TipoDoc", DbType.String, 1, p_strTipoDoc, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@NumDoc", DbType.String, 15, p_strNumDoc, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@ApePaterno", DbType.String, 50, p_strApePaterno, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@ApeMaterno", DbType.String, 50, p_strApeMaterno, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Nombre", DbType.String, 50, p_strNombre, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Txn_Txn", DbType.String, 10, p_strTxn_Txn, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Resolucion", DbType.Int16, 2, p_intResolucion, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@CompCode", DbType.Int16, 2, p_intCompCode, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@ErrCode", DbType.Int32, 4, p_intErrCode, ParameterDirection.Input), _
                                  New DAAB.DAABRequest.Parameter("@Estado", DbType.Int32, 4, p_intEstado, ParameterDirection.Input)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "CREDSI_LogTransaccion"
        objRequest.Parameters.AddRange(arrParam)

        FP_Agregar = objRequest.Factory.ExecuteScalar(objRequest)

    End Function

End Class
