// Copyright (c) 2013 Adam Ralph.

namespace LiteGuard
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    /// <summary>
    /// Provides guard clauses.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Guards against a null argument.
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="argument" /> is <c>null</c>.</exception>
        /// <remarks>
        /// <typeparamref name="TArgument"/> is restricted to reference types to avoid boxing of value type objects.
        /// </remarks>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Source package.")]
        [DebuggerStepThrough]
        public static void AgainstNullArgument<TArgument>(string parameterName, [ValidatedNotNull]TArgument argument)
            where TArgument : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(
                    parameterName, string.Format(CultureInfo.InvariantCulture, "{0} is null.", parameterName));
            }
        }

        /// <summary>
        /// Guards against a null argument if <typeparamref name="TArgument" /> can be <c>null</c>.
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="argument" /> is <c>null</c>.</exception>
        /// <remarks>
        /// Performs a type check to avoid boxing of value type objects.
        /// </remarks>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Source package.")]
        [DebuggerStepThrough]
        public static void AgainstNullArgumentIfNullable<TArgument>(
            string parameterName, [ValidatedNotNull]TArgument argument)
        {
#if NETSTANDARD2_0
            if (typeof(TArgument).IsNullableType() && argument == null)
#else
            if (ReferenceEquals(argument, null))
#endif
            {
                throw new ArgumentNullException(
                    parameterName, string.Format(CultureInfo.InvariantCulture, "{0} is null.", parameterName));
            }
        }

        /// <summary>
        /// Guards against a null argument property value.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="argumentProperty">The argument property.</param>
        /// <exception cref="System.ArgumentException"><paramref name="argumentProperty" /> is <c>null</c>.</exception>
        /// <remarks>
        /// <typeparamref name="TProperty"/> is restricted to reference types to avoid boxing of value type objects.
        /// </remarks>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Source package.")]
        [DebuggerStepThrough]
        public static void AgainstNullArgumentProperty<TProperty>(
            string parameterName, string propertyName, [ValidatedNotNull]TProperty argumentProperty)
            where TProperty : class
        {
            if (argumentProperty == null)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "{0}.{1} is null.", parameterName, propertyName),
                    parameterName);
            }
        }

        /// <summary>
        /// Guards against a null argument property value if <typeparamref name="TProperty"/> can be <c>null</c>.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="argumentProperty">The argument property.</param>
        /// <exception cref="System.ArgumentException"><paramref name="argumentProperty" /> is <c>null</c>.</exception>
        /// <remarks>
        /// Performs a type check to avoid boxing of value type objects.
        /// </remarks>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Source package.")]
        [DebuggerStepThrough]
        public static void AgainstNullArgumentPropertyIfNullable<TProperty>(
            string parameterName, string propertyName, [ValidatedNotNull]TProperty argumentProperty)
        {
#if NETSTANDARD2_0
            if (typeof(TProperty).IsNullableType() && argumentProperty == null)
#else
            if (ReferenceEquals(argumentProperty, null))
#endif
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "{0}.{1} is null.", parameterName, propertyName),
                    parameterName);
            }
        }

        /// <summary>
        /// Guards against an empty string.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="argument">The string to check</param>
        /// <exception cref="ArgumentException"><paramref name="argument" /> is the empty string.</exception>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Source package.")]
        [DebuggerStepThrough]
        public static void AgainstEmptyString(string parameterName, string argument)
        {
            if (argument == string.Empty)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "{0} is an empty string.", parameterName),
                    parameterName);
            }
        }

        /// <summary>
        /// Guards against a null or empty string.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="argument">The string to check</param>
        /// <exception cref="ArgumentException"><paramref name="argument" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="argument" /> is the empty string.</exception>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Source package.")]
        [DebuggerStepThrough]
        public static void AgainstNullOrEmptyString(string parameterName, string argument)
        {
            AgainstNullArgument(parameterName, argument);
            AgainstEmptyString(parameterName, argument);
        }

        /// <summary>
        /// Guards against a null, empty, or white-space string.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="argument">The string to check</param>
        /// <exception cref="ArgumentException"><paramref name="argument" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="argument" /> is empty or consists only of white-space characters.</exception>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Source package.")]
        [DebuggerStepThrough]
        public static void AgainstNullOrWhiteSpaceString(string parameterName, string argument)
        {
            AgainstNullArgument(parameterName, argument);

            if (argument.Trim() == string.Empty)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "{0} is an empty or white-space string.", parameterName),
                    parameterName);
            }
        }

#if NETSTANDARD2_0
        /// <summary>
        /// Determines whether the specified type is a nullable type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is a nullable type; otherwise, <c>false</c>.
        /// </returns>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Source package.")]
        private static bool IsNullableType(this Type type)
        {
            return !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
#endif

        /// <summary>
        /// When applied to a parameter,
        /// this attribute provides an indication to code analysis that the argument has been null checked.
        /// </summary>
        private sealed class ValidatedNotNullAttribute : Attribute
        {
        }
    }
}
