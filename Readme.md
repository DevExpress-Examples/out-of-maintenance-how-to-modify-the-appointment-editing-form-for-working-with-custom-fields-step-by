<!-- default file list -->
*Files to look at*:

* [CustomEvents.cs](./CS/WebSite/App_Code/CustomEvents.cs) (VB: [CustomEvents.vb](./VB/WebSite/App_Code/CustomEvents.vb))
* [Helpers.cs](./CS/WebSite/App_Code/Helpers.cs) (VB: [Helpers.vb](./VB/WebSite/App_Code/Helpers.vb))
* [UserAppointmentFormClass.cs](./CS/WebSite/App_Code/UserAppointmentFormClass.cs) (VB: [UserAppointmentFormClass.vb](./VB/WebSite/App_Code/UserAppointmentFormClass.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
* [UserAppointmentForm.ascx](./CS/WebSite/MyForms/UserAppointmentForm.ascx) (VB: [UserAppointmentForm.ascx](./VB/WebSite/MyForms/UserAppointmentForm.ascx))
* [UserAppointmentForm.ascx.cs](./CS/WebSite/MyForms/UserAppointmentForm.ascx.cs) (VB: [UserAppointmentForm.ascx.vb](./VB/WebSite/MyForms/UserAppointmentForm.ascx.vb))
<!-- default file list end -->
# How to modify the appointment editing form for working with custom fields - step by step guide


<p>This example illustrates a technique used to customize an appointment editing form to show custom fields.</p>
<p><strong>See Also:</strong><br /><a href="https://documentation.devexpress.com/#AspNet/CustomDocument5464">How to: Modify the Appointment Editing Form for Working with Custom Fields</a></p>


<h3>Description</h3>

<p>A new helper class ObjectDataSourceRowInsertionProvider is added to the example, started with the v2010 vol.1 version. It implements data insertion logic and shows the correct approach for assigning the unique identifier to an appointment.</p>

<br/>


