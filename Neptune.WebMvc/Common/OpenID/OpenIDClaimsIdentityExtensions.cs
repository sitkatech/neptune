using System.Security.Claims;

namespace Neptune.WebMvc.Common.OpenID;

public static class OpenIDClaimsIdentityExtensions
{
    /// <summary>
    /// Finds all instances of the specified claim.
    /// </summary>
    /// <param name="identity">The identity.</param><param name="predicate">The search predicate.</param>
    /// <returns>
    /// List of claims that match the search criteria
    /// </returns>
    public static IEnumerable<Claim> FindClaims(this ClaimsIdentity identity, Predicate<Claim> predicate)
    {
        return identity.Claims.Where(claim => predicate(claim));
    }

    /// <summary>
    /// Finds all instances of the specified claim.
    /// </summary>
    /// <param name="identity">The identity.</param><param name="Type">Type of the claim.</param>
    /// <returns>
    /// List of claims that match the search criteria
    /// </returns>
    public static IEnumerable<Claim> FindClaims(this ClaimsIdentity identity, string Type)
    {
        return FindClaims(identity, x => x.Type.Equals(Type, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Finds all instances of the specified claim.
    /// </summary>
    /// <param name="identity">The identity.</param><param name="Type">Type of the claim.</param><param name="issuer">The issuer.</param>
    /// <returns>
    /// List of claims that match the search criteria
    /// </returns>
    public static IEnumerable<Claim> FindClaims(this ClaimsIdentity identity, string Type, string issuer)
    {
        return FindClaims(identity, x => x.Type.Equals(Type, StringComparison.OrdinalIgnoreCase) && x.Issuer.Equals(issuer, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Finds all instances of the specified claim.
    /// </summary>
    /// <param name="identity">The identity.</param><param name="Type">Type of the claim.</param><param name="issuer">The issuer.</param><param name="value">The value.</param>
    /// <returns>
    /// List of claims that match the search criteria
    /// </returns>
    public static IEnumerable<Claim> FindClaims(this ClaimsIdentity identity, string Type, string issuer, string value)
    {
        return FindClaims(identity, x => x.Type.Equals(Type, StringComparison.OrdinalIgnoreCase) && x.Value.Equals(value, StringComparison.OrdinalIgnoreCase) && x.Issuer.Equals(issuer, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Finds all instances of the specified claim.
    /// </summary>
    /// <param name="identity">The identity.</param><param name="claim">Search claim.</param>
    /// <returns>
    /// List of claims that match the search criteria
    /// </returns>
    public static IEnumerable<Claim> FindClaims(this ClaimsIdentity identity, Claim claim)
    {
        return FindClaims(identity, x => x.Type.Equals(claim.Type, StringComparison.OrdinalIgnoreCase) && x.Value.Equals(claim.Value, StringComparison.OrdinalIgnoreCase) && x.Issuer.Equals(claim.Issuer, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Retrieves the issuer name of an ClaimsIdentity. The algorithm checks the name claim first, and if no name is found, the first claim.
    /// </summary>
    /// <param name="identity">The identity.</param>
    /// <returns>
    /// The issuer name
    /// </returns>
    public static string GetIssuerName(this ClaimsIdentity identity)
    {
        var claim1 = FindClaims(identity, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault();
        if (claim1 != null && claim1.Issuer != null)
            return claim1.Issuer;

        if (identity.Claims.Any())
        {
            var claim2 = identity.Claims.First();
            if (claim2 != null && claim2.Issuer != null)
                return claim2.Issuer;
        }

        return string.Empty;
    }

    /// <summary>
    /// Checks whether a given claim exists
    /// </summary>
    /// <param name="identity">The identity.</param><param name="predicate">The search predicate.</param>
    /// <returns>
    /// true/false
    /// </returns>
    public static bool ClaimExists(this ClaimsIdentity identity, Predicate<Claim> predicate)
    {
        return FindClaims(identity, predicate).FirstOrDefault() != null;
    }

    /// <summary>
    /// Checks whether a given claim exists
    /// </summary>
    /// <param name="identity">The identity.</param><param name="Type">Type of the claim.</param>
    /// <returns>
    /// true/false
    /// </returns>
    public static bool ClaimExists(this ClaimsIdentity identity, string Type)
    {
        return ClaimExists(identity, x => x.Type.Equals(Type, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Checks whether a given claim exists
    /// </summary>
    /// <param name="identity">The identity.</param><param name="Type">Type of the claim.</param><param name="value">The value.</param>
    /// <returns>
    /// true/false
    /// </returns>
    public static bool ClaimExists(this ClaimsIdentity identity, string Type, string value)
    {
        return ClaimExists(identity, x => x.Type.Equals(Type, StringComparison.OrdinalIgnoreCase) && x.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Checks whether a given claim exists
    /// </summary>
    /// <param name="identity">The identity.</param><param name="Type">Type of the claim.</param><param name="value">The value.</param><param name="issuer">The issuer.</param>
    /// <returns>
    /// true/false
    /// </returns>
    public static bool ClaimExists(this ClaimsIdentity identity, string Type, string value, string issuer)
    {
        return ClaimExists(identity, x => x.Type.Equals(Type, StringComparison.OrdinalIgnoreCase) && x.Value.Equals(value, StringComparison.OrdinalIgnoreCase) && x.Issuer.Equals(issuer, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Retrieves the value of a claim.
    /// </summary>
    /// <param name="identity">The identity.</param><param name="Type">Type of the claim.</param>
    /// <returns>
    /// The value
    /// </returns>
    public static string GetClaimValue(this ClaimsIdentity identity, string Type)
    {
        string claimValue;
        if (TryGetClaimValue(identity, Type, out claimValue))
            return claimValue;

        throw new OpenIDClaimNotFoundException(Type);
    }

    /// <summary>
    /// Retrieves the value of a claim.
    /// </summary>
    /// <param name="identity">The identity.</param><param name="Type">Type of the claim.</param><param name="issuer">The issuer.</param>
    /// <returns>
    /// The value
    /// </returns>
    public static string GetClaimValue(this ClaimsIdentity identity, string Type, string issuer)
    {
        string claimValue;
        if (TryGetClaimValue(identity, Type, issuer, out claimValue))
            return claimValue;

        throw new OpenIDClaimNotFoundException(Type);
    }

    /// <summary>
    /// Tries to retrieve the value of a claim.
    /// </summary>
    /// <param name="identity">The identity.</param><param name="Type">Type of the claim.</param><param name="claimValue">The claim value.</param>
    /// <returns>
    /// The value
    /// </returns>
    public static bool TryGetClaimValue(this ClaimsIdentity identity, string Type, out string claimValue)
    {
        claimValue = null;

        var claim = FindClaims(identity, Type).FirstOrDefault();
        if (claim == null)
            return false;

        claimValue = claim.Value;

        return true;
    }

    /// <summary>
    /// Tries to retrieve the value of a claim.
    /// </summary>
    /// <param name="identity">The identity.</param><param name="Type">Type of the claim.</param><param name="issuer">The issuer.</param><param name="claimValue">The claim value.</param>
    /// <returns>
    /// The value
    /// </returns>
    public static bool TryGetClaimValue(this ClaimsIdentity identity, string Type, string issuer, out string claimValue)
    {
        claimValue = null;

        var claim = FindClaims(identity, Type, issuer).FirstOrDefault();
        if (claim == null)
            return false;

        claimValue = claim.Value;

        return true;
    }
}