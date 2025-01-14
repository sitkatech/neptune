import * as L from "leaflet";
declare var require: any;

export class MarkerHelper {
    //Known bug in leaflet that during bundling the default image locations can get messed up
    //https://stackoverflow.com/questions/41144319/leaflet-marker-not-found-production-env
    static iconDefault = MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath("/assets/main/map-icons/marker-icon-black.png");
    static selectedMarker = MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath("/assets/main/map-icons/marker-icon-selected.png");
    static treatmentBMPMarker = MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath("/assets/main/map-icons/marker-icon-violet.png");
    static inventoriedTreatmentBMPMarker = MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath("/assets/main/map-icons/marker-icon-orange.png");

    public static fixMarkerPath() {
        delete L.Icon.Default.prototype._getIconUrl;

        L.Icon.Default.mergeOptions({
            iconRetinaUrl: "assets/main/map-icons/marker-icon-2x-black.png",
            iconUrl: "assets/main/map-icons/marker-icon-black.png",
            shadowUrl: "assets/main/map-icons/marker-shadow.png",
        });
    }

    //Function assumes there is a retina version of your image under the same name just with "-2x" appended
    public static buildDefaultLeafletMarkerFromMarkerPath(iconUrl: string): any {
        var retinaUrl = iconUrl.replace("marker-icon", "marker-icon-2x");
        return MarkerHelper.buildDefaultLeafletMarker(iconUrl, retinaUrl);
    }

    private static buildDefaultLeafletMarker(iconUrl: string, iconRetinaUrl: string): any {
        let shadowUrl = "assets/main/map-icons/marker-shadow.png";
        return L.icon({
            iconRetinaUrl,
            iconUrl,
            shadowUrl,
            iconSize: [25, 41],
            iconAnchor: [12, 41],
            popupAnchor: [1, -34],
            tooltipAnchor: [16, -28],
            shadowSize: [41, 41],
        });
    }
}
