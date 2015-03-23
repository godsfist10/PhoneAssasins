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

namespace Amazon.DynamoDBv2.DocumentModel
{
    /// <summary>
    /// Filter for use with scan and query operations
    /// </summary>
    public class Filter
    {
        #region Private members

        internal Dictionary<string, Condition> Conditions = new Dictionary<string, Condition>();

        #endregion

        #region Conversions

        /// <summary>
        /// Converts filter to a map of conditions
        /// </summary>
        /// <returns>Map from attribute name to condition</returns>
        public Dictionary<string, Condition> ToConditions()
        {
            return new Dictionary<string, Condition>(Conditions);
        }

        /// <summary>
        /// Creates a Filter from a conditions map
        /// </summary>
        /// <param name="conditions">Map from attribute name to condition</param>
        /// <returns>Equivalent Filter</returns>
        public static Filter FromConditions(Dictionary<string, Condition> conditions)
        {
            Filter ret = new Filter();
            ret.Conditions = conditions;
            return ret;
        }
        /// <summary>
        /// Implicitly converts Filter to map of conditions
        /// </summary>
        /// <param name="f">Filter to convert</param>
        /// <returns>Map from attribute name to condition</returns>
        public static implicit operator Dictionary<string, Condition>(Filter f)
        {
            return f.ToConditions();
        }
        /// <summary>
        /// Implicitly converts map of conditions to Filter
        /// </summary>
        /// <param name="conditions">Map from attribute name to condition</param>
        /// <returns>Equivalent Filter</returns>
        public static implicit operator Filter(Dictionary<string, Condition> conditions)
        {
            return Filter.FromConditions(conditions);
        }

        // Creates a list of AttributeValues from a list of Primitives
        protected static List<AttributeValue> ConvertToAttributeValues(params DynamoDBEntry[] values)
        {
            List<AttributeValue> attributes = new List<AttributeValue>();
            foreach (DynamoDBEntry value in values)
            {
                AttributeValue nativeValue = value.ConvertToAttributeValue();
                if (nativeValue != null)
                {
                    attributes.Add(nativeValue);
                }
            }
            return attributes;
        }


        #endregion

        #region Add methods

        /// <summary>
        /// Adds a condition for a specified attribute.
        /// 
        /// If a condition for the attribute already exists,
        /// it will be replaced with the new condition.
        /// </summary>
        /// <param name="attributeName">Target attribute name</param>
        /// <param name="condition">Condition to be added</param>
        public void AddCondition(string attributeName, Condition condition)
        {
            Conditions[attributeName] = condition;
        }

        /// <summary>
        /// Removes a condition for a specific attribute name.
        /// </summary>
        /// <param name="attributeName">Target attribute</param>
        public void RemoveCondition(string attributeName)
        {
            Conditions.Remove(attributeName);
        }

        #endregion
    }

    /// <summary>
    /// Scan filter.
    /// </summary>
    public class ScanFilter : Filter
    {
        /// <summary>
        /// Adds a condition for a specified attribute that consists
        /// of an operator and any number of AttributeValues.
        /// </summary>
        /// <param name="attributeName">Target attribute name</param>
        /// <param name="op">Comparison operator</param>
        /// <param name="values">AttributeValues to compare to</param>
        public void AddCondition(string attributeName, ScanOperator op, List<AttributeValue> values)
        {
            AddCondition(attributeName, new Condition
            {
                ComparisonOperator = EnumMapper.Convert(op),
                AttributeValueList = values
            });
        }

        /// <summary>
        /// Adds a condition for a specified attribute that consists
        /// of an operator and any number of values
        /// </summary>
        /// <param name="attributeName">Target attribute name</param>
        /// <param name="op">Comparison operator</param>
        /// <param name="values">Values to compare to</param>
        public void AddCondition(string attributeName, ScanOperator op, params DynamoDBEntry[] values)
        {
            AddCondition(attributeName, new Condition
            {
                ComparisonOperator = EnumMapper.Convert(op),
                AttributeValueList = ConvertToAttributeValues(values)
            });
        }
    }

    /// <summary>
    /// Query filter.
    /// </summary>
    public class QueryFilter : Filter
    {
        /// <summary>
        /// Constructs an empty QueryFilter instance
        /// </summary>
        public QueryFilter() { }

        /// <summary>
        /// Constructs an instance of QueryFilter with a single condition.
        /// More conditions can be added after initialization.
        /// </summary>
        /// <param name="attributeName">Target attribute name</param>
        /// <param name="op">Comparison operator</param>
        /// <param name="values">Attributes to compare</param>
        public QueryFilter(string attributeName, QueryOperator op, List<AttributeValue> values)
        {
            AddCondition(attributeName, op, values);
        }

        /// <summary>
        /// Constructs an instance of QueryFilter with a single condition.
        /// More conditions can be added after initialization.
        /// </summary>
        /// <param name="attributeName">Target attribute name</param>
        /// <param name="op">Comparison operator</param>
        /// <param name="values">Attributes to compare</param>
        public QueryFilter(string attributeName, QueryOperator op, params DynamoDBEntry[] values)
        {
            AddCondition(attributeName, op, values);
        }

        // Creates new filter that has the same conditions as base
        internal QueryFilter(QueryFilter baseFilter)
        {
            foreach (var kvp in baseFilter.Conditions)
            {
                string key = kvp.Key;
                Condition condition = kvp.Value;

                AddCondition(key, condition);
            }
        }

        /// <summary>
        /// Adds a condition for a specified key attribute that consists
        /// of an operator and any number of AttributeValues.
        /// </summary>
        /// <param name="keyAttributeName">Target key attribute name</param>
        /// <param name="op">Comparison operator</param>
        /// <param name="values">AttributeValues to compare to</param>
        public void AddCondition(string keyAttributeName, QueryOperator op, List<AttributeValue> values)
        {
            AddCondition(keyAttributeName, new Condition
            {
                ComparisonOperator = EnumMapper.Convert(op),
                AttributeValueList = values
            });
        }

        /// <summary>
        /// Adds a condition for a specified key attribute that consists
        /// of an operator and any number of values
        /// </summary>
        /// <param name="keyAttributeName">Target key attribute name</param>
        /// <param name="op">Comparison operator</param>
        /// <param name="values">Values to compare to</param>
        public void AddCondition(string keyAttributeName, QueryOperator op, params DynamoDBEntry[] values)
        {
            AddCondition(keyAttributeName, new Condition
            {
                ComparisonOperator = EnumMapper.Convert(op),
                AttributeValueList = ConvertToAttributeValues(values)
            });
        }

        /// <summary>
        /// Adds a condition for a specified non-key attribute that consists
        /// of an operator and any number of AttributeValues.
        /// </summary>
        /// <param name="nonKeyAttributeName">Target non-key attribute name</param>
        /// <param name="op">Comparison operator</param>
        /// <param name="values">AttributeValues to compare to</param>
        public void AddCondition(string nonKeyAttributeName, ScanOperator op, List<AttributeValue> values)
        {
            AddCondition(nonKeyAttributeName, new Condition
            {
                ComparisonOperator = EnumMapper.Convert(op),
                AttributeValueList = values
            });
        }

        /// <summary>
        /// Adds a condition for a specified non-key attribute that consists
        /// of an operator and any number of values
        /// </summary>
        /// <param name="nonKeyAttributeName">Target non-key attribute name</param>
        /// <param name="op">Comparison operator</param>
        /// <param name="values">Values to compare to</param>
        public void AddCondition(string nonKeyAttributeName, ScanOperator op, params DynamoDBEntry[] values)
        {
            AddCondition(nonKeyAttributeName, new Condition
            {
                ComparisonOperator = EnumMapper.Convert(op),
                AttributeValueList = ConvertToAttributeValues(values)
            });
        }
    }
}
