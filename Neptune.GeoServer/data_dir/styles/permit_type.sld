<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0"
                       xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd"
                       xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
                       xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <NamedLayer>
    <Name>permit_type</Name>
    <UserStyle>
      <Title>Permit Types</Title>
      <FeatureTypeStyle>

        <Rule>
          <Name>Phase I MS4</Name>
          <Title>Phase I MS4</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PermitTypeID</ogc:PropertyName>
              <ogc:Literal>1</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color1</ogc:Literal>
                  <ogc:Literal>#0075A4</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Name>Phase II MS4</Name>
          <Title>Phase II MS4</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PermitTypeID</ogc:PropertyName>
              <ogc:Literal>2</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color2</ogc:Literal>
                  <ogc:Literal>#008fad</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Name>IGP</Name>
          <Title>IGP</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PermitTypeID</ogc:PropertyName>
              <ogc:Literal>3</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color3</ogc:Literal>
                  <ogc:Literal>#00a697</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Name>Individual Permit</Name>
          <Title>Individual Permit</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PermitTypeID</ogc:PropertyName>
              <ogc:Literal>4</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color4</ogc:Literal>
                  <ogc:Literal>#1eb769</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Name>CalTrans MS4</Name>
          <Title>CalTrans MS4</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PermitTypeID</ogc:PropertyName>
              <ogc:Literal>5</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color5</ogc:Literal>
                  <ogc:Literal>#94c031</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

        <Rule>
          <Name>Other</Name>
          <Title>Other</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>PermitTypeID</ogc:PropertyName>
              <ogc:Literal>6</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">
                <ogc:Function name="env">
                  <ogc:Literal>color6</ogc:Literal>
                  <ogc:Literal>#F3BC00</ogc:Literal>
                </ogc:Function>
              </CssParameter><CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>

      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>