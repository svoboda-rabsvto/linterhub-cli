namespace Linterhub.Engine.Schema
{
	using System.Collections.Generic;
	
	/// <summary>
	/// Linterhub output is an array of engines results
	/// </summary>
	public class LinterhubOutputSchema
	{
		
		/// <summary>
		/// The engine result
		/// </summary>
		public class ResultType
		{
			
			/// <summary>
			/// Gets or sets the engine name that performed analysis
			/// </summary>
			public string Engine { get; set; }
			
			/// <summary>
			/// Gets or sets the list of analysis results
			/// </summary>
			public List<EngineOutputSchema> Results = new List<EngineOutputSchema>();
			
			/// <summary>
			/// Gets or sets the problem definition if analysis is not possible
			/// </summary>
			public ErrorType Error { get; set; }
		}
		
		/// <summary>
		/// The problem definition if analysis is not possible
		/// </summary>
		public class ErrorType
		{
			
			/// <summary>
			/// Gets or sets the error code
			/// </summary>
			public int Code { get; set; }
			
			/// <summary>
			/// Gets or sets the error title
			/// </summary>
			public string Title { get; set; }
			
			/// <summary>
			/// Gets or sets the error decription
			/// </summary>
			public string Description { get; set; }
		}
	}
}
