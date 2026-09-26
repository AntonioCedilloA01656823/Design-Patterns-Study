# Strategy Pattern

## Category
Behavioral pattern.

## Intent
Define a family of algorithms, encapsulate each one, and make them interchangeable.
The client (through a context object) can pick or swap an algorithm at runtime
without changing the code that uses the result.

The problem it solves: a class grows a thicket of conditionals (`if mode == "express"`,
`elif mode == "overnight"`, ...) because several ways of doing the same job live
inside it. Adding a new algorithm then means editing that class. Strategy moves
each algorithm into its own object behind a shared interface, so the context
delegates instead of branching.

## Roles and structure

- **Strategy** — declares a common interface for the algorithm (e.g., `cost()`).
- **ConcreteStrategy** — implements one algorithm variant
  (`StandardShipping`, `ExpressShipping`).
- **Context** — holds a Strategy reference and delegates the varying work to it.
  It may also let the client replace the strategy later.
- **Client** — chooses a concrete strategy and hands it to the context.

```
Client --> Context ----delegates----> Strategy (interface)
                                          ^
                           ConcreteStrategyA, ConcreteStrategyB
```

## When to use it

- Several algorithms solve the same problem and you need to switch among them
  (including at runtime).
- A class is full of conditionals that select behavior.
- You want to isolate algorithm details so the context stays simple and testable.

## When not to use it

- There is only one algorithm, or it never varies — a plain method is enough.
- Variants differ only by a number or a flag — pass a parameter, not a class.
- The extra types do not pay for themselves in a small script.

## Tradeoffs

- **Pro:** Open/Closed for new algorithms (add a class, leave the context alone);
  runtime substitution; each algorithm can be tested in isolation.
- **Con:** More types to name and wire; the client must know which strategy to
  pick; context and strategy may need to share data through the interface.
- Strategy is not always the best choice. A function object or a simple `if`
  can be clearer when you have two tiny variants.

## Comparison with State (closest confused pattern)

- **Strategy:** the *client* (or configuration) chooses the algorithm. Strategies
  are usually independent and do not switch themselves.
- **State:** the *object* changes behavior as its internal state changes. States
  often know about each other and trigger transitions.
- Rule of thumb: Strategy = "how should this job be done?" State = "what mode
  is this object in right now?"

Template Method is a second cousin: it varies steps through inheritance inside
one class hierarchy. Strategy varies the whole algorithm through composition.

## Running the example

Open `example.ipynb` and run all cells. A `Checkout` context delegates shipping
cost to interchangeable `ShippingStrategy` objects.

Then read `example.cs` for the same example in C# (`IShippingStrategy` +
`Checkout`). If you have the .NET SDK, you can paste it into a console
`Program.cs` and run it.

Then work through `exercises.ipynb`. Do not look up solutions until you have
tried each exercise.
