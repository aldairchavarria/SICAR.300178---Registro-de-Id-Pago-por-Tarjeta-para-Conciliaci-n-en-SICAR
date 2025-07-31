Option Strict Off
Public NotInheritable Class Funciones

    Public Shared Function CheckStr(ByVal value As Object) As String

        Dim salida As String = ""
        If IsNothing(value) Or IsDBNull(value) Then
            salida = ""
        Else
            salida = value.ToString()
        End If

        Return salida.Trim()
    End Function

    Public Shared Function CheckDbl(ByVal value As Object) As Double
        Dim salida As Double = 0
        If IsNothing(value) Or IsDBNull(value) Then
            salida = 0
        Else
            If Convert.ToString(value) = "" Then
                salida = 0
            Else
                salida = Convert.ToDouble(value)
            End If
        End If

        Return salida
    End Function

    Public Shared Function CheckDecimal(ByVal value As Object) As Decimal
        Dim salida As Decimal = 0
        If IsNothing(value) Or IsDBNull(value) Then
            salida = 0
        Else
            If Convert.ToString(value) = "" Then
                salida = 0
            Else
                salida = Convert.ToDecimal(value)
            End If
        End If

        Return salida
    End Function

    Public Shared Function CheckDate(ByVal value As Object) As DateTime

        Dim salida As DateTime

        If IsNothing(value) Or IsDBNull(value) Then
            salida = DateTime.Now
        Else
            If CheckStr(value) = "" Then
                salida = DateTime.Now
            Else
                salida = Convert.ToDateTime(value)
            End If
        End If
        Return salida
    End Function

    Public Shared Function CheckInt64(ByVal value As Object) As Int64
        Dim salida As Int64 = 0
        If IsNothing(value) Or IsDBNull(value) Then
            salida = 0
        Else
            If Convert.ToString(value) = "" Then
                salida = 0
            Else
                salida = Convert.ToInt64(value)
            End If
        End If

        Return salida
    End Function

    Public Shared Function CheckInt(ByVal value As Object) As Int32
        Dim salida As Int64 = 0
        If IsNothing(value) Or IsDBNull(value) Then
            salida = 0
        Else
            If Convert.ToString(value) = "" Then
                salida = 0
            Else
                salida = Convert.ToInt32(value)
            End If
        End If
        Return salida
    End Function

    'Inicio INI-936 - JCI - Creada funcion para retornar la fecha en el formato indicado o DBNull
    Public Shared Function CheckDateNull(ByVal value As Object, ByVal format As String) As Object
        Dim salida As Object

        If IsNothing(value) Or CheckStr(value) = "" Then
            salida = DBNull.Value
        Else
            If format Is Nothing Or format = "" Then
                salida = Convert.ToDateTime(value)
            Else
                salida = DateTime.ParseExact(value, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None)
            End If
        End If

        Return salida
    End Function
    'Fin INI-936 - JCI
End Class
