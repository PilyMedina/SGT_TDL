using MailKit.Net.Smtp;
using MimeKit;
using System.Security.Cryptography;
using TDL.Models;
using TDL.Repositories.Interfaces;
using TDL.Services.Interfaces;

public class TokenService
    : ITokenService
{
    private readonly ITokenRepository
        _repository;

    public TokenService(
        ITokenRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> EnviarTokenAsync(
        string username)
    {
        var usuario =
            _repository
            .ObtenerUsuarioPorUsername(
                username);

        if (usuario == null)
            return false;

        string token =
            RandomNumberGenerator
            .GetInt32(100000, 1000000)
            .ToString();

        var tokenBD =
            new TokenRecuperacion
            {
                Id = usuario.ID,

                Token = token,

                FechaExpiracion =
                    DateTime.Now
                    .AddMinutes(10),

                Usado = false
            };

        _repository.GuardarToken(tokenBD);

        _repository.GuardarCambios();

        var mensaje = new MimeMessage();

        mensaje.From.Add(
            new MailboxAddress(
                "Sistema TDL",
                "dgbatecnologia@gmail.com"));

        mensaje.To.Add(
            MailboxAddress.Parse(
                "dgbatecnologia@gmail.com"));

        mensaje.Subject =
            "Código de recuperación";

        mensaje.Body =
           mensaje.Body =
    new TextPart("html")
    {
        Text = $@"
        <div style='
            font-family: Arial;
            padding: 30px;
            background-color: #f5f5f5;'>

            <div style='
                max-width: 500px;
                margin: auto;
                background: white;
                border-radius: 10px;
                padding: 30px;
                box-shadow: 0 0 10px rgba(0,0,0,0.1);'>

                <h1 style='
                    color: #2563eb;
                    text-align: center;'>
                    Recuperación de contraseña
                </h1>

                <p style='
                    font-size: 16px;
                    color: #333;'>
                    Hola,
                </p>

                <p style='
                    font-size: 16px;
                    color: #333;'>
                    Se solicitó un cambio de contraseña
                    para el usuario:
                    <b>{username}</b>
                </p>

                <div style='
                    text-align: center;
                    margin: 30px 0;'>

                    <span style='
                        display: inline-block;
                        background-color: #2563eb;
                        color: white;
                        font-size: 32px;
                        letter-spacing: 5px;
                        padding: 15px 30px;
                        border-radius: 8px;
                        font-weight: bold;'>

                        {token}
                    </span>
                </div>

                <p style='
                    color: #666;
                    font-size: 14px;
                    text-align: center;'>

                    Este código expirará en 10 minutos.
                </p>

            </div>
        </div>"
    };

        using var smtp =
            new SmtpClient();

        await smtp.ConnectAsync(
            "smtp.gmail.com",
            587,
            MailKit.Security
            .SecureSocketOptions
            .StartTls);

        await smtp.AuthenticateAsync(
            "dgbatecnologia@gmail.com",
            "qkiwoskhwtvdvpbv");

        await smtp.SendAsync(mensaje);

        await smtp.DisconnectAsync(true);

        return true;
    }

    public bool ValidarToken(
        string username,
        string token)
    {
        var usuario =
            _repository
            .ObtenerUsuarioPorUsername(
                username);

        if (usuario == null)
            return false;

        var tokenBD =
            _repository
            .ObtenerTokenValido(
                usuario.ID,
                token);

        if (tokenBD == null)
            return false;

        tokenBD.Usado = true;

        _repository.GuardarCambios();

        return true;
    }
}