# Composition

## Category
Object-oriented design technique / reuse mechanism.

## What it is and the problem it addresses
Composition lets an object reuse behavior by **holding a reference** to one or more other objects and **delegating** work to them, instead of inheriting their code. It models a **"has-a"** (or "uses-a") relationship: a `Car` *has an* `Engine`.

The problem it addresses: how do you reuse and combine independent behaviors flexibly — including swapping them at runtime — without being locked into a rigid class hierarchy?

## Key vocabulary
- **Delegation**: forwarding a call to a contained (composed) object instead of implementing the behavior directly.
- **Has-a relationship**: the composing object owns/holds a reference to another object that provides some capability.
- **Dependency injection**: passing the composed object in (typically via the constructor) rather than creating or hardcoding it internally, so it can be swapped or mocked.
- **Interface / Protocol**: a small shared contract (e.g., Python's `Protocol` or an ABC) that composed objects implement, enabling polymorphism without inheritance from a common base.
- **Strategy**: a common pattern built on composition — a behavior object ("strategy") is injected and can be swapped at runtime.

## Structural outline
```text
Notifier
  - sender: MessageSender      <-- has-a, injected via constructor
  + notify(text) -> sender.send(text)

MessageSender (Protocol)      EmailSender     SmsSender
  + send(text)                 send(text)      send(text)
```

## When to use
- You need to combine independent, orthogonal behaviors (e.g., a character with a movement style, a weapon, and an AI strategy, mixed freely).
- Behavior must change at runtime (e.g., switching a `PaymentMethod` or `CompressionStrategy` based on user input).
- You want easier unit testing — composed dependencies can be replaced with mocks/fakes without touching a class hierarchy.
- You notice you'd otherwise subclass just to reuse one or two methods, not to be substitutable as that type.

## When not to use
- The relationship is truly "is-a" and you need built-in polymorphic substitution — composition alone doesn't give you that without also defining a shared interface.
- The relationship is simple and stable, and delegating every single method of the composed object adds needless boilerplate with no real flexibility gained.

## Tradeoffs
- (+) Loose coupling — depends only on the composed object's public interface, not its internals.
- (+) Runtime flexibility — swap composed objects on the fly.
- (+) Easier to test in isolation (inject fakes/mocks).
- (-) More boilerplate — constructors and delegating methods must be written explicitly.
- (-) No built-in polymorphism — you must define and share a small interface/protocol yourself if you want substitutability.
- (-) Overuse can produce a "thin wrapper" class that just forwards every call with no added value.

## Composition vs. Inheritance (closest confused alternative)
- Composition reuses by **assembling** — the containing object *has* helper objects and delegates specific responsibilities to them.
- Inheritance reuses by **extending** a type — the subclass *is* a kind of the base and inherits its whole contract.
- The common guidance "favor composition over inheritance" means: default to composition for flexible reuse, and reserve inheritance for genuine, stable "is-a" polymorphism.
- See `../inheritance/summary.md` and `../composition-vs-inheritance/comparison-report.md` for the full comparison.

## Running the example
Open `example.ipynb` and run all cells. It models a `Notifier` that delegates message delivery to an injected `MessageSender` (`EmailSender` or `SmsSender`), demonstrating delegation and swapping behavior at runtime without any class hierarchy.
