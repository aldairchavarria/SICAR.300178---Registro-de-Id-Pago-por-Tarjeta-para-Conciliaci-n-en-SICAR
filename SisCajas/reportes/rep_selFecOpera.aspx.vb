Imports SisCajas.clsLib_Session
Imports SisCajas.clsContantes_site

Public Class rep_selFecOpera
    Inherits System.Web.UI.Page


#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Public strFecha As String
    Public strSiteOcultarCapas As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Session("FechaAct") = "08/03/2006"
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Session("FechaAct") = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'Format(Now, "d")
            strFecha = Session("FechaAct")

            'Introducir aquí el código de usuario para inicializar la página
            strFecha = Session("FechaAct")
            If (Len(Session("STRMessage")) > 0) Then
                Response.Write("<script language=JavaScript type='text/javascript'>")
                Response.Write("alert('" & Session("STRMessage") & "');")
                Response.Write("</script>")
            End If
        Session("STRMessage") = ""
        End If
    End Sub

End Class
