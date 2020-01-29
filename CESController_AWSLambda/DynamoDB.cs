using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Internal;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
//using Amazon.Lambda.Tools.Options;
using Amazon.Lambda.Core;

using Newtonsoft.Json;

namespace CESController_AWSLambda
{
    class DynamoDB
    {
        static AmazonDynamoDBClient dynamoDBClient;
        static string tableName = "AlexaMsg";

        static private AmazonDynamoDBClient DynamoDBClient
        {
            get
            {
                if(dynamoDBClient == null)
                {
                    dynamoDBClient = new AmazonDynamoDBClient();                    
                }
                return dynamoDBClient;
            }            
        }

        static public string DynamoDBTableName
        {
            get
            {
                return tableName;
            }
            set
            {
                tableName = value;
            }
        }
        
        async static public Task<bool> PutAlexaMsg(AlexaMsg msg)
        {
            try
            {
                DynamoDBContext context = new DynamoDBContext(DynamoDBClient);
                await context.SaveAsync<AlexaMsg>(msg);
            }
            catch (Exception ex)
            {
                LambdaLogger.Log(ex.Message);
            }

            return true;

        }

        async static public Task<bool> PutAlexaMsg(LightsAlexaMsg msg)
        {
            try
            {
                DynamoDBContext context = new DynamoDBContext(DynamoDBClient);
                await context.SaveAsync<LightsAlexaMsg>(msg);
            }
            catch (Exception ex)
            {
                LambdaLogger.Log(ex.Message);
            }

            return true;

        }
    }
}
