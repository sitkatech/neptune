/* AUTO-GENERATED from swagger.json. DO NOT EDIT BY HAND. */

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
  'DELETE': new Set([]),
  'GET': new Set(["/","/custom-attribute-types","/field-definitions","/land-use-blocks","/trash-generating-units/last-update-date","/treatment-bmp-type-custom-attribute-types","/treatment-bmp-types"]),
  'POST': new Set([]),
  'PUT': new Set([]),
};

const OPT_EXACT: ExactMap = {
  'DELETE': new Set([]),
  'GET': new Set(["/jurisdictions/bounding-box","/jurisdictions/user-viewable","/treatment-bmps","/treatment-bmps/modeling-attributes","/treatment-bmps/verified/feature-collection"]),
  'POST': new Set([]),
  'PUT': new Set([]),
};

const SEC_EXACT: ExactMap = {
  'DELETE': new Set([]),
  'GET': new Set(["/ai/vector-stores","/delineations","/funding-sources","/hru-characteristics","/jurisdictions","/load-generating-units","/nereid/config","/nereid/delta-solve","/nereid/delta-solve-test","/nereid/health","/nereid/land-surface-loading","/nereid/land-surface-loading-baseline","/nereid/land-surface-table","/nereid/no-treatment-facility-validate","/nereid/solution-sequence","/nereid/solution-test-case","/nereid/subgraph","/nereid/total-network-graph","/nereid/treatment-facilities","/nereid/treatment-facility-validate","/nereid/treatment-sites","/nereid/validate","/onland-visual-trash-assessment-areas","/onland-visual-trash-assessments","/organization-types","/organizations","/projects","/projects/OCTAM2Tier2GrantProgram","/projects/OCTAM2Tier2GrantProgram/download","/projects/OCTAM2Tier2GrantProgram/treatmentBMPs/download","/projects/delineations","/projects/download","/projects/treatmentBMPs/download","/regional-subbasins","/trash-generating-units","/treatment-bmps/octa-m2-tier2-grant-program","/treatment-bmps/planned-projects","/users","/water-quality-management-plan-documents","/water-quality-management-plans","/water-quality-management-plans/display-dtos","/water-quality-management-plans/with-final-document"]),
  'POST': new Set(["/ai/clean-up","/funding-sources","/graph-trace-as-feature-collection-from-point","/onland-visual-trash-assessments","/organizations","/projects","/treatment-bmps","/user-claims","/users","/water-quality-management-plan-documents","/water-quality-management-plans"]),
  'PUT': new Set(["/land-use-blocks"]),
};

const ANON_REGEX: RegexMap = {
  'DELETE': [

  ],
  'GET': [
    new RegExp("^/custom-attribute-types/[^/]+$"),
    new RegExp("^/custom-rich-texts/[^/]+$"),
    new RegExp("^/field-definitions/[^/]+$"),
    new RegExp("^/file-resources/[^/]+$"),
    new RegExp("^/jurisdictions/[^/]+/bounding-box$"),
    new RegExp("^/onland-visual-trash-assessment-areas/jurisdictions/[^/]+$"),
    new RegExp("^/purpose/[^/]+$"),
    new RegExp("^/trash-generating-units/[^/]+$"),
    new RegExp("^/trash-results-by-jurisdiction/[^/]+/area-based-results-calculations$"),
    new RegExp("^/trash-results-by-jurisdiction/[^/]+/load-based-results-calculations$"),
    new RegExp("^/trash-results-by-jurisdiction/[^/]+/ovta-based-results-calculations$"),
    new RegExp("^/treatment-bmp-type-custom-attribute-types/[^/]+$"),
    new RegExp("^/treatment-bmp-type-custom-attribute-types/purpose/[^/]+$"),
    new RegExp("^/treatment-bmp-types/[^/]+/custom-attribute-types$"),
    new RegExp("^/treatment-bmps/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+/benchmarks-and-thresholds$"),
    new RegExp("^/treatment-bmps/[^/]+/benchmarks-and-thresholds/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+/custom-attributes$"),
    new RegExp("^/treatment-bmps/[^/]+/field-visits$"),
    new RegExp("^/treatment-bmps/[^/]+/funding-events$"),
    new RegExp("^/treatment-bmps/[^/]+/funding-events/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+/treatment-bmp-documents$"),
    new RegExp("^/treatment-bmps/[^/]+/treatment-bmp-documents/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+/treatment-bmp-images$"),
    new RegExp("^/treatment-bmps/[^/]+/treatment-bmp-images/[^/]+$"),
  ],
  'POST': [

  ],
  'PUT': [

  ],
};

const OPT_REGEX: RegexMap = {
  'DELETE': [

  ],
  'GET': [
    new RegExp("^/treatment-bmps/jurisdictions/[^/]+/verified/feature-collection$"),
  ],
  'POST': [

  ],
  'PUT': [
    new RegExp("^/treatment-bmps/[^/]+/basic-info$"),
  ],
};

const SEC_REGEX: RegexMap = {
  'DELETE': [
    new RegExp("^/funding-sources/[^/]+$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/observations/observation-photos/[^/]+$"),
    new RegExp("^/organizations/[^/]+$"),
    new RegExp("^/project-documents/[^/]+$"),
    new RegExp("^/projects/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+/benchmarks-and-thresholds/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+/funding-events/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+/treatment-bmp-documents/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+/treatment-bmp-images/[^/]+$"),
    new RegExp("^/users/[^/]+$"),
    new RegExp("^/water-quality-management-plan-documents/[^/]+$"),
    new RegExp("^/water-quality-management-plans/[^/]+$"),
  ],
  'GET': [
    new RegExp("^/funding-sources/[^/]+$"),
    new RegExp("^/jurisdictions/[^/]+$"),
    new RegExp("^/jurisdictions/[^/]+/treatment-bmps$"),
    new RegExp("^/jurisdictions/[^/]+/users$"),
    new RegExp("^/load-generating-units/[^/]+$"),
    new RegExp("^/load-generating-units/[^/]+/hru-characteristics$"),
    new RegExp("^/nereid/treatment-facility-validate/[^/]+$"),
    new RegExp("^/onland-visual-trash-assessment-areas/[^/]+$"),
    new RegExp("^/onland-visual-trash-assessment-areas/[^/]+/area-as-feature-collection$"),
    new RegExp("^/onland-visual-trash-assessment-areas/[^/]+/onland-visual-trash-assessments$"),
    new RegExp("^/onland-visual-trash-assessment-areas/[^/]+/parcel-geometries$"),
    new RegExp("^/onland-visual-trash-assessment-areas/[^/]+/transect-line-as-feature-collection$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/area-as-feature-collection$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/observations$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/observations/feature-collection$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/parcels$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/progress$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/review-and-finalize$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/transect-line-as-feature-collection$"),
    new RegExp("^/organizations/[^/]+$"),
    new RegExp("^/project-documents/[^/]+$"),
    new RegExp("^/projects/[^/]+$"),
    new RegExp("^/projects/[^/]+/attachments$"),
    new RegExp("^/projects/[^/]+/bounding-box$"),
    new RegExp("^/projects/[^/]+/delineations$"),
    new RegExp("^/projects/[^/]+/load-reducing-results$"),
    new RegExp("^/projects/[^/]+/progress$"),
    new RegExp("^/projects/[^/]+/project-network-solve-histories$"),
    new RegExp("^/projects/[^/]+/treatment-bmp-hru-characteristics$"),
    new RegExp("^/projects/[^/]+/treatment-bmps$"),
    new RegExp("^/projects/[^/]+/treatment-bmps/as-upsert-dtos$"),
    new RegExp("^/regional-subbasins/[^/]+$"),
    new RegExp("^/regional-subbasins/[^/]+/hru-characteristics$"),
    new RegExp("^/regional-subbasins/[^/]+/load-generating-units$"),
    new RegExp("^/treatment-bmps/[^/]+/delineation-errors$"),
    new RegExp("^/treatment-bmps/[^/]+/hru-characteristics$"),
    new RegExp("^/treatment-bmps/[^/]+/other-treatment-bmps-in-regional-subbasin$"),
    new RegExp("^/treatment-bmps/[^/]+/parameterization-errors$"),
    new RegExp("^/treatment-bmps/[^/]+/upstreamRSBCatchmentGeoJSON$"),
    new RegExp("^/treatment-bmps/[^/]+/upstreamest-errors$"),
    new RegExp("^/user-claims/[^/]+$"),
    new RegExp("^/users/[^/]+$"),
    new RegExp("^/water-quality-management-plan-documents/[^/]+$"),
    new RegExp("^/water-quality-management-plans/[^/]+$"),
    new RegExp("^/water-quality-management-plans/[^/]+/documents$"),
  ],
  'POST': [
    new RegExp("^/ai/water-quality-management-plan-documents/[^/]+/ask$"),
    new RegExp("^/ai/water-quality-management-plan-documents/[^/]+/extract-all$"),
    new RegExp("^/onland-visual-trash-assessment-areas/[^/]+/parcel-geometries$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/observations$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/observations/observation-photo$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/parcels$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/refine-area$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/refresh-parcels$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/return-to-edit$"),
    new RegExp("^/onland-visual-trash-assessments/[^/]+/review-and-finalize$"),
    new RegExp("^/projects/[^/]+/attachments$"),
    new RegExp("^/projects/[^/]+/copy$"),
    new RegExp("^/projects/[^/]+/modeled-performance$"),
    new RegExp("^/treatment-bmps/[^/]+/benchmarks-and-thresholds$"),
    new RegExp("^/treatment-bmps/[^/]+/funding-events$"),
    new RegExp("^/treatment-bmps/[^/]+/treatment-bmp-documents$"),
    new RegExp("^/treatment-bmps/[^/]+/treatment-bmp-images$"),
  ],
  'PUT': [
    new RegExp("^/custom-rich-texts/[^/]+$"),
    new RegExp("^/field-definitions/[^/]+$"),
    new RegExp("^/funding-sources/[^/]+$"),
    new RegExp("^/onland-visual-trash-assessment-areas/[^/]+$"),
    new RegExp("^/organizations/[^/]+$"),
    new RegExp("^/project-documents/[^/]+$"),
    new RegExp("^/projects/[^/]+/delineations$"),
    new RegExp("^/projects/[^/]+/treatment-bmps$"),
    new RegExp("^/projects/[^/]+/treatment-bmps/update-locations$"),
    new RegExp("^/projects/[^/]+/update$"),
    new RegExp("^/treatment-bmps/[^/]+/benchmarks-and-thresholds/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+/custom-attribute-type-purposes/[^/]+/custom-attributes$"),
    new RegExp("^/treatment-bmps/[^/]+/funding-events/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+/location$"),
    new RegExp("^/treatment-bmps/[^/]+/queue-refresh-land-use$"),
    new RegExp("^/treatment-bmps/[^/]+/treatment-bmp-documents/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+/treatment-bmp-images$"),
    new RegExp("^/treatment-bmps/[^/]+/treatment-bmp-types/[^/]+$"),
    new RegExp("^/treatment-bmps/[^/]+/type$"),
    new RegExp("^/treatment-bmps/[^/]+/upstream-bmp$"),
    new RegExp("^/users/[^/]+$"),
    new RegExp("^/water-quality-management-plan-documents/[^/]+$"),
    new RegExp("^/water-quality-management-plans/[^/]+$"),
  ],
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
