# Composition vs. Inheritance — Comparison Report

This report compares composition and inheritance directly and gives concrete guidance on which to reach for in common situations.

## Side-by-side

| Aspect | Inheritance | Composition |
|---|---|---|
| Relationship modeled | "is-a" | "has-a" / "uses-a" |
| Reuse mechanism | Extend a base class | Hold a reference and delegate |
| Coupling to implementation | Tight — subclass can depend on base internals (protected members, call order) | Loose — depends only on the composed object's public interface |
| Flexibility at runtime | Fixed once the object is constructed (its class doesn't change) | High — the composed object can be swapped out at runtime |
| Combining behaviors | Awkward — combining N independent behaviors needs up to 2^N subclasses or multiple inheritance | Natural — inject as many behavior objects as needed |
| Testability | Harder to isolate — testing a subclass often exercises base class code too | Easier — replace composed dependencies with mocks/fakes |
| Boilerplate | Low — behavior is inherited for free | Higher — constructors and delegating methods needed |
| Typical failure mode | Fragile base class; deep/rigid hierarchies; forced subclassing to reuse one method | Over-delegation clutter; "God object" gluing too many parts together |
| Polymorphism support | Native (`isinstance`, virtual dispatch) | Requires explicitly shared interface/protocol |

## Situational guidance

**Prefer inheritance when:**
- Modeling a genuine taxonomy that rarely changes, e.g. `Shape -> Circle, Square` where every subclass truly *is* a shape and shares a stable contract (`area()`, `perimeter()`).
- Working with a framework/library that requires subclassing (e.g., Python's `Exception`, Django's `Model`, `unittest.TestCase`).
- The hierarchy is shallow (ideally one level) and all subclasses use the base's behavior largely as-is, only overriding a small, well-defined part (template method pattern).

**Prefer composition when:**
- You need to combine independent, orthogonal behaviors (e.g., a game character that can independently have a movement style, a weapon, and an AI strategy).
- Behavior needs to change at runtime (e.g., switching a `CompressionStrategy` or `PaymentMethod` based on user input).
- You're building for testability — composed dependencies can be injected as fakes/mocks in unit tests without touching a class hierarchy.
- You notice you're subclassing only to reuse one or two methods, not to be substitutable as that type.

**Warning signs you're using the wrong one:**
- Inheritance misuse: you override a method to throw `NotImplementedError` or leave it empty just to "opt out" of inherited behavior — the subclass isn't really an "is-a" of the base.
- Inheritance misuse: you need multiple inheritance or deep chains (3+ levels) to express valid combinations of behavior.
- Composition misuse: you're manually re-declaring and forwarding every single method of the composed object with no added value — a thin interface plus dependency injection may be all you need, or the relationship might actually be a true "is-a" after all.

## Practical rule of thumb
Ask: *"Is this new class fundamentally a specialized version of the other, satisfying the same contract everywhere it's used (Liskov substitution)?"*
- If **yes** and the hierarchy stays shallow and stable — inheritance is a reasonable, low-boilerplate choice.
- If **no**, or you're really just borrowing some behavior/data — use composition, and if you need polymorphism, extract a small interface (ABC/Protocol) that both the container and composed parts can honor.

Most real-world codebases use **both**: composition for assembling flexible, testable objects, and a thin layer of inheritance for genuine type hierarchies (e.g., implementing a shared interface, or lightweight framework base classes).
