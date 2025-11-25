This file is a short guide for AI coding agents to be productive in the Neptune.Web repository.

High-level architecture

- Single Page Application (Angular) built from `src/` and output to the server `wwwroot/` folder. See `angular.json` (project `Neptune.Web`) for build targets and assets.
- The frontend talks to a separate API (Neptune.API). A generated OpenAPI TypeScript client lives in `src/app/shared/generated/` (generated via `npm run gen-model`) — key files: `configuration.ts`, `api.module.ts`, `api.base.service.ts`.

Dev / build / test workflows

- Node version is located in: `.nvmrc`. Dev flows expect `nvm` on Windows PowerShell (see `package.json` `prestart` script which uses PowerShell `nvm` commands).
- Common commands (run from repository root or the Neptune.Web folder):
    - Install + set node: use nvm and (project uses `@angular/cli` 20.x).
    - Dev server: `npm run prestart` then `npm start` (prestart ensures node version and dev cert). `start` runs `ng serve` with SSL using `server.crt`/`server.key`.
    - Build: `npm run build` (dev), `npm run build-qa` or `npm run build-prod` for production/qa configurations. `build-ci` uses the `qa` configuration.
    - Generate API client: `npm run gen-model` (uses OpenAPI generator and paths from `package.json` `config` section; `swagger.json` path is `%npm_package_config_swagger_json_path%`).
    - Tests: `npm test` runs Karma; e2e: `npm run e2e`.

Project-specific patterns & conventions

- Output path: `angular.json` config writes compiled assets into `wwwroot/` so the ASP.NET Core host serves them unchanged. When editing bundling or assets, check `angular.json` `assets`, `styles`, and `scripts` arrays.
- Generated API client:
    - Located at `src/app/shared/generated/` and committed to the repo. Treat it as generated code (there's a `.openapi-generator-ignore` file in the folder). Regenerate only via `npm run gen-model` when the API schema changes.
    - Base path defaults to `http://localhost` in `configuration.ts` — runtime code usually sets the real API URL via a Configuration provider (`ApiModule.forRoot(...)`). Search for `ApiModule.forRoot` to find where the base path is injected.
- Authentication: Uses OAuth2 / OIDC via `angular-oauth2-oidc`. The app loads runtime auth configuration during startup (see `src/app/app.init.ts` which reads `assets/config.json`) and wires `OAuthModule` and an `OAuthStorage` implementation (`CookieStorageService`) in `src/app/app.config.ts`. Routes use the project's custom guards (see `src/app/shared/guards/` — for example the `unauthenticated-access/*` guards and `unsaved-changes-guard.ts`). When changing route protection or auth wiring, update those files.
  Integration points & external dependencies

- Neptune.API: OpenAPI spec referenced at `../Neptune.API/swagger.json` (see `package.json` `config.swagger_json_path`). Updates to the API require regenerating the client.
- Authentication / OIDC: Look for initialization and configuration code in `src/app/app.init.ts` (runtime config loader) and `src/app/app.config.ts` (where `OAuthModule`, `OAuthStorage` and related providers are registered) to understand how authentication and protected routes are wired.
- Third-party libs: Leaflet, Tinymce, Vega, Ag-grid etc. Assets are copied from node_modules via `angular.json` assets and script entries.

Where to look for examples

- Dev cert: `dev-cert.cnf`, `server.crt`, `server.key`, and `Program.cs` (Kestrel local HTTPS in development) show local SSL expectations.
- Generated client usage pattern: inspect `src/app/shared/generated/api.base.service.ts` and `src/app/shared/generated/configuration.ts` to understand header/query credential handling.
- Routing + guards: open `src/app/app.routes.ts` (or similar route file in `src`) and `src/app/guards/` for guard examples and route param patterns.

Angular specifics

- Always use the modern `@if`/`@for` flow control in templates when available (prefer `@if`/`@for` to legacy `*ngIf`/`*ngFor`). This repo uses modern Angular (20.x); check component templates under `src/app/**/` and prefer the newer flow-control directives for clarity and incremental rendering.
- Prefer standalone components and lazy-loading via `loadComponent` (the routing configuration already uses `loadComponent` in many places). When adding a new route, follow the `routes` style in `src/app/app.routes.ts`.
- Use the generated API client (`src/app/shared/generated/`) for server calls; do not hand-roll models that duplicate generated DTOs.
- For ts imports, please always use full paths starting from `src/app/` (e.g. `import { MyService } from 'src/app/services/my-service';`).
- For styles and assets, follow `angular.json`'s `styles`, `scripts`, and `assets` entries (Leaflet and TinyMCE assets are copied directly from node_modules there).
- Always import abstracts into created scss files like so: `@use "/src/scss/abstracts" as *;`.
- Try to reuse existing scss variables and mixins from the abstracts instead of creating new ones.
- Use reactive forms with our reusable form field component located in `src/app/shared/components/forms/form-field/`.
- Use observable patterns with RxJS for async data handling (e.g., use `async` pipe in templates, prefer `switchMap` for dependent calls).

Rules for AI edits (keep changes minimal and discoverable)

- Don't modify generated files in `src/app/shared/generated/` by hand — change the OpenAPI spec and run `npm run gen-model` instead.
- When changing build/serve behavior, update `package.json` scripts and `angular.json` simultaneously.
- Preserve the ASP.NET Core `Startup.cs` behavior (response compression, static files, redirects). Changing it may alter how the SPA is served in production.

If you need more detail

- Ask for which area to expand (authentication wiring, routing, where environment-specific API base URLs are set, or how to run in Docker). Include an example task and I'll expand the instructions with concrete file-level steps.
