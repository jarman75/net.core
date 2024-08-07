using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using EventSourcingTutorianl.Events;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace EventSourcingTutorianl;

public class StudentDataBase
{


    private readonly IAmazonDynamoDB _amazonDynamoDb; 
    private const string TableName = "students";
    readonly IConfiguration _configuration;

    //private static readonly JsonSerializerOptions SeerializerSettings = new()
    //{
        
    //    AllowOutOfOrderMetadataProperties = true       
        
    //};

    public StudentDataBase(IConfiguration configuration)
    {
        //get secrets from project secrets
        var awsAccessKey = configuration["AWS:AccessKey"];
        var awsSecretKey = configuration["AWS:SecretKey"];


        var awsCredentials = new BasicAWSCredentials(awsAccessKey, awsSecretKey);
        _amazonDynamoDb = new AmazonDynamoDBClient(awsCredentials, RegionEndpoint.USEast1);
        _configuration = configuration;
    }

    public async Task AppendAsync<T>(T @event) where T : Event
    {
        @event.CreatedAtUTC = DateTime.UtcNow;
        var eventAsJson = JsonSerializer.Serialize<Event>(@event);
        var itemAsDocument = Document.FromJson(eventAsJson);
        var itemAsAttribute = itemAsDocument.ToAttributeMap();

        var studentView = await GetStudentAsync(@event.StreamId) ?? new Student();
        studentView.Apply(@event);
        var studentViewAsJson = JsonSerializer.Serialize(studentView);
        var studentViewAsDocument = Document.FromJson(studentViewAsJson);
        var studentViewAsAttribute = studentViewAsDocument.ToAttributeMap();

        var transactionRequest = new TransactWriteItemsRequest
        {
            TransactItems = [
            
                new TransactWriteItem
                {
                    Put = new Put
                    {
                        TableName = TableName,
                        Item = itemAsAttribute
                    }
                },
                new TransactWriteItem
                {
                    Put = new Put
                    {
                        TableName = TableName,
                        Item = studentViewAsAttribute
                    }
                }
            ]
        };

        await _amazonDynamoDb.TransactWriteItemsAsync(transactionRequest);
    }

    public async Task<Student?> GetStudentAsync(Guid studentId)
    {
        var request = new GetItemRequest
        {
            TableName = TableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "pk", new AttributeValue { S = $"{studentId.ToString()}_view" } },
                { "sk", new AttributeValue { S = $"{studentId.ToString()}_view" } }
            }
        };

        var response = await _amazonDynamoDb.GetItemAsync(request);
        if (response.Item == null)
        {
            return null;
        }

        var itemAsDocument = Document.FromAttributeMap(response.Item);
        var studentViewAsJson = itemAsDocument.ToJson();
        return JsonSerializer.Deserialize<Student>(studentViewAsJson);
    }

    
    
}