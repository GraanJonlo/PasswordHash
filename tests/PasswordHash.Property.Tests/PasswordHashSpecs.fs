module Tests

open Expecto
open FsCheck

open PasswordHash

let config =
    { FsCheckConfig.defaultConfig with maxTest = 10000 }

[<Tests>]
let properties = testList "PasswordHash properties" [
    testProperty "Hash matches plaintext" <|
        fun (plaintext:string) ->
            not (isNull plaintext) ==>
                lazy (
                        let hash = PasswordHash.From(plaintext, (uint)100, (uint)128)
                        hash.IsMatch(plaintext))
]
