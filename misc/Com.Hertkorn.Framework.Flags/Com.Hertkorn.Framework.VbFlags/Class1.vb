Imports System.Runtime.CompilerServices

Public Module FlagHelper
    ' Methods
    <Extension()> _
    Public Function IsAnySet(ByVal toBeTested As [Enum]) As Boolean
        Return (Convert.ToUInt32(toBeTested) <> Convert.ToUInt32(0))
    End Function

    Public Function IsBitSet(ByVal testEnum As [Enum], ByVal position As Integer) As Boolean
        Throw New NotImplementedException
    End Function

    Public Function IsDefined(Of T)(ByVal value As Object) As Boolean
        If [Enum].IsDefined(GetType(T), value) Then
            Return True
        End If
        If Convert.ChangeType(0, [Enum].GetUnderlyingType(GetType(T))).Equals(value) Then
            Return False
        End If
        Dim t As Array = [Enum].GetValues(GetType(T))
        Dim array As UInt32 = 0
        Dim item As T
        For Each item In T
            array = (array Or Convert.ToUInt32(item))
        Next
        Return ((Convert.ToUInt32(value) Or array) = array)
    End Function

    <Extension()> _
    Public Function IsNoneSet(ByVal toBeTested As [Enum]) As Boolean
        Return (Convert.ToUInt32(toBeTested) = Convert.ToUInt32(0))
    End Function

    <Extension()> _
    Public Function IsSet(ByVal toBeTested As [Enum], ByVal flag As [Enum]) As Boolean
        Return ((Convert.ToUInt32(toBeTested) And Convert.ToUInt32(flag)) = Convert.ToUInt32(flag))
    End Function

    '<Extension()> _
    'Public Function [Set](Of T)(ByVal flags As [Enum], ByVal flagToSet As T) As T
    '    Return DirectCast(flags.Or(Of T)(flagToSet), T)
    'End Function

    <Extension()> _
    Public Function Toggle(Of T)(ByVal flags As [Enum], ByVal flagToToggle As T) As T




        'Return DirectCast(DirectCast((Convert.ToUInt32(flags) Xor Convert.ToUInt32(flagToToggle)), Object), T)
    End Function

    <Extension()> _
    Public Function Unset(Of T)(ByVal flags As [Enum], ByVal flagToUnset As T) As T
        Return DirectCast(DirectCast((Convert.ToUInt32(flags) And Not Convert.ToUInt32(flagToUnset)), Object), T)
    End Function

End Module

