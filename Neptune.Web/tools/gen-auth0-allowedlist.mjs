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
    const withParams = escaped.replace(/\\\{[^}]+\\\}/g, "[^/]+");
    return `^${withParams}$`;
}

function isTemplatedPath(p) {
    return p.includes("{") && p.includes("}");
}

// Collect per-method exact paths and regexes, split by anonymous/secured
// method -> Set<string>
const anonExact = new Map();
const securedExact = new Map();
// method -> Set<regexString>
const anonRegex = new Map();
const securedRegex = new Map();

function add(map, method, value) {
    if (!map.has(method)) map.set(method, new Set());
    map.get(method).add(value);
}

for (const [p, operations] of Object.entries(doc.paths ?? {})) {
    for (const [m, op] of Object.entries(operations ?? {})) {
        const method = m.toLowerCase();
        if (!VALID_METHODS.has(method)) continue;
        if (!op || typeof op !== "object") continue;

        const isAnon = op["x-anonymous"] === true;

        if (isTemplatedPath(p)) {
            const rx = openApiTemplateToRegex(p);
            add(isAnon ? anonRegex : securedRegex, method, rx);
        } else {
            add(isAnon ? anonExact : securedExact, method, p);
        }
    }
}

function sorted(set) {
    return [...(set ?? new Set())].sort();
}

// Build stable list of methods that exist in swagger
const methodKeys = new Set([...anonExact.keys(), ...securedExact.keys(), ...anonRegex.keys(), ...securedRegex.keys()]);

const methodsUpper = [...methodKeys].map((m) => m.toUpperCase()).sort();

/**
 * Output TS
 */
const ts = `/* AUTO-GENERATED from swagger.json. DO NOT EDIT BY HAND. */

import { HttpInterceptorRouteConfig } from '@auth0/auth0-angular';

export type AllowedHttpMethod = 'GET'|'POST'|'PUT'|'PATCH'|'DELETE'|'OPTIONS'|'HEAD';

type ExactMap = Partial<Record<AllowedHttpMethod, ReadonlyArray<string>>>;
type RegexMap = Partial<Record<AllowedHttpMethod, ReadonlyArray<RegExp>>>;

function stripBase(apiBaseUrl: string, uri: string): string | null {
  const base = apiBaseUrl.endsWith('/') ? apiBaseUrl.slice(0, -1) : apiBaseUrl;

  // Only match our API base
  if (!uri.startsWith(base)) return null;

  // Remove the base. Ensure leading "/" for comparison with OpenAPI paths.
  const rest = uri.substring(base.length);
  if (rest === '') return '/';
  return rest.startsWith('/') ? rest : '/' + rest;
}

const ANON_EXACT: ExactMap = {
${methodsUpper
    .map((M) => {
        const m = M.toLowerCase();
        const arr = sorted(anonExact.get(m));
        return `  '${M}': ${JSON.stringify(arr)},`;
    })
    .join("\n")}
};

const SECURED_EXACT: ExactMap = {
${methodsUpper
    .map((M) => {
        const m = M.toLowerCase();
        const arr = sorted(securedExact.get(m));
        return `  '${M}': ${JSON.stringify(arr)},`;
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

const SECURED_REGEX: RegexMap = {
${methodsUpper
    .map((M) => {
        const m = M.toLowerCase();
        const arr = sorted(securedRegex.get(m));
        return `  '${M}': [\n${arr.map((r) => `    new RegExp(${JSON.stringify(r)}),`).join("\n")}\n  ],`;
    })
    .join("\n")}
};

/**
 * Auth0 httpInterceptor.allowedList generator.
 *
 * This generates route configs that:
 * - For anonymous routes: attach token if user is logged in, allow request if not (allowAnonymous: true)
 * - For secured routes: require token (no allowAnonymous flag)
 *
 * This allows endpoints like /projects to work for both anonymous users (showing public data)
 * and authenticated users (showing additional data based on their identity).
 */
export function buildAuth0AllowedList(apiBaseUrl: string): HttpInterceptorRouteConfig[] {
  const methods: AllowedHttpMethod[] = ['GET','POST','PUT','PATCH','DELETE','OPTIONS','HEAD'];
  const entries: HttpInterceptorRouteConfig[] = [];
  const base = apiBaseUrl.endsWith('/') ? apiBaseUrl.slice(0, -1) : apiBaseUrl;

  // Anonymous routes: attach token if available, allow anonymous access
  for (const method of methods) {
    const exactPaths = ANON_EXACT[method];
    if (exactPaths) {
      for (const path of exactPaths) {
        entries.push({
          uri: base + path,
          httpMethod: method,
          allowAnonymous: true
        });
      }
    }
    const regexPatterns = ANON_REGEX[method];
    if (regexPatterns) {
      for (const rx of regexPatterns) {
        entries.push({
          uriMatcher: (uri: string) => {
            const p = stripBase(apiBaseUrl, uri);
            return p !== null && rx.test(p);
          },
          httpMethod: method,
          allowAnonymous: true
        });
      }
    }
  }

  // Secured routes: require token
  for (const method of methods) {
    const exactPaths = SECURED_EXACT[method];
    if (exactPaths) {
      for (const path of exactPaths) {
        entries.push({
          uri: base + path,
          httpMethod: method
        });
      }
    }
    const regexPatterns = SECURED_REGEX[method];
    if (regexPatterns) {
      for (const rx of regexPatterns) {
        entries.push({
          uriMatcher: (uri: string) => {
            const p = stripBase(apiBaseUrl, uri);
            return p !== null && rx.test(p);
          },
          httpMethod: method
        });
      }
    }
  }

  return entries;
}
`;

fs.mkdirSync(outDir, { recursive: true });
fs.writeFileSync(outFile, ts, "utf8");

const count = (m) => [...m.values()].reduce((a, s) => a + s.size, 0);
console.log(
    `Generated ${outFile}
  anonExact=${count(anonExact)} anonRegex=${count(anonRegex)}
  securedExact=${count(securedExact)} securedRegex=${count(securedRegex)}
  swagger=${swaggerPath}`
);
