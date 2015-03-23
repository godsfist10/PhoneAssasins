﻿/*
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

using Amazon.DynamoDBv2.Model;
using System.IO;

namespace Amazon.DynamoDBv2.DocumentModel
{
    /// <summary>
    /// Expected state of an attribute in DynamoDB.
    /// Exists cannot be set at the same time as Comparison and Values.
    /// </summary>
    public class ExpectedValue
    {
        /// <summary>
        /// Flag whether the attribute should exist or not.
        /// </summary>
        public bool Exists { get; set; }

        /// <summary>
        /// Comparison operator for the expected value.
        /// </summary>
        public ScanOperator Comparison { get; private set; }

        /// <summary>
        /// Values to compare the attribute against.
        /// </summary>
        public List<Primitive> Values { get; private set; }


        /// <summary>
        /// Constructs an ExpectedValue with a given Exists value.
        /// </summary>
        /// <param name="exists">
        /// Flag whether the attribute should exist or not.
        /// </param>
        public ExpectedValue(bool exists)
        {
            Exists = exists;
            Values = new List<Primitive>();
        }

        /// <summary>
        /// Constructs an ExpectedValue with a given comparison and value(s).
        /// </summary>
        /// <param name="comparison"></param>
        /// <param name="values"></param>
        public ExpectedValue(ScanOperator comparison, params Primitive[] values)
        {
            Exists = true;
            Comparison = comparison;
            Values = new List<Primitive>(values);
        }


        /// <summary>
        /// Converts this ExpectedValue instance to Amazon.DynamoDBv2.Model.ExpectedAttributeValue
        /// </summary>
        /// <returns>Amazon.DynamoDBv2.Model.ExpectedAttributeValue</returns>
        public ExpectedAttributeValue ToExpectedAttributeValue()
        {
            var eav = new ExpectedAttributeValue();

            if (this.Exists)
            {
                eav.ComparisonOperator = EnumMapper.Convert(this.Comparison);
                foreach (var val in this.Values)
                    eav.AttributeValueList.Add(val.ConvertToAttributeValue());
            }
            else
                eav.Exists = this.Exists;

            return eav;
        }
    }

    /// <summary>
    /// Expected state of an item in DynamoDB.
    /// </summary>
    public class ExpectedState
    {
        /// <summary>
        /// Constructs an empty ExpectedState with ConditionalOeprator set to AND.
        /// </summary>
        public ExpectedState()
        {
            ExpectedValues = new Dictionary<string, ExpectedValue>(StringComparer.Ordinal);
            ConditionalOperator = ConditionalOperatorValues.And;
        }

        /// <summary>
        /// Attribute name to ExpectedValue mapping.
        /// Represents the expected state of a number of attributes of a DynamoDB item.
        /// </summary>
        public Dictionary<string, ExpectedValue> ExpectedValues { get; set; }

        /// <summary>
        /// Operator dictating whether ALL or SOME of the expected values must be true to
        /// satisfy the overall expected state.
        /// </summary>
        public ConditionalOperatorValues ConditionalOperator { get; set; }


        /// <summary>
        /// Adds an ExpectedValue with the specific Exists value for the attribute.
        /// </summary>
        /// <param name="attributeName">Attribute that is being tested</param>
        /// <param name="exists">Flag whether the attribute should exist or not.</param>
        public void AddExpected(string attributeName, bool exists)
        {
            ExpectedValues[attributeName] = new ExpectedValue(exists);
        }
        /// <summary>
        /// Adds an ExpectedValue with the specific Comparison and Values for the attribute.
        /// </summary>
        /// <param name="attributeName">Attribute that is being tested</param>
        /// <param name="comparison">Comparison operator for the expected value.</param>
        /// <param name="values">Values to compare the attribute against.</param>
        public void AddExpected(string attributeName, ScanOperator comparison, params Primitive[] values)
        {
            ExpectedValues[attributeName] = new ExpectedValue(comparison, values);
        }

        /// <summary>
        /// Creates a map of attribute names mapped to ExpectedAttributeValue objects.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ExpectedAttributeValue> ToExpectedAttributeMap()
        {
            Dictionary<string, ExpectedAttributeValue> ret = new Dictionary<string, ExpectedAttributeValue>();

            foreach (var kvp in ExpectedValues)
            {
                string attributeName = kvp.Key;
                ExpectedValue expectedValue = kvp.Value;
                ExpectedAttributeValue eav = expectedValue.ToExpectedAttributeValue();
                ret[attributeName] = eav;
            }

            return ret;
        }
    }
}
