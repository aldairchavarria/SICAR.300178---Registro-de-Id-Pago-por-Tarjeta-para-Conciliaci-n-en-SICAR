Imports System
Imports SisCajas.Funciones

Public Class clsKeyDAC
    Private Shared _strCondicionPago As String
    Private Shared _strControlCredito As String
    Private Shared _strDefaultCP As String
    Private Shared _strDefaultCC As String
    Private Shared _strOfDefault As String

    Private Shared strParamGrupo As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConstSubOfi_GrupoRecaudacionDAC"))
    Private Shared bolParam As Boolean

    Public Shared Property strOfDefault() As String
        Get
            If (bolParam) Then
                Return _strOfDefault
            Else
                ObtenerParametro(strParamGrupo)
                Return _strOfDefault
            End If
        End Get
        Set(ByVal Value As String)
            _strOfDefault = Value
        End Set
    End Property

    Public Shared Property strDefaultCP() As String
        Get
            If (bolParam) Then
                Return _strDefaultCP
            Else
                ObtenerParametro(strParamGrupo)
                Return _strDefaultCP
            End If
        End Get
        Set(ByVal Value As String)
            _strDefaultCP = Value
        End Set
    End Property

    Public Shared Property strDefaultCC() As String
        Get
            If (bolParam) Then
                Return _strDefaultCC
            Else
                ObtenerParametro(strParamGrupo)
                Return _strDefaultCC
            End If
        End Get
        Set(ByVal Value As String)
            _strDefaultCC = Value
        End Set
    End Property

    Public Shared Property strControlCredito() As String
        Get
            If (bolParam) Then
                Return _strControlCredito
            Else
                ObtenerParametro(strParamGrupo)
                Return _strControlCredito
            End If
        End Get
        Set(ByVal Value As String)
            _strControlCredito = Value
        End Set
    End Property

    Public Shared Property strCondicionPago() As String
        Get
            If (bolParam) Then
                Return _strCondicionPago
            Else
                ObtenerParametro(strParamGrupo)
                Return _strCondicionPago
            End If
        End Get
        Set(ByVal Value As String)
            _strCondicionPago = Value
        End Set
    End Property

    Public Shared Function ObtenerParametro(ByVal strCodGrupo As String) As Boolean
        bolParam = False
        Dim dsParametros As DataSet = (New COM_SIC_Cajas.clsCajas).FP_ConsultaParametros(strCodGrupo)
        Dim i As Integer
        Dim strCodigo As String = String.Empty
        Dim strValor As String = String.Empty

        If Not IsNothing(dsParametros) Then
            For i = 0 To dsParametros.Tables(0).Rows.Count - 1
                strCodigo = CheckStr(dsParametros.Tables(0).Rows(i).Item("SPARV_KEY"))
                strValor = CheckStr(dsParametros.Tables(0).Rows(i).Item("SPARV_VALUE"))

                If (strCodigo = "1") Then
                    _strCondicionPago = strValor
                ElseIf (strCodigo = "2") Then
                    _strControlCredito = strValor
                ElseIf (strCodigo = "75") Then
                    _strDefaultCP = strValor
                ElseIf (strCodigo = "76") Then
                    _strDefaultCC = strValor
                ElseIf (strCodigo = "77") Then
                    _strOfDefault = strValor
                End If

            Next
            bolParam = True
        End If
    End Function
End Class
