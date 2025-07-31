Public Class AppSettings
    'INICIO-PROY-25335-Contratacion Electronica R2 - GAPS
    Public Shared Property Key_FlagHTML() As String
        Get
            Return m_Key_FlagHTML
        End Get
        Set(ByVal Value As String)
            m_Key_FlagHTML = Value
        End Set
    End Property
    Private Shared m_Key_FlagHTML As String

    'Public Shared Property Key_EnvioCorreoError() As String
    '    Get
    '        Return m_Key_EnvioCorreoError
    '    End Get
    '    Set(ByVal Value As String)
    '        m_Key_EnvioCorreoError = Value
    '    End Set
    'End Property
    'Private Shared m_Key_EnvioCorreoError As String

    Public Shared Property Key_BioSistema() As String
        Get
            Return m_Key_BioSistema
        End Get
        Set(ByVal Value As String)
            m_Key_BioSistema = Value
        End Set
    End Property
    Private Shared m_Key_BioSistema As String

    Public Shared Property Key_BioModalidadVenta() As String
        Get
            Return m_Key_BioModalidadVenta
        End Get
        Set(ByVal Value As String)
            m_Key_BioModalidadVenta = Value
        End Set
    End Property
    Private Shared m_Key_BioModalidadVenta As String

    Public Shared Property Key_BioOperacionAlta() As String
        Get
            Return m_Key_BioOperacionAlta
        End Get
        Set(ByVal Value As String)
            m_Key_BioOperacionAlta = Value
        End Set
    End Property
    Private Shared m_Key_BioOperacionAlta As String

    Public Shared Property Key_BioOperacionMigracion() As String
        Get
            Return m_Key_BioOperacionMigracion
        End Get
        Set(ByVal Value As String)
            m_Key_BioOperacionMigracion = Value
        End Set
    End Property
    Private Shared m_Key_BioOperacionMigracion As String

    Public Shared Property Key_BioOperacionPorta() As String
        Get
            Return m_Key_BioOperacionPorta
        End Get
        Set(ByVal Value As String)
            m_Key_BioOperacionPorta = Value
        End Set
    End Property
    Private Shared m_Key_BioOperacionPorta As String

    Public Shared Property Key_BioLinea() As String
        Get
            Return m_Key_BioLinea
        End Get
        Set(ByVal Value As String)
            m_Key_BioLinea = Value
        End Set
    End Property
    Private Shared m_Key_BioLinea As String

    Public Shared Property Key_BioOrigenFirmaDigital() As String
        Get
            Return m_Key_BioOrigenFirmaDigital
        End Get
        Set(ByVal Value As String)
            m_Key_BioOrigenFirmaDigital = Value
        End Set
    End Property
    Private Shared m_Key_BioOrigenFirmaDigital As String

    Public Shared Property Key_BioTipoValFirmaDigital() As String
        Get
            Return m_Key_BioTipoValFirmaDigital
        End Get
        Set(ByVal Value As String)
            m_Key_BioTipoValFirmaDigital = Value
        End Set
    End Property
    Private Shared m_Key_BioTipoValFirmaDigital As String

    Public Shared Property Key_BioWsOrigenFirmaDigital() As String
        Get
            Return m_Key_BioWsOrigenFirmaDigital
        End Get
        Set(ByVal Value As String)
            m_Key_BioWsOrigenFirmaDigital = Value
        End Set
    End Property
    Private Shared m_Key_BioWsOrigenFirmaDigital As String
    'Public Shared Property Key_CorreoRemitente() As String
    '    Get
    '        Return m_Key_CorreoRemitente
    '    End Get
    '    Set(ByVal Value As String)
    '        m_Key_CorreoRemitente = Value
    '    End Set
    'End Property
    'Private Shared m_Key_CorreoRemitente As String
    Public Shared Property Key_AsuntoCorreo() As String
        Get
            Return m_Key_AsuntoCorreo
        End Get
        Set(ByVal Value As String)
            m_Key_AsuntoCorreo = Value
        End Set
    End Property
    Private Shared m_Key_AsuntoCorreo As String

    Public Shared Property Key_MensajeAltaPost() As String
        Get
            Return m_Key_MensajeAltaPost
        End Get
        Set(ByVal Value As String)
            m_Key_MensajeAltaPost = Value
        End Set
    End Property
    Private Shared m_Key_MensajeAltaPost As String

    'Public Shared Property Key_TipoProductoFirmaDigital() As String
    '    Get
    '        Return m_Key_TipoProductoFirmaDigital
    '    End Get
    '    Set(ByVal Value As String)
    '        m_Key_TipoProductoFirmaDigital = Value
    '    End Set
    'End Property
    'Private Shared m_Key_TipoProductoFirmaDigital As String

    Public Shared Property Key_GrupoTipificacionEnvioCorreo() As String
        Get
            Return m_Key_GrupoTipificacionEnvioCorreo
        End Get
        Set(ByVal Value As String)
            m_Key_GrupoTipificacionEnvioCorreo = Value
        End Set
    End Property
    Private Shared m_Key_GrupoTipificacionEnvioCorreo As String

    Public Shared Property Key_GrupoTipificacionEnvioCorreoGen() As String
        Get
            Return m_Key_GrupoTipificacionEnvioCorreoGen
        End Get
        Set(ByVal Value As String)
            m_Key_GrupoTipificacionEnvioCorreoGen = Value
        End Set
    End Property
    Private Shared m_Key_GrupoTipificacionEnvioCorreoGen As String
    'FIN-PROY-25335-Contratacion Electronica R2 - GAPS

'INICIO PROY-140573 -Automatizacion Fija Fase 2
    '-------------------------

    Public Shared Property Key_AsuntoCorreoHFC() As String
        Get
            Return m_Key_AsuntoCorreoHFC
        End Get
        Set(ByVal Value As String)
            m_Key_AsuntoCorreoHFC = Value
        End Set
    End Property
    Private Shared m_Key_AsuntoCorreoHFC As String


    Public Shared Property Key_BannerAltaPostHFC() As String
        Get
            Return m_Key_BannerAltaPostHFC
        End Get
        Set(ByVal Value As String)
            m_Key_BannerAltaPostHFC = Value
        End Set
    End Property
    Private Shared m_Key_BannerAltaPostHFC As String

    Public Shared Property Key_CorreoRemitenteHFC() As String
        Get
            Return m_Key_CorreoRemitenteHFC
        End Get
        Set(ByVal Value As String)
            m_Key_CorreoRemitenteHFC = Value
        End Set
    End Property
    Private Shared m_Key_CorreoRemitenteHFC As String

    Public Shared Property Key_NumeroReenvioCorreo() As String
        Get
            Return m_Key_NumeroReenvioCorreo
        End Get
        Set(ByVal Value As String)
            m_Key_NumeroReenvioCorreo = Value
        End Set
    End Property
    Private Shared m_Key_NumeroReenvioCorreo As String

    Public Shared Property Key_PlantillaMailPersonaJuridica_HFC() As String
        Get
            Return m_Key_PlantillaMailPersonaJuridica_HFC
        End Get
        Set(ByVal Value As String)
            m_Key_PlantillaMailPersonaJuridica_HFC = Value
        End Set
    End Property
    Private Shared m_Key_PlantillaMailPersonaJuridica_HFC As String
    Public Shared Property Key_PlantillaMailPersonaNatural_HFC() As String 'AGREGADO 28032021 INI
        Get
            Return m_Key_PlantillaMailPersonaNatural_HFC
        End Get
        Set(ByVal Value As String)
            m_Key_PlantillaMailPersonaNatural_HFC = Value
        End Set
    End Property
    Private Shared m_Key_PlantillaMailPersonaNatural_HFC As String 'AGREGADO 28032021 INI

    Public Shared Property Key_RutaPlantillaCorreoAutomatizacionDoc() As String
        Get
            Return m_Key_RutaPlantillaCorreoAutomatizacionDoc
        End Get
        Set(ByVal Value As String)
            m_Key_RutaPlantillaCorreoAutomatizacionDoc = Value
        End Set
    End Property
    Private Shared m_Key_RutaPlantillaCorreoAutomatizacionDoc As String

    Public Shared Property Key_FlagGeneralEnvioCorreoCAC() As String
        Get
            Return m_Key_FlagGeneralEnvioCorreoCAC
        End Get
        Set(ByVal Value As String)
            m_Key_FlagGeneralEnvioCorreoCAC = Value
        End Set
    End Property
    Private Shared m_Key_FlagGeneralEnvioCorreoCAC As String

    Public Shared Property Key_CodigoProductoIFI() As String
        Get
            Return m_Key_CodigoProductoIFI
        End Get
        Set(ByVal Value As String)
            m_Key_CodigoProductoIFI = Value
        End Set
    End Property
    Private Shared m_Key_CodigoProductoIFI As String

    Public Shared Property Key_CodigoProductoMovil() As String
        Get
            Return m_Key_CodigoProductoMovil
        End Get
        Set(ByVal Value As String)
            m_Key_CodigoProductoMovil = Value
        End Set
    End Property
    Private Shared m_Key_CodigoProductoMovil As String
'FIN PROY-140573 -Automatizacion Fija Fase 2
End Class
