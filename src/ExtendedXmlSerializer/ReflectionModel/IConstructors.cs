using System.Collections.Generic;
using System.Reflection;
using ExtendedXmlSerializer.Core.Sources;

namespace ExtendedXmlSerializer.ReflectionModel
{
	interface IConstructors : IParameterizedSource<TypeInfo, IEnumerable<ConstructorInfo>> {}
}