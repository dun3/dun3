<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Timer.aspx.cs" Inherits="LogViewerTest.Timer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="Stylesheet" href="style.css" />

    <script type="text/javascript" src="jquery-1.2.6-intellisense.js"></script>

    <script type="text/javascript" src="jquery.timers.js"></script>

    <script type="text/javascript">
			//<![CDATA[
			var lastCustomer = 0;
			
			$(function() { 
				//===				
				$("#uncontrolled-timeout p").oneTime(2000, function() {
					showhide("#uncontrolled-timeout p");
				}); 
				//===
			});
			
            function showhide(detail)
            { 
                
                //if the detail DIV is empty Initiate AJAX Call, if not that means DIV already populated with data             
                
                    //Prepare Parameters
                    var params = '{lastLogId:"'+ lastCustomer +'"}';                    
                    //Issue AJAX Call
                    $.ajax({
                            type: "POST", //POST
                            url: "Timer.aspx/GetLogItemz", //Set call to Page Method
                            data: params, // Set Method Params
                            beforeSend: function(xhr) {
                                xhr.setRequestHeader("Content-type", "application/json; charset=utf-8");
                                },
                            contentType: "application/json; charset=utf-8", //Set Content-Type
                            dataType: "json", // Set return Data Type
                            success: function(msg, status) {
                                var s = msg.d.Text+$(detail).html();                                
                                $(detail).html(s);
                                lastCustomer = msg.d.LastId;
                                $("#uncontrolled-timeout p").oneTime(2000, function() {
					                showhide("#uncontrolled-timeout p");
				                    }); 
                                },
                            error: function(xhr,msg,e){
                                alert(msg);//Error Callback
                                }
                            });               
            }
			//]]>
    </script>

</head>
<body>
    <h3>
        Uncontrolled oneTime</h3>
    <div id="uncontrolled-timeout">
        <p>
        </p>
    </div>
    <form id="form1" runat="server">
    
    </form>
</body>
</html>
