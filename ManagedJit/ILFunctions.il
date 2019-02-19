.assembly extern mscorlib {}
.assembly ILFunctions {}
.module ILFunctions.dll

.class public auto ansi abstract sealed ILFunctions.Utils
	extends [System.Runtime]System.Object
{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
            01 00 00 00
        )

	.method public static hidebysig !!T GetDelegateUnsafe<([mscorlib]System.Delegate) T>(
	native int ptr) cil managed
	{
		ldnull
		ldarg.0
		newobj instance void !!T::.ctor(object, native int)
		ret
	}

	.method public static hidebysig uint8& GetPinnableReference(
	object& obj) cil managed
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
            01 00 00 00
        )

		.locals init (
			object pinned 'obj',
			uint8** 'ptr'
		)

		ldarg.0
		dup
		ldind.ref
		stloc.0
		conv.u
		ldind.i
		conv.u
		ret
	}
}