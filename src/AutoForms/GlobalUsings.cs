// Global using directives

global using System;
global using System.Linq;

#region Fix Error CS0518

// https://stackoverflow.com/a/64749403
// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices;

internal static class IsExternalInit { }

#endregion
