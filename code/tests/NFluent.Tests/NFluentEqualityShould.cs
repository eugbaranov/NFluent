﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NFluentEqualityShould.cs" company="">
//   Copyright 2020 Cyrille DUPUYDAUBY
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//       http://www.apache.org/licenses/LICENSE-2.0
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using NUnit.Framework;

namespace NFluent.Tests
{
    using NFluent.Helpers;

    [TestFixture]
    public class NFluentEqualityShould
    {
        [Test]
        public void HandleAnonymousTuples()
        {
            var x = (1, 2);

            Check.That(x).IsEqualTo((1, 2));
        }

        [Test]
        public void HandleAnonymousTypesWhenEquals()
        {
            Check.That(new {x = 1, y = 2}).IsEqualTo(new {x = 1, y = 2});
        }

        [Test]
        public void HandleAnonymousTypesWhenDifferent()
        {
            Check.ThatCode(()=>
            Check.That(new {x = 1, y = 2}).IsEqualTo(new {x = 1, y = 3}))
                .IsAFailingCheck();
        }

        [Test]
        public void HandleAnonymousTypesAgainstTuples()
        {
            Check.That((1, 2)).IsEqualTo(new {Item1 = 1, Item2 = 2});
        }
    }
}