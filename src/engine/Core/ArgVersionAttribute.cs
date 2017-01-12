namespace Linterhub.Engine
{
    /// <summary>
    /// Represents configuration for linter argument with path to file/folder.
    /// </summary>
    public class ArgVersionAttribute : ArgAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ArgAttribute"/> class.
        /// </summary>
        /// <param name="name">The argument name.</param>
        /// <param name="add">Whether to add a prefix and the value.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="order">The order.</param>
        public ArgVersionAttribute(string name = "--version", bool add = true, string separator = "", int order = int.MaxValue)
            : base(name, add, separator, order)
        {
        }
    }
}