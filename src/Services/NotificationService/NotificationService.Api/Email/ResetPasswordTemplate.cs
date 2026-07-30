using System.Globalization;
using NotificationService.Api.Email.Resource;

namespace NotificationService.Api.Email;

public static class ResetPasswordTemplate
{
    public static string Execute(string username, string code) =>
      $$"""
        <!DOCTYPE html>
        <html lang="{{CultureInfo.CurrentUICulture.TwoLetterISOLanguageName}}">
        <head>
          <meta charset="UTF-8" />
          <meta name="viewport" content="width=device-width, initial-scale=1.0" />
          <title>{{ResourceEmailMessage.TITLE_RESET_PASSWORD}}</title>
          <style>
            * { margin: 0; padding: 0; box-sizing: border-box; }

            body {
              background-color: #F8F4F0;
              font-family: 'Public Sans', Arial, sans-serif;
              color: #201F24;
              padding: 40px 16px;
            }

            .email-wrapper {
              max-width: 520px;
              margin: 0 auto;
            }

            /* Header */
            .email-header {
              background-color: #201F24;
              border-radius: 12px;
              padding: 24px;
              text-align: center;
              margin-bottom: 24px;
            }

            .logo-text {
              font-size: 22px;
              font-weight: 700;
              color: #FFFFFF;
              letter-spacing: -0.5px;
            }

            /* Card */
            .email-card {
              background-color: #FFFFFF;
              border-radius: 12px;
              padding: 40px;
            }

            .email-accent {
              font-size: 11px;
              font-weight: 700;
              letter-spacing: 2.5px;
              text-transform: uppercase;
              color: #277C78;
              margin-bottom: 12px;
            }

            .email-title {
              font-size: 22px;
              font-weight: 700;
              color: #201F24;
              line-height: 1.3;
              margin-bottom: 20px;
            }

            .email-body {
              font-size: 14px;
              color: #696868;
              line-height: 1.65;
              margin-bottom: 16px;
            }

            /* Bloco do código */
            .code-block {
              margin: 28px 0;
              background-color: #F8F4F0;
              border-radius: 12px;
              padding: 28px 24px;
              text-align: center;
            }

            .code-label {
              font-size: 11px;
              font-weight: 700;
              letter-spacing: 2px;
              text-transform: uppercase;
              color: #98908B;
              margin-bottom: 16px;
            }

            .code-value {
              display: inline-block;
              background-color: #FFFFFF;
              border-radius: 8px;
              border: 1px solid #F2F2F2;
              font-size: 24px;
              font-weight: 700;
              letter-spacing: 4px;
              color: #277C78;
              padding: 12px 24px;
            }

            .code-expiry {
              margin-top: 16px;
              font-size: 12px;
              color: #696868;
            }

            .code-expiry strong {
              color: #C94736;
            }

            /* Alerta */
            .email-alert {
              font-size: 12px;
              color: #C94736;
              padding: 10px 12px;
              background-color: rgba(201, 71, 54, 0.08);
              border-radius: 8px;
              border-left: 3px solid #C94736;
              margin-bottom: 16px;
              line-height: 1.5;
            }

            /* Divider */
            .email-divider {
              border: none;
              border-top: 1px solid #F2F2F2;
              margin: 32px 0;
            }

            .email-note {
              font-size: 12px;
              color: #98908B;
              line-height: 1.6;
            }

            /* Footer */
            .email-footer {
              text-align: center;
              margin-top: 24px;
              font-size: 12px;
              color: #98908B;
            }
          </style>
        </head>
        <body>
          <div class="email-wrapper">

            <div class="email-header">
              <span class="logo-text">finance</span>
            </div>

            <div class="email-card">
              <p class="email-accent">{{ResourceEmailMessage.RESET_ACCENT}}</p>
              <h1 class="email-title">{{string.Format(format: ResourceEmailMessage.RESET_HEADING, arg0: username.Split(separator: " ")[0])}}</h1>

              <p class="email-body">{{ResourceEmailMessage.RESET_BODY_1}}</p>

              <div class="code-block">
                <p class="code-label">{{ResourceEmailMessage.RESET_CODE_LABEL}}</p>
                <p><span class="code-value">{{code}}</span></p>
                <p class="code-expiry">{{ResourceEmailMessage.RESET_CODE_EXPIRY}}</p>
              </div>

              <div class="email-alert">{{ResourceEmailMessage.RESET_ALERT}}</div>

              <p class="email-body">{{ResourceEmailMessage.RESET_BODY_2}}</p>

              <hr class="email-divider" />

              <p class="email-note">{{ResourceEmailMessage.RESET_NOTE}}</p>
            </div>

            <div class="email-footer">
              <p>Personal Finance &mdash; {{ResourceEmailMessage.STUDY_PROJECT}} <a href="https://jonatasgdec-portifolio.vercel.app/" target="_blank" style="color: #201F24; text-decoration: underline; font-weight: 600;">JonatasGdeC</a> &copy; {{DateTime.Now.Year}}</p>
            </div>

          </div>
        </body>
        </html>
        """;
}
