using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using System.Web.UI;
using System.Web.UI.WebControls;

public static class DataHelper
{
    public static void SetupMappings(ASPxScheduler control)
    {
        ASPxSchedulerStorage storage = control.Storage;
        ASPxAppointmentMappingInfo mappings = storage.Appointments.Mappings;
        storage.BeginUpdate();
        try {
            mappings.AppointmentId = "Id";
            mappings.Start = "StartTime";
            mappings.End = "EndTime";
            mappings.Subject = "Subject";
            mappings.AllDay = "AllDay";
            mappings.Description = "Description";
            mappings.Label = "Label";
            mappings.Location = "Location";
            mappings.RecurrenceInfo = "RecurrenceInfo";
            mappings.ReminderInfo = "ReminderInfo";
            mappings.ResourceId = "OwnerId";
            mappings.Status = "Status";
            mappings.Type = "EventType";
            #region #custommappings
            storage.Appointments.CustomFieldMappings.Add
                (new ASPxAppointmentCustomFieldMapping("Field1", "Amount"));
            storage.Appointments.CustomFieldMappings.Add
                (new ASPxAppointmentCustomFieldMapping("Field2", "Memo"));
            #endregion #custommappings
        }
        finally {
            storage.EndUpdate();
        }
    }

    public static void ProvideRowInsertion(ASPxScheduler control, DataSourceControl dataSource)
    {
        ObjectDataSource objectDataSource = dataSource as ObjectDataSource;
        if (objectDataSource != null) {
            ObjectDataSourceRowInsertionProvider provider = new ObjectDataSourceRowInsertionProvider();
            provider.ProvideRowInsertion(control, objectDataSource);
        }
    }
}
public class ObjectDataSourceRowInsertionProvider
{
    object lastInsertedId;

    public void ProvideRowInsertion(ASPxScheduler control, ObjectDataSource dataSource)
    {
        control.AppointmentsInserted += new PersistentObjectsEventHandler(control_AppointmentsInserted);
        dataSource.Inserted += new ObjectDataSourceStatusEventHandler(dataSource_Inserted);
    }
    void dataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.lastInsertedId = e.ReturnValue;
    }
    void control_AppointmentsInserted(object sender, PersistentObjectsEventArgs e)
    {
        ASPxSchedulerStorage storage = (ASPxSchedulerStorage)sender;
        System.Diagnostics.Debug.Assert(e.Objects.Count == 1);
        Appointment apt = (Appointment)e.Objects[0];
        storage.SetAppointmentId(apt, this.lastInsertedId);
    }
}

public class ResourceFiller
{
    public static string[] Users = new string[] { "Sarah Brighton", "Ryan Fischer", "Andrew Miller" };
    public static string[] Usernames = new string[] { "sbrighton", "rfischer", "amiller" };

    public static void FillResources(ASPxSchedulerStorage storage, int count)
    {
        ResourceCollection resources = storage.Resources.Items;
        storage.BeginUpdate();
        try {
            int cnt = System.Math.Min(count, Users.Length);
            for (int i = 1; i <= cnt; i++) {
                resources.Add(new Resource(Usernames[i - 1], Users[i - 1]));
            }
        }
        finally {
            storage.EndUpdate();
        }
    }
}
