Public Class clsAudi

    '************************************************************************************
    ' Formulario		: audi_Add.asp
    ' Proposito   		: Función Generica para el registro de Auditoria
    ' Inputs    		: Lista de Parametros de la función
    ' Se asume  		: N/A
    ' Efectos   		: N/A
    ' Retorno   		: wIdAuditoria
    ' Notas				: N/A
    ' ModIficaciones	: N/A
    ' Autor     		: Pablo Tuesta Cóndor
    ' Fecha y hora  	: 09/01/2002 - 14:50 hrs.
    '************************************************************************************

    '*************************************************
    '**** Parametros de la Funcion Registra_Audi *****
    '*************************************************
    'CodigoUsuario,CodigoOpcion,Resultado,Descripcion,CodigoAplicacion,CodigoEvento,CodigoPerfil,NombrePC,ArregloDetalleAuditoria

    Function Registro_Audi(ByVal pCodUsuario, ByVal pOpcion, ByVal pResultado, ByVal pDescripcion, ByVal pAplicacion, ByVal pEvento, ByVal pPerfil, ByVal pHost, ByVal pDetalle())
        '    'Declaracion de Variable que soporta Array de detalle Auditoria
        '    Dim wDetalle
        '    Dim wintCodUsuario
        '	Dim wstrIP
        '	Dim wstrHost
        '	Dim wObjHost
        '	On Error resume Next
        '
        '	'Parametros de Cebecera de Auditoria
        '	'***************
        '	wintCodUsuario = pCodUsuario
        '	wstrIP         = Request.ServerVariables("Remote_Addr")
        '	
        '	'if trim(pHost)<>"" Then
        '	'   wstrHost       = pHost
        '	'Else
        '	
        '	'BGuerra 02-09-2004: Para que si es metaframe no se obtenga nombre de pc
        '	Dim	IpMetaFrame
        '	IpMetaFrame = Request.ServerVariables("REMOTE_ADDR")
        '	if InStr( 1, Session("IPMETAFRAME"), IpMetaFrame) = 0 then
        '		'Obtiene Nombre de Host
        '		Dim strIP,strNombreHost
        '		strIP        = Request.ServerVariables("REMOTE_ADDR")
        '		Set wObjHost = Server.CreateObject("Segu_Bus.Host")
        '		strNombreHost= wObjHost.GetHost(strIP)
        '		set wObjHost = Nothing
        '	    wstrHost     = strNombreHost
        '	    'Fin Obtiene Nombre Host
        '	else
        '		strIP        = Request.ServerVariables("REMOTE_ADDR")
        '		wstrHost     = Session("Almacen")
        '	end If
        '	'#######################################################################
        '	
        '	Dim wintOpcion
        '	Dim wstrResultado
        '	Dim wstrDescripcion
        '	Dim wintAplicacion
        '	Dim wintEvento
        '	Dim wintPerfil
        '	Dim wstrLogin
        '	Dim wintEstado
        '	Dim wObj
        '	Dim wIdAuditoria
        '
        '	
        '	wintOpcion     = pOpcion
        '	wstrResultado  = pResultado
        '	wstrDescripcion= pDescripcion
        '	wintAplicacion = pAplicacion
        '	wintEvento     = pEvento
        '	wintPerfil     = pPerfil
        '    wstrLogin      = Mid(Request.ServerVariables("Logon_User"),InStr(1,Request.ServerVariables("Logon_User"),"\",vbTextCompare)+1,20)
        '	'wstrLogin	   = Session("codUsuario")
        '    wintEstado     = 1 'Activo
        '
        '    'Asigna variable de Tipo Array para Detalle de Auditoria
        '    wDetalle=pDetalle
        ' 	'Carga de Datos de Asp de Cabecera de Auditoria
        ' 	'Se crea el Objeto wObj
        ' 	'oscar
        '	Set wObj = Server.CreateObject("Audi_Bus.Auditoria")
        '	wIdAuditoria = wObj.AddAuditoria(wintCodUsuario, wstrIP, wstrHost, wintOpcion, wstrResultado, wstrDescripcion, wintAplicacion, wintEvento, wintPerfil, wstrLogin, wintEstado,wDetalle)
        '	set wObj=Nothing
        '	If Err.Number <> 0 then
        '	   Dim ObjLogAudi
        '	   set ObjLogAudi= server.createobject("COM_PVU_LOG.Reg_Log")
        '	   ObjLogAudi.GrabarLog "Err Auditoria :" & Err.Description ,"X"
        '	   ObjLogAudi.GrabarLog "Datos Err Auditoria :" & wintCodUsuario & ":::" & wstrIP & ":::" & wstrHost & ":::" & wintOpcion & ":::" & wstrResultado & ":::" & wstrDescripcion & ":::" & wintAplicacion & ":::" & wintEvento & ":::" & wintPerfil & ":::" & wstrLogin & ":::" & wintEstado  & ":::" & wDetalle ,"X"
        '	   set ObjLogAudi=nothing
        '	   Err.Clear
        '	   On Error GoTo 0
        '	End If
        '	Registro_Audi=wIdAuditoria
        Registro_Audi = 1
    End Function



End Class
