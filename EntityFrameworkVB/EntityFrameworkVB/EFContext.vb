Imports System.Data.Entity
Imports System.Configuration
Public Class EFContext
    Inherits DbContext

    Public Sub New()
        MyBase.New(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        MyBase.OnModelCreating(modelBuilder)
        Database.SetInitializer(Of EFContext)(Nothing)
    End Sub

    Private _systemUsers As DbSet(Of Users)
    Public Property SystemUsers() As DbSet(Of Users)
        Get
            Return _systemUsers
        End Get
        Set(value As DbSet(Of Users))
            _systemUsers = value
        End Set
    End Property

End Class

Public Class Users
    Property UserId As Integer
    Property Username As Integer
    Property FirstName As String
    Property LastName As String

End Class
