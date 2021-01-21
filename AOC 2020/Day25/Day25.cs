using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2020
{
    public class Day25
    {
        public long DoorPublicKey = (long)6270530;
        public long CardPublicKey = (long)14540258;

        public void RunDay25Part1()
        {
            var doorLoopSize = FindLoopSize(DoorPublicKey);
            var cardLoopSize = FindLoopSize(CardPublicKey);

            var encryptionKey = GetEncryptionKey(DoorPublicKey, cardLoopSize);
            var encryptionKeyCheck = GetEncryptionKey(CardPublicKey, doorLoopSize);

            if(encryptionKey == encryptionKeyCheck)
            {
                Console.WriteLine($"The encryptionKey is {encryptionKey}");
            }
            else
            {
                Console.WriteLine($"Something went wrong, encryptionkeys are not the same");
            }
        }

        public long GetEncryptionKey(long publicKey, long loopSize)
        {
            var encryptionKey = (long)1; 
            for(int i = 0; i < loopSize; i++)
            {
                encryptionKey = encryptionKey * publicKey;
                encryptionKey = encryptionKey % 20201227;
            }

            return encryptionKey;
        }

        public long FindLoopSize(long publicKey)
        {
            var result = (long)1;
            var loopsize = (long)0;
            while(result != publicKey)
            {
                loopsize++;
                result = result * 7;
                result = result % 20201227;

            }

            return loopsize;
        }

        public void RunDay25Part2()
        {
            
        }
    }
}
