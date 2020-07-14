module Tests

open Expecto
open FsCheck

open PasswordHash

[<Tests>]
let properties =
    testList "PasswordHash properties"
        [ testProperty "Hash matches plaintext"
            <| fun plaintext iterations hashSizeInBytes ->
                (not (isNull plaintext)
                && iterations > 0u
                && hashSizeInBytes >= 8u)
                ==> lazy
                    (let hash = PasswordHash.From(plaintext, iterations, hashSizeInBytes)
                    hash.IsMatch(plaintext)) ]
