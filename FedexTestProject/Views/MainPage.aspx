<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
CodeBehind="MainPage.aspx.cs" Inherits="FedexTestProject.Web.Views.MainPage" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
    <asp:Panel ID= "MainPanel"  runat = "server">
        <h1>Welcome to the Fedex Test Project</h1>
            <p class="lead">Please upload *.xlsx file or a tabbed delimited \*.txt file with one tracking number per line and click upload button</p>
            <div class="form-group">
                <asp:FileUpload id="FileUploadControl" runat="server" />
                <br/>
                <asp:Button runat="server" id="UploadButton" class="btn btn-primary" text="Upload" onclick="UploadButton_Click"/>
                <br/>
            </div>
            <div>
                <asp:RegularExpressionValidator   
                    ID="FileUpLoadValidator" runat="server" 
                    Display="Dynamic"
                    CssClass="text-danger"
                    ErrorMessage="You should select .xlsx or .txt file"   
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.txt|.TXT|.xlsx|.XLSX)$"
                    ControlToValidate="FileUploadControl">  
                </asp:RegularExpressionValidator> 
            </div>
    </asp:Panel>
    <asp:Panel ID= "TrackNumberPanel" runat = "server">
        <asp:Button runat="server" ID="ProcessTracking" class="btn btn-primary" text="Process Tracking Numbers" onclick="ProcessTrackingNumbersButton_Click" />
        <asp:ListView runat="server" ID="TrackingNumbersList">
            <LayoutTemplate>
                <table class="table table-striped">
                    <thead>
                    <tr>
                        <th scope="col">Imported Tracking Numbers</th>
                    </tr>
                    </thead>
                    <tr id="groupPlaceholder" runat="server">
                        <td id="itemPlaceHolder" runat="server" />
                    </tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Container.DataItem %>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
    <asp:Panel ID= "TrackingInfoPanel" runat = "server">
        <asp:Button runat="server" id="ExportXlsx" class="btn btn-primary" text="Export to excel" onclick="ExportXlsxButton_Click" />
        <asp:Button runat="server" id="ExportTxt" class="btn btn-primary" text="Export to csv" onclick="ExportTxtButton_Click" />
        <br/><br/>
        <asp:Gridview ID="TrackingInfoGrid"
                      GridLines="None"
                      CssClass="table table-striped"
                      AlternatingRowStyle-CssClass="alt"
                      AutoGenerateColumns="false"
                      runat="server">
            <Columns>
                <asp:BoundField DataField="TrackNumber" HeaderText="Track Number" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="OriginAddress" HeaderText="Origin City" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="TerminalAddress" HeaderText="Origin Terminal" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="OriginDate" HeaderText="Origin Date" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="TrackingStatus" HeaderText="Tracking Status" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="DeliveryAddress" HeaderText="Destination City" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" ReadOnly="True"></asp:BoundField>
            </Columns>
        </asp:Gridview>
    </asp:Panel>
    <asp:Label ID="ErrorLbl" class="text-danger" runat="server"></asp:Label>
    </div>

    <asp:Panel Id="LockPanel" class="Lock text-center" style="display:none" runat = "server">
        Uploading...
    </asp:Panel>

    <script type="text/javascript">
        function CallFunction() {
            debugger;
            alert("Test");
        }
        $(function () {
            var blockUI = true;
            $('form').on("submit", function (e) {
                if (blockUI) {
                    $('#<%=LockPanel.ClientID%>').show();
                }
                if (!Page_IsValid && blockUI) {
                    $('#<%=LockPanel.ClientID%>').Hide();
                    e.stopPropagation();
                }
            });

            $('#<%=ProcessTracking.ClientID%>').click(function () {
                $('#<%=LockPanel.ClientID%>').html('Processing...');
            });

            $('#<%=ExportXlsx.ClientID%>, #<%=ExportTxt.ClientID%>').click(function () {
                blockUI = false;
            });

        });
    </script>
</asp:Content>