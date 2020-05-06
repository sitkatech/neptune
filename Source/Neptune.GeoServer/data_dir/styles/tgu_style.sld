<?xml version="1.0" encoding="ISO-8859-1"?>
<StyledLayerDescriptor version="1.0.0"  xsi:schemaLocation="http://www.opengis.net/sld StyledLayerDescriptor.xsd"  xmlns="http://www.opengis.net/sld"  xmlns:ogc="http://www.opengis.net/ogc"  xmlns:xlink="http://www.w3.org/1999/xlink"  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <NamedLayer>
    <Name>Trash Generating Units</Name>
    <UserStyle>
      <Title>Trash Generating Units</Title>
      <FeatureTypeStyle>
        <Rule>
          <Name>PluFullTrashCapture</Name>
          <Title>PLU: Full Trash Capture</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>IsPriorityLandUse</ogc:PropertyName>
                <ogc:Literal>1</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>TrashCaptureStatus</ogc:PropertyName>
                <ogc:Literal>Full</ogc:Literal>
              </ogc:PropertyIsEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#006400</CssParameter>
              <CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>PluPartialTrashCapture</Name>
          <Title>PLU: Partial Capture</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>IsPriorityLandUse</ogc:PropertyName>
                <ogc:Literal>1</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>TrashCaptureStatus</ogc:PropertyName>
                <ogc:Literal>Partial</ogc:Literal>
              </ogc:PropertyIsEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#b3ffbb</CssParameter>
              <CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>PluScoreA</Name>
          <Title>PLU: OVTA Score A</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>IsPriorityLandUse</ogc:PropertyName>
                <ogc:Literal>1</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:PropertyIsNotEqualTo>
                <ogc:PropertyName>TrashCaptureStatus</ogc:PropertyName>
                <ogc:Literal>Full</ogc:Literal>
              </ogc:PropertyIsNotEqualTo>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>AssessmentScore</ogc:PropertyName>
                <ogc:Literal>A</ogc:Literal>
              </ogc:PropertyIsEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#5ce600</CssParameter>
              <CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>PluUntreated</Name>
          <Title>PLU: Untreated</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>IsPriorityLandUse</ogc:PropertyName>
                <ogc:Literal>1</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:Or>
                <ogc:PropertyIsEqualTo>
                  <ogc:PropertyName>TrashCaptureStatus</ogc:PropertyName>
                  <ogc:Literal>None</ogc:Literal>
                </ogc:PropertyIsEqualTo>
                <ogc:PropertyIsEqualTo>
                  <ogc:PropertyName>TrashCaptureStatus</ogc:PropertyName>
                  <ogc:Literal>NotProvided</ogc:Literal>
                </ogc:PropertyIsEqualTo>
              </ogc:Or>
              <ogc:PropertyIsNotEqualTo>
                <ogc:PropertyName>AssessmentScore</ogc:PropertyName>
                <ogc:Literal>A</ogc:Literal>
              </ogc:PropertyIsNotEqualTo>
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#c500ff</CssParameter>
              <CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>AluFullTrashCapture</Name>
          <Title>ALU: Full Trash Capture</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>IsPriorityLandUse</ogc:PropertyName>
                <ogc:Literal>0</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>TrashCaptureStatus</ogc:PropertyName>
                <ogc:Literal>Full</ogc:Literal>
              </ogc:PropertyIsEqualTo> 
              <ogc:PropertyIsNotEqualTo>
                <ogc:PropertyName>NoDataProvided</ogc:PropertyName>
                <ogc:Literal>1</ogc:Literal>
              </ogc:PropertyIsNotEqualTo>                            
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#d2b48c</CssParameter>
              <CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
        <Rule>
          <Name>AluUntreated</Name>
          <Title>ALU: Untreated</Title>
          <ogc:Filter>
            <ogc:And>
              <ogc:PropertyIsEqualTo>
                <ogc:PropertyName>IsPriorityLandUse</ogc:PropertyName>
                <ogc:Literal>0</ogc:Literal>
              </ogc:PropertyIsEqualTo>
              <ogc:PropertyIsNotEqualTo>
                <ogc:PropertyName>TrashCaptureStatus</ogc:PropertyName>
                <ogc:Literal>Full</ogc:Literal>
              </ogc:PropertyIsNotEqualTo> 
              <ogc:PropertyIsNotEqualTo>
                <ogc:PropertyName>NoDataProvided</ogc:PropertyName>
                <ogc:Literal>1</ogc:Literal>
              </ogc:PropertyIsNotEqualTo>                            
            </ogc:And>
          </ogc:Filter>
          <PolygonSymbolizer>
            <Fill>
              <CssParameter name="fill">#d2d2d2</CssParameter>
              <CssParameter name="fill-opacity">.7</CssParameter>
            </Fill>
          </PolygonSymbolizer>
        </Rule>
      </FeatureTypeStyle>
    </UserStyle>
  </NamedLayer>
</StyledLayerDescriptor>