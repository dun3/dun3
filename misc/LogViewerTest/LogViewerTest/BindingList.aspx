<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindingList.aspx.cs" Inherits="LogViewerTest.BindingList" %>

<%@ Register Src="LogItemDetail.ascx" TagName="LogItemDetail" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ListView ID="ListView1" runat="server" DataSourceID="LinqDataSource1">
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                <tr runat="server" style="">                                   
                                    <th runat="server">
                                        Bla
                                    </th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="">
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="">
                    <tr>
                        <td>
                            No data was returned.
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <ItemTemplate>
                <tr style="">
                    <td>
                        <uc1:LogItemDetail Id="LogItemDetail1" runat="server" LogId='<%# Eval("Id") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="LogViewerTest.BindingList"
        OrderBy="Id desc" Select="new (Id, Summary, Incident)" TableName="SummaryLogItemz">
    </asp:LinqDataSource>
    </form>
</body>
</html>
