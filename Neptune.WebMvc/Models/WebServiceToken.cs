using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Neptune.Common.DesignByContract;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Models
{
    /// <summary>
    /// Maintains the logic of being a valid web service token, including the ASP.NET MVC <see cref="IModelBinder"/> stuff for binding in from a URL string.
    /// Also includes what is a legal token.
    /// </summary>
    [ModelBinder(typeof(WebServiceTokenModelBinder))] // ModelBinder is for Action parameter parsing
    public class WebServiceToken
    {
        /// <summary>
        /// The Unit Test GUID as a web service token
        /// </summary>
        public readonly WebServiceToken WebServiceTokenForUnitTests;

        public readonly WebServiceToken WebServiceTokenForParameterizedReplacements;

        /// <summary>
        /// The Unit Test GUID which can be used to make the web service token
        /// </summary>
        public static readonly Guid WebServiceTokenGuidForUnitTests = new Guid("535C44A1-A76C-40E0-BFF5-5A081BCC305A"); // corresponds to Ray Lee, PersonID = 1

        public static readonly Guid WebServiceTokenGuidForParameterizedReplacement = new Guid("535C44A1-A76C-40E0-BFF5-5A081BCC305A");

        public WebServiceToken(NeptuneDbContext dbContext)
        {
            const bool isBeingCalledByStaticConstructor = true;
            WebServiceTokenForUnitTests = new WebServiceToken(dbContext, WebServiceTokenGuidForUnitTests.ToString(), isBeingCalledByStaticConstructor);
            // this should always be available, but it's also not a real web service token, we only use it for replacements
            WebServiceTokenForParameterizedReplacements = new WebServiceToken(dbContext, WebServiceTokenGuidForParameterizedReplacement.ToString(), isBeingCalledByStaticConstructor);
        }

        /// <summary>
        /// Indicates if the token is valid in these circumstances as a unit test token, has to be the same GUID as <see cref="WebServiceTokenGuidForUnitTests"/>
        /// </summary>
        private static bool IsValidAsUnitTestToken(Guid tokenGuidToCheck, bool isBeingCalledByStaticConstructor)
        {
            return (tokenGuidToCheck == WebServiceTokenGuidForUnitTests && isBeingCalledByStaticConstructor);
        }

        /// <summary>
        ///Throws an exception if the string <param name="allegedWebServiceToken"/> is not valid as a <see cref="WebServiceToken"/>
        /// </summary>
        private static Guid DemandValidWebServiceToken(NeptuneDbContext dbContext, string allegedWebServiceToken, bool isBeingCalledByStaticConstructor)
        {
            Check.Require(TryParseGuid(allegedWebServiceToken, out var tokenGuid),
                $"The provided token {WebServiceTokenModelBinder.WebServiceTokenParameterName} = \"{allegedWebServiceToken}\" is not a GUID.");

            if (IsValidAsUnitTestToken(tokenGuid, isBeingCalledByStaticConstructor))
            {
                return tokenGuid;
            }

            Check.Require(tokenGuid != WebServiceTokenGuidForUnitTests, "Code appears to be trying to use the unit test web service token inappropriately, check environments and callers - that GUID is restricted use.");

            Check.RequireNotNull(People.GetByWebServiceAccessToken(dbContext, tokenGuid),
                $"The provided token {WebServiceTokenModelBinder.WebServiceTokenParameterName} = \"{allegedWebServiceToken}\" is not associated with a person.");
            return tokenGuid;
        }

        private static bool TryParseGuid(string stringToParse, out Guid parsedGuid)
        {
            parsedGuid = Guid.Empty;
            try
            {
                parsedGuid = new Guid(stringToParse);
            }
            catch
            {
                // Deliberately suppress exception and return false for "bad parse"
                return false;
            }
            return true;
        }


        private readonly Person _person;
        private readonly Guid _tokenGuid;

        public WebServiceToken(NeptuneDbContext dbContext, string allegedWebServiceToken) : this(dbContext, allegedWebServiceToken, false)
        {
        }

        private WebServiceToken(NeptuneDbContext dbContext, string allegedWebServiceToken, bool isBeingCalledByStaticConstructor)
        {
            var guidPassedIn = DemandValidWebServiceToken(dbContext, allegedWebServiceToken, isBeingCalledByStaticConstructor);
            _tokenGuid = guidPassedIn;

            if (IsValidAsUnitTestToken(_tokenGuid, isBeingCalledByStaticConstructor))
            {
                _person = People.GetByID(dbContext, 3); // TODO: Laryea's ID; might want to make a system person?
            }
            else
            {
                _person = People.GetByWebServiceAccessToken(dbContext, _tokenGuid);
            }
            Check.EnsureNotNull(_person, $"Could not find valid person for WebServiceToken {_tokenGuid}");
        }

        /// <summary>
        /// Returns the <see cref="EFModels.Entities.Person.PersonID"/> associated with this <see cref="WebServiceToken"/>.
        /// In unit test situation using <see cref="WebServiceTokenGuidForUnitTests"/> that would be Laryea's person ID for now
        /// Might want to introduce a system person at some point.
        /// </summary>
        public Person Person => _person;
        public bool IsWebServiceTokenForUnitTests => _tokenGuid == WebServiceTokenGuidForUnitTests;
        public override string ToString()
        {
            return _tokenGuid.ToString();
        }

        /// <summary>
        /// Throws an exception if the <see cref="Person"/> associated with this <see cref="WebServiceToken"/> does not have access to <see cref="LakeTahoeInfoBaseFeature" />
        /// In a unit test using <see cref="WebServiceTokenGuidForUnitTests"/> this will always pass, and <see cref="Person"/> will return Laryea's person ID for now
        /// Might want to introduce a system person at some point.
        /// </summary>
        /// <param name="feature"></param>
        public void DemandHasPermission(NeptuneFeature feature)
        {
            if (IsValidAsUnitTestToken(_tokenGuid, false))
            {
                // We consider the Unit Test one good if it's in the right environment
                return;
            }

            Check.RequireNotNull(_person, 
                $"The provided token {WebServiceTokenModelBinder.WebServiceTokenParameterName} = \"{_tokenGuid}\" is not associated with a person. Cannot check for access to the requested feature.");
            
            var hasPermission = feature.HasPermissionByPerson(_person);
            Check.Require(hasPermission,
                $"Web service token \"{_tokenGuid}\" is for person \"{_person.GetFullNameFirstLast()}\" PersonID={_person.PersonID}, but that person does not have access to the requested feature.");
        }

        public class WebServiceTokenModelBinder : SitkaModelBinder
        {
            /// <summary>
            /// Name of the required parameters on the controller actions
            /// </summary>
            public const string WebServiceTokenParameterName = "webServiceToken";
            public WebServiceTokenModelBinder(NeptuneDbContext dbContext) : base(s => new WebServiceToken(dbContext, s)) { }
        }
    }
}