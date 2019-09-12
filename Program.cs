using System;
using System.Collections.Generic;
using System.Linq;

static class Program
{
    static void Main(string[] args)
    {
        const int deckSize = 40;
        const int rounds = 10_000;
        var exchanges = PlayMultipleRounds(GranNannySolitaire, deckSize, rounds).ToArray();
        var success = exchanges.Where(e => e == 40).Count();
        var count = exchanges.Count();
        var successPerc = (double)success / (double) count;
        Console.WriteLine(successPerc * 100);
    }

    static IEnumerable<int> PlayMultipleRounds(Func<int, int> game, int deckSize, int rounds) =>
        Enumerable.Range(0, rounds).Select(_ => game(deckSize));


    static void Shuffle<T>(Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    static int GranNannySolitaire(int deckSize)
    {
        var rng = new Random();
        var deck = Enumerable.Range(0, deckSize).ToArray();
        Shuffle(rng, deck);

        var exchanges = 0;
        var aces = 0;
        var idx = 0;
        while (aces <= 4)
        {
            var newIdx = deck[idx];
            deck[idx] = idx;
            idx = newIdx;
            exchanges++;
            if (idx % 10 == 0) aces++;
        }
        return exchanges;
    }
}
