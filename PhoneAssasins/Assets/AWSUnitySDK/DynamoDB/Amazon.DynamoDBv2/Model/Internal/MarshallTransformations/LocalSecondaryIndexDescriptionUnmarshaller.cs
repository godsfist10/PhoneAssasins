/*
 * Copyright 2014-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 *
 * Licensed under the AWS Mobile SDK for Unity Developer Preview License Agreement (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located in the "license" file accompanying this file.
 * See the License for the specific language governing permissions and limitations under the License.
 *
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
using ThirdParty.Json.LitJson;

namespace Amazon.DynamoDBv2.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Response Unmarshaller for LocalSecondaryIndexDescription Object
    /// </summary>  
    public class LocalSecondaryIndexDescriptionUnmarshaller : IUnmarshaller<LocalSecondaryIndexDescription, XmlUnmarshallerContext>, IUnmarshaller<LocalSecondaryIndexDescription, JsonUnmarshallerContext>
    {
        LocalSecondaryIndexDescription IUnmarshaller<LocalSecondaryIndexDescription, XmlUnmarshallerContext>.Unmarshall(XmlUnmarshallerContext context)
        {
            throw new NotImplementedException();
        }

        public LocalSecondaryIndexDescription Unmarshall(JsonUnmarshallerContext context)
        {
            context.Read();
            if (context.CurrentTokenType == JsonToken.Null) 
                return null;

            LocalSecondaryIndexDescription unmarshalledObject = new LocalSecondaryIndexDescription();
        
            int targetDepth = context.CurrentDepth;
            while (context.ReadAtDepth(targetDepth))
            {
                if (context.TestExpression("IndexName", targetDepth))
                {
                    var unmarshaller = StringUnmarshaller.Instance;
                    unmarshalledObject.IndexName = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("IndexSizeBytes", targetDepth))
                {
                    var unmarshaller = LongUnmarshaller.Instance;
                    unmarshalledObject.IndexSizeBytes = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("ItemCount", targetDepth))
                {
                    var unmarshaller = LongUnmarshaller.Instance;
                    unmarshalledObject.ItemCount = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("KeySchema", targetDepth))
                {
                    var unmarshaller = new ListUnmarshaller<KeySchemaElement, KeySchemaElementUnmarshaller>(KeySchemaElementUnmarshaller.Instance);
                    unmarshalledObject.KeySchema = unmarshaller.Unmarshall(context);
                    continue;
                }
                if (context.TestExpression("Projection", targetDepth))
                {
                    var unmarshaller = ProjectionUnmarshaller.Instance;
                    unmarshalledObject.Projection = unmarshaller.Unmarshall(context);
                    continue;
                }
            }
          
            return unmarshalledObject;
        }


        private static LocalSecondaryIndexDescriptionUnmarshaller _instance = new LocalSecondaryIndexDescriptionUnmarshaller();        

        public static LocalSecondaryIndexDescriptionUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}