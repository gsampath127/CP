//-----------------------------------------------------------------------
// <copyright file="Guard.cs" company="R. R. Donnelley &amp; Sons Company">
//     Copyright (c) R. R. Donnelley &amp; Sons Company. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace BCS.Core
{
    /// <summary>
    /// <para>Provides a means of ensuring conditions are met.</para>
    /// <para>This could be seen as an emulation of a subset of <see cref="System.Diagnostics.Contracts.Contract"/>.</para>
    /// <para>This is only until we have a firmer grasp of the benefits of Code Contracts, or the C# compiler is more tightly integrated.</para>
    /// <para>
    /// The Contracts namespace is part of the BCL, but there are still some hoops you have to jump through to use Contracts.
    /// For instance, to require a condition and have an exception raised, you would write the following:
    /// </para>
    /// <para><code>Contract.Requires&lt;ArgumentNullException&gt;(condition, "some error message if the condition fails");</code></para>
    /// <para>
    /// If you wrote this, you might expect an ArgumentNullException to be thrown with the message "some error message if the condition fails".
    /// </para>
    /// <para>
    /// Unfortunately, the actual error message is prefixed with a useful message stating that a precondition failed, 
    /// and the actual source code of the condition. There is no way to turn this prefixing off, which means
    /// you are unable to display this message to the user without some complex parsing.
    /// </para>
    /// <para>
    /// These limitations might exist only with out of the box Contracts code, and may be overcome using some of the 
    /// custom contract failure exception handling that is provided, but these are more hoops you have to jump through, 
    /// and the documentation is lacking.
    /// </para>
    /// So, we will have our own class, that we can easily convert to use the BCL Contract class when/if the time comes.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Specifies a condition that must be met, otherwise a <see cref="RRD.DSA.Core.CoreException"/> is thrown with the given message.
        /// </summary>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="message">The message to raise in an exception if the condition is not met.</param>
        public static void Requires(bool condition, string message)
        {
            if (!condition)
            {
                throw new CoreException(message);
            }
        }
    }
}