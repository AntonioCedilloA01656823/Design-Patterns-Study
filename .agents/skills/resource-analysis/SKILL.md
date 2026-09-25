---
name: resource-analysis
description: Analyze a general programming resource or concept (e.g. composition, inheritance, a specific language, SOLID principles) that is not a named design pattern, explain when and how to use it, and create small code examples and exercises when requested.
---

# Resource analysis

Use this skill when the user asks to understand, compare, or practice a general programming concept, technique, principle, or language that is **not** itself a named design pattern (for example: composition vs. inheritance, polymorphism, dependency injection, SOLID principles, a specific programming language's features, or immutability). 
## Inputs

- The resource/topic name.
- Optional context, such as a related design problem, comparison target, or desired difficulty.

Generated examples and exercises always use Python, unless the topic itself is a specific language (e.g. "learn Rust ownership"), in which case use that language. Do not otherwise ask the user to choose a programming language.

If the topic is missing, too broad, or ambiguous (e.g. "programming"), ask one concise question to narrow it before proceeding.

## Workflow

1. Identify the category of the topic (language feature, principle, technique, paradigm, or a specific language) and the problem or goal it addresses.
2. Explain the core idea, the key concepts/vocabulary involved, and how it relates to other concepts the learner likely knows.
3. Distinguish the topic from the closest commonly confused alternative (e.g. composition vs. inheritance, interface vs. abstract class).
4. Create a minimal, runnable example that demonstrates the concept's defining behavior. Avoid unrelated framework code.
5. Add one or more exercises that require the learner to apply, extend, or critique the concept. Include a short goal and acceptance criteria, but do not reveal the solution unless the user asks for it.
6. Check that the example matches the explanation, uses consistent names, and presents tradeoffs honestly rather than claiming the approach is always best.

## Generated artifacts

Create a folder only when the user asks for files, a study guide, examples, or exercises. Name it with the topic in lowercase kebab-case (e.g. `composition-vs-inheritance/`, `solid-principles/`). Use this structure:

```text
<topic-name>/
	summary.md
	example.ipynb
	exercises.ipynb
```

`summary.md` must contain:

- What the concept is and the problem or goal it addresses.
- Key vocabulary and a small structural or conceptual outline.
- When to use it and when not to use it.
- Tradeoffs and a comparison with a related or commonly confused concept.
- Instructions for running or reading the example.

`example.ipynb` must contain one focused example in the chosen language. `exercises.ipynb` must contain exercises ordered from basic application to adaptation or critique. Include markdown cells for explanations and instructions, and code cells for runnable code.

Every notebook must be valid JSON. Each cell must be an object in the `cells` array with a `metadata.language` property set to `markdown` or the code language (e.g. `python`). Existing cells must retain a unique `metadata.id`; new cells do not need an id. Include the standard notebook fields such as `nbformat`, `nbformat_minor`, and `metadata`.

If the user asks only for an explanation, answer in the conversation and do not create a folder. After creating artifacts, report the exact relative path and list the files created.
