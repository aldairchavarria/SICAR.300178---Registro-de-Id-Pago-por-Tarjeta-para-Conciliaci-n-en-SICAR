Imports System.Data.OleDb
Imports ADODB
Public Class Conexion
    Inherits System.Web.UI.Page
    'strCadenaConexion = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
    Const g_modName = "Database.bas"
    Dim strProveedorBD As Integer
    Dim strKey As String
    Dim objRecordSet As Recordset
    Dim daRecord As New OleDbDataAdapter
    Dim dsRecord As New DataSet
    Dim objEmpleado

    Function GetConnectionString() As String
        strProveedorBD = 2    'Proveedor 2= ORACLE;  1= SQL SERVER
        strKey = "PVU"   'Key del registry de WIN
        'ObjUser = CreateObject("Seguridad_UTL.clsSeguridad")
        'GetConnectionString = ObjUser.GetCadenaConexion(strProveedorBD, strKey)
        'ObjUser = Nothing
        '**********HARDCODE**************
        objEmpleado = Server.CreateObject("Seguridad_UTL.clsSeguridad")
        objRecordSet = objEmpleado.GetCadenaConexion(CInt(strProveedorBD), CStr(strKey))
        daRecord.MissingSchemaAction = MissingSchemaAction.AddWithKey
        daRecord.Fill(dsRecord, objRecordSet, "Conexion")

        GetConnectionString = dsRecord.Tables(0).Rows(0).Item(0)
        'ObjUser = CreateObject("Seguridad_Test_UTL.clsSeguridad")
        'strCadena = ObjUser.BaseDatos(ObjetoITSAP)
        'strcliente = ObjUser.BaseDatos(ObjetoITSAP)
        'strUsuario = ObjUser.Usuario(ObjetoITSAP)
        'strPassword = ObjUser.Password(ObjetoITSAP)
        'strLanguage = ObjUser.Proveedor(ObjetoITSAP)
        'strApplicationServer = ObjUser.Servidor(ObjetoITSAP)

    End Function

End Class
