using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace dbBrowser.Infrastructure.Utils
{
    public static class DynamicClassGenerator
    {
        public static (object instance, Type type) CreateNewObject(IDictionary<string, Type> propertyesParams)
        {
            var type = CompileResultType(propertyesParams);
            var obj = Activator.CreateInstance(type);
            return (obj, type);
        }

        public static void FillProperties(object instance, Type instanceType, IEnumerable<object> propsValues)
        {
            var propsInfo = instanceType.GetProperties();
            var currPropVal = propsValues.GetEnumerator();

            if (propsInfo.Length != propsValues.Count())
                throw new ArgumentException("PropsValues length not equal instanceType propperties count.");

            foreach (var prop in propsInfo)
            {
                currPropVal.MoveNext();
                prop.SetValue(instance, currPropVal.Current);
            }

        }
        private static Type CompileResultType(IDictionary<string, Type> propertyesParams)
        {
            TypeBuilder tb = GetTypeBuilder();

            foreach (var field in propertyesParams)
                CreateProperty(tb, field.Key, field.Value);

            Type objectType = tb.CreateType();
            return objectType;
        }

        private static TypeBuilder GetTypeBuilder()
        {
            var typeSignature = "Type" + Guid.NewGuid();
            var an = new AssemblyName(typeSignature);
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder tb = moduleBuilder.DefineType(typeSignature,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout,
                    null);
            return tb;
        }

        private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName,
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.HideBySig,
                propertyType, Type.EmptyTypes);

            ILGenerator getIL = getPropMthdBldr.GetILGenerator();
            getIL.Emit(OpCodes.Ldarg_0);
            getIL.Emit(OpCodes.Ldfld, fieldBuilder);
            getIL.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });

            ILGenerator setIL = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIL.DefineLabel();
            Label exitSet = setIL.DefineLabel();

            setIL.MarkLabel(modifyProperty);
            setIL.Emit(OpCodes.Ldarg_0);
            setIL.Emit(OpCodes.Ldarg_1);
            setIL.Emit(OpCodes.Stfld, fieldBuilder);

            setIL.Emit(OpCodes.Nop);
            setIL.MarkLabel(exitSet);
            setIL.Emit(OpCodes.Ret);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);
        }
    }
}
