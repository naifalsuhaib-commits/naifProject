<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="sendEmail.aspx.cs" Inherits="naif14207App.n14207.sendEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .email-container {
        max-width: 600px;
        margin: 40px auto;
        background: #ffffff;
        padding: 30px;
        border-radius: 10px;
        box-shadow: 0 8px 20px rgba(0,0,0,0.1);
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        direction: rtl;
        text-align: right;
        border: 1px solid #eaeaea;
    }
    .email-header {
        background-color: #2c3e50;
        color: #ffffff;
        padding: 20px;
        border-radius: 10px 10px 0 0;
        margin: -30px -30px 25px -30px;
        text-align: center;
        font-size: 1.4em;
        font-weight: bold;
    }
    .form-group-custom {
        margin-bottom: 20px;
    }
    .form-label-custom {
        display: block;
        font-weight: bold;
        margin-bottom: 8px;
        color: #333333;
    }
    .form-control-custom {
        width: 100%;
        padding: 12px;
        border: 1px solid #cccccc;
        border-radius: 6px;
        font-size: 14px;
        box-sizing: border-box;
        transition: border-color 0.3s;
    }
    .form-control-custom:focus {
        border-color: #3498db;
        outline: none;
        box-shadow: 0 0 5px rgba(52,152,219,0.3);
    }
    .btn-custom {
        background-color: #3498db;
        color: white;
        border: none;
        padding: 14px 20px;
        font-size: 16px;
        border-radius: 6px;
        cursor: pointer;
        width: 100%;
        font-weight: bold;
        transition: background-color 0.3s;
    }
    .btn-custom:hover {
        background-color: #2980b9;
    }
</style>

<div class="email-container">
    <div class="email-header">
        إرسال بريد إلكتروني
    </div>
    
    <!-- رسالة التنبيه -->
    <div style="text-align: center; margin-bottom: 15px;">
        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="#e74c3c" Font-Bold="true"></asp:Label>
    </div>

    <!-- البريد الإلكتروني للمرسل -->
    <div class="form-group-custom">
        <label class="form-label-custom">من (البريد) :</label>
        <asp:TextBox ID="txtSenderEmail" runat="server" CssClass="form-control-custom" placeholder="example@domain.com"></asp:TextBox>
    </div>

    <!-- عنوان الموضوع -->
    <div class="form-group-custom">
        <label class="form-label-custom">الموضوع :</label>
        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control-custom" placeholder="عنوان الرسالة..." OnLoad="lblOutputClear_txtSubject"></asp:TextBox>
    </div>

    <!-- المرفقات -->
    <div class="form-group-custom">
        <label class="form-label-custom">المرفقات :</label>
        <asp:FileUpload ID="fuAttachment" runat="server" Enabled="true" AllowMultiple="true" CssClass="form-control-custom" style="padding: 9px;" />
    </div>

    <!-- نص الرسالة -->
    <div class="form-group-custom">
        <label class="form-label-custom">نص الرسالة :</label>
        <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Height="130px" CssClass="form-control-custom" placeholder="اكتب محتوى الرسالة هنا..."></asp:TextBox>
    </div>

    <!-- الأزرار -->
    <div class="form-group-custom" style="margin-top: 30px;">
        <asp:Button ID="btnSendMailViaMailMgr" runat="server" OnClick="btnSendMailViaMailMgr_Click" Text="إرسـال الرسالة" CssClass="btn-custom" />
        
        <asp:Button ID="btnSendViaCode" runat="server" Text="إرسال عبر الكود" OnClick="btnSendViaCode_Click" Visible="false" />
    </div>
</div>

</asp:Content>
