using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pung
{
    public class UnspecifiedPlayerException : System.Exception
    {
        public UnspecifiedPlayerException() { }
        public UnspecifiedPlayerException(string message) { }
        public UnspecifiedPlayerException(string message, System.Exception inner) { }

        // Constructor needed for serialization 
        // when exception propagates from a remoting server to the client.
        protected UnspecifiedPlayerException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }

}
