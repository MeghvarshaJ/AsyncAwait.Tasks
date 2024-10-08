﻿using System.Threading;

namespace AsyncAwait.Task1.CancellationTokens;

internal static class Calculator
{
    // todo: change this method to support cancellation token
    
    public static long Calculate(int n, CancellationToken token)
    {
        long sum = 0;

        for (var i = 0; i < n; i++)
        {
            //token.ThrowIfCancellationRequested(); // Check for cancellation
            if (token.IsCancellationRequested)
            {                
                return 0;
            }
            // i + 1 is to allow 2147483647 (Max(Int32)) 
            sum += (i + 1);
            Thread.Sleep(10); // Simulate work
        }

        return sum;
    }
}
