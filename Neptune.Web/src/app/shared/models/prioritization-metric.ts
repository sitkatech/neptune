export class PrioritizationMetric {
    static readonly TPI = new PrioritizationMetric(
        "Transportation Nexus Score",
        PrioritizationMetric.getColorPallette(),
        ["0 - 1", "1 - 2", "2 - 3", "3 - 3.5", "3.5 - 4", "4 - 4.5", "4.5 - 4.75", "4.75 - 5"],
        "prioritization_metric_transportation_nexus_score"
    )

    static readonly WQNLU = new PrioritizationMetric(
        "Land Use Based Water Quality Need Score",
        PrioritizationMetric.getColorPallette(),
        ["0 - 5", "5 - 10", "10 - 15", "15 - 19", "19 - 22",  "22 - 25", "25 - 28", "28 - 30"],
        "prioritization_metric_land_use_based_water_quality_need_score"
    )

    static readonly WQNMON = new PrioritizationMetric(
        "Receiving Water Score",
        PrioritizationMetric.getColorPallette(),
        ["0 - 2", "2 - 4", "4 - 6", "6 - 7", "7 - 8", "8 - 9", "9 - 9.5", "9.5 - 10"],
        "prioritization_metric_receiving_water_score"
    )

    static readonly SEA = new PrioritizationMetric(
        "Strategically Effective Area Score",
        PrioritizationMetric.getColorPallette(),
        ["0 - 10", "10 - 20", "20 - 25", "25 - 30", "30 - 35",  "35 - 38", "38 - 42", "42 - 45"],
        "prioritization_metric_strategically_effective_area_score"
    )

    static readonly NoMetric = new PrioritizationMetric(
        "No Metric, Map Only",
        null,
        null,
        null,
    )   

    private constructor(private readonly key:string, 
        public readonly legendColors:Array<string>, 
        public readonly legendValues:Array<string>,
        public readonly geoserverStyle:string) {}

    toString() {
        return this.key;
    }

    private static getColorPallette(): Array<string> {
        return ["#440154", "#453781", "#33638D", "#238A8D", "#29AF7F", "#73D055", "#DCE319", "#FDE725"];
    }

}