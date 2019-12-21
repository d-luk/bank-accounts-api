using System;
using System.Collections.Generic;

namespace BankAccountsAPI.Infrastructure.InMemory
{
    public sealed class IDGenerator
    {
        private readonly Dictionary<Type, int> highestIDs = new Dictionary<Type, int>();

        public int GenerateFor<T>()
        {
            var type = typeof(T);
            var highestID = 0;

            if (highestIDs.ContainsKey(type))
            {
                highestID = highestIDs[type] + 1;
                highestIDs[type] = highestID;
            }
            else
            {
                highestIDs.Add(type, highestID);
            }

            return highestID;
        }
    }
}
