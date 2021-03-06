﻿#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Axle.Text.Data.Xml
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class XmlDocumentDataReader : AbstractXmlDataReader<XmlDocument>
    {
        public XmlDocumentDataReader(StringComparer comparer) : base(comparer) { }

        protected override XmlDocument GetXmlObject(Stream stream, Encoding encoding)
        {
            var doc = new XmlDocument();
            doc.Load(new StreamReader(stream, encoding));
            return doc;
        }

        protected override XmlDocument GetXmlObject(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

        internal override XmlNodeAdapter CreateXmlNode(XmlDocument xmlObj) 
            => CreateXmlNode(xmlObj.ChildNodes.OfType<XmlNode>().Last());
        
        private XmlNodeAdapter CreateXmlNode(XmlNode xmlElement)
        {
            var allChildren = xmlElement.ChildNodes.OfType<XmlNode>();
            var value = allChildren.Where(x => x.NodeType == XmlNodeType.Text).Select(x => x.Value).SingleOrDefault();
            var children = value != null ? new XmlNode[0] : allChildren.Where(x => x.NodeType != XmlNodeType.Text);
            return new XmlNodeAdapter(
                xmlElement.Name, 
                value, 
                children.Select(CreateXmlNode).ToArray());
        }
    }
}
#endif