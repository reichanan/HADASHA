using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datagridview
{
    public class errorValues
    {



        private string ROW = string.Empty;
        private string TYPE = string.Empty;
        private string MESSAGE = string.Empty;
 

        public string _ROW
        {
            get
            {
                return ROW;
            }
            set
            {
                ROW = value;
            }
        }
        public string _TYPE
        {
            get
            {
                return TYPE;
            }
            set
            {
                TYPE = value;
            }
        }
        public string _MESSAGE
        {
            get
            {
                return MESSAGE;
            }
            set
            {
                MESSAGE = value;
            }
        }
 
    }
}
