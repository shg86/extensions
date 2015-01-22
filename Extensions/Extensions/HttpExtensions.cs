using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Extensions
{
    public static class HttpExtensions
    {
        //Example:
        //String value1 = Session.GetValue<String>("key1");
        //String value2 = Session.GetValue<String>("key2", "default text");

        //MyObject value3 = Session.GetValue<MyObject>("key3");
        //MyObject value4 = Session.GetValue<MyObject>("key4", new MyObject());

        public static T GetValue<T>(this HttpSessionStateBase session, string key)
        {
            return session.GetValue<T>(key, default(T));
        }

        public static T GetValue<T>(this HttpSessionStateBase session, string key, T defaultValue)
        {
            if (session[key] != null)
            {
                return (T)Convert.ChangeType(session[key], typeof(T));
            }

            return defaultValue;
        }
    }
}
