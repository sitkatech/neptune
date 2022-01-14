/*-----------------------------------------------------------------------
<copyright file="NeptuneValidationMessages.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using Neptune.Web.Models;

namespace Neptune.Web.Common
{
    public static class NeptuneValidationMessages
    {
        public static readonly string PasswordRequiredForNewUser = $"{FieldDefinitionType.Password.GetFieldDefinitionLabel()} is required for new users";
        public static readonly string MustEnterOldPasswordWhenChangingPassword = $"Old {FieldDefinitionType.Password.GetFieldDefinitionLabel()} is required when changing {FieldDefinitionType.Password.GetFieldDefinitionLabel()}";
        public static readonly string OldPasswordProvidedIsNotCorrect = $"Old {FieldDefinitionType.Password.GetFieldDefinitionLabel()} is incorrect";
        public static readonly string CantChangePasswordToBeSameAsOldPassword = $"Can't reset {FieldDefinitionType.Password.GetFieldDefinitionLabel()} to same as old {FieldDefinitionType.Password.GetFieldDefinitionLabel()}";
        public static readonly string UpdateSectionIsDependentUponBasicsSection = "Your project's \"Basics\" page must be complete before you can begin updating this section.";
        public static readonly string OrganizationNameUnique = $"{FieldDefinitionType.Organization.GetFieldDefinitionLabel()} name already exists";
        public static readonly string OrganizationShortNameUnique = $"{FieldDefinitionType.Organization.GetFieldDefinitionLabel()} short name already exists";
        public static readonly string OrganizationMustBeSetForNewUser = $"{FieldDefinitionType.Organization.GetFieldDefinitionLabel()} must be set for a new user";
        public const string LettersNumbersSpacesDashesAndUnderscoresOnly = "Only letters, numbers, spaces, dashes and underscores are allowed.";
        public const string LettersOnly = "Only letters are allowed.";
        public const string MoreThanOneProjectUpdateInProgress = "Cannot determine latest update state; more than one update is in progress!";
        public const string EmailAlreadyUsed = "Email already exists";
        public const string ExpectedFundingValuesCannotBothBeZeroOrEmpty = "Enter a Secured or Unsecured amount for each Funding Source, or remove Funding Sources with no funding amounts.";
        public static readonly string FundingSourceNameUnique = $"{FieldDefinitionType.FundingSource.GetFieldDefinitionLabel()} name already exists.";
    }
}

