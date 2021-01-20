﻿/*
 * SonarAnalyzer for .NET
 * Copyright (C) 2015-2021 SonarSource SA
 * mailto: contact AT sonarsource DOT com
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program; if not, write to the Free Software Foundation,
 * Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SonarAnalyzer.UnitTest.MetadataReferences;
using SonarAnalyzer.UnitTest.TestFramework;
using CS = SonarAnalyzer.Rules.CSharp;

namespace SonarAnalyzer.UnitTest.Rules
{
    [TestClass]
    public class MethodOverrideChangedDefaultValueTest
    {
        [TestMethod]
        [TestCategory("Rule")]
        public void MethodOverrideChangedDefaultValue() =>
            Verifier.VerifyAnalyzer(@"TestCases\MethodOverrideChangedDefaultValue.cs",
                new CS.MethodOverrideChangedDefaultValue(),
#if NETFRAMEWORK
                additionalReferences: NuGetMetadataReference.NETStandardV2_1_0,
#endif
                options: ParseOptionsHelper.FromCSharp8);

#if NET
        [TestMethod]
        [TestCategory("Rule")]
        public void MethodOverrideChangedDefaultValue_CSharp9() =>
            Verifier.VerifyAnalyzerFromCSharp9Library(@"TestCases\MethodOverrideChangedDefaultValue.CSharp9.cs", new CS.MethodOverrideChangedDefaultValue());
#endif

        [TestMethod]
        [TestCategory("CodeFix")]
        public void MethodOverrideChangedDefaultValue_CodeFix() =>
            Verifier.VerifyCodeFix(
                @"TestCases\MethodOverrideChangedDefaultValue.cs",
                @"TestCases\MethodOverrideChangedDefaultValue.Fixed.cs",
                @"TestCases\MethodOverrideChangedDefaultValue.Fixed.Batch.cs",
                new CS.MethodOverrideChangedDefaultValue(),
                new CS.MethodOverrideChangedDefaultValueCodeFixProvider(),
                options: ParseOptionsHelper.FromCSharp8,
                additionalReferences: NuGetMetadataReference.NETStandardV2_1_0);
    }
}
