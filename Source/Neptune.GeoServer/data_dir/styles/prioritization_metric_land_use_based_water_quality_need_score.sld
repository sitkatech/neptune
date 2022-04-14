<?xml version="1.0" encoding="UTF-8"?>
<StyledLayerDescriptor version="1.0.0" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
                       xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                       xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd">
  <NamedLayer>
    <Name>OCTA Prioritization Land Use Based Water Quality Need Score</Name>
    <UserStyle>
      <Title>Chloropleth style fill for Land Use Based Water Quality Need Score (WQNLU in source table)</Title>
      <Abstract>Chloropleth style fill for Land Use Based Water Quality Need Score</Abstract>
      <FeatureTypeStyle>
        <Rule>
          <Title>0 - 5</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>LandUseBasedWaterQualityNeedScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>0</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>5</ogc:Literal>
              </ogc:UpperBoundary>
            </ogc:PropertyIsBetween>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#440154</CssParameter>
              <CssParameter name="fill-opacity">0.75</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#E9E9E9</CssParameter>
              <CssParameter name="stroke-width">0.1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Title>5 - 10</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>LandUseBasedWaterQualityNeedScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>5</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>10</ogc:Literal>
              </ogc:UpperBoundary>
            </ogc:PropertyIsBetween>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#453781</CssParameter>
              <CssParameter name="fill-opacity">0.75</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#E9E9E9</CssParameter>
              <CssParameter name="stroke-width">0.1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Title>10 - 15</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>LandUseBasedWaterQualityNeedScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>10</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>15</ogc:Literal>
              </ogc:UpperBoundary>
            </ogc:PropertyIsBetween>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#33638D</CssParameter>
              <CssParameter name="fill-opacity">0.75</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#E9E9E9</CssParameter>
              <CssParameter name="stroke-width">0.1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Title>15 - 19</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>LandUseBasedWaterQualityNeedScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>15</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>19</ogc:Literal>
              </ogc:UpperBoundary>
            </ogc:PropertyIsBetween>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#238A8D</CssParameter>
              <CssParameter name="fill-opacity">0.75</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#E9E9E9</CssParameter>
              <CssParameter name="stroke-width">0.1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Title>19 - 22</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>LandUseBasedWaterQualityNeedScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>19</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>22</ogc:Literal>
              </ogc:UpperBoundary>
            </ogc:PropertyIsBetween>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#29AF7F</CssParameter>
              <CssParameter name="fill-opacity">0.75</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#E9E9E9</CssParameter>
              <CssParameter name="stroke-width">0.1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Title>22 - 25</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>LandUseBasedWaterQualityNeedScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>22</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>25</ogc:Literal>
              </ogc:UpperBoundary>
            </ogc:PropertyIsBetween>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#73D055</CssParameter>
              <CssParameter name="fill-opacity">0.75</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#E9E9E9</CssParameter>
              <CssParameter name="stroke-width">0.1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Title>25 - 28</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>LandUseBasedWaterQualityNeedScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>25</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>28</ogc:Literal>
              </ogc:UpperBoundary>
            </ogc:PropertyIsBetween>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#DCE319</CssParameter>
              <CssParameter name="fill-opacity">0.75</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#E9E9E9</CssParameter>
              <CssParameter name="stroke-width">0.1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Title>28 - 30</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>LandUseBasedWaterQualityNeedScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>28</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>30</ogc:Literal>
              </ogc:UpperBoundary>
            </ogc:PropertyIsBetween>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#FDE725</CssParameter>
              <CssParameter name="fill-opacity">0.75</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#E9E9E9</CssParameter>
              <CssParameter name="stroke-width">0.1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>