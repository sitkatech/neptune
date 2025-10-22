/**
 * OverlayMode documents the usage patterns for overlay wrappers:
 *
 * Single: Show a single feature (WFS only, not in layer control)
 *   - Only the selected feature is shown via WFS
 *   - WMS is bypassed
 *   - Not added to layer control
 *
 * ReferenceOnly: Show all features via WMS (reference only, no interactivity)
 *   - All features are shown via WMS
 *   - No selection/highlighting
 *   - Not interactive
 *   - Added to layer control
 *
 * ReferenceWithInteractivity: Show all features via WMS, with selection/highlighting via WFS and map/grid interactivity
 *   - All features are shown via WMS
 *   - Selected feature is highlighted via WFS
 *   - Interactive (map click, grid selection)
 *   - Added to layer control
 */
export enum OverlayMode {
    Single = "Single",
    ReferenceOnly = "ReferenceOnly",
    ReferenceWithInteractivity = "ReferenceWithInteractivity",
}
