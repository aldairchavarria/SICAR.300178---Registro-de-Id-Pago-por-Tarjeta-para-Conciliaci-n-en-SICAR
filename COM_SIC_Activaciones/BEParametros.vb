Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class BEParametros
    Public intCodigo As Int64
    Public strDescripcion As String
    Public strValor As String
    Public strValor1 As String
    Public strEstado As String
    Public strGrupo As String
    Public strGrupo_des As String
    Public strEstado_des As String
    Public strFlag_sistema As String
    Public BEParametros() 'PROY-140126
End Class
