<?xml version="1.0" encoding="UTF-8"?>
<StyledLayerDescriptor version="1.0.0" 
                       xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" 
                       xmlns="http://www.opengis.net/sld" 
                       xmlns:ogc="http://www.opengis.net/ogc" 
                       xmlns:xlink="http://www.w3.org/1999/xlink" 
                       xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <NamedLayer>
    <Name>delineation</Name>
    <UserStyle>
      <Title>Delineation</Title>
      <Abstract>Delineation</Abstract>
      <FeatureTypeStyle>
        <Rule>
          <Name>Centralized (Verified)</Name>
          <Title>Centralized (Verified)</Title>
          <Abstract>Centralized Delineations (Verified)</Abstract>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>DelineationType</ogc:PropertyName>
                <ogc:Literal>Centralized</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>DelineationStatus</ogc:PropertyName>
                <ogc:Literal>Verified</ogc:Literal>
              </ogc:PropertyIsEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#611570</CssParameter>
              <CssParameter name="fill-opacity">.4</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#611570</CssParameter>
              <CssParameter name="stroke-width">2</CssParameter>
              <CssParameter name="stroke-opacity">1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>Distributed (Verified)</Name>
          <Title>Distributed (Verified)</Title>
          <Abstract>Distributed Delineations (Verified)</Abstract>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>DelineationType</ogc:PropertyName>
                <ogc:Literal>Distributed</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>DelineationStatus</ogc:PropertyName>
                <ogc:Literal>Verified</ogc:Literal>
              </ogc:PropertyIsEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#0000ff</CssParameter>
              <CssParameter name="fill-opacity">.4</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#0000ff</CssParameter>
              <CssParameter name="stroke-width">2</CssParameter>
              <CssParameter name="stroke-opacity">1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>WQMP Boundary (Verified)</Name>
          <Title>WQMP Boundary (Verified)</Title>
          <Abstract>WQMP Boundaries (Verified)</Abstract>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>DelineationType</ogc:PropertyName>
                <ogc:Literal>WQMP</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>DelineationStatus</ogc:PropertyName>
                <ogc:Literal>Verified</ogc:Literal>
              </ogc:PropertyIsEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#de54b2</CssParameter>
              <CssParameter name="fill-opacity">.4</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#de54b2</CssParameter>
              <CssParameter name="stroke-width">2</CssParameter>
              <CssParameter name="stroke-opacity">1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>Centralized (Provisional)</Name>
          <Title>Centralized (Provisional)</Title>
          <Abstract>Centralized Delineations (Provisional)</Abstract>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>DelineationType</ogc:PropertyName>
                <ogc:Literal>Centralized</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>DelineationStatus</ogc:PropertyName>
                <ogc:Literal>Provisional</ogc:Literal>
              </ogc:PropertyIsEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#af25cb</CssParameter>
              <CssParameter name="fill-opacity">.4</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#af25cb</CssParameter>
              <CssParameter name="stroke-width">2</CssParameter>
              <CssParameter name="stroke-opacity">1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>Distributed (Provisional)</Name>
          <Title>Distributed (Provisional)</Title>
          <Abstract>Distributed Delineations (Provisional)</Abstract>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>DelineationType</ogc:PropertyName>
                <ogc:Literal>Distributed</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>DelineationStatus</ogc:PropertyName>
                <ogc:Literal>Provisional</ogc:Literal>
              </ogc:PropertyIsEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#8ac0ff</CssParameter>
              <CssParameter name="fill-opacity">.4</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#8ac0ff</CssParameter>
              <CssParameter name="stroke-width">2</CssParameter>
              <CssParameter name="stroke-opacity">1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>WQMP Boundary (Provisional)</Name>
          <Title>WQMP Boundary (Provisional)</Title>
          <Abstract>WQMP Boundaries (Provisional)</Abstract>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>DelineationType</ogc:PropertyName>
                <ogc:Literal>WQMP</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>DelineationStatus</ogc:PropertyName>
                <ogc:Literal>Provisional</ogc:Literal>
              </ogc:PropertyIsEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#eda1d5</CssParameter>
              <CssParameter name="fill-opacity">.4</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#eda1d5</CssParameter>
              <CssParameter name="stroke-width">2</CssParameter>
              <CssParameter name="stroke-opacity">1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>