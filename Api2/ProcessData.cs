using Api2.Model;
using ServiceBusMessaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api2
{
    public class ProcessData : IProcessData
    {
        public void Process(MyPayload myPayload)
        {
            DataServiceSimi.Data.Add(new Payload
            {
                Name = myPayload.Name,
                Goals = myPayload.Goals
            });
        }
    }
}
