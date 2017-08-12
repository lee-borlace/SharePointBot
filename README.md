# SharePoint Bot
A Microsoft Bot Framework bot, used for interacting with SharePoint Online.

It uses [BotAuth](https://github.com/richdizz/BotAuth) for authentication with Office 365.

## Limitations
SharePoint Bot uses the V1 authentication endpoint in order to use CSOM. This means that you need to sign in with with an organisational account, and that you need to specify the root site collection URL up front.


