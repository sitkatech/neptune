/*-----------------------------------------------------------------------
<copyright file="NeptuneFeatureTest.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using ApprovalTests;
using ApprovalTests.Reporters;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using LtInfo.Common.EntityModelBinding;
using NUnit.Framework;

namespace Neptune.Web.Security
{
    [TestFixture]
    public class NeptuneFeatureTest
    {
        private readonly Type _typeOfNeptuneFeatureWithContext = typeof(NeptuneFeatureWithContext);

        private readonly Type _typeOfNeptuneBaseFeature = typeof(NeptuneBaseFeature);
        private readonly Type _typeofINeptuneBaseFeatureWithContext = typeof(INeptuneBaseFeatureWithContext<>);
        private readonly Type _typeOfAllowAnonymousAttribute = typeof(AllowAnonymousAttribute);

        [Test]
        [Description("Each controller action should have exactly one feature on it. Less means it's not secure, more means that there is confusion over which feature wins out.")]
        public void EachControllerActionShouldHaveOneFeature()
        {
            var allControllerActionMethods = NeptuneBaseController.AllControllerActionMethods;

            var info = allControllerActionMethods.Select(method => new { Name = MethodName(method), FeatureCount = NumberOfNeptuneFeatureAttributesOnMethod(method) }).ToList();

            // Remove exceptions
            info = info.Where(x => x.Name != "JasmineController.Run").ToList();

            Assert.That(info.Where(x => x.FeatureCount == 0).ToList(), Is.Empty, string.Format("All should have at least one {0}", _typeOfNeptuneBaseFeature.Name));
            Assert.That(info.Where(x => x.FeatureCount > 1).ToList(), Is.Empty, string.Format("Should have no more than one{0}", _typeOfNeptuneBaseFeature.Name));
        }

        private static string MethodName(MethodInfo method)
        {
            // ReSharper disable PossibleNullReferenceException
            return method.DeclaringType.Name + "." + method.Name;
            // ReSharper restore PossibleNullReferenceException
        }

        private int NumberOfNeptuneFeatureAttributesOnMethod(MethodInfo method)
        {
            var list = method.GetCustomAttributes().ToList();
            var attributes = list.Where(a => a.GetType().IsSubclassOf(_typeOfNeptuneBaseFeature)).ToList();
            return attributes.Count;
        }

        [Test]
        [Description("All security features must be decorated with SecurityFeatureDescription")]
        public void SecurityFeaturesMustHaveDescription()
        {
            var baseFeatureClass = typeof(NeptuneBaseFeature);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => baseFeatureClass.IsAssignableFrom(p) && p.Name != baseFeatureClass.Name && !p.IsAbstract);

            var listOfSecurityFeaturesWithoutDescription = new List<string>();
            foreach (var type in types)
            {
                Attribute[] attributes = Attribute.GetCustomAttributes(type);
                if (!attributes.Any(x => x is SecurityFeatureDescription))
                {
                    listOfSecurityFeaturesWithoutDescription.Add(type.FullName);
                }
                else if (attributes.Where(x => x is SecurityFeatureDescription).Any(attr => ((SecurityFeatureDescription)attr).DescriptionMessage == ""))
                {
                    listOfSecurityFeaturesWithoutDescription.Add(type.FullName); //Also flag anything where the description is blank
                }
            }

            if (listOfSecurityFeaturesWithoutDescription.Count > 0)
            {
                Assert.Fail(Environment.NewLine + Environment.NewLine + String.Join(Environment.NewLine, listOfSecurityFeaturesWithoutDescription));
            }
        }

        [Test]
        [Description("Administrators should have access to all features in that area")]
        [UseReporter(typeof(DiffReporter))]
        public void AdministratorsCanAccessAllFeaturesAndUnassignedCantAccessAnyFeatures()
        {
            //If we start getting exceptions, then this should become an acceptance test
            var baseFeatureClass = typeof(NeptuneBaseFeature);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => baseFeatureClass.IsAssignableFrom(p) && p.Name != baseFeatureClass.Name && !p.IsAbstract);
            var listOfErrors = new List<string>();
            foreach (var type in types)
            {
                var obj = NeptuneBaseFeature.InstantiateFeature(type);
                if (!obj.GrantedRoles.Contains(Role.Admin) && obj.GrantedRoles.Count != 0)
                {
                    var errorMessage = String.Format("Feature {0} is not available to Administrators", type.FullName);
                    listOfErrors.Add(errorMessage);
                }

                //Validate Unassigned does NOT have access                
                if (obj.GrantedRoles.Contains(Role.Unassigned))
                {
                    string errorMessage = String.Format("Feature {0} is available to the Unassigned role", type.FullName);
                    listOfErrors.Add(errorMessage);
                }
            }

            if (listOfErrors.Count > 0)
            {
                string message = string.Format("{0}{0}{1}", Environment.NewLine, string.Join(Environment.NewLine, listOfErrors));
                Approvals.Verify(message);
            }
        }

        [Test]
        [Description("Sitka Administrators should have access to all features in that area")]
        [UseReporter(typeof(DiffReporter))]
        public void SitkaAdministratorsCanAccessAllFeaturesAndUnassignedCantAccessAnyFeatures()
        {
            //If we start getting exceptions, then this should become an acceptance test
            var baseFeatureClass = typeof(NeptuneBaseFeature);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => baseFeatureClass.IsAssignableFrom(p) && p.Name != baseFeatureClass.Name && !p.IsAbstract);
            var listOfErrors = new List<string>();
            foreach (var type in types)
            {
                var obj = NeptuneBaseFeature.InstantiateFeature(type);
                if (!obj.GrantedRoles.Contains(Role.SitkaAdmin) && obj.GrantedRoles.Count != 0)
                {
                    var errorMessage = String.Format("Feature {0} is not available to Administrators", type.FullName);
                    listOfErrors.Add(errorMessage);
                }

                //Validate Unassigned does NOT have access                
                if (obj.GrantedRoles.Contains(Role.Unassigned))
                {
                    string errorMessage = String.Format("Feature {0} is available to the Unassigned role", type.FullName);
                    listOfErrors.Add(errorMessage);
                }
            }

            if (listOfErrors.Count > 0)
            {
                string message = string.Format("{0}{0}{1}", Environment.NewLine, string.Join(Environment.NewLine, listOfErrors));
                Approvals.Verify(message);
            }
        }

        [Test]
        [Description("All context features have to follow a pattern that includes implementing an interface")]
        public void AllContextFeaturesImplementInterface()
        {
            //Get a list of all features inheriting from one of our four FeatureWithContext
            var neptuneFeatureWithContextTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => p.IsSubclassOf(_typeOfNeptuneFeatureWithContext)).Select(t => t.FullName).ToList();

            //Get a list of all features inheriting from the INeptuneBaseFeatureWithContext interface
            var iNeptuneFeatureTypeWithContextTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => p.GetInterfaces().Any(i => HasInterface(i, _typeofINeptuneBaseFeatureWithContext))).Select(t => t.FullName).ToList();

            //The two lists should be the same
            Assert.That(neptuneFeatureWithContextTypes, Is.EquivalentTo(iNeptuneFeatureTypeWithContextTypes), string.Format("All features of type {0} must implement {1}", _typeOfNeptuneFeatureWithContext.Name, _typeofINeptuneBaseFeatureWithContext.Name));
        }

        private static bool HasInterface(Type i, Type iNeptuneFeatureTypeWithContext)
        {
            return i.IsGenericType && i.GetGenericTypeDefinition() == iNeptuneFeatureTypeWithContext;
        }

        [Test]
        [Description("All context features have to follow a pattern that includes implementing an interface")]
        public void ControllerActionsWithContextFeatureHasParameterAlignment()
        {
            var allControllerActionMethods = NeptuneBaseController.AllControllerActionMethods;
            var releventControllerActions = allControllerActionMethods.Where(x => !AreControllerActionParametersAlignedWithFeature(x)).ToList();
            Assert.That(releventControllerActions.Select(MethodName).ToList(), Is.Empty, "Found some controller actions without proper alignment with parameters");
        }

        private bool AreControllerActionParametersAlignedWithFeature(MethodInfo method)
        {
            // Is this a context feature on the controller action?
            var list = method.GetCustomAttributes().ToList();
            var attributes = list.Where(a => a.GetType().IsSubclassOf(_typeOfNeptuneFeatureWithContext)).ToList();
            if (!attributes.Any())
            {
                return true;
            }
            Assert.That(attributes.Count == 1, string.Format("Method had more than one {0}", _typeOfNeptuneFeatureWithContext.Name));
            var attribute = attributes.Single();

            // Does it have a matching parameter?
            var featureType = attribute.GetType();
            var allInterfaces = featureType.GetInterfaces().ToList();
            var matchingInterfaces = allInterfaces.Where(t => HasInterface(t, _typeofINeptuneBaseFeatureWithContext)).ToList();

            Assert.That(matchingInterfaces.Count == 1, string.Format("Feature type {0} doesn't implement {1}", featureType.Name, _typeofINeptuneBaseFeatureWithContext));

            var contextInterface = matchingInterfaces.Single();
            var entityObjectType = contextInterface.GetGenericArguments().First();
            var expectedParameterType = typeof(LtInfoEntityPrimaryKey<>).MakeGenericType(entityObjectType);

            var matchingParameters = method.GetParameters().Where(p => p.ParameterType == expectedParameterType || p.ParameterType.IsSubclassOf(expectedParameterType)).ToList();
            Assert.That(matchingParameters.Count < 2, string.Format("Method {0} has more than one parameter that aligns with doesn't implement {1}", featureType.Name, _typeofINeptuneBaseFeatureWithContext));
            return matchingParameters.Count == 1;
        }

        [Test]
        public void NotUsingAllowAnonymousAttribute()
        {
            var allControllerActionMethods = NeptuneBaseController.AllControllerActionMethods;
            var usingAllowAnonymous = allControllerActionMethods.Where(m => m.GetCustomAttributes().Any(a => a.GetType() == _typeOfAllowAnonymousAttribute || a.GetType().IsSubclassOf(_typeOfAllowAnonymousAttribute))).ToList();

            var x = usingAllowAnonymous.Select(MethodName).ToList();
            Assert.That(x, Is.Empty, string.Format("Found some uses of \"{0}\", should be using types of \"{1}\" only.", _typeOfAllowAnonymousAttribute.FullName, _typeOfNeptuneBaseFeature.FullName));
        }
    }
}
