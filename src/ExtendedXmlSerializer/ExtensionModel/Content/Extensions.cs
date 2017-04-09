﻿// MIT License
// 
// Copyright (c) 2016 Wojciech Nagórski
//                    Michael DeMond
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using ExtendedXmlSerializer.Configuration;
using ExtendedXmlSerializer.ContentModel.Conversion;
using ExtendedXmlSerializer.Core.Sources;
using ExtendedXmlSerializer.ExtensionModel.Content.Members;

namespace ExtendedXmlSerializer.ExtensionModel.Content
{
	public static class Extensions
	{
		public static IConfiguration Emit(this IConfiguration @this, IEmitBehavior behavior) => behavior.Get(@this);

		public static IMemberConfiguration Ignore(this IMemberConfiguration @this)
		{
			@this.Configuration.With<AllowedMembersExtension>().Blacklist.Add(@this.Get());
			return @this;
		}

		public static IMemberConfiguration Include(this IMemberConfiguration @this)
		{
			@this.Configuration.With<AllowedMembersExtension>().Whitelist.Add(@this.Get());
			return @this;
		}

		public static IConfiguration OnlyConfiguredProperties(this IConfiguration @this)
		{
			foreach (var type in TypeConfigurations.Defaults.Get(@this))
			{
				type.OnlyConfiguredProperties();
			}
			return @this;
		}

		public static ITypeConfiguration OnlyConfiguredProperties(this ITypeConfiguration @this)
		{
			foreach (var member in @this)
			{
				member.Include();
			}
			return @this;
		}

		public static IConfiguration Alter(this IConfiguration @this, IAlteration<IConverter> alteration)
		{
			@this.With<ConverterAlterationsExtension>().Alterations.Add(alteration);
			return @this;
		}

		public static IConfiguration OptimizeConverters(this IConfiguration @this)
			=> OptimizeConverters(@this, new Optimizations());

		public static IConfiguration OptimizeConverters(this IConfiguration @this, Optimizations optimizations)
			=> @this.Alter(optimizations);
	}
}