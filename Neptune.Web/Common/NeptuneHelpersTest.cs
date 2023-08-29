namespace Neptune.Web.Common
{
    public class NeptuneHelpersTest
    {
        //[Test]
        //public void PostLogonDestinationTest()
        //{
        //    var homeUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.Index());

        //    var logInUrl = new Uri(SitkaRoute<AccountController>.BuildUrlFromExpression(x => x.LogOn()));
        //    var unprotectedUrl = new Uri(SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.About()));
        //    var protectedUrl = new Uri(SitkaRoute<Areas.Trash.Controllers.HomeController>.BuildUrlFromExpression(x => x.Index()));

        //    // Act 1 - log-in url redirects (post log-in) to referrer 
        //    var redirectFromLoginToReferrer = NeptuneHelpers.PostLogonDestinationImpl(logInUrl, unprotectedUrl);
        //    Assert.That(redirectFromLoginToReferrer.ToString() == unprotectedUrl.ToString());

        //    // Act 2 - log-in url without referrer redirects (etc) to home
        //    var redirectFromLoginToHome = NeptuneHelpers.PostLogonDestinationImpl(logInUrl, null);
        //    Assert.That(redirectFromLoginToHome.ToString() == homeUrl);

        //    // Act 3 - protected url without referrer redirects to self
        //    var redirectFromProtectedToSelf = NeptuneHelpers.PostLogonDestinationImpl(protectedUrl, null);
        //    Assert.That(redirectFromProtectedToSelf.ToString() == protectedUrl.ToString());
            
        //    // Act 4 - protected url with referrer redirects to self
        //    var redirectFromProtectedToSelfEvenWithReferrer = NeptuneHelpers.PostLogonDestinationImpl(protectedUrl, unprotectedUrl);
        //    Assert.That(redirectFromProtectedToSelfEvenWithReferrer.ToString() == protectedUrl.ToString());
        //}
    }
}
