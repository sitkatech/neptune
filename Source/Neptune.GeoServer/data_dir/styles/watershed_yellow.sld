<?xml version="1.0" encoding="UTF-8"?>
<StyledLayerDescriptor version="1.0.0" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
  xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd">
  <NamedLayer>
    <Name>watershed</Name>
    <UserStyle>
      <Name>watershed</Name>
      <Title>Watershed custom styled polygon but YELLOW</Title>
      <Abstract>YELLOW fill with 60% transparency and watershed-blue opaque outline</Abstract>
      <FeatureTypeStyle>
        <Rule>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#ffff00</CssParameter>
              <CssParameter name="fill-opacity">0.4</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#ffff00</CssParameter>
              <CssParameter name="stroke-width">1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>