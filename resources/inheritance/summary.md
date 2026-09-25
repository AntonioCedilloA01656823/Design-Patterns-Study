# Inheritance

## Category
Object-oriented language feature / reuse mechanism.

## What it is and the problem it addresses
Inheritance lets a class (the **subclass**) automatically acquire the attributes and methods of another class (the **superclass**), and override or extend specific parts of that behavior. It models an **"is-a"** relationship: a `Car` *is a* `Vehicle`.

The problem it addresses: how do you define a family of related types that share a common contract and much of their implementation, while allowing each member to specialize a small part of it — without copy-pasting the shared logic into every type?

## Key vocabulary
- **Superclass / base class**: the class being extended.
- **Subclass / derived class**: the class that extends another and inherits its members.
- **Method overriding**: redefining an inherited method in the subclass to change its behavior.
- **`super()`**: calls the superclass's version of a method, letting a subclass extend rather than fully replace it.
- **Polymorphism**: treating objects of different subclasses uniformly through their shared superclass/interface.
- **Template method**: a pattern where the base class defines an algorithm's skeleton and subclasses override specific steps.
- **Liskov Substitution Principle (LSP)**: a subclass instance should be usable anywhere its superclass is expected without breaking correctness.

## Structural outline
```text
      Shape (base)
      + area() -> float
      + describe() -> str      # template method, calls self.area()
        ^              ^
        |              |
    Circle          Rectangle
    area() override  area() override
```

## When to use
- There's a genuine, stable "is-a" relationship, and subclasses satisfy the Liskov Substitution Principle everywhere the base is used.
- You want polymorphism: client code should work uniformly across all subclasses via the shared base type.
- A framework/library requires it (e.g., subclassing `Exception`, `unittest.TestCase`, a GUI framework's `Widget`).
- The hierarchy is shallow and the shared logic is stable — a template method pattern where subclasses only override a small, well-defined step.

## When not to use
- You only want to reuse a fragment of behavior or data, not model a taxonomy — that's a job for composition.
- Combining many independent behaviors would require deep hierarchies or multiple inheritance (e.g., `FlyingCar`, `AmphibiousCar`, `FlyingAmphibiousCar`).
- The base class changes often — subclasses become fragile because they depend on its internals.
- A subclass has to override a method to throw `NotImplementedError` or leave it as a no-op just to "opt out" of inherited behavior — a sign it isn't really an "is-a" of the base.

## Tradeoffs
- (+) Free reuse of an entire contract and implementation with minimal boilerplate.
- (+) Native polymorphism — `isinstance` checks and virtual dispatch work out of the box.
- (-) **Fragile base class problem**: a change to the base class can silently break subclasses that depend on its internal behavior or call order.
- (-) Tight coupling between subclass and superclass internals.
- (-) Hard to combine independent behaviors without a combinatorial explosion of subclasses.

## Inheritance vs. Composition (closest confused alternative)
- Inheritance reuses by **extending** a type — the subclass *is* a kind of the base and inherits its whole contract.
- Composition reuses by **assembling** — a class *has* helper objects and delegates specific responsibilities to them, without taking on the helper's full type identity.
- Rule of thumb: use inheritance for genuine, stable "is-a" polymorphism; default to composition when you're combining or swapping independent behaviors.
- See `../composition/summary.md` and `../composition-vs-inheritance/comparison-report.md` for the full comparison.

## Running the example
Open `example.ipynb` and run all cells. It models a `Shape` base class with a template method `describe()` that calls an overridden `area()`, demonstrating overriding, `super()`, and polymorphism across `Circle` and `Rectangle` subclasses.
