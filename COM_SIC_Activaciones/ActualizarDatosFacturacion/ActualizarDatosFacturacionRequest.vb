Namespace ActualizarDatosFacturacion

    'INICIATIVA-219
    Public Class ActualizarDatosFacturacionRequest

        Private _bmId As String
        Private _adrBirthdt As String
        Private _adrCheck As String
        Private _adrCity As String
        Private _adrCode As String
        Private _adrCompno As String
        Private _adrCounty As String
        Private _adrCusttype As String
        Private _adrDeleted As String
        Private _adrDrivelicence As String
        Private _adrEmail As String
        Private _adrEmployee As String
        Private _adrEmployer As String
        Private _adrFax As String
        Private _adrFaxarea As String
        Private _adrFname As String
        Private _adrForward As String
        Private _adrIdno As String
        Private _adrInccode As String
        Private _adrJbdes As String
        Private _adrLname As String
        Private _adrLocation1 As String
        Private _adrLocation2 As String
        Private _adrMname As String
        Private _adrName As String
        Private _adrNationality As String
        Private _adrNationalitypub As String
        Private _adrNote1 As String
        Private _adrNote2 As String
        Private _adrNote3 As String
        Private _adrPhn1 As String
        Private _adrPhn1Area As String
        Private _adrPhn2 As String
        Private _adrPhn2Area As String
        Private _adrRemark As String
        Private _adrRoles As String
        Private _adrSeq As String
        Private _adrSex As String
        Private _adrSmsno As String
        Private _adrSocialSeno As String
        Private _adrState As String
        Private _adrStreet As String
        Private _adrStreetNo As String
        Private _adrTaxno As String
        Private _adrUrgent As String
        Private _adrValidDate As String
        Private _adrWriteOnReq As String
        Private _adrYears As String
        Private _adrzIp As String
        Private _countryId As String
        Private _countryIdPub As String
        Private _csId As String
        Private _csIdPub As String
        Private _idTypeCode As String
        Private _lngCode As String
        Private _lngCodePub As String
        Private _masCode As String
        Private _masCodePub As String
        Private _ttlId As String
        Private _ttlidpub As String
        Private _billingAccountId As String
        Private _listaOpcional As ActualizarDatosFacturacion.ListaOpcional

        Public Sub New()
            _listaOpcional = New ActualizarDatosFacturacion.ListaOpcional
        End Sub

        Public Property bmId() As String
            Get
                Return _bmId
            End Get
            Set(ByVal Value As String)
                _bmId = Value
            End Set
        End Property

        Public Property adrBirthdt() As String
            Get
                Return _adrBirthdt
            End Get
            Set(ByVal Value As String)
                _adrBirthdt = Value
            End Set
        End Property

        Public Property adrCheck() As String
            Get
                Return _adrCheck
            End Get
            Set(ByVal Value As String)
                _adrCheck = Value
            End Set
        End Property

        Public Property adrCity() As String
            Get
                Return _adrCity
            End Get
            Set(ByVal Value As String)
                _adrCity = Value
            End Set
        End Property

        Public Property adrCode() As String
            Get
                Return _adrCode
            End Get
            Set(ByVal Value As String)
                _adrCode = Value
            End Set
        End Property

        Public Property adrCompno() As String
            Get
                Return _adrCompno
            End Get
            Set(ByVal Value As String)
                _adrCompno = Value
            End Set
        End Property

        Public Property adrCounty() As String
            Get
                Return _adrCounty
            End Get
            Set(ByVal Value As String)
                _adrCounty = Value
            End Set
        End Property

        Public Property adrCusttype() As String
            Get
                Return _adrCusttype
            End Get
            Set(ByVal Value As String)
                _adrCusttype = Value
            End Set
        End Property

        Public Property adrDeleted() As String
            Get
                Return _adrDeleted
            End Get
            Set(ByVal Value As String)
                _adrDeleted = Value
            End Set
        End Property

        Public Property adrDrivelicence() As String
            Get
                Return _adrDrivelicence
            End Get
            Set(ByVal Value As String)
                _adrDrivelicence = Value
            End Set
        End Property

        Public Property adrEmail() As String
            Get
                Return _adrEmail
            End Get
            Set(ByVal Value As String)
                _adrEmail = Value
            End Set
        End Property

        Public Property adrEmployee() As String
            Get
                Return _adrEmployee
            End Get
            Set(ByVal Value As String)
                _adrEmployee = Value
            End Set
        End Property

        Public Property adrEmployer() As String
            Get
                Return _adrEmployer
            End Get
            Set(ByVal Value As String)
                _adrEmployer = Value
            End Set
        End Property

        Public Property adrFax() As String
            Get
                Return _adrFax
            End Get
            Set(ByVal Value As String)
                _adrFax = Value
            End Set
        End Property

        Public Property adrFaxarea() As String
            Get
                Return _adrFaxarea
            End Get
            Set(ByVal Value As String)
                _adrFaxarea = Value
            End Set
        End Property

        Public Property adrFname() As String
            Get
                Return _adrFname
            End Get
            Set(ByVal Value As String)
                _adrFname = Value
            End Set
        End Property

        Public Property adrForward() As String
            Get
                Return _adrForward
            End Get
            Set(ByVal Value As String)
                _adrForward = Value
            End Set
        End Property

        Public Property adrIdno() As String
            Get
                Return _adrIdno
            End Get
            Set(ByVal Value As String)
                _adrIdno = Value
            End Set
        End Property

        Public Property adrInccode() As String
            Get
                Return _adrInccode
            End Get
            Set(ByVal Value As String)
                _adrInccode = Value
            End Set
        End Property

        Public Property adrJbdes() As String
            Get
                Return _adrJbdes
            End Get
            Set(ByVal Value As String)
                _adrJbdes = Value
            End Set
        End Property

        Public Property adrLname() As String
            Get
                Return _adrLname
            End Get
            Set(ByVal Value As String)
                _adrLname = Value
            End Set
        End Property

        Public Property adrLocation1() As String
            Get
                Return _adrLocation1
            End Get
            Set(ByVal Value As String)
                _adrLocation1 = Value
            End Set
        End Property

        Public Property adrLocation2() As String
            Get
                Return _adrLocation2
            End Get
            Set(ByVal Value As String)
                _adrLocation2 = Value
            End Set
        End Property

        Public Property adrMname() As String
            Get
                Return _adrMname
            End Get
            Set(ByVal Value As String)
                _adrMname = Value
            End Set
        End Property

        Public Property adrName() As String
            Get
                Return _adrName
            End Get
            Set(ByVal Value As String)
                _adrName = Value
            End Set
        End Property

        Public Property adrNationality() As String
            Get
                Return _adrNationality
            End Get
            Set(ByVal Value As String)
                _adrNationality = Value
            End Set
        End Property

        Public Property adrNationalitypub() As String
            Get
                Return _adrNationalitypub
            End Get
            Set(ByVal Value As String)
                _adrNationalitypub = Value
            End Set
        End Property

        Public Property adrNote1() As String
            Get
                Return _adrNote1
            End Get
            Set(ByVal Value As String)
                _adrNote1 = Value
            End Set
        End Property

        Public Property adrNote2() As String
            Get
                Return _adrNote2
            End Get
            Set(ByVal Value As String)
                _adrNote2 = Value
            End Set
        End Property

        Public Property adrNote3() As String
            Get
                Return _adrNote3
            End Get
            Set(ByVal Value As String)
                _adrNote3 = Value
            End Set
        End Property

        Public Property adrPhn1() As String
            Get
                Return _adrPhn1
            End Get
            Set(ByVal Value As String)
                _adrPhn1 = Value
            End Set
        End Property

        Public Property adrPhn1Area() As String
            Get
                Return _adrPhn1Area
            End Get
            Set(ByVal Value As String)
                _adrPhn1Area = Value
            End Set
        End Property

        Public Property adrPhn2() As String
            Get
                Return _adrPhn2
            End Get
            Set(ByVal Value As String)
                _adrPhn2 = Value
            End Set
        End Property

        Public Property adrPhn2Area() As String
            Get
                Return _adrPhn2Area
            End Get
            Set(ByVal Value As String)
                _adrPhn2Area = Value
            End Set
        End Property

        Public Property adrRemark() As String
            Get
                Return _adrRemark
            End Get
            Set(ByVal Value As String)
                _adrRemark = Value
            End Set
        End Property

        Public Property adrRoles() As String
            Get
                Return _adrRoles
            End Get
            Set(ByVal Value As String)
                _adrRoles = Value
            End Set
        End Property

        Public Property adrSeq() As String
            Get
                Return _adrSeq
            End Get
            Set(ByVal Value As String)
                _adrSeq = Value
            End Set
        End Property

        Public Property adrSex() As String
            Get
                Return _adrSex
            End Get
            Set(ByVal Value As String)
                _adrSex = Value
            End Set
        End Property

        Public Property adrSmsno() As String
            Get
                Return _adrSmsno
            End Get
            Set(ByVal Value As String)
                _adrSmsno = Value
            End Set
        End Property

        Public Property adrSocialSeno() As String
            Get
                Return _adrSocialSeno
            End Get
            Set(ByVal Value As String)
                _adrSocialSeno = Value
            End Set
        End Property

        Public Property adrState() As String
            Get
                Return _adrState
            End Get
            Set(ByVal Value As String)
                _adrState = Value
            End Set
        End Property

        Public Property adrStreet() As String
            Get
                Return _adrStreet
            End Get
            Set(ByVal Value As String)
                _adrStreet = Value
            End Set
        End Property

        Public Property adrStreetNo() As String
            Get
                Return _adrStreetNo
            End Get
            Set(ByVal Value As String)
                _adrStreetNo = Value
            End Set
        End Property

        Public Property adrTaxno() As String
            Get
                Return _adrTaxno
            End Get
            Set(ByVal Value As String)
                _adrTaxno = Value
            End Set
        End Property

        Public Property adrUrgent() As String
            Get
                Return _adrUrgent
            End Get
            Set(ByVal Value As String)
                _adrUrgent = Value
            End Set
        End Property

        Public Property adrValidDate() As String
            Get
                Return _adrValidDate
            End Get
            Set(ByVal Value As String)
                _adrValidDate = Value
            End Set
        End Property

        Public Property adrWriteOnReq() As String
            Get
                Return _adrWriteOnReq
            End Get
            Set(ByVal Value As String)
                _adrWriteOnReq = Value
            End Set
        End Property

        Public Property adrYears() As String
            Get
                Return _adrYears
            End Get
            Set(ByVal Value As String)
                _adrYears = Value
            End Set
        End Property

        Public Property adrzIp() As String
            Get
                Return _adrzIp
            End Get
            Set(ByVal Value As String)
                _adrzIp = Value
            End Set
        End Property

        Public Property countryId() As String
            Get
                Return _countryId
            End Get
            Set(ByVal Value As String)
                _countryId = Value
            End Set
        End Property

        Public Property countryIdPub() As String
            Get
                Return _countryIdPub
            End Get
            Set(ByVal Value As String)
                _countryIdPub = Value
            End Set
        End Property

        Public Property csId() As String
            Get
                Return _csId
            End Get
            Set(ByVal Value As String)
                _csId = Value
            End Set
        End Property

        Public Property csIdPub() As String
            Get
                Return _csIdPub
            End Get
            Set(ByVal Value As String)
                _csIdPub = Value
            End Set
        End Property

        Public Property idTypeCode() As String
            Get
                Return _idTypeCode
            End Get
            Set(ByVal Value As String)
                _idTypeCode = Value
            End Set
        End Property

        Public Property lngCode() As String
            Get
                Return _lngCode
            End Get
            Set(ByVal Value As String)
                _lngCode = Value
            End Set
        End Property

        Public Property lngCodePub() As String
            Get
                Return _lngCodePub
            End Get
            Set(ByVal Value As String)
                _lngCodePub = Value
            End Set
        End Property

        Public Property masCode() As String
            Get
                Return _masCode
            End Get
            Set(ByVal Value As String)
                _masCode = Value
            End Set
        End Property

        Public Property masCodePub() As String
            Get
                Return _masCodePub
            End Get
            Set(ByVal Value As String)
                _masCodePub = Value
            End Set
        End Property

        Public Property ttlId() As String
            Get
                Return _ttlId
            End Get
            Set(ByVal Value As String)
                _ttlId = Value
            End Set
        End Property

        Public Property ttlidpub() As String
            Get
                Return _ttlidpub
            End Get
            Set(ByVal Value As String)
                _ttlidpub = Value
            End Set
        End Property

        Public Property billingAccountId() As String
            Get
                Return _billingAccountId
            End Get
            Set(ByVal Value As String)
                _billingAccountId = Value
            End Set
        End Property

        Public Property listaOpcional() As ActualizarDatosFacturacion.ListaOpcional
            Get
                Return _listaOpcional
            End Get
            Set(ByVal Value As ActualizarDatosFacturacion.ListaOpcional)
                _listaOpcional = Value
            End Set
        End Property

    End Class

End Namespace
