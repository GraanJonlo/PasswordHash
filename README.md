# PasswordHash

An opinionated wrapper around Pbkdf2.

## Build

```
dotnet tool restore
dotnet build
```

Tool restore will only be needed the first time and installs Paket as a CLI tool for the project.

## Test

```
dotnet test
```

Runs both the unit tests, PasswordHash.Tests, and property tests, PasswordHash.Property.Tests.

## Usage

A hash can be created via a static factory:

```csharp
string plaintext = "Fo0!";
int iterations = 1000;
int bits = 256;
int bytes = bits / 8;

PasswordHash hash = PasswordHash.From(plaintext, iterations, bytes);
```

Plaintext can be validated against a hash:

```csharp
string plaintext = "Fo0!";

bool matches = hash.IsMatch(plaintext);
```

There is a constructor that takes hash bytes, salt bytes and iterations, and the same can be retrieved as properties to enable easy persisting and restoring from a datastore.
