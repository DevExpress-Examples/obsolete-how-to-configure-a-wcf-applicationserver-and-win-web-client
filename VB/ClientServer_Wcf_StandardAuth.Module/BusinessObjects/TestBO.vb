Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base

Namespace ClientServer_Wcf_StandardAuth.Module.BusinessObjects
    <DefaultClassOptions> _
    Public Class TestBO
        Inherits XPObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Property Name() As String
            Get
                Return GetPropertyValue(Of String)("Name")
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", value)
            End Set
        End Property
    End Class

    <DefaultClassOptions> _
    Public Class FullAccessBO
        Inherits XPObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Property Name() As String
            Get
                Return GetPropertyValue(Of String)("Name")
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", value)
            End Set
        End Property
    End Class

    <DefaultClassOptions> _
    Public Class InaccessibleByUserBO
        Inherits XPObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Property Name() As String
            Get
                Return GetPropertyValue(Of String)("Name")
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", value)
            End Set
        End Property
    End Class
End Namespace
