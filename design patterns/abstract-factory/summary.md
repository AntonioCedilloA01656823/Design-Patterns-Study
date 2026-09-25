# Abstract Factory

## Intent

Provide an interface for creating **families of related or dependent objects**
without specifying their concrete classes. The client works only against
abstract product interfaces and an abstract factory interface; it never
instantiates concrete classes directly.

The problem it solves: when a system must stay consistent across a *set* of
products (e.g., all UI widgets must match one theme, all DB objects must
belong to one driver), letting the client pick classes one at a time risks
mixing incompatible objects. Abstract Factory groups related creation logic
behind one factory per family, so swapping the whole family is a single
substitution.

## Roles and structure

- **AbstractFactory** — declares creation methods for each kind of product
  (e.g., `create_button()`, `create_checkbox()`).
- **ConcreteFactory** — implements those methods to produce one consistent
  family of products (e.g., `DarkThemeFactory`, `LightThemeFactory`).
- **AbstractProduct** — declares the interface for a kind of product
  (e.g., `Button`, `Checkbox`).
- **ConcreteProduct** — a specific product implementation belonging to one
  family (e.g., `DarkButton`, `LightButton`).
- **Client** — is given an `AbstractFactory` instance and uses only the
  abstract product interfaces; it is unaware of concrete classes.

```
Client -> AbstractFactory -> creates -> AbstractProduct(s)
             ^                              ^
      ConcreteFactoryA               ConcreteProductA (family A)
      ConcreteFactoryB               ConcreteProductB (family B)
```

## When to use it

- The system must be independent of how its products are created, composed,
  and represented.
- Products from one family must always be used together, and this constraint
  must be enforced by the system, not by convention.
- You want to support adding new families later by adding a new factory,
  without touching client code.

## When not to use it

- You only ever create one kind of object, or the objects have no family
  relationship — a plain Factory Method or a constructor is simpler.
- The set of product *kinds* changes often. Every new product kind (e.g.,
  adding `create_slider()`) forces a change to the abstract factory interface
  and every concrete factory, which is expensive.
- The extra layer of indirection isn't paying for itself in a small program.

## Tradeoffs

- **Pro:** Enforces consistency within a family; isolates concrete classes
  from client code; adding a new family is a pure extension (new factory +
  new products), honoring the Open/Closed Principle for families.
- **Con:** Adding a new *product kind* violates the Open/Closed Principle
  from the other direction — it requires touching the abstract factory and
  every concrete factory.
- **Con:** More classes/interfaces than direct instantiation; can be
  overkill for simple object creation.

## Comparison with Factory Method

- **Factory Method** is a single virtual method, usually overridden by
  subclassing, that creates *one* product. Variation happens through
  inheritance.
- **Abstract Factory** is an object (often built *using* several Factory
  Methods internally) that creates *several related products* as a
  cohesive family. Variation happens through object composition — the
  client is handed a different factory instance, no subclassing required.
- Rule of thumb: if you're picking one class to vary, Factory Method; if
  you're picking a matched set of classes that must vary together, Abstract
  Factory.

## Running the example

Open `example.ipynb` and run all cells. It defines a `Button`/`Checkbox`
abstract product pair, `LightThemeFactory` and `DarkThemeFactory` concrete
factories, and a `render_ui(factory)` client function that renders a
consistent UI regardless of which factory it receives.

Then work through `exercises.ipynb`, which builds on the same example.
