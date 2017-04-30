namespace Linterhub.Cli.Strategy
{
    using System.Linq;
    using Linterhub.Engine.Factory;
    using Linterhub.Engine.Schema;

    /// <summary>
    /// The 'catalog linter' strategy logic.
    /// </summary>
    public class CatalogStrategy : IStrategy
    {
        /// <summary>
        /// Run strategy.
        /// </summary>
        /// <param name="locator">The service locator.</param>
        /// <returns>Run results (list of linters).</returns>
        public object Run(ServiceLocator locator)
        {
            var factory = locator.Get<ILinterFactory>();
            var projectConfig = locator.Get<LinterhubSchema>();
            
            // List all linters and detect active linters (active for the project)
            var linters = factory.GetSpecifications().Select(x => x.Schema).OrderBy(x => x.Name);
            var result = linters.Select(linter => 
            {
                linter.Active = projectConfig.Linters.Any(projectLinter => projectLinter.Name == linter.Name && projectLinter.Active != false);
                return linter;
            });

            return result;
        }
    }
}