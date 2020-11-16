using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using NSwag.Annotations;

namespace NSwag.Examples
{
    public class RequestBodyExampleProcessor : IOperationProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        public RequestBodyExampleProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool Process(OperationProcessorContext context)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<RequestBodyExampleProcessor>>();
            var exampleProvider = _serviceProvider.GetRequiredService<SwaggerExampleProvider>();
            foreach (var apiParameter in context.OperationDescription.Operation.Parameters.Where(x => x.Kind == OpenApiParameterKind.Body))
            {
                var parameter = context.Parameters.SingleOrDefault(x => x.Value.Name == apiParameter.Name);
                apiParameter.ActualSchema.Example = exampleProvider.GetProviderValue(parameter.Key.ParameterType);
            }

            foreach (var response in context.OperationDescription.Operation.Responses)
            {
                if (!int.TryParse(response.Key, out int responseStatusCode))
                {
                    continue;
                }

                var attributesWithSameKey = GetAttributesWithSameStatusCode(context.MethodInfo, response.Key);

                if (!attributesWithSameKey.Any())
                {
                    //get attributes from controller, in case when no attribute on action was found
                    attributesWithSameKey = GetAttributesWithSameStatusCode(context.MethodInfo.DeclaringType, response.Key);
                }

                if (attributesWithSameKey.Count > 1)
                    logger.LogWarning($"Multiple {nameof(SwaggerResponseAttribute)} defined for method {context.MethodInfo.Name}, selecting first.");
                response.Value.Examples = exampleProvider.GetProviderValue(attributesWithSameKey.FirstOrDefault()?.Type);
            }

            return true;
        }

        private List<SwaggerResponseAttribute> GetAttributesWithSameStatusCode(MemberInfo memberInfo, string responseStatusCode)
        {
            return memberInfo
                    .GetCustomAttributes<SwaggerResponseAttribute>(true)
                    .Where(x => x.StatusCode == responseStatusCode)
                    .ToList();
        }
    }
}