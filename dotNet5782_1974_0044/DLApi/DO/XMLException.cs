using System;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        public string FilePath { get; set; }
        public Exception XmlException { get; set; }

        public XMLFileLoadCreateException() { }
        public XMLFileLoadCreateException(string filePath,string message):base(message) { FilePath = filePath; }
        public XMLFileLoadCreateException(string filePath,string message,Exception exception):base(message) { FilePath = filePath;
                                                                                                              XmlException = exception; }

        public XMLFileLoadCreateException(string message) : base(message) { }

        public XMLFileLoadCreateException(string message, Exception innerException) : base(message, innerException) { }

        protected XMLFileLoadCreateException(SerializationInfo info, StreamingContext context) : base(info, context){ }
   
    }
}