<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Control Language="vb" Inherits="DNNStuff.Welcome.Settings" CodeBehind="Settings.ascx.vb" AutoEventWireup="false" Explicit="True" %>
<div class="dnnForm dnnClear">
    <div id="editsettings" class="tabslayout">
        <ul id="editsettings-nav" class="tabslayout">
            <li><a href="#tab1"><span>
                <%=Localization.GetString("TabCaption_Tab1", LocalResourceFile)%></span></a></li>
            <li><a href="#tab2"><span>
                <%=Localization.GetString("TabCaption_Tab2", LocalResourceFile)%></span></a></li>
            <li><a href="#help"><span>
                <%=Localization.GetString("TabCaption_Help", LocalResourceFile)%></span></a></li>
        </ul>
        <div class="tabs-container">
            <div class="tab" id="tab1">
                <div class="dnnFormItem">
                    <dnn:Label ID="plText" runat="server" ControlName="txtText" Suffix=":" />
                    <dnn:TextEditor ID="teText" runat="server" Width="100%" Height="300"></dnn:TextEditor>
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="plViews" runat="server" ControlName="txtViews" Suffix=":" />
                    <asp:TextBox ID="txtViews" runat="server" Columns="10" />
                    <asp:RequiredFieldValidator ID="vldViewsRequired" runat="server" Display="dynamic" ErrorMessage="<br />You must enter a number of 0 or greater" ControlToValidate="txtViews" />
                    <asp:CompareValidator ID="vldViewsCompare" runat="server" Display="dynamic" ErrorMessage="<br />You must enter a number of 0 or greater" ControlToValidate="txtViews" ValueToCompare="0" Operator="GreaterThanEqual" Type="Integer" />
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="plViewsSessionBased" runat="server" ControlName="chkViewsSessionBased" Suffix=":" />
                    <asp:CheckBox ID="chkViewsSessionBased" runat="server" />
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="plDisplayHide" runat="server" ControlName="chkDisplayHide" Suffix=":" />
                    <asp:CheckBox ID="chkDisplayHide" runat="server" />
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="plHideText" runat="server" ControlName="txtHideText" Suffix=":" />
                    <asp:TextBox ID="txtHideText" runat="server" Columns="60" TextMode="MultiLine" Rows="2" />
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="plDisplayKeepHidden" runat="server" ControlName="ddlDisplayKeepHidden" Suffix=":" />
                    <asp:DropDownList ID="ddlDisplayKeepHidden" runat="server">
                        <asp:ListItem Value="None" />
                        <asp:ListItem Value="Checkbox" />
                        <asp:ListItem Value="Link" />
                    </asp:DropDownList>
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="plKeepHiddenText" runat="server" ControlName="txtKeepHiddenText" Suffix=":" />
                    <asp:TextBox ID="txtKeepHiddenText" runat="server" Columns="60" TextMode="MultiLine" Rows="2" />
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="plVersion" runat="server" ControlName="txtVersion" Suffix=":" />
                    <asp:TextBox ID="txtVersion" runat="server" Columns="10" />
                    <asp:RequiredFieldValidator ID="vldVersionRequired" runat="server" Display="dynamic" ErrorMessage="<br />You must enter a number of 0 or greater" ControlToValidate="txtVersion" />
                    <asp:CompareValidator ID="vldVersionCompare" runat="server" Display="dynamic" ErrorMessage="<br />You must enter a number of 1 or greater" ControlToValidate="txtVersion" ValueToCompare="1" Operator="GreaterThanEqual" Type="Integer" />
                </div>
            </div>
            <div class="tab" id="tab2">
                <div class="dnnFormItem">
                    <dnn:Label ID="plWhenHiddenText" runat="server" ControlName="teWhenHiddenText" Suffix=":" />
                    <dnn:TextEditor ID="teWhenHiddenText" runat="server" Width="100%" Height="200"></dnn:TextEditor>
                </div>
                <div class="dnnFormItem">
                    <dnn:Label ID="plUnhideText" runat="server" ControlName="txtUnhideText" Suffix=":" />
                    <asp:TextBox ID="txtUnhideText" runat="server" Columns="60" TextMode="MultiLine" Rows="2" />
                </div>
            </div>
            <div class="tab" id="help">
                <div>
                    <%=Localization.GetString("DocumentationHelp.Text", LocalResourceFile)%></div>
                <div>
                    <%=Localization.GetString("TokenHelp.Text", LocalResourceFile)%></div>
            </div>
        </div>
    </div>
    <ul class="dnnActions dnnClear">
        <li>
            <asp:LinkButton ID="cmdUpdate" Text="Update" resourcekey="cmdUpdate" CausesValidation="True" runat="server" CssClass="dnnPrimaryAction" /></li>
        <li>
            <asp:LinkButton ID="cmdCancel" Text="Cancel" resourcekey="cmdCancel" CausesValidation="False" runat="server" CssClass="dnnSecondaryAction" /></li>
    </ul>
</div>
<script type="text/javascript">
    var tabber1 = new Yetii({
        id: 'editsettings',
        persist: true
    });
</script>
