import fs from "node:fs";
import path from "node:path";

const swaggerPath = process.argv[2] || process.env.npm_package_config_swagger_json_path || "swagger.json";

const outDir = process.argv[3] || process.env.npm_package_config_output_dir || "./src/app/shared/generated";

const outFile = path.join(outDir, "auth0-allowedlist.ts");

const raw = fs.readFileSync(swaggerPath, "utf8");
const doc = JSON.parse(raw);

const VALID_METHODS = new Set(["get", "post", "put", "patch", "delete", "options", "head"]);

function escapeRegex(s) {
    return s.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
}

/**
 * NOTE: Why this generator does path-parameter type narrowing
 *
 * Auth0's Angular interceptor uses an "allowedList" to decide whether to attach an access token.
 * Our generated list also supports "anonymous" routes (x-anonymous) that MUST NOT receive a token.
 *
 * Previously, templated OpenAPI paths like `/treatment-bmps/{treatmentBMPID}` were converted into the
 * regex `^/treatment-bmps/[^/]+$`. That is too broad: it also matches non-ID subpaths like
 * `/treatment-bmps/planned-projects`.
 *
 * Because anonymous routes explicitly block token attachment in `auth0-allowedlist.ts`, the overly-broad
 * anonymous regex caused the interceptor to skip adding `Authorization` on `/treatment-bmps/planned-projects`.
 * The API then correctly returned 401 even though the user was authenticated.
 *
 * Fix: when we can infer that a path param is numeric (int32/int64/etc), we emit `\\d+` instead of `[^/]+`.
 * That keeps `/treatment-bmps/{treatmentBMPID}` matching only numeric IDs and prevents collisions with
 * literal routes under the same prefix.
 */

function getRef(root, ref) {
    // Supports local refs like "#/components/parameters/Foo"
    if (typeof ref !== "string" || !ref.startsWith("#/")) return null;
    const parts = ref
        .slice(2)
        .split("/")
        .map((p) => p.replace(/~1/g, "/").replace(/~0/g, "~"));

    let cur = root;
    for (const key of parts) {
        if (!cur || typeof cur !== "object") return null;
        cur = cur[key];
    }
    return cur ?? null;
}

function resolveMaybeRef(root, obj) {
    if (!obj || typeof obj !== "object") return obj;
    if (typeof obj.$ref === "string") {
        return getRef(root, obj.$ref) ?? obj;
    }
    return obj;
}

function getParamMatcherFromSchema(schema) {
    // Prefer a type-specific matcher when swagger declares a numeric/uuid path param.
    // This prevents templated paths from accidentally matching literal subroutes.
    const resolved = resolveMaybeRef(doc, schema);
    const type = resolved?.type;
    const format = resolved?.format;

    if (type === "integer" || type === "number") {
        return "\\d+";
    }
    if (format === "uuid") {
        // UUIDs are hex with hyphens in fixed positions (8-4-4-4-12).
        // This avoids matching strings like "------------------------------------".
        return "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";
    }

    return "[^/]+";
}

function buildPathParamMatchers(pathItem, operation) {
    // Path params can be declared at either the path-item level or the operation level.
    // Combine both, resolve any $refs, then pick a safe regex matcher per param name.
    const matchers = new Map();
    const combined = [...(pathItem?.parameters ?? []), ...(operation?.parameters ?? [])];

    for (const raw of combined) {
        const param = resolveMaybeRef(doc, raw);
        if (!param || typeof param !== "object") continue;
        if (param.in !== "path") continue;
        if (typeof param.name !== "string" || param.name.length === 0) continue;

        const matcher = getParamMatcherFromSchema(param.schema);
        matchers.set(param.name, matcher);
    }

    return matchers;
}

function openApiTemplateToRegex(templatePath, paramMatchers) {
    // Example:
    //   "/jurisdictions/{id}/users" -> "^/jurisdictions/\\d+/users$" (when id is integer)
    // Fallback:
    //   unknown param type -> "[^/]+"
    const escaped = escapeRegex(templatePath);
    return `^${escaped.replace(/\\\{([^}]+)\\\}/g, (_, name) => paramMatchers?.get(name) ?? "[^/]+")}$`;
}
function isTemplatedPath(p) {
    return p.includes("{") && p.includes("}");
}
function add(map, method, value) {
    if (!map.has(method)) map.set(method, new Set());
    map.get(method).add(value);
}
function sorted(set) {
    return [...(set ?? new Set())].sort();
}

// method -> Set<string>
const anonExact = new Map();
const optExact = new Map();
const secExact = new Map();

// method -> Set<regexString>
const anonRegex = new Map();
const optRegex = new Map();
const secRegex = new Map();

for (const [p, operations] of Object.entries(doc.paths ?? {})) {
    for (const [m, op] of Object.entries(operations ?? {})) {
        const method = m.toLowerCase();
        if (!VALID_METHODS.has(method)) continue;
        if (!op || typeof op !== "object") continue;

        // Classification rules:
        // - x-optional-auth wins (even if x-anonymous is also present)
        // - else x-anonymous => anonymous
        // - else => secured
        const isOptional = op["x-optional-auth"] === true;
        const isAnonOnly = !isOptional && op["x-anonymous"] === true;

        const exactMap = isOptional ? optExact : isAnonOnly ? anonExact : secExact;
        const regexMap = isOptional ? optRegex : isAnonOnly ? anonRegex : secRegex;

        if (isTemplatedPath(p)) {
            const paramMatchers = buildPathParamMatchers(operations, op);
            add(regexMap, method, openApiTemplateToRegex(p, paramMatchers));
        } else {
            add(exactMap, method, p);
        }
    }
}

const methodKeys = new Set([...anonExact.keys(), ...optExact.keys(), ...secExact.keys(), ...anonRegex.keys(), ...optRegex.keys(), ...secRegex.keys()]);

const methodsUpper = [...methodKeys].map((m) => m.toUpperCase()).sort();

const ts = `/* AUTO-GENERATED from swagger.json. DO NOT EDIT BY HAND. */

export type AllowedHttpMethod = 'GET'|'POST'|'PUT'|'PATCH'|'DELETE'|'OPTIONS'|'HEAD';

type ExactMap = Partial<Record<AllowedHttpMethod, ReadonlySet<string>>>;
type RegexMap = Partial<Record<AllowedHttpMethod, ReadonlyArray<RegExp>>>;

function stripBase(apiBaseUrl: string, uri: string): string | null {
  const base = apiBaseUrl.endsWith('/') ? apiBaseUrl.slice(0, -1) : apiBaseUrl;
  if (!uri.startsWith(base)) return null;

  const rest = uri.substring(base.length);
  if (rest === '') return '/';
  return rest.startsWith('/') ? rest : '/' + rest;
}

const ANON_EXACT: ExactMap = {
${methodsUpper
    .map((M) => {
        const m = M.toLowerCase();
        return `  '${M}': new Set(${JSON.stringify(sorted(anonExact.get(m)))}),`;
    })
    .join("\n")}
};

const OPT_EXACT: ExactMap = {
${methodsUpper
    .map((M) => {
        const m = M.toLowerCase();
        return `  '${M}': new Set(${JSON.stringify(sorted(optExact.get(m)))}),`;
    })
    .join("\n")}
};

const SEC_EXACT: ExactMap = {
${methodsUpper
    .map((M) => {
        const m = M.toLowerCase();
        return `  '${M}': new Set(${JSON.stringify(sorted(secExact.get(m)))}),`;
    })
    .join("\n")}
};

const ANON_REGEX: RegexMap = {
${methodsUpper
    .map((M) => {
        const m = M.toLowerCase();
        const arr = sorted(anonRegex.get(m));
        return `  '${M}': [\n${arr.map((r) => `    new RegExp(${JSON.stringify(r)}),`).join("\n")}\n  ],`;
    })
    .join("\n")}
};

const OPT_REGEX: RegexMap = {
${methodsUpper
    .map((M) => {
        const m = M.toLowerCase();
        const arr = sorted(optRegex.get(m));
        return `  '${M}': [\n${arr.map((r) => `    new RegExp(${JSON.stringify(r)}),`).join("\n")}\n  ],`;
    })
    .join("\n")}
};

const SEC_REGEX: RegexMap = {
${methodsUpper
    .map((M) => {
        const m = M.toLowerCase();
        const arr = sorted(secRegex.get(m));
        return `  '${M}': [\n${arr.map((r) => `    new RegExp(${JSON.stringify(r)}),`).join("\n")}\n  ],`;
    })
    .join("\n")}
};

function matches(exact: ExactMap, regex: RegexMap, method: AllowedHttpMethod, p: string): boolean {
  const e = exact[method];
  if (e?.has(p)) return true;
  const rs = regex[method] ?? [];
  return rs.some(rx => rx.test(p));
}

function isAnon(method: AllowedHttpMethod, p: string) {
  return matches(ANON_EXACT, ANON_REGEX, method, p);
}
function isOptional(method: AllowedHttpMethod, p: string) {
  return matches(OPT_EXACT, OPT_REGEX, method, p);
}
function isSecured(method: AllowedHttpMethod, p: string) {
  return matches(SEC_EXACT, SEC_REGEX, method, p);
}

/**
 * Builds Auth0 interceptor rules:
 * - Optional auth: attach token if available (allowAnonymous: true)
 * - Secured: attach token (required)
 * - Anonymous: never attach token
 */
export function buildAuth0AllowedList(apiBaseUrl: string) {
  const methods: AllowedHttpMethod[] = ['GET','POST','PUT','PATCH','DELETE','OPTIONS','HEAD'];

  const optional = methods.map(httpMethod => ({
    httpMethod,
    allowAnonymous: true,
    uriMatcher: (uri: string) => {
      const p = stripBase(apiBaseUrl, uri);
      if (p === null) return false;
      return isOptional(httpMethod, p);
    }
  }));

  const required = methods.map(httpMethod => ({
    httpMethod,
    uriMatcher: (uri: string) => {
      const p = stripBase(apiBaseUrl, uri);
      if (p === null) return false;

      // Optional wins (handled above), anon blocks, secured allows
      if (isOptional(httpMethod, p)) return false;
      if (isAnon(httpMethod, p)) return false;
      return isSecured(httpMethod, p);
    }
  }));

  return [...optional, ...required];
}
`;

fs.mkdirSync(outDir, { recursive: true });
fs.writeFileSync(outFile, ts, "utf8");
console.log(`Generated ${outFile}`);
