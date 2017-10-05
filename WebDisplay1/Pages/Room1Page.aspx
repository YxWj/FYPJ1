<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="Room1Page.aspx.cs" Inherits="WebDisplay1.Pages.Room1Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div class="row">
            <ol class="breadcrumb">
                <li><a href="Main Dashboard Page.aspx">
                    <em class="fa fa-home"></em>
                </a></li>
                <li class="active">Room 4xx1</li>
            </ol>
        </div>          <%--breadcrumbs--%>
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">Room 4xx1</h1>
            </div>
        </div>          <%--page header--%>
        <div class="row">
            <div class="col-xs-6 col-md-3">
                <div class="panel panel-default">
                    <div class="panel-body easypiechart-panel">
                        <div class="easypiechart" id="easypiechart-teal" data-percent="1">
                            <span class="percent">
                                <asp:Label ID="tempLblrm1" runat="server"></asp:Label></span>
                        </div>
                        <div class="text-muted">Temperature</div>
                    </div>
                </div>
            </div>      <%--Temp reading--%>
            <div class="col-xs-6 col-md-3">
                <div class="panel panel-default">
                    <div class="panel-body easypiechart-panel">
                        <div class="easypiechart" id="easypiechart-blue" data-percent="92">
                            <span class="percent">
                                <asp:Label ID="humidityLblrm1" runat="server"></asp:Label></span>
                        </div>
                        <div class="text-muted">Humidity</div>
                    </div>
                </div>
            </div>      <%--Humidity reading--%>
            <div class="col-xs-6 col-md-3">
                <div class="panel panel-default">
                    <div class="panel-body easypiechart-panel">
                        <div class="easypiechart" id="easypiechart-orange" data-percent="65">
                            <span class="percent">
                                <asp:Label ID="lightLblrm1" runat="server"></asp:Label></span>
                        </div>
                        <div class="text-muted">Light</div>
                    </div>
                </div>
            </div>      <%--Light reading--%>
            <div class="col-xs-6 col-md-3">
                <div class="panel panel-default">
                    <div class="panel-body easypiechart-panel">
                        <div class="easypiechart" id="easypiechart-red" data-percent="100">
                            <span class="percent">
                                <asp:Label ID="motionLblrm1" runat="server"></asp:Label></span>
                        </div>
                        <div class="text-muted">Motion</div>
                    </div>
                </div>
            </div>      <%--Motion reading--%>
        </div>          <%--Readings--%>
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading dark-overlay">
                    Measurement information     
                    <asp:DropDownList ID="DropDownList1" CssClass="pull-right panel-settings panel-button-tab-right" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        <asp:ListItem>--</asp:ListItem>

                        <asp:ListItem Value="View past day">View past day</asp:ListItem>

                        <asp:ListItem Value="View past week">View past week</asp:ListItem>

                        <asp:ListItem Value="View past month">view past month</asp:ListItem>

                    </asp:DropDownList>
                </div>
                <div class="panel-body">
                    <asp:Label ID="Label1" runat="server">Average temperature: </asp:Label><asp:Label ID="avgtemprm1" runat="server" ForeColor="#FF3300"></asp:Label>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Average Humidity: "></asp:Label><asp:Label ID="avghumidityrm1" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Average Light Consumption: "></asp:Label><asp:Label ID="avglightrm1" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:Label ID="Label6" runat="server" Text="Amount of people that booked room: "></asp:Label><asp:Label ID="peoplebrm1" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="Percentage of people that confirmed booking: "></asp:Label><asp:Label ID="cfmbrm1" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="Label4" runat="server">Last booked session: </asp:Label><asp:Label ID="lbdLbl" runat="server" ForeColor="Red"></asp:Label>
                </div>                                  <%--Reading Panel--%>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading dark-overlay">Issues</div>
                <div class="panel-body">
                    <div class="col-xs-6 col-md-3 col-lg-3 no-padding">
                        <div class="panel panel-teal panel-widget border-right">
                            <div class="row no-padding">
                                <em class="fa fa-xl fa-shopping-cart color-blue"></em>
                                <div class="large"></div>
                                <div class="text-muted">Aircon Issue</div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3 col-lg-3 no-padding">
                        <div class="panel panel-blue panel-widget border-right">
                            <div class="row no-padding">
                                <em class="fa fa-xl fa-comments color-orange"></em>
                                <div class="large"></div>
                                <div class="text-muted">Light Issue</div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-6 col-md-3 col-lg-3 no-padding">
                        <div class="panel panel-orange panel-widget border-right">
                            <div class="row no-padding">
                                <em class="fa fa-xl fa-users color-teal"></em>
                                <div class="large"></div>
                                <div class="text-muted">Sensor Issue</div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>                              <%--Issue Panel--%>
        </div>
        <%--Test DDL--%>
    </form>
</asp:Content>
