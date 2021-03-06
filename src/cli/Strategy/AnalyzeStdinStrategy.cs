﻿namespace Linterhub.Cli.Strategy
{
    using System.Collections.Generic;
    using System.Linq;
    using Runtime;
    using Core.Schema;
    using Core.Runtime;
    using Core.Factory;

    public class AnalyzeStdinStrategy : IStrategy
    {
        /*public static IEnumerable<string> MergeEngines(IEnumerable<string> enginesFromCommand, IEnumerable<LinterhubConfigSchema.ConfigurationType> enginesFromConfig)
        {
            return enginesFromConfig
                .Where(x => x.Active != false)
                .Select(x => x.Name)
                .Concat(enginesFromCommand)
                .Distinct();
        }*/

        public object Run(ServiceLocator locator)
        {
            var context = locator.Get<RunContext>();
            var config = locator.Get<LinterhubConfigSchema>();
            var engineRunner = locator.Get<EngineWrapper>();
            var engineFactory = locator.Get<IEngineFactory>();
            var engines = context.Engines.Count() == 0 ?
                config.Engines.Where(x => x.Active != false)
                .Select(x => x.Name) : context.Engines;

            var contexts =
                from engine in engines
                let specification = engineFactory.GetSpecification(engine)
                let configOptions = config.Engines.FirstOrDefault(y => y.Name == engine)?.Config ?? specification.Schema.Defaults
                let path = !string.IsNullOrEmpty(context.File) ? context.File : null
                let fileName = path ?? "#stdin"
                let runOptions = new EngineOptions
                {
                    { "{path}", /*System.IO.Path.GetFullPath(path)*/ path },
                    { "file://{schema}", context.Linterhub },
                    { "file://{stdin}", fileName },
                }
                let workingDirectory = context.Directory ?? context.Project
                select new EngineWrapper.EngineContext
                {
                    Specification = specification,
                    ConfigOptions = (EngineOptions)configOptions,
                    RunOptions = runOptions,
                    WorkingDirectory = workingDirectory,
                    Stdin = specification.Schema.Stdin != null
                            ? EngineWrapper.EngineContext.stdinType.UseWithEngine : EngineWrapper.EngineContext.stdinType.Use,
                    Locally = context.Locally,
                    Project = context.Project
                };

            return engineRunner.RunAnalyze(contexts.ToList(), new EngineWrapper.RunContext
            {
                File = context.File,
                Directory = context.Directory,
                InputStream = context.Input,
                Project = context.Project
            }, config);
        }
    }
}