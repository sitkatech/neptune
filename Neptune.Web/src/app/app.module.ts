import { AppInitService } from "./app.init";

export function init_app(appLoadService: AppInitService) {
    return () => appLoadService.init();
}
