# Composition vs. Inheritance

## Category
Object-oriented design technique / structuring principle (how classes reuse and share behavior).

## What it is and the problem it addresses
Both **inheritance** and **composition** let you reuse behavior across classes instead of duplicating code.

- **Inheritance** ("is-a"): a subclass extends a base class, automatically acquiring its attributes and methods, and may override them.
- **Composition** ("has-a"): an object holds a reference to one or more other objects and delegates work to them, rather than inheriting their code.

The problem both address: *how do I reuse existing behavior without copy-pasting it?* They differ in how tightly the reusing class is coupled to the reused implementation.

## Key vocabulary
- **Superclass / base class**: the class being extended.
- **Subclass / derived class**: the class that extends another.
- **Delegation**: forwarding a call to a contained (composed) object instead of implementing it directly.
- **Has-a vs is-a relationship**: composition models "has-a" (a `Car` has an `Engine`); inheritance models "is-a" (a `Car` is a `Vehicle`).
- **Fragile base class problem**: changes to a base class can silently break subclasses that depend on its internal behavior.
- **Composition root / dependency injection**: passing composed objects in (usually via constructor) rather than hardcoding them.

## Structural outline
```text
Inheritance:                     Composition:
  Vehicle (base)                   Vehicle
    speed()                          - engine: Engine   <-- has-a
    ^                                  speed() -> engine.power()
    |
  Car(Vehicle)                     Engine
    speed() override                  power()
```

## When to use inheritance
- There is a genuine, stable "is-a" relationship that is unlikely to change.
- You want polymorphism through a shared interface/base class and subclasses share most of the base's behavior unchanged.
- The hierarchy is shallow (1-2 levels) and well understood (e.g., framework-required base classes like `Exception` subclasses).

## When not to use inheritance
- You only want to reuse a fragment of behavior, not model a taxonomy.
- The hierarchy would need multiple inheritance or deep chains to express combinations of behavior (e.g., `FlyingCar`, `SwimmingCar`, `FlyingSwimmingCar`).
- The base class may change independently of subclasses, risking the fragile base class problem.

## When to use composition
- You need to mix and match independent behaviors (engines, loggers, storage strategies) without an explosion of subclasses.
- You want to swap behavior at runtime (e.g., change a `PaymentStrategy` object).
- You want to keep classes decoupled and easier to unit test (compose with mocks/fakes).

## When not to use composition
- The relationship truly is "is-a" and polymorphic substitution is the goal — composition can't give you `isinstance` style substitutability without also defining a shared interface.
- Excessive delegation boilerplate for a trivial, stable relationship adds needless indirection.

## Tradeoffs
| | Inheritance | Composition |
|---|---|---|
| Coupling | Tight (subclass depends on base internals) | Loose (depends only on composed object's interface) |
| Flexibility | Fixed at compile/class-definition time | Can change at runtime |
| Reuse granularity | Whole class behavior | Pick-and-choose individual behaviors |
| Risk | Fragile base class, deep/rigid hierarchies | More boilerplate (constructors, delegation methods) |
| Polymorphism | Built-in via subclassing | Requires an explicit shared interface/protocol |

## Comparison with the closest confused alternative
Inheritance and composition are often confused because both enable "reuse", but they solve different problems:
- Inheritance reuses by **extending** a type — the subclass *is* a kind of the base type and inherits its full contract.
- Composition reuses by **assembling** — the containing object *has* helper objects and delegates specific responsibilities to them, without taking on the helper's full type identity.
- The common design guidance "favor composition over inheritance" doesn't mean inheritance is wrong — it means use inheritance only for genuine is-a polymorphism, and default to composition for code reuse and flexibility.

## Running the example
Open `example.ipynb` and run all cells. It models vehicles two ways: first with a rigid inheritance hierarchy that breaks down when combining behaviors (e.g., a car that can also fly), then the same problem solved with composition using swappable movement-behavior objects.

See `comparison-report.md` in this folder for a deeper side-by-side comparison and situational guidance.
