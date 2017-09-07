<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Agregador.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Inicio - Agregador</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet"/>


    <!-- Custom CSS -->
    <link href="css/sb-admin.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    
    <div>
        <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="logo-login"><img src="images/logo-camara.jpg"></div>
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Inicie sesión</h3>
                    </div>
                    <div class="panel-body">
                        <form role="form" runat="server" id="form1">
                            <fieldset>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtUsuario" class="form-control" placeholder="Usuario ó correo electrónico" name="email" type="email" autofocus/>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtClave" class="form-control" placeholder="Contraseña" name="password" type="password" value=""/>
                                </div>
                                <div class="checkbox">
                                    <asp:CheckBox runat="server" ID="chkRemember" Text="Recuérdame"/>
                                </div>
                                <!-- Change this to a button or input when using this as a form -->
                                <asp:Button runat="server" ID="btnEnviar" Text="Ingresar" class="btn btn-lg btn-success btn-block" OnClick="btnEnviar_Click"/>
                                <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
                            </fieldset>
                        </form>
                    </div>
                </div>
                <div class="olvido"><i class="fa fa-key" aria-hidden="true"></i> Olvidó su contraseña? <a href="recuperarClave.aspx">Recuperar</a></div>
            </div>
        </div>
    </div>

    <!-- jQuery -->
    <script src="js/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="js/sb-admin.js"></script>
    </div>
</body>
</html>
