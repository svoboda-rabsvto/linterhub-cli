namespace Linterhub.Core.Runtime
{
    using System;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// The cmd wrapper.
    /// </summary>
    public class TerminalWrapper
    {

        /// <summary>
        /// Gets the path to the terminal.
        /// </summary>
        protected string TerminalPath { get; private set; }

        /// <summary>
        /// Gets the format of terminal command.
        /// </summary>
        protected string TerminalCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <seealso cref="TerminalWrapper"/>.
        /// </summary>
        /// <param name="terminalPath">The path to the terminal.</param>
        /// <param name="terminalCommand">The format of terminal command.</param>
        public TerminalWrapper(string terminalPath, string terminalCommand)
        {
            TerminalPath = terminalPath;
            TerminalCommand = terminalCommand;
        }

        /// <summary>
        /// Run terminal.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="workingDirectory">The working directory.</param>
        /// <param name="waitTimeout">The amount of time, in milliseconds, to wait for the associated process to exit.</param>
        /// <returns>The <seealso cref="Result"/>.</returns>
        public Result RunTerminal(string arguments, string workingDirectory = null, int waitTimeout = -1, string stdin = "")
        {
            var command = string.Format(TerminalCommand, arguments);
            var result = this.RunExecutable(TerminalPath, command, workingDirectory, waitTimeout, stdin);
            return result;
        }

        /// <summary>
        /// Run executable.
        /// </summary>
        /// <param name="executablePath">The executable path.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="workingDirectory">The working directory.</param>
        /// <param name="waitTimeout">The amount of time, in milliseconds, to wait for the associated process to exit.</param>
        /// <returns>The <seealso cref="Result"/>.</returns>
        public Result RunExecutable(string executablePath, string arguments, string workingDirectory = null, int waitTimeout = -1, string stdin = "")
        {
            var result = new Result
            {
                Output = new StringBuilder(),
                Error = new StringBuilder(),
                RunException = null
            };

            try
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = executablePath;
                    process.StartInfo.Arguments = arguments;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    if (stdin != string.Empty)
                    {
                        process.StartInfo.RedirectStandardInput = true;
                    }
                    process.OutputDataReceived += (o, e) => result.Output.Append(e.Data).Append(Environment.NewLine);
                    process.ErrorDataReceived += (o, e) => result.Error.Append(e.Data).Append(Environment.NewLine);
                    if (!string.IsNullOrEmpty(workingDirectory))
                    {
                        process.StartInfo.WorkingDirectory = workingDirectory;
                    }

                    process.Start();
                    if (stdin != string.Empty)
                    {
                        process.StandardInput.Write(stdin);
                        process.StandardInput.Flush();
                        process.StandardInput.Dispose();
                    }
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit(waitTimeout);
                    result.ExitCode = process.ExitCode;
                }
            }
            catch (Exception exception)
            {
                result.RunException = exception;
                result.ExitCode = -1;
            }
            return result;
        }

        /// <summary>
        /// The cmd run results.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// Gets the exit code.
            /// </summary>
            public int ExitCode { get; internal set; }

            /// <summary>
            /// Gets the run exception.
            /// </summary>
            public Exception RunException { get; internal set; }

            /// <summary>
            /// Gets output content.
            /// </summary>
            public StringBuilder Output { get; internal set; }

            /// <summary>
            /// Gets error content.
            /// </summary>
            public StringBuilder Error { get; internal set; }
        }
    }
}