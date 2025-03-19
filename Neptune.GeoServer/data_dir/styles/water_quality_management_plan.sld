<?xml version="1.0" encoding="UTF-8"?>
<StyledLayerDescriptor version="1.0.0" 
                       xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" 
                       xmlns="http://www.opengis.net/sld" 
                       xmlns:ogc="http://www.opengis.net/ogc" 
                       xmlns:xlink="http://www.w3.org/1999/xlink" 
                       xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <NamedLayer>
    <Name>Water Quality Management Plan</Name>
    <UserStyle>
      <Title>Water Quality Management Plan</Title>
      <Abstract>Water Quality Management Plan Boundaries</Abstract>
      <FeatureTypeStyle>
        <Rule>
          <Name>WQMP Boundary</Name>
          <Title>WQMP Boundary</Title>
          <Abstract>WQMP Boundaries</Abstract>
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
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>