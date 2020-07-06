<?xml version="1.0" encoding="UTF-8"?>
<StyledLayerDescriptor version="1.0.0" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
  xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd">
  <NamedLayer>
    <Name>watershed</Name>
    <UserStyle>
      <Name>watershed</Name>
      <Title>Watershed custom styled polygon</Title>
      <Abstract>Watershed-blue fill with 60% transparency and watershed-blue opaque outline</Abstract>
      <FeatureTypeStyle>
        <Rule>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#59acff</CssParameter>
              <CssParameter name="fill-opacity">0.1</CssParameter>
            </Fill>
            <Stroke>
              <CssParameter name="stroke">#59acff</CssParameter>
              <CssParameter name="stroke-width">0.5</CssParameter>
            </Stroke>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>Label</Name>
          <MaxScaleDenominator>500000</MaxScaleDenominator>
            <TextSymbolizer>
                <Label>
                    <ogc:PropertyName>DroolToolWatershedName</ogc:PropertyName>
                </Label>
                <Font>
                    <CssParameter name="font-family">Arial</CssParameter>
                    <CssParameter name="font-size">11</CssParameter>
                </Font>
                <LabelPlacement>
                    <PointPlacement>
                        <AnchorPoint>
                            <AnchorPointX>0.5</AnchorPointX>
                            <AnchorPointY>0.5</AnchorPointY>
                        </AnchorPoint>
                    </PointPlacement>
                </LabelPlacement>
                <Fill>
                  	<CssParameter name="fill">#357ac0</CssParameter>
                </Fill>
                <VendorOption name="autoWrap">60</VendorOption>
                <VendorOption name="maxDisplacement">150</VendorOption>
                <VendorOption name="repeat">-1</VendorOption>
                <VendorOption name="partials">true</VendorOption>
            </TextSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>