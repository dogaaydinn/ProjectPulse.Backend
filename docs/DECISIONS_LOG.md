# 🧠 Architectural Decision Log

## [2025-05-03] Removed `utils` folder
- Reason: Violates SRP. Replaced with specific classes like `DateFormatter`.

## [2025-05-03] Custom Validation instead of FluentValidation
- Reason: More control, centralized messages, easier debugging.

## [2025-05-03] No MediatR
- Reason: Avoid hidden control flow, easier debugging, better performance.

## [2025-05-03] Used `Factory Pattern` for entity creation
- Reason: Ensures encapsulated object construction, avoids misuse of constructors.

(→ Devamı günlük olarak yazılmalı.)
