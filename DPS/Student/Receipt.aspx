<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Receipt.aspx.cs" Inherits="DPS.Student.Receipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Fee Receipt</title>
    <style>
        body {
            font-family: Arial, sans-serif; 
            margin: 0; 
            padding: 20px; 
            background-color: #f4f4f4; 
            display: flex; 
            justify-content: center; 
            align-items: center; 
            height: 100vh;
        }
        .container {
            border: 2px solid #007bff; 
            padding: 20px; 
            background: #fff; 
            width: 100%; 
            max-width: 600px; 
            box-sizing: border-box; 
            overflow: auto;
        }
        .header, .hero-section, .details, .footer {
            margin-bottom: 20px;
        }
        h1, h2 {
            color: #007bff;
        }
        .buttons {
            text-align: center;
        }
        .print-button {
            padding: 10px 15px; 
            font-size: 16px; 
            color: #fff; 
            background-color: #007bff; 
            border: none; 
            border-radius: 5px; 
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br /><br /><br /><br /><br /><br />
        <div class="container">
            <header class="header" style="display: flex; align-items: center; justify-content: space-between;">
                <asp:Image ID="Image1" runat="server" style="max-width: 100px; margin-right: 20px;" />
                <div style="text-align: center; flex-grow: 1; min-width: 200px;">
                    <h1><asp:Label ID="lblSchoolName" runat="server" Text="Label"></asp:Label></h1>
                    <p><asp:Label ID="lblSchoolAddress" runat="server" Text="Label"></asp:Label></p>
                    <p>Phone: <asp:Label ID="lblSchoolPhone" runat="server" Text="Label"></asp:Label> | Email: <asp:Label ID="lblSchoolEmail" runat="server" Text="Label"></asp:Label></p>
                </div>
            </header>
            
            <hr style="border: 1px solid #007bff; margin: 0 0 20px;">

            <div style="text-align: right; margin-bottom: 15px;">
                <p><strong>Receipt No:</strong> <asp:Label ID="lblreceiptNo" runat="server" Text="Label"></asp:Label></p>
                <p><strong>Date:</strong> <span id="date"><%= DateTime.Now.ToString("yyyy-MM-dd") %></span></p>
            </div>

            <div class="hero-section">
                <h2>Student Details</h2>
                <div style="display: flex; justify-content: space-between;">
                    <div style="width: 48%; display: flex; flex-direction: column;">
                        <p><strong>Scholar No:</strong> <asp:Label ID="lblScholarNo" runat="server" Text="Label"></asp:Label></p>
                        <p><strong>Name:</strong> <asp:Label ID="lblStudentName" runat="server" Text="Label"></asp:Label></p>
                        <p><strong>Father Name:</strong> <asp:Label ID="lblFatherName" runat="server" Text="Label"></asp:Label></p>
                        <p><strong>Fee Category:</strong><asp:Label ID="lblfeecategory" runat="server" Text="Label"></asp:Label></p>
                    </div>
                    <div style="width: 48%; display: flex; flex-direction: column;">
                        <p><strong>Class:</strong> <asp:Label ID="lblstudentclass" runat="server" Text="Label"></asp:Label></p>
                        <p><strong>Section:</strong> <asp:Label ID="lblstudentsection" runat="server" Text="Label"></asp:Label></p>
                        <p><strong>Stream:</strong> <asp:Label ID="lblStudentStream" runat="server" Text="Label"></asp:Label></p>
                    </div>
                </div>
            </div>

            <div class="details">
                <h2>Fee Details</h2>
                <asp:GridView ID="GridViewFeeDetails" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="FeeType" HeaderText="Fee Type" />
                        <asp:BoundField DataField="FeeName" HeaderText="Fee Name" />
                        <asp:BoundField DataField="FeeAmount" HeaderText="Fee Amount" />
                    </Columns>
                </asp:GridView>
                <br />
                <br />
                <p><strong>Fine Amount:</strong> <asp:Label ID="lblFineAmt" runat="server" Text="Label"></asp:Label></p>
            </div>

            <div class="footer">
                <p>Thank you for your payment!</p>
                <p>Powered by dpstechnologies</p>
            </div>

            <div class="buttons">
                <button type="button" class="print-button" onclick="window.print();">Print Receipt</button>
            </div>
        </div>
    </form>
    <script>
        // Set the current date in the format of YYYY-MM-DD
        document.getElementById('date').textContent = new Date().toLocaleDateString();
    </script>
</body>
</html>
