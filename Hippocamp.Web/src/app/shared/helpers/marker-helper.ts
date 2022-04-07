import * as L from 'leaflet'
declare var require: any

export class MarkerHelper {
    //Known bug in leaflet that during bundling the default image locations can get messed up
    //https://stackoverflow.com/questions/41144319/leaflet-marker-not-found-production-env
    static iconDefault = MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath('/assets/marker-icon.png');
    static selectedMarker = MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath('/assets/main/map-icons/marker-icon-selected.png');
    static treatmentBMPMarker = MarkerHelper.buildDefaultLeafletMarkerFromMarkerPath('/assets/main/map-icons/marker-icon-violet.png');
    
    public static fixMarkerPath() {
        delete L.Icon.Default.prototype._getIconUrl;

        L.Icon.Default.mergeOptions({
            iconRetinaUrl: 'assets/marker-icon-2x.png',
            iconUrl: 'assets/marker-icon.png',
            shadowUrl: 'assets/marker-shadow.png',
        });
    }

    //Function assumes there is a retina version of your image under the same name just with "-2x" appended
    public static buildDefaultLeafletMarkerFromMarkerPath(iconUrl: string): any{
        var retinaUrl = iconUrl.replace("marker-icon", "marker-icon-2x");
        return MarkerHelper.buildDefaultLeafletMarker(iconUrl, retinaUrl);
    }
    
    private static buildDefaultLeafletMarker(iconUrl: string, iconRetinaUrl: string): any {
        let shadowUrl = 'assets/marker-shadow.png';
        return L.icon({
            iconRetinaUrl,
            iconUrl,
            shadowUrl,
            iconSize: [25, 41],
            iconAnchor: [12, 41],
            popupAnchor: [1, -34],
            tooltipAnchor: [16, -28],
            shadowSize: [41, 41]
        });
    }
}
