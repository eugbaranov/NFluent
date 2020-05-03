﻿// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CheckMember.cs" company="NFluent">
//   Copyright 2020 Thomas PIERRAIN & Cyrille DUPUYDAUBY
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

namespace NFluent
{
    using System;
    using Extensibility;
    using Kernel;

    /// <summary>
    /// Offers checks on members of an object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TM"></typeparam>
    public class CheckMember<T, TM> : IErrorReporter
    {
        private readonly ICheck<T> originalCheck;
        private readonly TM value;
        private readonly string name;
        private string errors;

        internal CheckMember(ICheck<T> originalCheck, TM value, string name)
        {
            this.originalCheck = originalCheck;
            this.value = value;
            this.name = name;
        }

        /// <summary>
        /// Allows to perform arbitrary checks on any member of an object.
        /// </summary>
        /// <param name="func">member extractor method</param>
        /// <returns>a check object for the extracted member.</returns>
        public CheckMember<T, TM> Verifies(Action<ICheck<TM>> func)
        {
            func(new FluentCheck<TM>(this.value, this).As(name));
            if (!string.IsNullOrEmpty(this.errors))
            {
                var message = "The {checked} fails the check because:"+this.errors;
                ExtensibilityHelper.ExtractChecker(this.originalCheck).BeginCheck().CantBeNegated("Verifies").Fail(message, MessageOption.NoCheckedBlock).EndCheck();
            }
            return this;
        }

        void IErrorReporter.ReportError(string message)
        {
            this.errors = message;
        }
    }
}