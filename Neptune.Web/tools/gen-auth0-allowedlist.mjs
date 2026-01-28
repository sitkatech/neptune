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
function openApiTemplateToRegex(templatePath) {
    // "/jurisdictions/{id}/users" -> "^/jurisdictions/[^/]+/users$"
    const escaped = escapeRegex(templatePath);
    return `^${escaped.replace(/\\\{[^}]+\\\}/g, "[^/]+")}$`;
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
            add(regexMap, method, openApiTemplateToRegex(p));
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
