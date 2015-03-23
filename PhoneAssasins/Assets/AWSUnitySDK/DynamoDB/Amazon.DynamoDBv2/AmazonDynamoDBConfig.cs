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

using Amazon.Runtime;


namespace Amazon.DynamoDBv2
{
    /// <summary>
    /// Configuration for accessing Amazon DynamoDB service
    /// </summary>
    public partial class AmazonDynamoDBConfig : ClientConfig
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public AmazonDynamoDBConfig()
        {
            this.AuthenticationServiceName = "dynamodb";
            this.MaxErrorRetry = 10;
        }

        /// <summary>
        /// The constant used to lookup in the region hash the endpoint.
        /// </summary>
        internal override string RegionEndpointServiceName
        {
            get
            {
                return "dynamodb";
            }
        }

        /// <summary>
        /// Gets the ServiceVersion property.
        /// </summary>
        public override string ServiceVersion
        {
            get
            {
                return "2012-08-10";
            }
        }
    }
}