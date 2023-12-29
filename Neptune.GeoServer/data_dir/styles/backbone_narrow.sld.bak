<?xml version="1.0" encoding="UTF-8"?>
<StyledLayerDescriptor version="1.0.0" xmlns="http://www.opengis.net/sld" xmlns:ogc="http://www.opengis.net/ogc"
                       xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                       xsi:schemaLocation="http://www.opengis.net/sld http://schemas.opengis.net/sld/1.0.0/StyledLayerDescriptor.xsd">
  <NamedLayer>
    <Name>Simple Streams</Name>
    <UserStyle>
      <Title>Default Styler for streams segments</Title>
      <Abstract>Blue lines, 2px wide</Abstract>
      <FeatureTypeStyle>
        <FeatureTypeName>Feature</FeatureTypeName>
        <Rule>
          <Title>Streams</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>BackboneSegmentType</ogc:PropertyName>
              <ogc:Literal>Channel</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <LineSymbolizer>
            <Stroke>
              <CssParameter name="stroke">
                <ogc:Literal>#17559a</ogc:Literal>
              </CssParameter>
              <CssParameter name="stroke-width">
                <ogc:Literal>2</ogc:Literal>
              </CssParameter>
            </Stroke>
          </LineSymbolizer>
          <TextSymbolizer>
            <Label>
              <ogc:PropertyName>StreamName</ogc:PropertyName>
            </Label>
            <LabelPlacement>
              <LinePlacement >
              <PerpendicularOffset>8</PerpendicularOffset>
              </LinePlacement>
            </LabelPlacement>
            <Halo>
              <Radius>3</Radius>
              <Fill>
                <CssParameter name="fill">#FFFFFF</CssParameter>
              </Fill>
            </Halo>
            <Fill>
              <CssParameter name="fill">#000000</CssParameter>
            </Fill>
            <VendorOption name="followLine">true</VendorOption>
            <VendorOption name="maxAngleDelta">15</VendorOption>
            <VendorOption name="maxDisplacement">400</VendorOption>
            <VendorOption name="repeat">150</VendorOption>
          </TextSymbolizer>
        </Rule>
        <Rule>
          <Title>Pipes</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>BackboneSegmentType</ogc:PropertyName>
              <ogc:Literal>StormDrain</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <LineSymbolizer>
            <Stroke>
              <CssParameter name="stroke">
                <ogc:Literal>#323232</ogc:Literal>
              </CssParameter>
              <CssParameter name="stroke-width">
                <ogc:Literal>2</ogc:Literal>
              </CssParameter>
            </Stroke>
          </LineSymbolizer>
        </Rule>
        <Rule>
          <Title>Dummy</Title>
          <ogc:Filter>
            <ogc:PropertyIsEqualTo>
              <ogc:PropertyName>BackboneSegmentType</ogc:PropertyName>
              <ogc:Literal>Dummy</ogc:Literal>
            </ogc:PropertyIsEqualTo>
          </ogc:Filter>
          <LineSymbolizer>
            <Stroke>
              <CssParameter name="stroke">
                <ogc:Literal>#323232</ogc:Literal>
              </CssParameter>
              <CssParameter name="stroke-width">
                <ogc:Literal>2</ogc:Literal>
              </CssParameter>
           	  <CssParameter name="stroke-dasharray">5 2</CssParameter>
            </Stroke>
          </LineSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>