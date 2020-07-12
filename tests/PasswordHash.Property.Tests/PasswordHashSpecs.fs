module Tests

open Expecto
open FsCheck

open PasswordHash

let config =
    { FsCheckConfig.defaultConfig with maxTest = 10000 }

[<Tests>]
let properties = testList "PasswordHash properties" [
    testProperty "Hash matches plaintext" <|
        fun plaintext ->
            not (isNull plaintext)
            ==>
            lazy
                (let hash = PasswordHash.From(plaintext, 100u, 128u)
                hash.IsMatch(plaintext))
]
