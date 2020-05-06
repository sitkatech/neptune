<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0"  xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd"  xmlns="http://www.opengis.net/sld"  xmlns:ogc="http://www.opengis.net/ogc"  xmlns:xlink="http://www.w3.org/1999/xlink"  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <NamedLayer>
    <Name>Load-based polygon</Name>
    <UserStyle>
      <Title>Load-based delta polygon</Title>
      <FeatureTypeStyle>
        <Rule>
          <Name>Less than -30</Name>
          <Title>Less Than -30</Title>
          <ogc:Filter>
            <ogc:PropertyIsLessThan>
              <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
              <ogc:Literal>-30</ogc:Literal>
            </ogc:PropertyIsLessThan>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#084594</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>-30 to less than -7.6 </Name>
          <Title>-30 to -7.6</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThanOrEqualTo>
                <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
                <ogc:Literal>-30</ogc:Literal>
              </ogc:PropertyIsGreaterThanOrEqualTo>
              <ogc:PropertyIsLessThan>
                <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
                <ogc:Literal>-7.5</ogc:Literal>
              </ogc:PropertyIsLessThan>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#2171b5</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>-7.5 to less than -2.6</Name>
          <Title>-7.5 to -2.6</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThanOrEqualTo>
                <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
                <ogc:Literal>-7.5</ogc:Literal>
              </ogc:PropertyIsGreaterThanOrEqualTo>
              <ogc:PropertyIsLessThan>
                <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
                <ogc:Literal>-2.5</ogc:Literal>
              </ogc:PropertyIsLessThan>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#6baed6</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>-2.5 to less than -0.1</Name>
          <Title>-2.5 to -0.1</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThanOrEqualTo>
                <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
                <ogc:Literal>-2.5</ogc:Literal>
              </ogc:PropertyIsGreaterThanOrEqualTo>
              <ogc:PropertyIsLessThan>
                <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
                <ogc:Literal>0</ogc:Literal>
              </ogc:PropertyIsLessThan>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#c6dbef</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>0</Name>
          <Title>0</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
              <ogc:Literal>0</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">##fee5d9</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>0.1 to less than 2.5</Name>
          <Title>0.1 to 2.5</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
                <ogc:Literal>0.1</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
                <ogc:Literal>-2.5</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#fcbba1</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>2.6 to less than 7.5</Name>
          <Title>2.6 to 7.5</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
                <ogc:Literal>2.5</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
                <ogc:Literal>7.5</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ef3b2c</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>7.6 to less than 30</Name>
          <Title>7.6 to 30</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
                <ogc:Literal>7.5</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
                <ogc:Literal>30</ogc:Literal>
              </ogc:PropertyIsLessThanOrEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#cb181d</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule><Rule>
        <Name>Greater than 30</Name>
        <Title>Greater Than 30</Title>
        <ogc:Filter>
          <ogc:PropertyIsGreaterThan>
            <ogc:PropertyName>LoadingRateDelta</ogc:PropertyName>
            <ogc:Literal>30</ogc:Literal>
          </ogc:PropertyIsGreaterThan>
        </ogc:Filter>
        <PolygonSymbolizer>
          <Fill>
            <CssParameter name="fill">#99000d</CssParameter>
          </Fill>
        </PolygonSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>  
</StyledLayerDescriptor>