<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0"
                       xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd"
                       xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
                       xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <NamedLayer>
    <Name>wqmp_trash_capture_status</Name>
    <UserStyle>
      <Title>WQMP Trash Capture Status</Title>
      <FeatureTypeStyle>
        <Rule>
          <Name>rule1</Name>
          <Title>Style for Trash Capture Status Type</Title>
          <PolygonSymbolizer>
            <Fill>
                <CssParameter name="fill">
                    <ogc:PropertyName>TrashCaptureStatusTypeColorCode</ogc:PropertyName>
                </CssParameter>
                <CssParameter name="fill-opacity">.35</CssParameter>
            </Fill>
        </PolygonSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>