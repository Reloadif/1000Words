using System;
using System.Collections.Generic;

namespace MobileMainProject.Services
{
    public static class Mediator
    {
        private static readonly IDictionary<string, Action<object>> mainDictionary = new Dictionary<string, Action<object>>();

        public static void Subscribe(string token, Action<object> callback)
        {
            if (!mainDictionary.ContainsKey(token))
            {
                mainDictionary.Add(token, callback);
            }
            else
            {
                mainDictionary[token] = callback;
            }
        }

        public static void Unsubscribe(string token)
        {
            if (mainDictionary.ContainsKey(token))
            {
                _ = mainDictionary.Remove(token);
            }
        }

        public static void Notify(string token, object args = null)
        {
            if (mainDictionary.ContainsKey(token))
            {
                mainDictionary[token].Invoke(args);
            }
        }
    }
}
