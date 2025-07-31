Public Class clsAuditoria
    Public Function AddAuditoria( _
                           ByVal k_usuac_cod As Long, ByVal k_audic_ip As String, _
                           ByVal k_audic_host As String, ByVal k_opcic_cod As Long, _
                           ByVal k_audic_Resultado As Integer, ByVal k_audic_descripcion As String, _
                           ByVal k_aplic_cod As Long, ByVal k_evenc_cod As Long, _
                           ByVal k_perfc_cod As Long, ByVal k_audic_login As String, _
                           ByVal k_Estac_cod As Integer, ByVal DetalleAuditoria(,) As Object) As Double


        'Dim objAudi As Object
        'Dim objSeg As New Seguridad_NET.clsSeguridad

        'objAudi = CreateObject("Audi_Bus.Auditoria")

        'k_audic_host = objSeg.GetHost(k_audic_ip)

        'AddAuditoria = objAudi.AddAuditoria(CDbl(k_usuac_cod), k_audic_ip, k_audic_host, CDbl(k_opcic_cod), k_audic_Resultado, k_audic_descripcion, _
        '        CDbl(k_aplic_cod), CDbl(k_evenc_cod), CDbl(k_perfc_cod), k_audic_login, k_Estac_cod, DetalleAuditoria)

        'objAudi = Nothing

        AddAuditoria = 1

    End Function

End Class
