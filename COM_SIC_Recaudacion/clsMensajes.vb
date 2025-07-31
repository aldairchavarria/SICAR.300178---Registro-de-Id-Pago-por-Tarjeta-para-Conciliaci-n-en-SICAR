Imports COM_SIC_Cajas

Public NotInheritable Class clsMensajes
	'---grupos (códigos)
    Public Const kGrupo_FijoPaginas As Short = 1

'---
    Private Const kGT_MensajeGenerico As String = "Error desconocido (Fijo y Páginas)."    '????

	'---grupo
    Public Shared Function DeterminaMensaje(ByVal pGrupo As Short, ByVal pCodigo As String, ByVal pMensajeOriginal As String) As String
        Dim sMensaje As String

        If pGrupo = kGrupo_FijoPaginas Then
            'sMensaje = clsError.Get_DescripcionError(String.Format("{0:D3}", pCodigo))
            sMensaje = clsError.Get_DescripcionError(pCodigo)
            If sMensaje = String.Empty Then
                If pMensajeOriginal <> String.Empty Then
                    sMensaje = pMensajeOriginal
                Else
                    sMensaje = kGT_MensajeGenerico
                End If
            End If
        Else
            sMensaje = "No ha definido un Grupo para identificar el Error"
        End If
        '---
        Return sMensaje
    End Function


End Class
