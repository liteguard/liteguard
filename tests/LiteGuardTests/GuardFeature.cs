// Copyright (c) 2013 Adam Ralph.

namespace LiteGuardTests.Acceptance
{
    using System;
    using LiteGuard;
    using Xbehave;
    using Xunit;

    public static class GuardFeature
    {
        [Scenario]
        public static void NullArgument(string parameterName, object argument, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a null argument"
                .x(() => argument = null);

            "When guarding against a null argument"
                .x(() => exception = Record.Exception(() => Guard.AgainstNullArgument(parameterName, argument)));

            "Then the exception should be an argument null exception"
                .x(() => Assert.IsType<ArgumentNullException>(exception));

            "And the exception message should contain the parameter name and \"null\""
                .x(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains("null", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .x(() => Assert.Equal(parameterName, ((ArgumentException)exception).ParamName));
        }

        [Scenario]
        public static void NullNullableValueTypeArgument(string parameterName, int? argument, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a null argument"
                .x(() => argument = null);

            "When guarding against a null argument"
                .x(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentIfNullable(parameterName, argument)));

            "Then the exception should be an argument null exception"
                .x(() => Assert.IsType<ArgumentNullException>(exception));

            "And the exception message should contain the parameter name and \"null\""
                .x(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains("null", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .x(() => Assert.Equal(parameterName, ((ArgumentException)exception).ParamName));
        }

        [Scenario]
        public static void NullArgumentProperty(
            string parameterName, string propertyName, object propertyValue, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a property name"
                .x(() => propertyName = "Bar");

            "And a null property value"
                .x(() => propertyValue = null);

            "When guarding against a null argument property"
                .x(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentProperty(parameterName, propertyName, propertyValue)));

            "Then the exception should be an argument exception"
                .x(() => Assert.IsType<ArgumentException>(exception));

            "And the exception message should contain the parameter name, the property name and \"null\""
                .x(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains(propertyName, exception.Message);
                    Assert.Contains("null", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .x(() => Assert.Equal(parameterName, ((ArgumentException)exception).ParamName));
        }

        [Scenario]
        public static void NullGenericArgument(string parameterName, object argument, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a null argument"
                .x(() => argument = null);

            "When guarding against a null argument"
                .x(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentIfNullable(parameterName, argument)));

            "Then the exception should be an argument null exception"
                .x(() => Assert.IsType<ArgumentNullException>(exception));

            "And the exception message should contain the parameter name and \"null\""
                .x(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains("null", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .x(() => Assert.Equal(parameterName, ((ArgumentException)exception).ParamName));
        }

        [Scenario]
        public static void NullGenericArgumentProperty(
            string parameterName, string propertyName, object propertyValue, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a property name"
                .x(() => propertyName = "Bar");

            "And a null property value"
                .x(() => propertyValue = null);

            "When guarding against a null argument property"
                .x(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentPropertyIfNullable(parameterName, propertyName, propertyValue)));

            "Then the exception should be an argument exception"
                .x(() => Assert.IsType<ArgumentException>(exception));

            "And the exception message should contain the parameter name, the property name and \"null\""
                .x(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains(propertyName, exception.Message);
                    Assert.Contains("null", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .x(() => Assert.Equal(parameterName, ((ArgumentException)exception).ParamName));
        }

        [Scenario]
        public static void NonNullArgument(string parameterName, object argument, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a non-null argument"
                .x(() => argument = new object());

            "When guarding against a null argument"
                .x(() => exception = Record.Exception(() => Guard.AgainstNullArgument(parameterName, argument)));

            "Then no exception should be thrown"
                .x(() => Assert.Null(exception));
        }

        [Scenario]
        public static void NonNullNullableValueTypeArgument(string parameterName, int? argument, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a non-null argument"
                .x(() => argument = 123);

            "When guarding against a null argument"
                .x(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentIfNullable(parameterName, argument)));

            "Then no exception should be thrown"
                .x(() => Assert.Null(exception));
        }

        [Scenario]
        public static void NonNullArgumentProperty(
            string parameterName, string propertyName, object propertyValue, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a property name"
                .x(() => propertyName = "Bar");

            "And a non-null property value"
                .x(() => propertyValue = new object());

            "When guarding against a null argument property"
                .x(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentProperty(parameterName, propertyName, propertyValue)));

            "Then no exception should be thrown"
                .x(() => Assert.Null(exception));
        }

        [Scenario]
        [Example("bar")]
        [Example(123)]
        public static void NonNullGenericArgument<TArgument>(
            TArgument argument, string parameterName, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And an argument of '{0}'"
                .x(() => { });

            "When guarding against a null argument"
                .x(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentIfNullable(parameterName, argument)));

            "Then no exception should be thrown"
                .x(() => Assert.Null(exception));
        }

        [Scenario]
        [Example("bar")]
        [Example(123)]
        public static void NonNullGenericArgumentProperty<TProperty>(
            TProperty propertyValue, string parameterName, string propertyName, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a property name"
                .x(() => propertyName = "Bar");

            "And a non-null property value of '{0}'"
                .x(() => { });

            "When guarding against a null argument property"
                .x(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentPropertyIfNullable(parameterName, propertyName, propertyValue)));

            "Then no exception should be thrown"
                .x(() => Assert.Null(exception));
        }

        [Scenario]
        public static void NullString(string parameterName, string argument, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a null string"
                .x(() => argument = null);

            "When guarding against a null or empty string"
                .x(() => exception = Record.Exception(() => Guard.AgainstNullOrEmptyString(parameterName, argument)));

            "Then the exception should be an argument null exception"
                .x(() => Assert.IsType<ArgumentNullException>(exception));

            "And the exception message should contain the parameter name and \"null\""
                .x(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains("null", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .x(() => Assert.Equal(parameterName, ((ArgumentNullException)exception).ParamName));
        }

        [Scenario]
        public static void EmptyString(string parameterName, string argument, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And an empty string"
                .x(() => argument = string.Empty);

            "When guarding against an empty string"
                .x(() => exception = Record.Exception(() => Guard.AgainstEmptyString(parameterName, argument)));

            "Then the exception should be an argument exception"
                .x(() => Assert.IsType<ArgumentException>(exception));

            "And the exception message should contain the parameter name and \"empty string\""
                .x(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains("empty string", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .x(() => Assert.Equal(parameterName, ((ArgumentException)exception).ParamName));
        }

        [Scenario]
        public static void NonEmptyNonWhiteSpaceString(string parameterName, string argument, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a non-empty string"
                .x(() => argument = "foobar");

            "When guarding against an empty string"
                .x(() => exception = Record.Exception(() => Guard.AgainstEmptyString(parameterName, argument)));

            "Then no exception should be thrown"
                .x(() => Assert.Null(exception));
        }

        [Scenario]
        public static void WhiteSpaceString(string parameterName, string argument, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a string consisting of white-space characters"
                .x(() => argument = "   ");

            "When guarding against an empty string"
                .x(() => exception = Record.Exception(() => Guard.AgainstEmptyString(parameterName, argument)));

            "Then no exception should be thrown"
                .x(() => Assert.Null(exception));
        }

        [Scenario]
        public static void AgainsNullOrWhiteSpaceString_WhiteSpace(string parameterName, string argument, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a string consisting of white-space characters"
                .x(() => argument = "   ");

            "When guarding against a null or white-space string"
                .x(() => exception = Record.Exception(() => Guard.AgainstNullOrWhiteSpaceString(parameterName, argument)));

            "Then the exception should be an argument exception"
                .x(() => Assert.IsType<ArgumentException>(exception));

            "And the exception message should contain the parameter name and \"white-space string\""
                .x(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains("white-space string", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .x(() => Assert.Equal(parameterName, ((ArgumentException)exception).ParamName));
        }

        [Scenario]
        public static void AgainsNullOrWhiteSpaceString_Null(string parameterName, string argument, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a null string"
                .x(() => argument = null);

            "When guarding against a null or white-space string"
                .x(() => exception = Record.Exception(() => Guard.AgainstNullOrWhiteSpaceString(parameterName, argument)));

            "Then the exception should be an argument null exception"
                .x(() => Assert.IsType<ArgumentNullException>(exception));

            "And the exception message should contain the parameter name and \"null\""
                .x(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains("null", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .x(() => Assert.Equal(parameterName, ((ArgumentNullException)exception).ParamName));
        }

        [Scenario]
        public static void AgainsNullOrWhiteSpaceString_ValidString(string parameterName, string argument, Exception exception)
        {
            "Given a parameter name"
                .x(() => parameterName = "foo");

            "And a valid string"
                .x(() => argument = "foobaz");

            "When guarding against a null or white-space string"
                .x(() => exception = Record.Exception(() => Guard.AgainstNullOrWhiteSpaceString(parameterName, argument)));

            "Then no exception should be thrown"
                .x(() => Assert.Null(exception));
        }
    }
}
