Imports SisCajas.clsLib_Session
Imports SisCajas.clsContantes_site

Public Class rep_selFecOpera
    Inherits System.Web.UI.Page


#Region " C�digo generado por el Dise�ador de Web Forms "

    'El Dise�ador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTA: el Dise�ador de Web Forms necesita la siguiente declaraci�n del marcador de posici�n.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Dise�ador de Web Forms requiere esta llamada de m�todo
        'No la modifique con el editor de c�digo.
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

            'Introducir aqu� el c�digo de usuario para inicializar la p�gina
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
