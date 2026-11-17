<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manit.aspx.cs" Inherits="naif14207App.n14207.Manit" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div style="max-width: 900px; margin: 0 auto; padding: 20px; font-family: Tahoma, Arial, sans-serif; direction: rtl;">
        
        <div style="text-align: center; margin-bottom: 15px;">
            <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
        </div>

        <fieldset style="border: 1px solid #ccc; padding: 20px; border-radius: 8px; background-color: #fcfcfc; margin-bottom: 20px;">
            <legend style="font-weight: bold; color: #005b96; padding: 0 10px;">بيانات طلب الصيانة</legend>
            
            <table style="width: 100%; border-spacing: 10px;">

                <tr style="background-color: #eef5fc;">
                    <td style="width: 20%; font-weight: bold;"><asp:Label ID="lblMaintenanceId" runat="server" Text="رقم الطلب (MaintenanceID):"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtMaintenanceId" runat="server" Width="150px" TextMode="Number" Placeholder="رقم الطلب"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="بحث 🔍" OnClick="btnSearch_Click" BackColor="#6c757d" ForeColor="White" BorderStyle="None" Padding="4px 10px" />
                        <span style="font-size: 11px; color: #666; margin-right: 10px;">(مطلوب للتحديث، الحذف، أو البحث)</span>
                    </td>
                </tr>
                <tr>
                    <td>اسم الجهاز:</td>
                    <td><asp:TextBox ID="txtDeviceName" runat="server" Width="90%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>حالة الجهاز:</td>
                    <td><asp:TextBox ID="txtDeviceStatus" runat="server" Width="90%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>الرقم الوظيفي:</td>
                    <td><asp:TextBox ID="txtEmployeeId" runat="server" Width="90%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>اسم الموظف:</td>
                    <td><asp:TextBox ID="txtEmployeeName" runat="server" Width="90%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>رقم الجوال:</td>
                    <td><asp:TextBox ID="txtCell" runat="server" Width="90%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>البريد الإلكتروني:</td>
                    <td><asp:TextBox ID="txtEmail" runat="server" Width="90%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>المدينة:</td>
                    <td><asp:DropDownList ID="ddlCity" runat="server" Width="92%"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>القسم الطبي:</td>
                    <td><asp:DropDownList ID="ddlDepartment" runat="server" Width="92%"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>المستشفى:</td>
                    <td><asp:DropDownList ID="ddlHospital" runat="server" Width="92%"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>الوصف / الملاحظات:</td>
                    <td><asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox></td>
                </tr>
            </table>
        </fieldset>

<div style="text-align: center; margin-bottom: 25px;">
<asp:Button ID="btninsert" runat="server" Text="إضافة جديد" OnClick="btnInsert_Click" BackColor="#28a745" ForeColor="White" BorderStyle="None" Height="35px" Width="110px" Font-Bold="True" />
    <asp:Button ID="btnUpdate" runat="server" Text="تحديث البيانات" OnClick="btnUpdate_Click" BackColor="#007bff" ForeColor="White" BorderStyle="None" Height="35px" Width="110px" Font-Bold="True" />
    <asp:Button ID="btnDelete" runat="server" Text="حذف الطلب" OnClick="btnDelete_Click" BackColor="#dc3545" ForeColor="White" BorderStyle="None" Height="35px" Width="110px" Font-Bold="True" OnClientClick="return confirm('هل أنت تأكد من رغبتك في حذف هذا الطلب؟');" />
    <asp:Button ID="btnClear" runat="server" Text="تفريغ الحقول" OnClick="btnClear_Click" BackColor="#17a2b8" ForeColor="White" BorderStyle="None" Height="35px" Width="110px" Font-Bold="True" />
</div>
        <div style="text-align: left; margin-bottom: 10px;">
            <asp:Button ID="btnExportExcel" runat="server" Text="تصدير Excel 📊" OnClick="btnExportExcel_Click" BackColor="#217346" ForeColor="White" BorderStyle="None" Padding="8px" Width="120px" />
            <asp:Button ID="btnExportWord" runat="server" Text="تصدير Word 📝" OnClick="btnExportWord_Click" BackColor="#2b579a" ForeColor="White" BorderStyle="None" Padding="8px" Width="120px" />
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" 
            BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
            CellPadding="8" ForeColor="Black" GridLines="Horizontal" DataKeyNames="MaintenanceId" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="تحديد 📌" HeaderText="اختيار" />
                <asp:BoundField DataField="MaintenanceId" HeaderText="رقم الطلب" />
                <asp:BoundField DataField="DeviceName" HeaderText="اسم الجهاز" />
                <asp:BoundField DataField="DeviceStatus" HeaderText="الحالة" />
                <asp:BoundField DataField="EmployeeName" HeaderText="اسم الموظف" />
                <asp:BoundField DataField="Cell" HeaderText="الجوال" />
                <asp:BoundField DataField="RequestDate" HeaderText="تاريخ الطلب" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            </Columns>
            <HeaderStyle BackColor="#005b96" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="#f8fafc" />
            <SelectedRowStyle BackColor="#e2e3e5" Font-Bold="True" />
        </asp:GridView>

    </div>

</asp:Content>
