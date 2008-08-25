<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JQueryList.aspx.cs" Inherits="LogViewerTest.JQueryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="Stylesheet" href="style.css" />

    <script type="text/javascript" src="jquery-1.2.6-intellisense.js" />

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="pnlUpdate" runat="server">
        <ContentTemplate>
            <div>
                <div id='customer0' class="group" style="display: inline" onclick='showhide("#customer0", "#order0", "0")'>
                    <asp:Image ID="imgCollapsible" ImageUrl="~/images/plus.png" Style="margin-right: 5px;"
                        runat="server" />
                    <span class="group">TESTESTSETSETSETSETSET</span>
                </div>
                <div id='order0' class="order">
                </div>
            </div>
            <div>
                <div id='customer1' class="group" style="display: inline" onclick='showhide("#customer1", "#order1", "1")'>
                    <asp:Image ID="Image1" ImageUrl="~/images/plus.png" Style="margin-right: 5px;" runat="server" />
                    <span class="group">TESTESTSETSETSETSETSET</span>
                </div>
                <div id='order1' class="order">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="progress" style="position: absolute; visibility: hidden; display: inline">
        <img alt="loading" src="images/ajaxloading.gif" />
    </div>

    <script type="text/javascript">
            function showhide(master,detail,customerId)
            { 
                //First child of master div is the image
                var src = $(master).children()[0].src;
                //Switch image from (+) to (-) or vice versa.
                if(src.endsWith("plus.png")) {
                    src = src.replace('plus.png','minus.png');
                } else {
                    src = src.replace('minus.png','plus.png');                                              
                }
                
                //if the detail DIV is empty Initiate AJAX Call, if not that means DIV already populated with data             
                if(jQuery.trim($(detail).html()) == "")
                {
                    //Prepare Progress Image
                    var $offset = $(master).offset();
                    $('#progress').css('visibility','visible');
                    $('#progress').css('top',$offset.top);
                    $('#progress').css('left',$offset.left+$(master).width());                    
                    //Prepare Parameters
                    var params = '{customerId:"'+ customerId +'"}';                    
                    //Issue AJAX Call
                    $.ajax({
                            type: "POST", //POST
                            url: "JQueryList.aspx/GetDetails", //Set call to Page Method
                            data: params, // Set Method Params
                            beforeSend: function(xhr) {
                                xhr.setRequestHeader("Content-type", "application/json; charset=utf-8");
                                },
                            contentType: "application/json; charset=utf-8", //Set Content-Type
                            dataType: "json", // Set return Data Type
                            success: function(msg, status) {                                
                                $('#progress').css('visibility','hidden');
                                $(master).children()[0].src = src;        
                                $(detail).html(msg.d[customerId]);
                                $(detail).slideToggle("normal"); // Succes Callback                        
                                },
                            error: function(xhr,msg,e){
                                alert(msg);//Error Callback
                                }
                            });
                }
                else
                {
                    //Toggle expand/collapse       
                    $(detail).slideToggle("normal");                               
                    $(master).children()[0].src = src;
                }
            }
    </script>

    </form>
</body>
</html>
