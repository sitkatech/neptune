<?xml version="1.0" encoding="UTF-8"?>
<StyledLayerDescriptor version="1.0.0" 
 xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd" 
 xmlns="http://www.opengis.net/sld" 
 xmlns:ogc="http://www.opengis.net/ogc" 
 xmlns:xlink="http://www.w3.org/1999/xlink" 
 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <!-- a Named Layer is the basic building block of an SLD document -->
  <NamedLayer>
    <Name>Neighborhood</Name>
    <UserStyle>
      <Title>Neighborhood</Title>
      <Abstract>Neighborhood</Abstract>
      <FeatureTypeStyle>
        <Rule>
          <Name>Neighborhood</Name>
          <Title>Neighborhood</Title>
          <Abstract>Neighborhood</Abstract>
          <PolygonSymbolizer>
            <Stroke>
              <CssParameter name="stroke">#7f00ff</CssParameter>
              <CssParameter name="stroke-width">2</CssParameter>
              <CssParameter name="stroke-opacity">1</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>