using System;

namespace ServiceBusMessaging
{
    public interface IProcessData
    {
        void Process(MyPayload myPayload);
    }
}
