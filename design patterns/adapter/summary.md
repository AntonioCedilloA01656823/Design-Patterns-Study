# Adapter Pattern

## Category
Structural pattern.

## Intent
Convert the interface of an existing class into another interface clients expect. Lets classes with incompatible interfaces work together without modifying their source code.

## Problem it solves
You have an existing class (the "adaptee") whose interface doesn't match what your client code needs. You can't or don't want to change the adaptee (third-party library, legacy code, no source access). You need a bridge that translates calls from the client's expected interface into calls the adaptee understands.

## Roles
- **Target**: the interface the client expects to use.
- **Client**: the code that calls methods on the Target interface.
- **Adaptee**: the existing class with an incompatible interface, containing the useful behavior.
- **Adapter**: implements the Target interface and translates/delegates calls to the Adaptee.

## Structural outline
```
Client ---> Target (interface)
                ^
                |
             Adapter ---> Adaptee (existing class)
```

Two variants:
- **Object Adapter** (composition): Adapter holds a reference to an Adaptee instance and delegates. Works with any subclass of Adaptee. (Used in the example below — preferred in Python.)
- **Class Adapter** (inheritance): Adapter inherits from both Target and Adaptee (needs multiple inheritance). Less flexible, tighter coupling.

## When to use
- Integrating a third-party or legacy class whose interface doesn't match your code.
- You want to reuse existing functionality but can't alter its interface.
- You need multiple existing classes with different interfaces to work behind one unified interface.

## When not to use
- You control the source of the incompatible class — just change its interface directly.
- The mismatch is trivial (a rename) — a simple wrapper function may suffice instead of a full pattern.
- You need to add new behavior, not just translate calls — consider Decorator instead.

## Tradeoffs
- (+) Decouples client from concrete adaptee implementation; adaptee stays unmodified.
- (+) Follows Open/Closed Principle: add new adapters without touching client code.
- (-) Adds a layer of indirection — more classes/objects to track.
- (-) Object adapter can't override adaptee behavior easily (no access to protected members); class adapter can, but costs multiple inheritance and language support.

## Adapter vs Facade (closest confused pattern)
- **Adapter**: converts one existing interface into another interface the client already expects. One-to-one translation, no new simplified interface — it matches something specific.
- **Facade**: defines a *new*, simplified interface over a complex subsystem (multiple classes). It doesn't need to match any pre-existing target interface — its goal is simplification, not translation/compatibility.
- Rule of thumb: Adapter = "make this fit an interface I already need." Facade = "make this subsystem simpler to use."

## Running the example
Open `example.ipynb` and run all cells. It demonstrates adapting a legacy/third-party `XMLDataSource` to the `JSONDataSource` interface a client expects, using an Object Adapter.
