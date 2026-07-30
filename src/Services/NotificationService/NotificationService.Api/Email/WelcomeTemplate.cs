using System.Globalization;
using NotificationService.Api.Email.Resource;

namespace NotificationService.Api.Email;

public static class WelcomeTemplate
{
    public static string Execute(string userName) =>
      $$"""
        <!DOCTYPE html>
        <html lang="{{CultureInfo.CurrentUICulture.TwoLetterISOLanguageName}}">
        <head>
          <meta charset="UTF-8" />
          <meta name="viewport" content="width=device-width, initial-scale=1.0" />
          <title>{{ResourceEmailMessage.TITLE_WELCOME}}</title>
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

            .email-body strong {
              color: #201F24;
              font-weight: 700;
            }

            /* CTA */
            .email-cta {
              display: inline-block;
              margin-top: 8px;
              padding: 14px 28px;
              background-color: #201F24;
              color: #FFFFFF;
              font-size: 13px;
              font-weight: 700;
              font-family: inherit;
              border-radius: 8px;
              text-decoration: none;
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
              <p class="email-accent">{{ResourceEmailMessage.WELCOME_ACCENT}}</p>
              <h1 class="email-title">{{string.Format(format: ResourceEmailMessage.WELCOME_HEADING, arg0: userName.Split(separator: " ")[0])}}</h1>

              <p class="email-body">{{ResourceEmailMessage.WELCOME_BODY_1}}</p>

              <p class="email-body">{{ResourceEmailMessage.WELCOME_BODY_2}}</p>

              <a href="http://localhost:5290" target="_blank" class="email-cta" style="color: #FFF">{{ResourceEmailMessage.WELCOME_CTA}}</a>

              <hr class="email-divider" />

              <p class="email-note">{{ResourceEmailMessage.WELCOME_NOTE}}</p>
            </div>

            <div class="email-footer">
              <p>Personal Finance &mdash; {{ResourceEmailMessage.STUDY_PROJECT}} <a href="https://jonatasgdec-portifolio.vercel.app/" target="_blank" style="color: #201F24; text-decoration: underline; font-weight: 600;">JonatasGdeC</a> &copy; {{DateTime.Now.Year}}</p>
            </div>

          </div>
        </body>
        </html>
        """;
}
