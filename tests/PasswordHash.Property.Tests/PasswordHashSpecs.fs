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
                  (
                   let hash =
                       PasswordHash.From(plaintext, iterations, hashSizeInBytes)

                   hash.IsMatch(plaintext))

          testProperty "Identical hashes perform identically"
          <| fun plaintext iterations hashSizeInBytes ->
              (not (isNull plaintext)
               && iterations > 0u
               && hashSizeInBytes >= 8u)
              ==> lazy
                  (let wrongPlaintext = plaintext + "!"

                   let hash1 =
                       PasswordHash.From(plaintext, iterations, hashSizeInBytes)

                   let hash2 =
                       PasswordHash(hash1.Hash, hash1.Salt, hash1.Iterations)

                   (hash1.IsMatch(plaintext) = hash2.IsMatch(plaintext))
                   && (hash1.IsMatch(wrongPlaintext) = hash2.IsMatch(wrongPlaintext))) ]
