<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0"  xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd"  xmlns="http://www.opengis.net/sld"  xmlns:ogc="http://www.opengis.net/ogc"  xmlns:xlink="http://www.w3.org/1999/xlink"  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <NamedLayer>
    <Name>Load-based polygon</Name>
    <UserStyle>
      <Title>SLD Cook Book: Load-based polygon</Title>
      <FeatureTypeStyle>
        <Rule>
          <Name>2.5</Name>
          <Title>2.5</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CurrentLoadingRate</ogc:PropertyName>
                <ogc:Literal>0.1</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CurrentLoadingRate</ogc:PropertyName>
                <ogc:Literal>2.5</ogc:Literal>
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
          <Name>7.5</Name>
          <Title>7.5</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CurrentLoadingRate</ogc:PropertyName>
                <ogc:Literal>2.5</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CurrentLoadingRate</ogc:PropertyName>
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
          <Name>30</Name>
          <Title>30</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsGreaterThan>
                <ogc:PropertyName>CurrentLoadingRate</ogc:PropertyName>
                <ogc:Literal>7.5</ogc:Literal>
              </ogc:PropertyIsGreaterThan>
              <ogc:PropertyIsLessThanOrEqualTo>
                <ogc:PropertyName>CurrentLoadingRate</ogc:PropertyName>
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
        <Name>100</Name>
        <Title>100</Title>
        <ogc:Filter>
          <ogc:PropertyIsGreaterThan>
            <ogc:PropertyName>CurrentLoadingRate</ogc:PropertyName>
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