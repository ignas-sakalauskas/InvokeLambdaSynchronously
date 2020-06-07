using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Lambda.SQSEvents;
using Xunit;

namespace SdkInvocation
{
    public class UnitTest1
    {
        [Fact]
        public async Task Given_valid_payload_when_lambda_invoked_synchronously_then_should_return_expected_response()
        {
            // Given
            var client = new AmazonLambdaClient();
            var sqsEvent = new SQSEvent
            {
                Records = new List<SQSEvent.SQSMessage>
                {
                    new SQSEvent.SQSMessage
                    {
                        Body = "Hello Lambda!"
                    }
                }
            };

            var request = new InvokeRequest
            {
                FunctionName = "1-direct-invocation",
                // force sync lambda invocation
                InvocationType = InvocationType.RequestResponse,
                LogType = LogType.Tail,
                Payload = JsonSerializer.Serialize(sqsEvent)
            };

            // When
            var result = await client.InvokeAsync(request);

            // Then
            Assert.Null(result.FunctionError);

            using var sr = new StreamReader(result.Payload);
            var response = await sr.ReadToEndAsync();
            Assert.Equal("HELLO LAMBDA!", response.Replace("\"", ""));
        }
    }
}
