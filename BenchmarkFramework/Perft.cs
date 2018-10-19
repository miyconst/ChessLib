﻿using System;
using BenchmarkDotNet.Running;
using Rudz.Chess;

namespace BenchmarkFramework
{
    public partial class Perft
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<PerftBench>();

            //Console.WriteLine("ChessLib Perft test program v0.1.");
            //Console.WriteLine("Use Perft.exe <depth> to set depth (1-6), default is 5.");
            //int depth = 0;
            //if (args.Length > 0)
            //    depth = args[0].ToIntegral();

            //if (!depth.InBetween(1, 6))
            //    depth = 5;

            //Console.WriteLine($"Running perft test with depth {depth}.");
            //if (depth == 6)
            //    Console.WriteLine("Brace yourself, this could take a little while.");

            //void Callback(string s, ulong v) { Console.WriteLine($"Position   : {s}\nMove Count : {v}"); }

            //Perft p = new Perft(depth) { CallBack = Callback };
            //Stopwatch watch = Stopwatch.StartNew();
            //ulong result = p.DoPerft();
            //long elapsedMs = watch.ElapsedMilliseconds;

            //Console.WriteLine($"Time (ms)  : {elapsedMs}");
            //Console.WriteLine($"Nps        : {1000 * result / (ulong)elapsedMs}");
            //Console.WriteLine(Positions[0].value[depth - 1] == result
            //    ? "Move count matches!"
            //    : "Move count failed!");
        }

        public Perft(int depth) => _perftLimit = depth;

        /// <summary>
        /// How deep the test should proceed.
        /// </summary>
        private readonly int _perftLimit;

        /// <summary>
        /// To notify about update.
        /// </summary>
        private Action<string, ulong> CallBack { get; set; }

        public ulong DoPerft()
        {
            Game game = new Game();
            ulong total = 0;
            foreach (PerftPositions perftPositions in Positions)
            {
                game.SetFen(perftPositions.fen);
                ulong res = game.Perft(_perftLimit);
                total += res;
                CallBack?.Invoke(perftPositions.fen, res);
            }

            return total;
        }
    }
}