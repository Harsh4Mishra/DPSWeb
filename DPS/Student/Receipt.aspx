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
        <div class="container">
            <header class="header" style="display: flex; align-items: center; justify-content: space-between;">
                <img src="<%= LogoPath %>" alt="School Logo" style="max-width: 100px; margin-right: 20px;">
                <div style="text-align: center; flex-grow: 1; min-width: 200px;">
                    <h1><%= SchoolName %></h1>
                    <p><%= SchoolAddress %></p>
                    <p>Phone: <%= SchoolPhone %> | Email: <%= SchoolEmail %></p>
                </div>
            </header>
            
            <hr style="border: 1px solid #007bff; margin: 0 0 20px;">

            <div style="text-align: right; margin-bottom: 15px;">
                <p><strong>Receipt No:</strong> <%= ReceiptNo %></p>
                <p><strong>Date:</strong> <span id="date"><%= DateTime.Now.ToString("yyyy-MM-dd") %></span></p>
            </div>

            <div class="hero-section">
                <h2>Student Details</h2>
                <div style="display: flex; justify-content: space-between;">
                    <div style="width: 48%; display: flex; flex-direction: column;">
                        <p><strong>Scholar No:</strong> <%= ScholarNo %></p>
                        <p><strong>Name:</strong> <%= StudentName %></p>
                        <p><strong>Father Name:</strong> <%= FatherName %></p>
                        <p><strong>Fee Category:</strong> <%= FeeCategory %></p>
                    </div>
                    <div style="width: 48%; display: flex; flex-direction: column;">
                        <p><strong>Class:</strong> <%= StudentClass %></p>
                        <p><strong>Section:</strong> <%= StudentSection %></p>
                        <p><strong>Stream:</strong> <%= StudentStream %></p>
                    </div>
                </div>
            </div>

            <div class="details">
                <h2>Fee Details</h2>
                <asp:GridView ID="GridViewFeeDetails" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="FeeType" HeaderText="Fee Type" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                    </Columns>
                </asp:GridView>
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
