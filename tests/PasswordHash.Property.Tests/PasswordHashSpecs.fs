module Tests

open Expecto
open FsCheck

open PasswordHash

[<Tests>]
let properties = testList "PasswordHash properties" [
    testProperty "Hash matches for not null plaintext" <|
        fun plaintext ->
            not (isNull plaintext)
            ==>
            lazy
                (let hash = PasswordHash.From(plaintext, 100u, 128u)
                 hash.IsMatch(plaintext))

    testProperty "Hash matches for positive iterations" <|
        fun iterations ->
            iterations > 0u
            ==>
            lazy
                (let plaintext = "Fo0!"
                 let hash = PasswordHash.From(plaintext, iterations, 128u)
                 hash.IsMatch(plaintext))
]
