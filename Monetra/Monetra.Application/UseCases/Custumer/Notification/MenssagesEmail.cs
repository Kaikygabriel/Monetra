namespace Monetra.Application.UseCases.Custumer.Notification;

public static class MenssagesEmail
{
    public static string RegisterCustomerMenssage(string email)
        => @"
    <html>
    <body style='font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 20px;'>
        <div style='max-width: 600px; margin: auto; background-color: #ffffff; border-radius: 10px; padding: 30px; text-align: center;'>
            <h1 style='color: #6f42c1;'>Bem-vindo(a) à Monetra!</h1>
            <p style='font-size: 16px; color: #333333;'>
                Olá <strong>" + email + @"</strong>, estamos muito felizes em ter você conosco!
            </p>
            <p style='font-size: 16px; color: #333333;'>
                Agora você faz parte da nossa comunidade. Explore nossos recursos e aproveite ao máximo nossos serviços.
            </p>
            <a href='https://www.seusite.com' style='display: inline-block; margin-top: 20px; padding: 12px 25px; background-color: #6f42c1; color: #ffffff; text-decoration: none; border-radius: 5px; font-weight: bold;'>Acessar sua conta</a>
            <p style='margin-top: 30px; font-size: 12px; color: #999999;'>Se você não solicitou este email, ignore-o.</p>
        </div>
    </body>
    </html>";
}