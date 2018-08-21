﻿namespace Axle

open System.Runtime.CompilerServices

/// Credit goes to Luis Diego Fallas @ http://langexplr.blogspot.com/2008/06/using-f-option-types-in-c.html
[<Extension>]
[<AutoOpen>]
[<CompiledName("Option")>]
module Option =
   [<Extension>]
   let HasValue<'a> (opt : 'a option) =
        match opt with
        | Some _ -> true
        | None -> false

   [<Extension>]
   let GetValueOrDefault<'a>(opt : 'a option, defaultValue: 'a) =
        match opt with
        | Some v -> v
        | None -> defaultValue
