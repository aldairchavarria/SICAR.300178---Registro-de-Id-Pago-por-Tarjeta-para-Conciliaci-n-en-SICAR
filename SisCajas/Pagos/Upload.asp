<%@ LANGUAGE=VBScript %>
<%
Dim RequestXForm
Dim objFile
Dim strFile
Dim strRuta
Dim strRutaWeb
Dim strControl

Response.Buffer = true


private Flag
private FlagFile
private strNombreArchivo
private strRutaUploadFileServer

strRutaUploadFileServer="\\fileserver1\Intradoc\SICAR"

Session("Carga") = 0

if request.Form("submit") = "Cargar" then
	Response.Write "HOLA1." & Trim(request.form("submit"))
	'Response.end 
else
	Response.Write "HOLA2." & Trim(request.form("submit"))
	'Response.end 
end if

'FLAG DE UPLOAD ARCHIVO
If Request("Action")<> "1" Then

	'******************************'
	'**** INICIO CODIGO UPLOAD ****'
	'******************************'
		
	Set RequestXForm = Server.CreateObject("ABCUpload4.XForm")
	RequestXForm.MaxUploadSize = 138860800
	RequestXForm.AbsolutePath  = True
	RequestXForm.Overwrite	   = True

	Set objFile = RequestXForm("txtFile")(1)

	if (objFile.rawfilename <> "") then
		strFile		 = objFile.rawFileName
	End If
	
	strRuta    = strRutaUploadFileServer & strFile 'Ruta en File Server incluyendo nombre de archivo
	'strRutaWeb = strRutaUploadFile & strFile       'Ruta en Servidor Web incluyendo nombre de archivo
	
	objFile.Save strRuta
	'objFile.Save strRutaWeb
	
	Session("Carga") = 1
	
	Response.Redirect "detapago.aspx?Action=1"
			
	'******************************'
	'**** FIN DE CODIGO UPLOAD ****'
	'******************************'
End If
%>
