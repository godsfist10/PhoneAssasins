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
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.DynamoDBv2.Model
{
    /// <summary>
    /// Represents a request to perform a <i>PutItem</i> operation on an item.
    /// </summary>
    public partial class PutRequest
    {
        private Dictionary<string, AttributeValue> _item = new Dictionary<string, AttributeValue>();


        /// <summary>
        /// Gets and sets the property Item. 
        /// <para>
        /// A map of attribute name to attribute values, representing the primary key of an item
        /// to be      processed by <i>PutItem</i>. All of the table's primary key attributes
        /// must be specified, and      their data types must match those of the table's key schema.
        /// If any attributes are present in      the item which are part of an index key schema
        /// for the table, their types must match the index      key schema.
        /// </para>
        /// </summary>
        public Dictionary<string, AttributeValue> Item
        {
            get { return this._item; }
            set { this._item = value; }
        }

        // Check to see if Item property is set
        internal bool IsSetItem()
        {
            return this._item != null && this._item.Count > 0; 
        }

    }
}