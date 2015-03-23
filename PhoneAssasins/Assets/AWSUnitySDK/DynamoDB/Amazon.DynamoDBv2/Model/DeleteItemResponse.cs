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

namespace Amazon.DynamoDBv2.Model
{
    /// <summary>
    /// Configuration for accessing Amazon DeleteItem service
    /// </summary>
    public partial class DeleteItemResponse : DeleteItemResult
    {
        /// <summary>
        /// Gets and sets the DeleteItemResult property.
        /// Represents the output of a DeleteItem operation.
        /// </summary>
        [Obsolete(@"This property has been deprecated. All properties of the DeleteItemResult class are now available on the DeleteItemResponse class. You should use the properties on DeleteItemResponse instead of accessing them through DeleteItemResult.")]
        public DeleteItemResult DeleteItemResult
        {
            get
            {
                return this;
            }
        }
    }
}