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
    /// DeleteItem Request Marshaller
    /// </summary>       
    public class DeleteItemRequestMarshaller : IMarshaller<IRequest, DeleteItemRequest> 
    {
        public IRequest Marshall(DeleteItemRequest publicRequest)
        {
            IRequest request = new DefaultRequest(publicRequest, "Amazon.DynamoDBv2");
            string target = "DynamoDB_20120810.DeleteItem";
            request.Headers["X-Amz-Target"] = target;
            request.Headers["Content-Type"] = "application/x-amz-json-1.0";
            request.HttpMethod = "POST";

            string uriResourcePath = "/";
            request.ResourcePath = uriResourcePath;
            using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                JsonWriter writer = new JsonWriter(stringWriter);
                writer.WriteObjectStart();
                if(publicRequest.IsSetConditionalOperator())
                {
                    writer.WritePropertyName("ConditionalOperator");
                    writer.Write(publicRequest.ConditionalOperator);
                }

                if(publicRequest.IsSetExpected())
                {
                    writer.WritePropertyName("Expected");
                    writer.WriteObjectStart();
                    foreach (var publicRequestExpectedKvp in publicRequest.Expected)
                    {
                        writer.WritePropertyName(publicRequestExpectedKvp.Key);
                        var publicRequestExpectedValue = publicRequestExpectedKvp.Value;

                        writer.WriteObjectStart();
                        if(publicRequestExpectedValue.IsSetAttributeValueList())
                        {
                            writer.WritePropertyName("AttributeValueList");
                            writer.WriteArrayStart();
                            foreach(var publicRequestExpectedValueAttributeValueListListValue in publicRequestExpectedValue.AttributeValueList)
                            {
                                writer.WriteObjectStart();
                                if(publicRequestExpectedValueAttributeValueListListValue.IsSetB())
                                {
                                    writer.WritePropertyName("B");
                                    writer.Write(StringUtils.FromMemoryStream(publicRequestExpectedValueAttributeValueListListValue.B));
                                }

                                if(publicRequestExpectedValueAttributeValueListListValue.IsSetBS())
                                {
                                    writer.WritePropertyName("BS");
                                    writer.WriteArrayStart();
                                    foreach(var publicRequestExpectedValueAttributeValueListListValueBSListValue in publicRequestExpectedValueAttributeValueListListValue.BS)
                                    {
                                        writer.Write(StringUtils.FromMemoryStream(publicRequestExpectedValueAttributeValueListListValueBSListValue));
                                    }
                                    writer.WriteArrayEnd();
                                }

                                if(publicRequestExpectedValueAttributeValueListListValue.IsSetN())
                                {
                                    writer.WritePropertyName("N");
                                    writer.Write(publicRequestExpectedValueAttributeValueListListValue.N);
                                }

                                if(publicRequestExpectedValueAttributeValueListListValue.IsSetNS())
                                {
                                    writer.WritePropertyName("NS");
                                    writer.WriteArrayStart();
                                    foreach(var publicRequestExpectedValueAttributeValueListListValueNSListValue in publicRequestExpectedValueAttributeValueListListValue.NS)
                                    {
                                        writer.Write(publicRequestExpectedValueAttributeValueListListValueNSListValue);
                                    }
                                    writer.WriteArrayEnd();
                                }

                                if(publicRequestExpectedValueAttributeValueListListValue.IsSetS())
                                {
                                    writer.WritePropertyName("S");
                                    writer.Write(publicRequestExpectedValueAttributeValueListListValue.S);
                                }

                                if(publicRequestExpectedValueAttributeValueListListValue.IsSetSS())
                                {
                                    writer.WritePropertyName("SS");
                                    writer.WriteArrayStart();
                                    foreach(var publicRequestExpectedValueAttributeValueListListValueSSListValue in publicRequestExpectedValueAttributeValueListListValue.SS)
                                    {
                                        writer.Write(publicRequestExpectedValueAttributeValueListListValueSSListValue);
                                    }
                                    writer.WriteArrayEnd();
                                }

                                writer.WriteObjectEnd();
                            }
                            writer.WriteArrayEnd();
                        }

                        if(publicRequestExpectedValue.IsSetComparisonOperator())
                        {
                            writer.WritePropertyName("ComparisonOperator");
                            writer.Write(publicRequestExpectedValue.ComparisonOperator);
                        }

                        if(publicRequestExpectedValue.IsSetExists())
                        {
                            writer.WritePropertyName("Exists");
                            writer.Write(publicRequestExpectedValue.Exists);
                        }

                        if(publicRequestExpectedValue.IsSetValue())
                        {
                            writer.WritePropertyName("Value");
                            writer.WriteObjectStart();
                            if(publicRequestExpectedValue.Value.IsSetB())
                            {
                                writer.WritePropertyName("B");
                                writer.Write(StringUtils.FromMemoryStream(publicRequestExpectedValue.Value.B));
                            }

                            if(publicRequestExpectedValue.Value.IsSetBS())
                            {
                                writer.WritePropertyName("BS");
                                writer.WriteArrayStart();
                                foreach(var publicRequestExpectedValueValueBSListValue in publicRequestExpectedValue.Value.BS)
                                {
                                    writer.Write(StringUtils.FromMemoryStream(publicRequestExpectedValueValueBSListValue));
                                }
                                writer.WriteArrayEnd();
                            }

                            if(publicRequestExpectedValue.Value.IsSetN())
                            {
                                writer.WritePropertyName("N");
                                writer.Write(publicRequestExpectedValue.Value.N);
                            }

                            if(publicRequestExpectedValue.Value.IsSetNS())
                            {
                                writer.WritePropertyName("NS");
                                writer.WriteArrayStart();
                                foreach(var publicRequestExpectedValueValueNSListValue in publicRequestExpectedValue.Value.NS)
                                {
                                    writer.Write(publicRequestExpectedValueValueNSListValue);
                                }
                                writer.WriteArrayEnd();
                            }

                            if(publicRequestExpectedValue.Value.IsSetS())
                            {
                                writer.WritePropertyName("S");
                                writer.Write(publicRequestExpectedValue.Value.S);
                            }

                            if(publicRequestExpectedValue.Value.IsSetSS())
                            {
                                writer.WritePropertyName("SS");
                                writer.WriteArrayStart();
                                foreach(var publicRequestExpectedValueValueSSListValue in publicRequestExpectedValue.Value.SS)
                                {
                                    writer.Write(publicRequestExpectedValueValueSSListValue);
                                }
                                writer.WriteArrayEnd();
                            }

                            writer.WriteObjectEnd();
                        }

                        writer.WriteObjectEnd();
                    }
                    writer.WriteObjectEnd();
                }

                if(publicRequest.IsSetKey())
                {
                    writer.WritePropertyName("Key");
                    writer.WriteObjectStart();
                    foreach (var publicRequestKeyKvp in publicRequest.Key)
                    {
                        writer.WritePropertyName(publicRequestKeyKvp.Key);
                        var publicRequestKeyValue = publicRequestKeyKvp.Value;

                        writer.WriteObjectStart();
                        if(publicRequestKeyValue.IsSetB())
                        {
                            writer.WritePropertyName("B");
                            writer.Write(StringUtils.FromMemoryStream(publicRequestKeyValue.B));
                        }

                        if(publicRequestKeyValue.IsSetBS())
                        {
                            writer.WritePropertyName("BS");
                            writer.WriteArrayStart();
                            foreach(var publicRequestKeyValueBSListValue in publicRequestKeyValue.BS)
                            {
                                writer.Write(StringUtils.FromMemoryStream(publicRequestKeyValueBSListValue));
                            }
                            writer.WriteArrayEnd();
                        }

                        if(publicRequestKeyValue.IsSetN())
                        {
                            writer.WritePropertyName("N");
                            writer.Write(publicRequestKeyValue.N);
                        }

                        if(publicRequestKeyValue.IsSetNS())
                        {
                            writer.WritePropertyName("NS");
                            writer.WriteArrayStart();
                            foreach(var publicRequestKeyValueNSListValue in publicRequestKeyValue.NS)
                            {
                                writer.Write(publicRequestKeyValueNSListValue);
                            }
                            writer.WriteArrayEnd();
                        }

                        if(publicRequestKeyValue.IsSetS())
                        {
                            writer.WritePropertyName("S");
                            writer.Write(publicRequestKeyValue.S);
                        }

                        if(publicRequestKeyValue.IsSetSS())
                        {
                            writer.WritePropertyName("SS");
                            writer.WriteArrayStart();
                            foreach(var publicRequestKeyValueSSListValue in publicRequestKeyValue.SS)
                            {
                                writer.Write(publicRequestKeyValueSSListValue);
                            }
                            writer.WriteArrayEnd();
                        }

                        writer.WriteObjectEnd();
                    }
                    writer.WriteObjectEnd();
                }

                if(publicRequest.IsSetReturnConsumedCapacity())
                {
                    writer.WritePropertyName("ReturnConsumedCapacity");
                    writer.Write(publicRequest.ReturnConsumedCapacity);
                }

                if(publicRequest.IsSetReturnItemCollectionMetrics())
                {
                    writer.WritePropertyName("ReturnItemCollectionMetrics");
                    writer.Write(publicRequest.ReturnItemCollectionMetrics);
                }

                if(publicRequest.IsSetReturnValues())
                {
                    writer.WritePropertyName("ReturnValues");
                    writer.Write(publicRequest.ReturnValues);
                }

                if(publicRequest.IsSetTableName())
                {
                    writer.WritePropertyName("TableName");
                    writer.Write(publicRequest.TableName);
                }

        
                writer.WriteObjectEnd();
                string snippet = stringWriter.ToString();
                request.Content = System.Text.Encoding.UTF8.GetBytes(snippet);
            }


            return request;
        }


    }
}