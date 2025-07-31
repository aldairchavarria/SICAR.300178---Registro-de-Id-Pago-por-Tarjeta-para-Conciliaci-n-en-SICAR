Imports COM_SIC_Activaciones
Imports SisCajas.Funciones

Public Class sicar_nota_canje
    Inherits System.Web.UI.Page

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
    'PROY-23700-IDEA-29415 - INI - ACB
    Public strNomDocNotaCanje As String = String.Empty
    Public strConsMensDocNotaCanje As String = String.Empty
    Public strConsDirecClaroCanje As String = String.Empty
    Public strConsDistritoClaroCanje As String = String.Empty
    Public strConsUrbClaroCanje As String = String.Empty
    Public CONS_TIPO_OPE_DEVO As String = String.Empty
    'PROY-23700-IDEA-29415 - FIN - ACB
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ConsultaParametros() 'PROY-23700-IDEA-29415 - ACB
    End Sub

    'PROY-23700-IDEA-29415 - INI - ACB
    Private Function ConsultaParametros() As ArrayList
        Dim objpvuDB As New COM_SIC_Activaciones.clsConsultaPvu
        Dim arrParametros As ArrayList
        Dim oParamteros As New COM_SIC_Activaciones.BEParametros
        Dim strCodGrupo As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("consCodGrupoParamSinergiaSegundaFase"))

        'Dim strNotaCanje As String
        Dim strValor As String

        If strCodGrupo <> "" Then
            arrParametros = objpvuDB.ConsultaParametros(strCodGrupo)

            For Each item As BEParametros In arrParametros
                strValor = item.strValor1
                Select Case strValor
                    Case "21"
                        strNomDocNotaCanje = Funciones.CheckStr(item.strValor)
                    Case "22"
                        strConsMensDocNotaCanje = Funciones.CheckStr(item.strValor)
                    Case "23"
                        strConsDirecClaroCanje = Funciones.CheckStr(item.strValor)
                    Case "24"
                        strConsDistritoClaroCanje = Funciones.CheckStr(item.strValor)
                    Case "25"
                        strConsUrbClaroCanje = Funciones.CheckStr(item.strValor)
                    Case "32"
                        CONS_TIPO_OPE_DEVO = Funciones.CheckStr(item.strValor)
                End Select
            Next

        End If
        Return arrParametros
    End Function
    'PROY-23700-IDEA-29415 - FIN - ACB

End Class
