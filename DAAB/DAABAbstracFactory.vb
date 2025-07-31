' Static Model

Imports System.Data
Imports System.Xml

Public MustInherit Class DAABAbstracFactory
    Implements IDisposable

    Public MustOverride Function ExecuteNonQuery(ByRef Request As DAABRequest) As Integer
    Public MustOverride Function ExecuteDataset(ByRef Request As DAABRequest) As DataSet
    Public MustOverride Sub FillDataset(ByRef Request As DAABRequest)
    Public MustOverride Sub UpdateDataSet(ByRef RequestInsert As DAABRequest, _
                                        ByRef RequestUpdate As DAABRequest, _
                                        ByRef RequestDelete As DAABRequest)
    Public MustOverride Function ExecuteReader(ByRef Repuest As DAABRequest) As DAABDataReader
    Public MustOverride Function ExecuteScalar(ByRef Request As DAABRequest) As Object
    Public MustOverride Sub CommitTransaction()
    Public MustOverride Sub RollBackTransaction()
    Public MustOverride Sub Dispose() Implements System.IDisposable.Dispose



End Class ' END CLASS DEFINITION DAABAbstracFactory



