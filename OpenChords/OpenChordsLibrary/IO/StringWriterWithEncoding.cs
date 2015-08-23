using System;
using System.IO;
using System.Text;

namespace OpenChords.IO
{
    /// <summary>
    /// we cannot override the Encoding the StringWriter so overriding StringWriter so we can encode to UTF-8 which is needed for OpenSong
    /// </summary>
    public class StringWriterWithEncoding : StringWriter
    {
        private Encoding _encoding;

        public StringWriterWithEncoding(Encoding encoding)
        : base()
        {
            this._encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return _encoding; }
        }
    }
}
