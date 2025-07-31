Imports System.Reflection
Imports System.Text
Public Class SERVICIO_WebBase

    Public Function LoadConfiguration(ByVal keyGroup As String) As clsTipificationConfiguration

        Dim propName As String
        Dim propValue As Object
        Dim keyList As New clsTipificationConfiguration

        Try

            Dim tipificationList As ArrayList = (New COM_SIC_Activaciones.clsConsultaPvu).ConsultaParametros(keyGroup)

            For Each prop As PropertyInfo In keyList.GetType.GetProperties()

                propName = prop.Name

                For Each oItem As COM_SIC_Activaciones.BEParametros In tipificationList

                    If prop.Name = oItem.strDescripcion Then
                        prop.SetValue(keyList, oItem.strValor, Nothing)
                        Exit For
                    End If

                Next

            Next

        Catch ex As Exception
            Throw ex
        End Try

        Return keyList

    End Function

End Class
