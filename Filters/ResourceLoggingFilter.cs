using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace MyWebSite.Filters
{
	public class ResourceLoggingFilter : Attribute, IResourceFilter, IOrderedFilter
	{
		int IOrderedFilter.Order => 1;

		public void OnResourceExecuting(ResourceExecutingContext context)
		{
			Console.WriteLine("Starting");
			
		}

		public void OnResourceExecuted(ResourceExecutedContext context)
		{
			Console.WriteLine("ending");
		}

	}
}
	