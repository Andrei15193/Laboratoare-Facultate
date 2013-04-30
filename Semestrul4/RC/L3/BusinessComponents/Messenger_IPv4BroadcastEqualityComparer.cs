using System;
using System.Collections.Generic;

namespace UDPMessenger.BusinessComponents
{
    partial class Messenger
    {
        private class _IPv4BroadcastEqualityComparer : IEqualityComparer<byte[]>
        {
            public bool Equals(byte[] x, byte[] y)
            {
                if (x != null && y != null && x.Length == y.Length)
                {
                    int i = 0;
                    while (i < x.Length && x[i] == y[i])
                        i++;
                    return (i == x.Length);
                }
                else
                    return (x == null && y == null);
            }

            public int GetHashCode(byte[] addressBytes)
            {
                if (addressBytes != null)
                {
                    int sum = 0;
                    foreach (byte addressByte in addressBytes)
                        sum += addressByte;
                    return sum;
                }
                else
                    throw new ArgumentNullException("Cannot calculate hash code of null reference!");
            }
        }
    }
}
