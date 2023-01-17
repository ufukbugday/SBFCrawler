using Microsoft.Extensions.Configuration;
using SBFCrawler.Model;

namespace SBFCrawler.Shared.Helpers
{
    public static class LoginHelper
    {
        public static string GetLoginScript(ConfigLogin configLogin) => $@"document.querySelector('[name=""user[email]""]').value = '{configLogin.Username}';
                                  document.querySelector('[name=""user[password]""]').value = '{configLogin.Password}';
                                  document.querySelector('button[type=submit]').click();";

    }
}