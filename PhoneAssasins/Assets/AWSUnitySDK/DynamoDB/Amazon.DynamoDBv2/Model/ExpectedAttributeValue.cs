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
    /// Represents a condition to be compared with an attribute value. This condition can
    /// be used      with <i>DeleteItem</i>, <i>PutItem</i> or <i>UpdateItem</i> operations;
    /// if the comparison      evaluates to true, the operation succeeds; if not the operation
    /// fails. You can use        <i>ExpectedAttributeValue</i> in one of two different ways:
    /// 
    ///     <ul>      <li>        
    /// <para>
    /// Use <i>AttributeValueList</i> to specify one or more values to compare against an
    ///          attribute. Use <i>ComparisonOperator</i> to specify how you want to perform
    /// the          comparison. If the comparison evaluates to true, then the conditional
    /// operation          succeeds.
    /// </para>
    ///       </li>      <li>        
    /// <para>
    /// Use <i>Value</i> to specify a value that DynamoDB will compare against an attribute.
    /// If the          values match, then <i>ExpectedAttributeValue</i> evaluates to true
    /// and the conditional          operation succeeds. Optionally, you can also set <i>Exists</i>
    /// to false, indicating that          you <i>do not</i> expect to find the attribute
    /// value in the table. In this case, the          conditional operation succeeds only
    /// if the comparison evaluates to false.
    /// </para>
    ///       </li>    </ul>    
    /// <para>
    /// <i>Value</i> and <i>Exists</i> are incompatible with <i>AttributeValueList</i> and
    ///       <i>ComparisonOperator</i>. If you attempt to use both sets of parameters at
    /// once, DynamoDB will      throw a <i>ValidationException</i>.
    /// </para>
    ///     <important>      
    /// <para>
    /// The <i>Value</i> and <i>Exists</i> parameters are deprecated. Even though DynamoDB
    /// will        continue to support these parameters, we recommend that you use <i>AttributeValueList</i>
    ///        and <i>ComparisonOperator</i> instead. <i>AttributeValueList</i> and      
    ///    <i>ComparisonOperator</i> let you construct a much wider range of conditions than
    /// is        possible with <i>Value</i> and <i>Exists</i>.
    /// </para>
    ///     </important>
    /// </summary>
    public partial class ExpectedAttributeValue
    {
        private List<AttributeValue> _attributeValueList = new List<AttributeValue>();
        private ComparisonOperator _comparisonOperator;
        private bool? _exists;
        private AttributeValue _value;


        /// <summary>
        /// Gets and sets the property AttributeValueList. 
        /// <para>
        /// One or more values to evaluate against the supplied attribute.      The number of
        /// values in the list depends on the <i>ComparisonOperator</i> being used.
        /// </para>
        ///     
        /// <para>
        /// For type Number, value comparisons are numeric.
        /// </para>
        ///     
        /// <para>
        /// String value comparisons for greater than, equals, or less than are based on ASCII
        /// character      code values. For example, <code>a</code> is greater than <code>A</code>,
        /// and <code>aa</code>      is greater than <code>B</code>. For a list of code values,
        /// see <a href="http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters">http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters</a>.
        /// </para>
        ///     
        /// <para>
        /// For Binary, DynamoDB treats each byte of the binary data as unsigned when it compares
        /// binary      values, for example when evaluating query expressions.
        /// </para>
        ///     
        /// <para>
        /// For information on specifying data types in JSON, see <a href="http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DataFormat.html">JSON
        /// Data Format</a> in the Amazon DynamoDB Developer Guide.
        /// </para>
        /// </summary>
        public List<AttributeValue> AttributeValueList
        {
            get { return this._attributeValueList; }
            set { this._attributeValueList = value; }
        }

        // Check to see if AttributeValueList property is set
        internal bool IsSetAttributeValueList()
        {
            return this._attributeValueList != null && this._attributeValueList.Count > 0; 
        }


        /// <summary>
        /// Gets and sets the property ComparisonOperator. 
        /// <para>
        /// A comparator for evaluating attributes in the <i>AttributeValueList</i>. For example,
        /// equals,      greater than, less than, etc.
        /// </para>
        ///      
        /// <para>
        /// The following comparison operators are available:
        /// </para>
        ///       
        /// <para>
        /// <code>EQ | NE | LE | LT | GE | GT | NOT_NULL | NULL | CONTAINS | NOT_CONTAINS | BEGINS_WITH
        /// | IN | BETWEEN</code>
        /// </para>
        ///      
        /// <para>
        /// The following are descriptions of each comparison operator.
        /// </para>
        ///     <ul>      <li>        
        /// <para>
        /// <code>EQ</code> : Equal. 
        /// </para>
        ///         
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> of type String,
        ///          Number, Binary, String Set, Number Set, or Binary Set. If an item contains
        /// an <i>AttributeValue</i> of a different          type than the one specified in the
        /// request, the value does not match. For example,            <code>{"S":"6"}</code>
        /// does not equal <code>{"N":"6"}</code>. Also,            <code>{"N":"6"}</code> does
        /// not equal <code>{"NS":["6", "2", "1"]}</code>.
        /// </para>
        ///         <p/></li>      <li>        
        /// <para>
        /// <code>NE</code> : Not equal. 
        /// </para>
        ///         
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> of type String,
        ///          Number, Binary, String Set, Number Set, or Binary Set. If an item contains
        /// an <i>AttributeValue</i> of a different          type than the one specified in the
        /// request, the value does not match. For example,            <code>{"S":"6"}</code>
        /// does not equal <code>{"N":"6"}</code>. Also,            <code>{"N":"6"}</code> does
        /// not equal <code>{"NS":["6", "2", "1"]}</code>.
        /// </para>
        ///         <p/></li>      <li>        
        /// <para>
        /// <code>LE</code> : Less than or equal. 
        /// </para>
        ///         
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> of type String,
        ///          Number, or Binary (not a set). If an item contains an <i>AttributeValue</i>
        /// of a different          type than the one specified in the request, the value does
        /// not match. For example,            <code>{"S":"6"}</code> does not equal <code>{"N":"6"}</code>.
        /// Also,            <code>{"N":"6"}</code> does not compare to <code>{"NS":["6", "2",
        /// "1"]}</code>.
        /// </para>
        ///         <p/></li>      <li>        
        /// <para>
        /// <code>LT</code> : Less than. 
        /// </para>
        ///         
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> of type String,
        ///          Number, or Binary (not a set). If an item contains an <i>AttributeValue</i>
        /// of a different          type than the one specified in the request, the value does
        /// not match. For example,            <code>{"S":"6"}</code> does not equal <code>{"N":"6"}</code>.
        /// Also,            <code>{"N":"6"}</code> does not compare to <code>{"NS":["6", "2",
        /// "1"]}</code>.
        /// </para>
        ///         <p/></li>      <li>        
        /// <para>
        /// <code>GE</code> : Greater than or equal. 
        /// </para>
        ///         
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> of type String,
        ///          Number, or Binary (not a set). If an item contains an <i>AttributeValue</i>
        /// of a different          type than the one specified in the request, the value does
        /// not match. For example,            <code>{"S":"6"}</code> does not equal <code>{"N":"6"}</code>.
        /// Also,            <code>{"N":"6"}</code> does not compare to <code>{"NS":["6", "2",
        /// "1"]}</code>.
        /// </para>
        ///         <p/></li>      <li>        
        /// <para>
        /// <code>GT</code> : Greater than. 
        /// </para>
        ///         
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> of type String,
        ///          Number, or Binary (not a set). If an item contains an <i>AttributeValue</i>
        /// of a different          type than the one specified in the request, the value does
        /// not match. For example,            <code>{"S":"6"}</code> does not equal <code>{"N":"6"}</code>.
        /// Also,            <code>{"N":"6"}</code> does not compare to <code>{"NS":["6", "2",
        /// "1"]}</code>.
        /// </para>
        ///         <p/></li>      <li>        
        /// <para>
        /// <code>NOT_NULL</code> : The attribute exists. 
        /// </para>
        /// </li>      <li>        
        /// <para>
        /// <code>NULL</code> : The attribute does not exist. 
        /// </para>
        /// </li>      <li>        
        /// <para>
        /// <code>CONTAINS</code> : checks for a subsequence, or value in a set.
        /// </para>
        ///         
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> of type String,
        ///          Number, or Binary (not a set). If the target attribute of the comparison
        /// is a String, then          the operation checks for a substring match. If the target
        /// attribute of the comparison is          Binary, then the operation looks for a subsequence
        /// of the target that matches the input.          If the target attribute of the comparison
        /// is a set ("SS", "NS", or "BS"), then the          operation checks for a member of
        /// the set (not as a substring).
        /// </para>
        /// </li>      <li>        
        /// <para>
        /// <code>NOT_CONTAINS</code> : checks for absence of a subsequence, or absence of a value
        /// in          a set.
        /// </para>
        ///         
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> of type String,
        ///          Number, or Binary (not a set). If the target attribute of the comparison
        /// is a String, then          the operation checks for the absence of a substring match.
        /// If the target attribute of the          comparison is Binary, then the operation checks
        /// for the absence of a subsequence of the          target that matches the input. If
        /// the target attribute of the comparison is a set ("SS",          "NS", or "BS"), then
        /// the operation checks for the absence of a member of the set (not as a          substring).
        /// </para>
        /// </li>      <li>        
        /// <para>
        /// <code>BEGINS_WITH</code> : checks for a prefix. 
        /// </para>
        ///         
        /// <para>
        /// <i>AttributeValueList</i> can contain only one <i>AttributeValue</i> of type String
        /// or          Binary (not a Number or a set). The target attribute of the comparison
        /// must be a String or          Binary (not a Number or a set).
        /// </para>
        ///         <p/></li>      <li>        
        /// <para>
        /// <code>IN</code> : checks for exact matches.
        /// </para>
        ///         
        /// <para>
        /// <i>AttributeValueList</i> can contain more than one <i>AttributeValue</i> of type
        /// String,          Number, or Binary (not a set). The target attribute of the comparison
        /// must be of the same          type and exact value to match. A String never matches
        /// a String set.
        /// </para>
        /// </li>      <li>        
        /// <para>
        /// <code>BETWEEN</code> : Greater than or equal to the first value, and less than or
        /// equal          to the second value. 
        /// </para>
        ///         
        /// <para>
        /// <i>AttributeValueList</i> must contain two <i>AttributeValue</i> elements of the same
        ///          type, either String, Number, or Binary (not a set). A target attribute matches
        /// if the          target value is greater than, or equal to, the first element and less
        /// than, or equal to,          the second element. If an item contains an <i>AttributeValue</i>
        /// of a different type than          the one specified in the request, the value does
        /// not match. For example,            <code>{"S":"6"}</code> does not compare to <code>{"N":"6"}</code>.
        /// Also,            <code>{"N":"6"}</code> does not compare to <code>{"NS":["6", "2",
        /// "1"]}</code>
        /// </para>
        /// </li>    </ul>
        /// </summary>
        public ComparisonOperator ComparisonOperator
        {
            get { return this._comparisonOperator; }
            set { this._comparisonOperator = value; }
        }

        // Check to see if ComparisonOperator property is set
        internal bool IsSetComparisonOperator()
        {
            return this._comparisonOperator != null;
        }


        /// <summary>
        /// Gets and sets the property Exists. 
        /// <para>
        /// Causes DynamoDB to evaluate the value before attempting a conditional operation:
        /// </para>
        ///     <ul>      <li>        
        /// <para>
        /// If <i>Exists</i> is <code>true</code>, DynamoDB will check to see if that attribute
        /// value          already exists in the table. If it is found, then the operation succeeds.
        /// If it is not          found, the operation fails with a <i>ConditionalCheckFailedException</i>.
        /// </para>
        ///       </li>      <li>        
        /// <para>
        /// If <i>Exists</i> is <code>false</code>, DynamoDB assumes that the attribute value
        /// does            <i>not</i> exist in the table. If in fact the value does not exist,
        /// then the assumption          is valid and the operation succeeds. If the value is
        /// found, despite the assumption that it          does not exist, the operation fails
        /// with a <i>ConditionalCheckFailedException</i>.
        /// </para>
        ///       </li>    </ul>    
        /// <para>
        /// The default setting for <i>Exists</i> is <code>true</code>. If you supply a <i>Value</i>
        /// all      by itself, DynamoDB assumes the attribute exists: You don't have to set <i>Exists</i>
        /// to        <code>true</code>, because it is implied.
        /// </para>
        ///     
        /// <para>
        /// DynamoDB returns a <i>ValidationException</i> if:
        /// </para>
        ///     <ul>      <li>        
        /// <para>
        /// <i>Exists</i> is <code>true</code> but there is no <i>Value</i> to check. (You expect
        /// a          value to exist, but don't specify what that value is.)
        /// </para>
        ///       </li>      <li>        
        /// <para>
        /// <i>Exists</i> is <code>false</code> but you also specify a <i>Value</i>. (You cannot
        ///          expect an attribute to have a value, while also expecting it not to exist.)
        /// </para>
        ///       </li>    </ul>
        /// </summary>
        public bool Exists
        {
            get { return this._exists.GetValueOrDefault(); }
            set { this._exists = value; }
        }

        // Check to see if Exists property is set
        internal bool IsSetExists()
        {
            return this._exists.HasValue; 
        }


        /// <summary>
        /// Gets and sets the property Value.
        /// </summary>
        public AttributeValue Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        // Check to see if Value property is set
        internal bool IsSetValue()
        {
            return this._value != null;
        }

    }
}