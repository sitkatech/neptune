declare module "leaflet-geometryutil" {
    import * as L from "leaflet";

    export default class GeometryUtil {
        static geodesicArea(latLngs: L.LatLng | L.LatLng[] | L.LatLng[][]): number;
        // Add other methods as needed
    }
}

// Or, if used as a global utility:
declare const GeometryUtil: {
    geodesicArea(latLngs: L.LatLng | L.LatLng[] | L.LatLng[][]): number;
    // Add other methods as needed
};
