/*-----------------------------------------------------------------------
<copyright file="PercentageTest.cs" company="Sitka Technology Group">
Copyright (c) Sitka Technology Group. All rights reserved.
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
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptune.WebMvc.Common.Models;

namespace Neptune.Tests
{
    [TestClass]
    public class PercentageTests
    {
        [TestMethod]
        public void PercentageHasValueEquality()
        {
            var percentage1 = new Percentage(.57M);
            var percentage2 = new Percentage(.57M);

            Assert.AreEqual(percentage2, percentage1);
            Assert.AreNotSame(percentage2, percentage1);
        }

        [TestMethod]
        public void PercentageImplicitlyConvertsFromPrimitiveNumbers()
        {
            Percentage percentage;
            const byte byteValue = 0;
            percentage = byteValue;
            Assert.AreEqual(percentage, new Percentage(0));

            const sbyte sByteValue = 1;
            percentage = sByteValue;
            Assert.AreEqual(percentage, new Percentage(1));

            const short int16Value = 0;
            percentage = int16Value;
            Assert.AreEqual(percentage, new Percentage(0));

            const int int32Value = 1;
            percentage = int32Value;
            Assert.AreEqual(percentage, new Percentage(1));

            const long int64Value = 1;
            percentage = int64Value;
            Assert.AreEqual(percentage, new Percentage(1));

            const ushort uInt16Value = 0;
            percentage = uInt16Value;
            Assert.AreEqual(percentage, new Percentage(0));

            const uint uInt32Value = 1;
            percentage = uInt32Value;
            Assert.AreEqual(percentage, new Percentage(1));

            const ulong uInt64Value = 1;
            percentage = uInt64Value;
            Assert.AreEqual(percentage, new Percentage(1));

            const float singleValue = 0;
            percentage = singleValue;
            Assert.AreEqual(percentage, new Percentage(0));

            const double doubleValue = .65;
            percentage = doubleValue;
            Assert.AreEqual(percentage, new Percentage(.65M));

            const decimal decimalValue = .77m;
            percentage = decimalValue;
            Assert.AreEqual(percentage, new Percentage(.77m));
        }

        [TestMethod]
        public void PercentageWholeAmountAdditionIsCorrect()
        {
            // whole number
            Percentage percentage1 = 0M;
            Percentage percentage2 = 1M;

            Assert.AreEqual(percentage1 + percentage2, new Percentage(1));
        }

        [TestMethod]
        public void PercentageFractionalAmountAdditionIsCorrect()
        {
            // fractions
            Percentage percentage1 = .22M;
            Percentage percentage2 = .71M;

            Assert.AreEqual(percentage1 + percentage2, new Percentage(.93M));
        }

        [TestMethod]
        public void PercentageWholeAmountSubtractionIsCorrect()
        {
            // whole number
            Percentage percentage1 = 1M;
            Percentage percentage2 = 1M;

            Assert.AreEqual(percentage1 - percentage2, new Percentage(0));
        }

        [TestMethod]
        public void PercentageFractionalAmountSubtractionIsCorrect()
        {
            // fractions
            Percentage percentage1 = .45M;
            Percentage percentage2 = 0.01M;

            Assert.AreEqual(percentage1 - percentage2, new Percentage(.44M));
        }

        [TestMethod]
        public void PercentageScalarWholeMultiplicationIsCorrect()
        {
            Percentage percentage = .125;

            Assert.AreEqual(percentage * 4, new Percentage(.5M));
        }

        [TestMethod]
        public void PercentageScalarFractionalMultiplicationIsCorrect()
        {
            Percentage percentage = .88125;

            Assert.AreEqual(percentage * 0.5M, new Percentage(.440625M));
        }

        [TestMethod]
        public void PercentageScalarWholeDivisionIsCorrect()
        {
            Percentage percentage = .75;

            Assert.AreEqual(percentage / 2, new Percentage(.375M));
        }

        [TestMethod]
        public void PercentageScalarFractionalDivisionIsCorrect()
        {
            Percentage percentage = .125;

            Assert.AreEqual(percentage / 0.5M, new Percentage(.25M));
        }

        [TestMethod]
        public void PercentageEqualOperatorIsCorrect()
        {
            Percentage percentage1 = .125;
            Percentage percentage2 = .125;

            Assert.IsTrue(percentage1 == percentage2);

            percentage2 = .135;
            Assert.IsFalse(percentage1 == percentage2);

            percentage2 = .25;
            Assert.IsFalse(percentage1 == percentage2);
        }

        [TestMethod]
        public void PercentageNotEqualOperatorIsCorrect()
        {
            Percentage percentage1 = .125;
            Percentage percentage2 = .125;

            Assert.IsFalse(percentage1 != percentage2);

            percentage2 = .135;
            Assert.IsTrue(percentage1 != percentage2);

            percentage2 = .25;
            Assert.IsTrue(percentage1 != percentage2);
        }

        [TestMethod]
        public void PercentageGreaterThanEqualOperatorIsCorrect()
        {
            Percentage percentage1 = .125;
            Percentage percentage2 = .125;

            Assert.IsTrue(percentage1 >= percentage2);

            percentage2 = .135;
            Assert.IsTrue(percentage2 >= percentage1);
            Assert.IsFalse(percentage1 >= percentage2);

            percentage2 = .25;
            Assert.IsTrue(percentage2 >= percentage1);
            Assert.IsFalse(percentage1 >= percentage2);
        }

        [TestMethod]
        public void PercentageLessThanEqualOperatorIsCorrect()
        {
            Percentage percentage1 = .125;
            Percentage percentage2 = .125;

            Assert.IsTrue(percentage1 <= percentage2);

            percentage2 = .135;
            Assert.IsFalse(percentage2 <= percentage1);
            Assert.IsTrue(percentage1 <= percentage2);

            percentage2 = .25;
            Assert.IsFalse(percentage2 <= percentage1);
            Assert.IsTrue(percentage1 <= percentage2);
        }

        [TestMethod]
        public void PercentageGreaterThanOperatorIsCorrect()
        {
            Percentage percentage1 = .125;
            Percentage percentage2 = .125;

            Assert.IsFalse(percentage1 > percentage2);

            percentage2 = .135;
            Assert.IsTrue(percentage2 > percentage1);
            Assert.IsFalse(percentage1 > percentage2);

            percentage2 = .25;
            Assert.IsTrue(percentage2 > percentage1);
            Assert.IsFalse(percentage1 > percentage2);
        }

        [TestMethod]
        public void PercentageLessThanOperatorIsCorrect()
        {
            Percentage percentage1 = .125;
            Percentage percentage2 = .125;

            Assert.IsFalse(percentage1 < percentage2);

            percentage2 = .135;
            Assert.IsFalse(percentage2 < percentage1);
            Assert.IsTrue(percentage1 < percentage2);

            percentage2 = .25;
            Assert.IsFalse(percentage2 < percentage1);
            Assert.IsTrue(percentage1 < percentage2);
        }

        [TestMethod]
        public void PercentagePrintsCorrectly()
        {
            var previousCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var percentage = new Percentage(.125M);
            var formattedPercentage = percentage.ToString();
            Assert.AreEqual(formattedPercentage, "12.50%");
            Thread.CurrentThread.CurrentCulture = previousCulture;
        }

        [TestMethod]
        public void PercentageTryParseIsCorrect()
        {
            Percentage actual;

            var result = Percentage.TryParse("76.54%", out actual);
            Assert.IsTrue(result);
            Assert.AreEqual(actual, new Percentage(.7654M));

            result = Percentage.TryParse("21.3%", out actual);
            Assert.IsTrue(result);
            Assert.AreEqual(actual, new Percentage(.213M));

            result = Percentage.TryParse(".368%", out actual);
            Assert.IsTrue(result);
            Assert.AreEqual(actual, new Percentage(.00368M));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Percentage.TryParse("101%", out actual), "We only do 0 to 100%");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Percentage.TryParse("-1%", out actual), "We only do 0 to 100%");
        }

        //[Test]
        //public void CanModelBind()
        //{
        //    var formCollection = new NameValueCollection
        //    {
        //        { "Budget", "$2,345.67" }
        //    };

        //    var boundModel = SetupAndBind<Percentage.PercentageModelBinder, Foo>(formCollection);
        //    Assert.AreEqual(new Percentage(2345.67m), boundModel.Budget);
        //}

        //private class Foo
        //{
        //    public Percentage? Budget { get; set; }
        //}

        //public static TModel SetupAndBind<TBinder, TModel>(NameValueCollection nameValueCollection) where TBinder : IModelBinder, new()
        //{
        //    var valueProvider = new NameValueCollectionValueProvider(nameValueCollection, null);
        //    var modelType = typeof(TModel);
        //    var modelMetaData = ModelMetadataProviders.Current.GetMetadataForType(null, modelType);
        //    var bindingContext = new ModelBindingContext { ModelName = modelType.Name, ValueProvider = valueProvider, ModelMetadata = modelMetaData, };
        //    return (TModel)new TBinder().BindModel(null, bindingContext);
        //}
    }
}
