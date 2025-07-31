Public Class reniec_Consolidado
    Inherits System.Web.UI.Page

    Public objDato As Object

    Public strNumDoc As String
    Public strChrVerifica As String
    Public strApePaterno As String
    Public strApeMaterno As String
    Public strNombres As String
    Public strFechaNac As String
    Public strSexo As String
    Public strEstadoCivil As String
    Public strGradoInstruccion As String
    Public strAnioEstudio As String
    Public strEstatura As String
    Public strDepartamentoNac As String
    Public strProvinciaNac As String
    Public strDistritoNac As String
    Public strRestricciones As String
    Public strFechaExpedicion As String
    Public strNombrePadre As String
    Public strNombreMadre As String
    Public strFechaInscripcion As String
    Public strDepartamento As String
    Public strProvincia As String
    Public strDistrito As String
    Public strDireccion As String
    Public strConstanciaVotacion As String
    Public strFoto As Object
    Public strFirma As Object
    Public strArchivoFoto As String
    Public strArchivoFirma As String



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            objDato = Session("objDato")

            If Not objDato Is Nothing Then
                strNumDoc = objDato("NumDoc")
                '''strChrVerifica = objDato("ChrVerifica") 'Esta vacio
                strApePaterno = objDato("ApePaterno")
                strApeMaterno = objDato("ApeMaterno")
                strNombres = objDato("Nombres")
                strFechaNac = FR_FmtFechaRENIEC(objDato("FechaNac"), "DMY")
                strSexo = objDato("Sexo")
                strEstadoCivil = objDato("EstadoCivil")
                strAnioEstudio = objDato("AnioEstudio")
                If UCase(Trim(objDato("GradoInstruccion"))) = "ILETRADO" Then
                    strAnioEstudio = ""
                End If
                strGradoInstruccion = objDato("GradoInstruccion") & " " & strAnioEstudio
                strEstatura = objDato("Estatura")
                strDepartamentoNac = objDato("DepartamentoNac")
                strProvinciaNac = objDato("ProvinciaNac")
                strDistritoNac = objDato("DistritoNac")
                strRestricciones = objDato("Restricciones")
                strFechaExpedicion = FR_FmtFechaRENIEC(objDato("FechaExpedicion"), "DMY")
                strNombrePadre = objDato("NombrePadre")
                strNombreMadre = objDato("NombreMadre")
                strFechaInscripcion = FR_FmtFechaRENIEC(objDato("FechaInscripcion"), "DMY")
                strDepartamento = objDato("Departamento")
                strProvincia = objDato("Provincia")
                strDistrito = objDato("Distrito")
                strDireccion = objDato("Direccion")
                strConstanciaVotacion = objDato("ConstanciaVotacion")
                strFoto = objDato("Foto")
                strFirma = objDato("Firma")
                strArchivoFoto = "Foto" & strNumDoc & ".jpg"
                strArchivoFirma = "Firma" & strNumDoc & ".jpg"

                Call SR_CrearArchivoImagen(strFoto, strArchivoFoto)
                Call SR_CrearArchivoImagen(strFirma, strArchivoFirma)
            End If

            objDato = Nothing
        End If
    End Sub

    Private Function FR_FmtFechaRENIEC(ByVal p_strFecha, ByVal p_strFormato)
        Dim strDia
        Dim strMes
        Dim strAnio
        strDia = Mid(p_strFecha, 7, 2)
        strMes = Mid(p_strFecha, 5, 2)
        strAnio = Mid(p_strFecha, 1, 4)
        Select Case p_strFormato
            Case "DMY"
                FR_FmtFechaRENIEC = strDia & "-" & strMes & "-" & strAnio
            Case "MDY"
                FR_FmtFechaRENIEC = strMes & "-" & strDia & "-" & strAnio
            Case "YMD"
                FR_FmtFechaRENIEC = strAnio & "-" & strMes & "-" & strDia
            Case Else
                FR_FmtFechaRENIEC = strDia & "-" & strMes & "-" & strAnio
        End Select
    End Function

    Private Sub SR_CrearArchivoImagen(ByVal p_strDato, ByVal p_strNombre)
        Dim objFSO
        Dim objArchivo
        Dim strRutaImagen
        strRutaImagen = Request.ServerVariables("APPL_PHYSICAL_PATH") & "Evaluacion\Imagen\"
        objFSO = Server.CreateObject("Scripting.FileSystemObject")
        objArchivo = objFSO.CreateTextFile(strRutaImagen & p_strNombre, True)
        objArchivo.Write(p_strDato)
        objArchivo.Close()
        objFSO = Nothing
    End Sub

    Public Function FR_FmtFechaHora(ByVal p_datFecha, ByVal p_strFormato)
        Dim strDia
        Dim strMes
        Dim strAnio
        Dim strHora
        Dim strMinuto
        Dim strFechaHora
        strDia = Right("00" & Day(p_datFecha), 2)
        strMes = Right("00" & Month(p_datFecha), 2)
        strAnio = Year(p_datFecha)
        strHora = Right("00" & Hour(p_datFecha), 2)
        strMinuto = Right("00" & Minute(p_datFecha), 2)
        Select Case p_strFormato
            Case "DMY"
                strFechaHora = strDia & "/" & strMes & "/" & strAnio
            Case "MDY"
                strFechaHora = strMes & "/" & strDia & "/" & strAnio
            Case "YMD"
                strFechaHora = strAnio & "/" & strMes & "/" & strDia
            Case Else
                strFechaHora = strDia & "/" & strMes & "/" & strAnio
        End Select
        FR_FmtFechaHora = strFechaHora & " " & strHora & ":" & strMinuto
    End Function

End Class
