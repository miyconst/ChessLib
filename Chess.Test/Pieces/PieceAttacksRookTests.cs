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

namespace Chess.Test.Pieces
{
    using FluentAssertions;
    using Rudz.Chess;
    using Rudz.Chess.Enums;
    using Rudz.Chess.Types;
    using System.Linq;
    using Xunit;

    public sealed class PieceAttacksRookTests : PieceAttacksSliders
    {
        [Fact]
        public override void AlphaPattern()
        {
            const int index = (int)EBands.Alpha;
            const int sliderIndex = 1;
            var expected = RookExpected[index];
            var actuals = Bands[index].Select(x => SlideAttacks[sliderIndex](sliderIndex, BitBoard.Empty).Count);

            actuals.Should().AllBeEquivalentTo(expected);
        }

        [Fact]
        public override void BetaPattern()
        {
            const int index = (int)EBands.Beta;
            const int sliderIndex = 1;
            var expected = RookExpected[index];
            var actuals = Bands[index].Select(x => SlideAttacks[sliderIndex](sliderIndex, BitBoard.Empty).Count);

            actuals.Should().AllBeEquivalentTo(expected);
        }

        [Fact]
        public override void GammaPattern()
        {
            const int index = (int)EBands.Gamma;
            const int sliderIndex = 1;
            var expected = RookExpected[index];
            var actuals = Bands[index].Select(x => SlideAttacks[sliderIndex](sliderIndex, BitBoard.Empty).Count);

            actuals.Should().AllBeEquivalentTo(expected);
        }

        [Fact]
        public override void DeltaPattern()
        {
            const int index = (int)EBands.Delta;
            const int sliderIndex = 1;
            var expected = RookExpected[index];
            var actuals = Bands[index].Select(x => SlideAttacks[sliderIndex](sliderIndex, BitBoard.Empty).Count);

            actuals.Should().AllBeEquivalentTo(expected);
        }

        /// <summary>
        /// Testing results of blocked rook attacks, they should always return 7 on the sides, and
        /// 14 in the corner
        /// </summary>
        [Fact]
        public void RookBorderBlocked()
        {
            /*
             * Test purpose : Testing blocked bishop attacks
             */
            BitBoard border = 0xff818181818181ff;
            BitBoard borderInner = 0x7e424242427e00;
            BitBoard corners = 0x8100000000000081;

            const int expectedCorner = 14;
            const int expectedSide = 8; // 7 to each side and 1 blocked

            /*
             * borderInner (X = set bit) :
             *
             * 0 0 0 0 0 0 0 0
             * 0 X X X X X X 0
             * 0 X 0 0 0 0 X 0
             * 0 X 0 0 0 0 X 0
             * 0 X 0 0 0 0 X 0
             * 0 X 0 0 0 0 X 0
             * 0 X X X X X X 0
             * 0 0 0 0 0 0 0 0
             *
             */

            // just to get the attacks
            var board = new Board();
            var pieceValue = new PieceValue();
            var pos = new Position(board, pieceValue);

            foreach (var square in border)
            {
                var attacks = pos.GetAttacks(square, PieceTypes.Rook, borderInner);
                attacks.IsEmpty.Should().BeFalse();
                var expected = corners & square ? expectedCorner : expectedSide;
                var actual = attacks.Count;
                actual.Should().Be(expected);
            }
        }
    }
}