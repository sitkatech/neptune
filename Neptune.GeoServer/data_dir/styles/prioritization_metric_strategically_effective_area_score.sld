<?xml version="1.0" encoding="UTF-8"?>
<StyledLayerDescriptor version="1.0.0" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
                       xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                       xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd">
  <NamedLayer>
    <Name>OCTA Prioritization Strategically Effective Area Score</Name>
    <UserStyle>
      <Title>Chloropleth style fill for Strategically Effective Area Score (SEA in source table)</Title>
      <Abstract>Chloropleth style fill for Strategically Effective Area Score</Abstract>
      <FeatureTypeStyle>
        <Rule>
          <Title>0 - 10</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>StrategicallyEffectiveAreaScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>0</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>10</ogc:Literal>
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
          <Title>10 - 20</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>StrategicallyEffectiveAreaScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>10</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>20</ogc:Literal>
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
          <Title>20 - 25</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>StrategicallyEffectiveAreaScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>20</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>25</ogc:Literal>
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
          <Title>25 - 30</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>StrategicallyEffectiveAreaScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>25</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>30</ogc:Literal>
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
          <Title>30 - 35</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>StrategicallyEffectiveAreaScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>30</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>35</ogc:Literal>
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
          <Title>35 - 38</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>StrategicallyEffectiveAreaScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>35</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>38</ogc:Literal>
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
          <Title>38 - 42</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>StrategicallyEffectiveAreaScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>38</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>42</ogc:Literal>
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
          <Title>42 - 45</Title>
          <ogc:Filter>
            <ogc:PropertyIsBetween>
              <ogc:PropertyName>StrategicallyEffectiveAreaScore</ogc:PropertyName>
              <ogc:LowerBoundary>
                <ogc:Literal>42</ogc:Literal>
              </ogc:LowerBoundary>
              <ogc:UpperBoundary>
                <ogc:Literal>45</ogc:Literal>
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