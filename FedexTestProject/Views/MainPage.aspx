<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
CodeBehind="MainPage.aspx.cs" Inherits="FedexTestProject.Web.Views.MainPage" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
    <asp:Panel ID= "MainPanel"  runat = "server">
        <h1>Welcome to the Fedex Test Project</h1>
            <p class="lead">Please upload *.xlsx file or a tabbed delimited \*.txt file with one tracking number per line and click upload button</p>
            <asp:FileUpload id="FileUploadControl" runat="server" />
            <br/>
            <asp:Button runat="server" id="UploadButton" class="btn btn-primary" text="Upload" onclick="UploadButton_Click" />
            <br/>
            <p>
                <asp:RegularExpressionValidator   
                    id="FileUpLoadValidator" runat="server"  
                    CssClass="text-danger"
                    ErrorMessage="Upload txt and xlsx only."   
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.txt|.TXT|.xlsx|.XLSX)$"
                    ControlToValidate="FileUploadControl">  
                </asp:RegularExpressionValidator> 
            </p>
            <p>
                <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator" runat="server"
                    CssClass="text-danger"
                    ControlToValidate="FileUploadControl"
                    ErrorMessage="Choose a file!">
                </asp:RequiredFieldValidator> 
            </p>
    </asp:Panel>
    <asp:DataGrid id="TrackingInfoGrid"
                  BorderColor="black"
                  BorderWidth="1"
                  CellPadding="3"
                  AutoGenerateColumns="true"
                  runat="server">
        <HeaderStyle BackColor="#00aaaa">
        </HeaderStyle>
    </asp:DataGrid>
    </div>
</asp:Content>