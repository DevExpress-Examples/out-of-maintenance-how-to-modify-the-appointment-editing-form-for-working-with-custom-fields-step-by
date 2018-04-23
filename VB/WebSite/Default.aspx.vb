Imports System
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Private ReadOnly Property Storage() As ASPxSchedulerStorage
        Get
            Return Me.ASPxScheduler1.Storage
        End Get
    End Property
    Public Shared RandomInstance As New Random()



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        DataHelper.SetupMappings(Me.ASPxScheduler1)
        ResourceFiller.FillResources(Me.ASPxScheduler1.Storage, 3)
        DataHelper.ProvideRowInsertion(ASPxScheduler1, appointmentDataSource)

        ASPxScheduler1.AppointmentDataSource = appointmentDataSource
        ASPxScheduler1.DataBind()
    End Sub

    Private Function GetCustomEvents() As CustomEventList

        Dim events_Renamed As CustomEventList = TryCast(Session("ListBoundModeObjects"), CustomEventList)
        If events_Renamed Is Nothing Then
            events_Renamed = GenerateCustomEventList()
            Session("ListBoundModeObjects") = events_Renamed
        End If
        Return events_Renamed
    End Function

    #Region "Random events generation"
    Private Function GenerateCustomEventList() As CustomEventList
        Dim eventList As New CustomEventList()
        Dim count As Integer = Storage.Resources.Count
        For i As Integer = 0 To count - 1
            Dim resource As Resource = Storage.Resources(i)
            Dim subjPrefix As String = resource.Caption & "'s "

            eventList.Add(CreateEvent(resource.Id, subjPrefix & "meeting", 2, 5))
            eventList.Add(CreateEvent(resource.Id, subjPrefix & "travel", 3, 6))
            eventList.Add(CreateEvent(resource.Id, subjPrefix & "phone call", 0, 10))
        Next i
        Return eventList
    End Function
    Private Function CreateEvent(ByVal resourceId As Object, ByVal subject As String, ByVal status As Integer, ByVal label As Integer) As CustomEvent
        Dim customEvent As New CustomEvent()
        customEvent.Subject = subject
        customEvent.OwnerId = resourceId
        Dim rnd As Random = RandomInstance
        Dim rangeInHours As Integer = 48
        customEvent.StartTime = Date.Today + TimeSpan.FromHours(rnd.Next(0, rangeInHours))
        customEvent.EndTime = customEvent.StartTime.Add(TimeSpan.FromHours(rnd.Next(0, rangeInHours \ 8)))
        customEvent.Status = status
        customEvent.Label = label
        customEvent.Id = "ev" & customEvent.GetHashCode()
        Return customEvent
    End Function
    #End Region
        #Region "#appointmentformshowing"
    Protected Sub ASPxScheduler1_AppointmentFormShowing(ByVal sender As Object, ByVal e As AppointmentFormEventArgs)
        e.Container = New UserAppointmentFormTemplateContainer(DirectCast(sender, ASPxScheduler))
    End Sub
    #End Region ' #appointmentformshowing

    Protected Sub appointmentsDataSource_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
        e.ObjectInstance = New CustomEventDataSource(GetCustomEvents())
    End Sub
    #Region "#beforeexecutecallbackcommand"
    Protected Sub ASPxScheduler1_BeforeExecuteCallbackCommand(ByVal sender As Object, ByVal e As SchedulerCallbackCommandEventArgs)
        If e.CommandId = SchedulerCallbackCommandId.AppointmentSave Then
            e.Command = New UserAppointmentSaveCallbackCommand(DirectCast(sender, ASPxScheduler))
        End If
    End Sub
    #End Region ' #beforeexecutecallbackcommand
End Class