using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBusMessaging
{

    public class MyPayload
    {
        public string Name { get; set; }

        public int Goals { get; set; }

        public bool Delete { get; set; }
    }

}
