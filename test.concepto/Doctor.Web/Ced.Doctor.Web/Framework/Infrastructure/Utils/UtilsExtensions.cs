using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Framework.Infrastructure.Utils
{
    public static class UtilsExtensions
    {
        /// <summary>
        /// To the XML document.
        /// </summary>
        /// <param name="xDocument">The x document.</param>
        /// <returns></returns>
        /// TODO Edit XML Comment Template for ToXmlDocument
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        /// <summary>
        /// To the x document.
        /// </summary>
        /// <param name="xmlDocument">The XML document.</param>
        /// <returns></returns>
        /// TODO Edit XML Comment Template for ToXDocument
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }

        /// <summary>
        /// To the XML document.
        /// </summary>
        /// <param name="xElement">The x element.</param>
        /// <returns></returns>
        /// TODO Edit XML Comment Template for ToXmlDocument
        public static XmlDocument ToXmlDocument(this XElement xElement)
        {
            var sb = new StringBuilder();
            var xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false };
            using (var xw = XmlWriter.Create(sb, xws))
            {
                xElement.WriteTo(xw);
            }
            var doc = new XmlDocument();
            doc.LoadXml(sb.ToString());
            return doc;
        }

        /// <summary>
        /// To the memory stream.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        /// TODO Edit XML Comment Template for ToMemoryStream
        public static Stream ToMemoryStream(this XmlDocument doc)
        {
            var xmlStream = new MemoryStream();
            doc.Save(xmlStream);
            xmlStream.Flush();//Adjust this if you want read your data 
            xmlStream.Position = 0;
            return xmlStream;
        }

        /// <summary>
        /// To the stream.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        /// TODO Edit XML Comment Template for ToStream
        public static Stream ToStream(this string str)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(str);
            return new MemoryStream(byteArray);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// TODO Edit XML Comment Template for ToString
        public static string ToString(this Stream stream)
        {
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
