using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AllYouMedia.Code
{
    class LogLINQSQL : System.IO.TextWriter
    {
        private readonly Action<string> action;

        public LogLINQSQL(Action<string> action)
        {
            this.action = action;
        }

        public override void Write(char[] buffer, int index, int count)
        {
            Write(new string(buffer, index, count));
        }

        public override void Write(string value)
        {
            action.Invoke(value);
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }
    }
}