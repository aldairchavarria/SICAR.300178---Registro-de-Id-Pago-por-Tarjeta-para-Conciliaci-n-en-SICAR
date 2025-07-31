
Public Class DAABOdbcDataReader
    Inherits DAABDataReader

    Dim m_oReturnedDataReader As IDataReader

    Public Overrides Property ReturnDataReader() As System.Data.IDataReader
        Get
            Return m_oReturnedDataReader
        End Get
        Set(ByVal Value As System.Data.IDataReader)
            m_oReturnedDataReader = Value
        End Set
    End Property
End Class
