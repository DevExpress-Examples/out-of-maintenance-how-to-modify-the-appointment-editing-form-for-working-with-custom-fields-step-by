Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public NotInheritable Class DataHelper

    Private Sub New()
    End Sub

    Public Shared Sub SetupMappings(ByVal control As ASPxScheduler)
        Dim storage As ASPxSchedulerStorage = control.Storage
        Dim mappings As ASPxAppointmentMappingInfo = storage.Appointments.Mappings
        storage.BeginUpdate()
        Try
            mappings.AppointmentId = "Id"
            mappings.Start = "StartTime"
            mappings.End = "EndTime"
            mappings.Subject = "Subject"
            mappings.AllDay = "AllDay"
            mappings.Description = "Description"
            mappings.Label = "Label"
            mappings.Location = "Location"
            mappings.RecurrenceInfo = "RecurrenceInfo"
            mappings.ReminderInfo = "ReminderInfo"
            mappings.ResourceId = "OwnerId"
            mappings.Status = "Status"
            mappings.Type = "EventType"
'            #Region "#custommappings"
            storage.Appointments.CustomFieldMappings.Add(New ASPxAppointmentCustomFieldMapping("Field1", "Amount"))
            storage.Appointments.CustomFieldMappings.Add(New ASPxAppointmentCustomFieldMapping("Field2", "Memo"))
'            #End Region ' #custommappings
        Finally
            storage.EndUpdate()
        End Try
    End Sub

    Public Shared Sub ProvideRowInsertion(ByVal control As ASPxScheduler, ByVal dataSource As DataSourceControl)
        Dim objectDataSource As ObjectDataSource = TryCast(dataSource, ObjectDataSource)
        If objectDataSource IsNot Nothing Then
            Dim provider As New ObjectDataSourceRowInsertionProvider()
            provider.ProvideRowInsertion(control, objectDataSource)
        End If
    End Sub
End Class
Public Class ObjectDataSourceRowInsertionProvider
    Private lastInsertedId As Object

    Public Sub ProvideRowInsertion(ByVal control As ASPxScheduler, ByVal dataSource As ObjectDataSource)
        AddHandler control.AppointmentsInserted, AddressOf control_AppointmentsInserted
        AddHandler dataSource.Inserted, AddressOf dataSource_Inserted
    End Sub
    Private Sub dataSource_Inserted(ByVal sender As Object, ByVal e As ObjectDataSourceStatusEventArgs)
        Me.lastInsertedId = e.ReturnValue
    End Sub
    Private Sub control_AppointmentsInserted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
        Dim storage As ASPxSchedulerStorage = DirectCast(sender, ASPxSchedulerStorage)
        System.Diagnostics.Debug.Assert(e.Objects.Count = 1)
        Dim apt As Appointment = CType(e.Objects(0), Appointment)
        storage.SetAppointmentId(apt, Me.lastInsertedId)
    End Sub
End Class

Public Class ResourceFiller
    Public Shared Users() As String = { "Sarah Brighton", "Ryan Fischer", "Andrew Miller" }
    Public Shared Usernames() As String = { "sbrighton", "rfischer", "amiller" }

    Public Shared Sub FillResources(ByVal storage As ASPxSchedulerStorage, ByVal count As Integer)
        Dim resources As ResourceCollection = storage.Resources.Items
        storage.BeginUpdate()
        Try
            Dim cnt As Integer = System.Math.Min(count, Users.Length)
            For i As Integer = 1 To cnt
                resources.Add(storage.CreateResource(Usernames(i - 1), Users(i - 1)))
            Next i
        Finally
            storage.EndUpdate()
        End Try
    End Sub
End Class
