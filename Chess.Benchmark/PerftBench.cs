﻿/*
ChessLib, a chess data structure library

MIT License

Copyright (c) 2017-2020 Rudy Alex Kohn

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rudz.Chess.Fen;

namespace Chess.Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Perft;
    using Perft.Interfaces;

    //[ClrJob(true), CoreJob]
    //[RPlotExporter, RankColumn]
    public class PerftBench
    {
        private IPerft _perft;

        private readonly IPerftPosition _pp;

        public PerftBench()
        {
            _pp = PerftPositionFactory.Create(
                Guid.NewGuid().ToString(),
                Fen.StartPositionFen,
                new List<(int, ulong)>(6)
                {
                    (1, 20),
                    (2, 400),
                    (3, 8902),
                    (4, 197281),
                    (5, 4865609),
                    (6, 119060324)
                });
        }

        [Params(5, 6)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            _perft = PerftFactory.Create();
            _perft.AddPosition(_pp);
        }

        [Benchmark]
        public async Task<ulong> PerftIAsync()
        {
            var total = 0ul;

            await foreach (var res in _perft.DoPerft(N).ConfigureAwait(false))
                total += res;

            return total;
        }

        [Benchmark]
        public async Task<ulong> Perft()
        {
            return await _perft.DoPerftAsync(N);
        }

    }
}