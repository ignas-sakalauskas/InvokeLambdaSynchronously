using System.Linq;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaEmpty
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="sqsEvent"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(SQSEvent sqsEvent, ILambdaContext context)
        {
            var agg = sqsEvent.Records
                .Aggregate("", (current, record) => current + record.Body);

            return agg.ToUpperInvariant();
        }
    }
}
