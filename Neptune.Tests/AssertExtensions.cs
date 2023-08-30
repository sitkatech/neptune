using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Neptune.Tests;

internal static class AssertExtensions
{
    public static void DoesNotThrow(this Assert assert, Action a)
    {
        try
        {
            a();
        }
        catch
        {
            Assert.Fail("Expected no exception to be thrown");
        }
    }
}