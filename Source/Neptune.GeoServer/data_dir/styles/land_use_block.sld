<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0"
                       xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd"
                       xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
                       xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <NamedLayer>
    <Name>land_use_block</Name>
    <UserStyle>
      <Title>Land Use Blocks</Title>
      <FeatureTypeStyle>

                <Rule>
          <Name>Commercial</Name>
          <Title>Commercial</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PriorityLandUseTypeID</ogc:PropertyName>
              <ogc:Literal>1</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color1</ogc:Literal>
                  <ogc:Literal>#c2fbfc</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Name>High Density</Name>
          <Title>High Density</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PriorityLandUseTypeID</ogc:PropertyName>
              <ogc:Literal>2</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color2</ogc:Literal>
                  <ogc:Literal>#c0d6fc</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Name>Industrial</Name>
          <Title>Industrial</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PriorityLandUseTypeID</ogc:PropertyName>
              <ogc:Literal>3</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color3</ogc:Literal>
                  <ogc:Literal>#b4fcb3</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Name>Mixed Urban</Name>
          <Title>Mixed Urban</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PriorityLandUseTypeID</ogc:PropertyName>
              <ogc:Literal>4</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color4</ogc:Literal>
                  <ogc:Literal>#fcb6b9</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Name>Retail</Name>
          <Title>Retail</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PriorityLandUseTypeID</ogc:PropertyName>
              <ogc:Literal>5</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color5</ogc:Literal>
                  <ogc:Literal>#f2cafc</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Name>Transportation</Name>
          <Title>Transportation</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PriorityLandUseTypeID</ogc:PropertyName>
              <ogc:Literal>6</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color6</ogc:Literal>
                  <ogc:Literal>#fcd6b6</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>ALU</Name>
          <Title>ALU</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PriorityLandUseTypeID</ogc:PropertyName>
              <ogc:Literal>7</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color6</ogc:Literal>
                  <ogc:Literal>#ffffed</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>