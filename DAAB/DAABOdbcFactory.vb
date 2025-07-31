

Public Class DAABOdbcFactory
    Inherits DAABAbstracFactory


    Private m_conSql As String


    Public Overrides Function ExecuteDataset(ByRef Request As DAABRequest) As System.Data.DataSet

    End Function

    Public Overrides Function ExecuteNonQuery(ByRef Request As DAABRequest) As Integer

    End Function

    Public Overrides Function ExecuteReader(ByRef Respuest As DAABRequest) As DAABDataReader

    End Function

    Public Overrides Function ExecuteScalar(ByRef Resquest As DAABRequest) As Object

    End Function



    Public Overrides Sub FillDataset(ByRef Request As DAABRequest)

    End Sub

    Public Overrides Sub UpdateDataSet(ByRef RequestInsert As DAABRequest, _
                                        ByRef RequestUpdate As DAABRequest, _
                                        ByRef RequestDelete As DAABRequest)

    End Sub

    Public Overrides Sub CommitTransaction()

    End Sub

    Public Overrides Sub RollBackTransaction()

    End Sub


    Public Overrides Sub Dispose()

    End Sub
End Class
