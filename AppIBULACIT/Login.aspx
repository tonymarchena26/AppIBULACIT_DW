﻿<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppIBULACIT.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Iniciar sesion</title>
      <style>
body {font-family: Arial, Helvetica, sans-serif;}
* {box-sizing: border-box;}

/* Full-width input fields */
input[type=text], input[type=password] {
  width: 100%;
  padding: 15px;
  margin: 5px 0 22px 0;
  display: inline-block;
  border: none;
  background: #f1f1f1;
}

/* Add a background color when the inputs get focus */
input[type=text]:focus, input[type=password]:focus {
  background-color: #ddd;
  outline: none;
}

/* Set a style for all buttons */
button {
  background-color:  #04AA6D;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
  opacity: 0.9;
}

button:hover {
  opacity:1;
}

/* Extra styles for the cancel button */
.cancelbtn {
  background-color:  gray;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
  opacity: 0.9;
}

.normalbtn {
  background-color:  navy;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
  opacity: 0.9;
}

button:hover {
  opacity:1;
}


/* Add padding to container elements */
.container {
  padding: 16px;
}

/* The Modal (background) */
.modal {
  display: normal; /* Hidden by default */
  position: fixed; /* Stay in place */
  z-index: 1; /* Sit on top */
  left: 0;
  top: 0;
  width: 100%; /* Full width */
  height: 100%; /* Full height */
  overflow: auto; /* Enable scroll if needed */
  background-color: #474e5d;
  padding-top: 50px;
}

/* Modal Content/Box */
.modal-content {
  background-color: #fefefe;
  margin: 5% auto 15% auto; /* 5% from the top, 15% from the bottom and centered */
  border: 1px solid #888;
  width: 80%; /* Could be more or less, depending on screen size */
}

/* Style the horizontal ruler */
hr {
  border: 1px solid #f1f1f1;
  margin-bottom: 25px;
}
 
/* The Close Button (x) */
.close {
  position: absolute;
  right: 35px;
  top: 15px;
  font-size: 40px;
  font-weight: bold;
  color: #f1f1f1;
}

.imgcontainer {
  text-align: center;
  margin: 24px 0 12px 0;
  position: relative;
}

.close:hover,
.close:focus {
  color: #f44336;
  cursor: pointer;
}

/* Clear floats */
.clearfix::after {
  content: "";
  clear: both;
  display: table;
}

/* Change styles for cancel button and signup button on extra small screens */
@media screen and (max-width: 300px) {
  .cancelbtn, .signupbtn {
     width: 100%;
  }
}
          .auto-style1 {
              width: 173px;
              height: 179px;
          }
      </style>  
</head>
<body>
   <div id="myModal" class="modal">
            <form class="modal-content animate" runat="server">
                <div class="imgcontainer">
                    &nbsp;<img src="img/ulacit.jpg" class="auto-style1" /></div>
                <div class="container">
                    <h1>Registro</h1>
                    <asp:TextBox ID="txtUsername" Placeholder="Ingrese su nombre de usuario" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                        ErrorMessage="El nombre de usuario es requerido" ControlToValidate="txtUsername" ForeColor="Maroon"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtPassword" Placeholder="Ingrese su password" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ForeColor="Maroon"
                        ErrorMessage="El password es requerido" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                     <asp:Label ID="lblStatus" runat="server" Text="" Visible="false" ForeColor="Maroon"></asp:Label>
                </div>
                <div class="container">
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="normalbtn" OnClick="btnAceptar_Click" />
                    <input type="reset" value="Limpiar" class="cancelbtn" />
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Registro.aspx">Registrarme</asp:HyperLink>
                </div>
            </form>
       </div>
</body>
</html>
