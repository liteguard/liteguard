// <copyright file="GuardFeature.cs" company="LiteGuard contributors">
//  Copyright (c) LiteGuard contributors. All rights reserved.
// </copyright>

namespace LiteGuard.Test.Acceptance
{
    using System;
    using Xbehave;
    using Xunit;

    public static class GuardFeature
    {
        [Scenario]
        public static void NullArgument(string parameterName, object argument, Exception exception)
        {
            "Given a parameter name"
                .f(() => parameterName = "foo");

            "And a null argument"
                .f(() => argument = null);

            "When guarding against a null argument"
                .f(() => exception = Record.Exception(() => Guard.AgainstNullArgument(parameterName, argument)));

            "Then the exception should be an argument null exception"
                .f(() => Assert.IsType<ArgumentNullException>(exception));

            "And the exception message should contain the parameter name and \"null\""
                .f(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains("null", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .f(() => Assert.Equal(parameterName, ((ArgumentException)exception).ParamName));
        }

        [Scenario]
        public static void NullNullableValueTypeArgument(string parameterName, int? argument, Exception exception)
        {
            "Given a parameter name"
                .f(() => parameterName = "foo");

            "And a null argument"
                .f(() => argument = null);

            "When guarding against a null argument"
                .f(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentIfNullable(parameterName, argument)));

            "Then the exception should be an argument null exception"
                .f(() => Assert.IsType<ArgumentNullException>(exception));

            "And the exception message should contain the parameter name and \"null\""
                .f(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains("null", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .f(() => Assert.Equal(parameterName, ((ArgumentException)exception).ParamName));
        }

        [Scenario]
        public static void NullArgumentProperty(
            string parameterName, string propertyName, object propertyValue, Exception exception)
        {
            "Given a parameter name"
                .f(() => parameterName = "foo");

            "And a property name"
                .f(() => propertyName = "Bar");

            "And a null property value"
                .f(() => propertyValue = null);

            "When guarding against a null argument property"
                .f(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentProperty(parameterName, propertyName, propertyValue)));

            "Then the exception should be an argument exception"
                .f(() => Assert.IsType<ArgumentException>(exception));

            "And the exception message should contain the parameter name, the property name and \"null\""
                .f(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains(propertyName, exception.Message);
                    Assert.Contains("null", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .f(() => Assert.Equal(parameterName, ((ArgumentException)exception).ParamName));
        }

        [Scenario]
        public static void NullGenericArgument(string parameterName, object argument, Exception exception)
        {
            "Given a parameter name"
                .f(() => parameterName = "foo");

            "And a null argument"
                .f(() => argument = null);

            "When guarding against a null argument"
                .f(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentIfNullable(parameterName, argument)));

            "Then the exception should be an argument null exception"
                .f(() => Assert.IsType<ArgumentNullException>(exception));

            "And the exception message should contain the parameter name and \"null\""
                .f(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains("null", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .f(() => Assert.Equal(parameterName, ((ArgumentException)exception).ParamName));
        }

        [Scenario]
        public static void NullGenericArgumentProperty(
            string parameterName, string propertyName, object propertyValue, Exception exception)
        {
            "Given a parameter name"
                .f(() => parameterName = "foo");

            "And a property name"
                .f(() => propertyName = "Bar");

            "And a null property value"
                .f(() => propertyValue = null);

            "When guarding against a null argument property"
                .f(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentPropertyIfNullable(parameterName, propertyName, propertyValue)));

            "Then the exception should be an argument exception"
                .f(() => Assert.IsType<ArgumentException>(exception));

            "And the exception message should contain the parameter name, the property name and \"null\""
                .f(() =>
                {
                    Assert.Contains(parameterName, exception.Message);
                    Assert.Contains(propertyName, exception.Message);
                    Assert.Contains("null", exception.Message);
                });

            "And the exception parameter name should be the parameter name"
                .f(() => Assert.Equal(parameterName, ((ArgumentException)exception).ParamName));
        }

        [Scenario]
        public static void NonNullArgument(string parameterName, object argument, Exception exception)
        {
            "Given a parameter name"
                .f(() => parameterName = "foo");

            "And a non-null argument"
                .f(() => argument = new object());

            "When guarding against a null argument"
                .f(() => exception = Record.Exception(() => Guard.AgainstNullArgument(parameterName, argument)));

            "Then no exception should be thrown"
                .f(() => Assert.Null(exception));
        }

        [Scenario]
        public static void NonNullNullableValueTypeArgument(string parameterName, int? argument, Exception exception)
        {
            "Given a parameter name"
                .f(() => parameterName = "foo");

            "And a non-null argument"
                .f(() => argument = 123);

            "When guarding against a null argument"
                .f(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentIfNullable(parameterName, argument)));

            "Then no exception should be thrown"
                .f(() => Assert.Null(exception));
        }

        [Scenario]
        public static void NonNullArgumentProperty(
            string parameterName, string propertyName, object propertyValue, Exception exception)
        {
            "Given a parameter name"
                .f(() => parameterName = "foo");

            "And a property name"
                .f(() => propertyName = "Bar");

            "And a non-null property value"
                .f(() => propertyValue = new object());

            "When guarding against a null argument property"
                .f(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentProperty(parameterName, propertyName, propertyValue)));

            "Then no exception should be thrown"
                .f(() => Assert.Null(exception));
        }

        [Scenario]
        [Example("bar")]
        [Example(123)]
        public static void NonNullGenericArgument<TArgument>(
            TArgument argument, string parameterName, Exception exception)
        {
            "Given a parameter name"
                .f(() => parameterName = "foo");

            "And an argument of '{0}'"
                .f(() => { });

            "When guarding against a null argument"
                .f(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentIfNullable(parameterName, argument)));

            "Then no exception should be thrown"
                .f(() => Assert.Null(exception));
        }

        [Scenario]
        [Example("bar")]
        [Example(123)]
        public static void NonNullGenericArgumentProperty<TProperty>(
            TProperty propertyValue, string parameterName, string propertyName, Exception exception)
        {
            "Given a parameter name"
                .f(() => parameterName = "foo");

            "And a property name"
                .f(() => propertyName = "Bar");

            "And a non-null property value of '{0}'"
                .f(() => { });

            "When guarding against a null argument property"
                .f(() => exception = Record.Exception(() =>
                    Guard.AgainstNullArgumentPropertyIfNullable(parameterName, propertyName, propertyValue)));

            "Then no exception should be thrown"
                .f(() => Assert.Null(exception));
        }
    }
}
