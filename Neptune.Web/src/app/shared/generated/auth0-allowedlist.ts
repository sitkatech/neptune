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
    new RegExp("^/custom-attribute-types/\\d+$"),
    new RegExp("^/custom-rich-texts/\\d+$"),
    new RegExp("^/field-definitions/\\d+$"),
    new RegExp("^/file-resources/[^/]+$"),
    new RegExp("^/jurisdictions/\\d+/bounding-box$"),
    new RegExp("^/onland-visual-trash-assessment-areas/jurisdictions/\\d+$"),
    new RegExp("^/purpose/\\d+$"),
    new RegExp("^/trash-generating-units/\\d+$"),
    new RegExp("^/trash-results-by-jurisdiction/\\d+/area-based-results-calculations$"),
    new RegExp("^/trash-results-by-jurisdiction/\\d+/load-based-results-calculations$"),
    new RegExp("^/trash-results-by-jurisdiction/\\d+/ovta-based-results-calculations$"),
    new RegExp("^/treatment-bmp-type-custom-attribute-types/\\d+$"),
    new RegExp("^/treatment-bmp-type-custom-attribute-types/purpose/\\d+$"),
    new RegExp("^/treatment-bmp-types/\\d+/custom-attribute-types$"),
    new RegExp("^/treatment-bmps/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+/benchmarks-and-thresholds$"),
    new RegExp("^/treatment-bmps/\\d+/benchmarks-and-thresholds/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+/custom-attributes$"),
    new RegExp("^/treatment-bmps/\\d+/field-visits$"),
    new RegExp("^/treatment-bmps/\\d+/funding-events$"),
    new RegExp("^/treatment-bmps/\\d+/funding-events/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+/treatment-bmp-documents$"),
    new RegExp("^/treatment-bmps/\\d+/treatment-bmp-documents/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+/treatment-bmp-images$"),
    new RegExp("^/treatment-bmps/\\d+/treatment-bmp-images/\\d+$"),
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
    new RegExp("^/treatment-bmps/jurisdictions/\\d+/verified/feature-collection$"),
  ],
  'POST': [

  ],
  'PUT': [
    new RegExp("^/treatment-bmps/\\d+/basic-info$"),
  ],
};

const SEC_REGEX: RegexMap = {
  'DELETE': [
    new RegExp("^/funding-sources/\\d+$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/observations/observation-photos/\\d+$"),
    new RegExp("^/organizations/\\d+$"),
    new RegExp("^/project-documents/\\d+$"),
    new RegExp("^/projects/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+/benchmarks-and-thresholds/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+/funding-events/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+/treatment-bmp-documents/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+/treatment-bmp-images/\\d+$"),
    new RegExp("^/users/\\d+$"),
    new RegExp("^/water-quality-management-plan-documents/\\d+$"),
    new RegExp("^/water-quality-management-plans/\\d+$"),
  ],
  'GET': [
    new RegExp("^/funding-sources/\\d+$"),
    new RegExp("^/jurisdictions/\\d+$"),
    new RegExp("^/jurisdictions/\\d+/treatment-bmps$"),
    new RegExp("^/jurisdictions/\\d+/users$"),
    new RegExp("^/load-generating-units/\\d+$"),
    new RegExp("^/load-generating-units/\\d+/hru-characteristics$"),
    new RegExp("^/nereid/treatment-facility-validate/\\d+$"),
    new RegExp("^/onland-visual-trash-assessment-areas/\\d+$"),
    new RegExp("^/onland-visual-trash-assessment-areas/\\d+/area-as-feature-collection$"),
    new RegExp("^/onland-visual-trash-assessment-areas/\\d+/onland-visual-trash-assessments$"),
    new RegExp("^/onland-visual-trash-assessment-areas/\\d+/parcel-geometries$"),
    new RegExp("^/onland-visual-trash-assessment-areas/\\d+/transect-line-as-feature-collection$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/area-as-feature-collection$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/observations$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/observations/feature-collection$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/parcels$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/progress$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/review-and-finalize$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/transect-line-as-feature-collection$"),
    new RegExp("^/organizations/\\d+$"),
    new RegExp("^/project-documents/\\d+$"),
    new RegExp("^/projects/\\d+$"),
    new RegExp("^/projects/\\d+/attachments$"),
    new RegExp("^/projects/\\d+/bounding-box$"),
    new RegExp("^/projects/\\d+/delineations$"),
    new RegExp("^/projects/\\d+/load-reducing-results$"),
    new RegExp("^/projects/\\d+/progress$"),
    new RegExp("^/projects/\\d+/project-network-solve-histories$"),
    new RegExp("^/projects/\\d+/treatment-bmp-hru-characteristics$"),
    new RegExp("^/projects/\\d+/treatment-bmps$"),
    new RegExp("^/projects/\\d+/treatment-bmps/as-upsert-dtos$"),
    new RegExp("^/regional-subbasins/\\d+$"),
    new RegExp("^/regional-subbasins/\\d+/hru-characteristics$"),
    new RegExp("^/regional-subbasins/\\d+/load-generating-units$"),
    new RegExp("^/treatment-bmps/\\d+/delineation-errors$"),
    new RegExp("^/treatment-bmps/\\d+/hru-characteristics$"),
    new RegExp("^/treatment-bmps/\\d+/other-treatment-bmps-in-regional-subbasin$"),
    new RegExp("^/treatment-bmps/\\d+/parameterization-errors$"),
    new RegExp("^/treatment-bmps/\\d+/upstreamRSBCatchmentGeoJSON$"),
    new RegExp("^/treatment-bmps/\\d+/upstreamest-errors$"),
    new RegExp("^/user-claims/[^/]+$"),
    new RegExp("^/users/\\d+$"),
    new RegExp("^/water-quality-management-plan-documents/\\d+$"),
    new RegExp("^/water-quality-management-plans/\\d+$"),
    new RegExp("^/water-quality-management-plans/\\d+/documents$"),
  ],
  'POST': [
    new RegExp("^/ai/water-quality-management-plan-documents/\\d+/ask$"),
    new RegExp("^/ai/water-quality-management-plan-documents/\\d+/extract-all$"),
    new RegExp("^/onland-visual-trash-assessment-areas/\\d+/parcel-geometries$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/observations$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/observations/observation-photo$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/parcels$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/refine-area$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/refresh-parcels$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/return-to-edit$"),
    new RegExp("^/onland-visual-trash-assessments/\\d+/review-and-finalize$"),
    new RegExp("^/projects/\\d+/attachments$"),
    new RegExp("^/projects/\\d+/copy$"),
    new RegExp("^/projects/\\d+/modeled-performance$"),
    new RegExp("^/treatment-bmps/\\d+/benchmarks-and-thresholds$"),
    new RegExp("^/treatment-bmps/\\d+/funding-events$"),
    new RegExp("^/treatment-bmps/\\d+/treatment-bmp-documents$"),
    new RegExp("^/treatment-bmps/\\d+/treatment-bmp-images$"),
  ],
  'PUT': [
    new RegExp("^/custom-rich-texts/\\d+$"),
    new RegExp("^/field-definitions/\\d+$"),
    new RegExp("^/funding-sources/\\d+$"),
    new RegExp("^/onland-visual-trash-assessment-areas/\\d+$"),
    new RegExp("^/organizations/\\d+$"),
    new RegExp("^/project-documents/\\d+$"),
    new RegExp("^/projects/\\d+/delineations$"),
    new RegExp("^/projects/\\d+/treatment-bmps$"),
    new RegExp("^/projects/\\d+/treatment-bmps/update-locations$"),
    new RegExp("^/projects/\\d+/update$"),
    new RegExp("^/treatment-bmps/\\d+/benchmarks-and-thresholds/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+/custom-attribute-type-purposes/\\d+/custom-attributes$"),
    new RegExp("^/treatment-bmps/\\d+/funding-events/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+/location$"),
    new RegExp("^/treatment-bmps/\\d+/queue-refresh-land-use$"),
    new RegExp("^/treatment-bmps/\\d+/treatment-bmp-documents/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+/treatment-bmp-images$"),
    new RegExp("^/treatment-bmps/\\d+/treatment-bmp-types/\\d+$"),
    new RegExp("^/treatment-bmps/\\d+/type$"),
    new RegExp("^/treatment-bmps/\\d+/upstream-bmp$"),
    new RegExp("^/users/\\d+$"),
    new RegExp("^/water-quality-management-plan-documents/\\d+$"),
    new RegExp("^/water-quality-management-plans/\\d+$"),
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
