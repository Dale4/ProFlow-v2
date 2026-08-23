# Architecture Decision Records

One record per decision. Newest first. Status: Proposed | Accepted | Superseded.

---

## ADR-0001 — Web stack: Minimal APIs + React + PostgreSQL

- Date:
- Status: Accepted
- Context: Need a maintainable web app for work orders and scheduling, with a C# backend and a modern SPA.
- Decision: ASP.NET Core Minimal APIs, React (Vite), PostgreSQL, Docker.
- Consequences:
  - One API surface for the SPA
  - Clear split between API and UI
  - Blazor/MAUI hybrid is not the default path unless we revisit this ADR

---

## Template

```markdown
## ADR-00XX — Title

- Date:
- Status: Proposed
- Context:
- Decision:
- Consequences:
```
