// MIT License
// 
// Copyright (c) 2016 Wojciech Nag�rski
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

using System;
using System.Reflection;
using ExtendedXmlSerialization.Core.Specifications;

namespace ExtendedXmlSerialization.ContentModel.Members
{
	class MemberSerialization : IMemberSerialization
	{
		readonly ISpecification<PropertyInfo> _property;
		readonly ISpecification<FieldInfo> _field;
		readonly Func<MemberDescriptor, MemberProfile> _profiles;

		public MemberSerialization(
			ISpecification<PropertyInfo> property, ISpecification<FieldInfo> field,
			Func<MemberDescriptor, MemberProfile> profiles
		)
		{
			_property = property;
			_field = field;
			_profiles = profiles;
		}

		public bool IsSatisfiedBy(PropertyInfo parameter) => _property.IsSatisfiedBy(parameter);
		public bool IsSatisfiedBy(FieldInfo parameter) => _field.IsSatisfiedBy(parameter);

		public MemberProfile Get(MemberDescriptor parameter) => _profiles(parameter);
	}
}