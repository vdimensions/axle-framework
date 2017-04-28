﻿using System;
using System.Diagnostics;
using System.Reflection;


namespace Axle.Verification
{
    partial class TypeVerifier
    {
        #if net45
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        [DebuggerStepThrough]
        private static ArgumentReference<Type> IsUnchecked(this ArgumentReference<Type> argument, Type expectedType)
        {
            var actualType = argument.Value;
            if (!expectedType.GetTypeInfo().IsAssignableFrom(actualType.GetTypeInfo()))
            {
                throw new ArgumentTypeMismatchException(expectedType, actualType, argument.Name);
            }
            return argument;
        }
    }
}
